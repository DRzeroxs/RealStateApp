using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Application.ViewModel.User;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Service
{
    public class AccountServiceApi : IAccountServiceApi
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AccountServiceApi(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(requuest.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {requuest.Email} ";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, requuest.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credential For {requuest.Email} ";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Accound No Confirmed for {requuest.Email} ";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJwToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;


            return response;
        }
        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<RegistrerResponse> RegistroUsuarioDesarrollador(RegistrerRequest request, string origin)
        {
            RegistrerResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username {request.UserName} is already Taken";

                return response;
            }


            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"username {request.Email} is already registrer";

                return response;
            }

            var user = new ApplicationUser
            {
                EmailConfirmed = true,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                ImgUrl = request.ImgUrl,
                TypeOfUser = request.TypeOfUser,
                PhoneNumberConfirmed = true,
                IsActive = true

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Developer.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An Error ocurred trying to register the user";

                return response;
            }

            return response;
        }
        public async Task<RegistrerResponse> RegistroUsuarioAdministrador(RegistrerRequest request, string origin)
        {
            RegistrerResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username {request.UserName} is already Taken";

                return response;
            }


            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"username {request.Email} is already registrer";

                return response;
            }

            var user = new ApplicationUser
            {
                EmailConfirmed = true,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                ImgUrl = request.ImgUrl,
                TypeOfUser = request.TypeOfUser,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An Error ocurred trying to register the user";

                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No Account Registrer with this User";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Accound confirmed for {user.Email} You can now use the app";
            }
            else
            {
                return $"An Error ocurred while confirming {user.Email}";
            }
        }


        private async Task<JwtSecurityToken> GenerateJwToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();

            foreach (var role in roles)
            {
                rolesClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

            }
            .Union(userClaims)
            .Union(rolesClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key));
            var sigingCredetials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: sigingCredetials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };

        }

        private string RandomTokenString()
        {
            using var rgnCrytoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rgnCrytoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }

        public async Task<bool> ActiveAccountAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            user.IsActive = true;

            var result = await _userManager.UpdateAsync(user);
            return user.IsActive;
        }

        public async Task<bool> InactiveAccountAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
            return user.IsActive;
        }

        public async Task<UserPostViewModel> GetById(string Id)
        {
            var u = await _userManager.FindByIdAsync(Id);

            UserPostViewModel UserVm = new()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Correo = u.Email,
                IsActive = u.IsActive
            };
            return UserVm;
        }
    }
}
