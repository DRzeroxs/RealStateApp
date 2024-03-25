using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Service;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IMapper _mapper;
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;

        _mapper = mapper;
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

        if (!user.IsActive)
        {
            response.HasError = true;
            response.Error = $"Accound No Confirmed for {requuest.Email} contact an administrator";
            return response;
        }


        response.Id = user.Id;
        response.Email = user.Email;
        response.UserName = user.UserName;

        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

        response.Roles = rolesList.ToList();
        response.IsVerified = user.EmailConfirmed;
        return response;
    }

    public async Task SingOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<RegistrerResponse> RegistrerClienteUserAsync(RegistrerRequest request, string origin)
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
            EmailConfirmed = false,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            TypeOfUser = request.TypeOfUser,
            IsActive = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
        }
        else
        {
            response.HasError = true;
            response.Error = $"An Error ocurred trying to register the user";

            return response;
        }

        return response;
    }

    public async Task<RegistrerResponse> RegistrerAgenteUserAsync(RegistrerRequest request, string origin)
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
            EmailConfirmed = false,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            TypeOfUser = request.TypeOfUser,
            IsActive = false,
            ImgUrl = request.ImgUrl,    
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Agente.ToString());
        }
        else
        {
            response.HasError = true;
            response.Error = $"An Error ocurred trying to register the user";

            return response;
        }

        return response;
    }


    public async Task ConfirmAccountAsync(string userId)
    {

        var user = await _userManager.FindByIdAsync(userId);

        user.IsActive = true;

        var result = await _userManager.UpdateAsync(user);
    }
    private async Task<string> UpdatePassword(string userId, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);

        return newPasswordHash;
    }

}
