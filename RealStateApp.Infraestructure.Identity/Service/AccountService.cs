using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Dto.Email;
using RealStateApp.Core.Application.Enum;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Application.Interfaces.IEmail;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Administrador;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.User;
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
    private readonly IEmailService _emailService;
    private readonly IAgenteService _agenteService;
    private readonly IAdministradorService _adminService;

    private readonly IMapper _mapper;
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IEmailService emailService ,IMapper mapper, IAgenteService agenteService, IAdministradorService administradorService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
        _mapper = mapper;
        _adminService = administradorService;   
        _agenteService = agenteService; 
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

    public async Task<RegistrerResponse> RegistrerDesarrolladorAsync(RegistrerRequest request)
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
            TypeOfUser = "Developer",
            IsActive = true,
            ImgUrl = "",
            PhoneNumber = request.PhoneNumber,
            Cedula = request.Cedula
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        var userId = await _userManager.FindByEmailAsync(request.Email);

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
        response.userId = userId.Id;
        return response;
    }
    public async Task InactivarUsuario(string userId)
    {
       var usuario = await _userManager.FindByIdAsync(userId);

        if(usuario.TypeOfUser == "Agente")
        {
            var agente =  await _agenteService.GetByIdentityId(usuario.Id);
            agente.IsActive = false;

            SaveAgenteViewModel saveAgente = _mapper.Map<SaveAgenteViewModel>(agente);

            await _agenteService.UpdateAsync(saveAgente, agente.Id);
        }    

        usuario.EmailConfirmed = false;
        usuario.IsActive = false;

        var result = await _userManager.UpdateAsync(usuario);
    }
    public async Task ActivarUsuario(string userId)
    {
        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario.TypeOfUser == "Agente")
        {
            var agente = await _agenteService.GetByIdentityId(usuario.Id);
            agente.IsActive = true;

            SaveAgenteViewModel saveAgente = _mapper.Map<SaveAgenteViewModel>(agente);

            await _agenteService.UpdateAsync(saveAgente, agente.Id);
        }

        usuario.EmailConfirmed = true;
        usuario.IsActive = true;

        var result = await _userManager.UpdateAsync(usuario);
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
            IsActive = false,
            ImgUrl = "",
            PhoneNumber = request.PhoneNumber,
            Cedula = ""
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (user != null && user.Id != null)
        {
            user.ImgUrl = UploadFile(request.file, user.Id);
            await _userManager.UpdateAsync(user);
        }

        var userId = await _userManager.FindByEmailAsync(request.Email);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Cliente.ToString());

            var verificationUri = await SendVerificationEmailUrl(user, origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please Confirm Your Account Visiting this Url {verificationUri}",
                Subject = "Confirm registration"
            });
        }
        else
        {
            response.HasError = true;
            response.Error = $"An Error ocurred trying to register the user";
         
            return response;
        }
        response.userId = userId.Id;
        return response;
    }
    public async Task<int> CountAgentesActivos()
    {
        var users = await _userManager.Users.ToListAsync();
        var count = users.Where(u => u.EmailConfirmed == true && u.TypeOfUser == "Agente").Count();

        await AddAgentesEsquemaDb();

        return count;

    }
    private async Task AddAgentesEsquemaDb()
    {
        var users = await _userManager.Users.ToListAsync();
        var agentes = users.Where(u => u.TypeOfUser == "Agente");

        List<SaveAgenteViewModel> agenteSave = new();

        foreach(var item in agentes)
        {
            agenteSave.Add(new SaveAgenteViewModel
            {
                Apellido = item.LastName,
                Nombre = item.FirstName,
                Telefono = item.PhoneNumber,
                ImgUrl = item.ImgUrl,
                IdentityId = item.Id,
                Cedula = item.Cedula,
                Correo = item.Email,
                IsActive = item.EmailConfirmed
            });

            foreach(var item2 in agenteSave)
            {
                var agentesdb = await _agenteService.GetAllAsync();
               var confirn = agentesdb.Where(a => a.IdentityId == item2.IdentityId).FirstOrDefault();

                if(confirn == null)
                {
                    await _agenteService.AddAsync(item2);  
                }
                 
            }
        }
    }

    public async Task EliminarAgente(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.DeleteAsync(user);   
    }
    public async Task<int> CountAgentesInactivos()
    {
        var users = await _userManager.Users.ToListAsync();
        var count = users.Where(u => u.EmailConfirmed == false && u.TypeOfUser == "Agente").Count();

        return count;

    }
    public async Task<int> CountClientesActivos()
    {
        var users = await _userManager.Users.ToListAsync();
        var count = users.Where(u => u.EmailConfirmed == true && u.TypeOfUser == "Cliente").Count();

        return count;
    }
    public async Task<int> CountClientesInactivos()
    {
        var users = await _userManager.Users.ToListAsync();
        var count = users.Where(u => u.EmailConfirmed == false && u.TypeOfUser == "Cliente").Count();

        return count;
    }
    public async Task<int> CountDesarrolladoresActivos()
    {
        var users = await _userManager.Users.ToListAsync();
        var count = users.Where(u => u.EmailConfirmed == true && u.TypeOfUser == "Developer").Count();

        return count;
    }
    public async Task<int> CountDesarrolladoresInactivos()
    {
        var users = await _userManager.Users.ToListAsync();
        var count = users.Where(u => u.EmailConfirmed == false && u.TypeOfUser == "Developer").Count();

        return count;
    }
    public async Task<UserViewModel> GetById(string userId)
    {
       var user = await _userManager.FindByIdAsync(userId);

        UserViewModel userVm = new UserViewModel
        {
            Email = user.Email,
            FirstName = user.FirstName, LastName = user.LastName,
            PhoneNumber = user.PhoneNumber, UserId = user.Id, ImgUrl = user.ImgUrl,
            Cedula = user.Cedula,   
            UserName = user.UserName
        };

        return userVm;
    }
    public async Task<List<UserViewModel>> GetUsuariosAdministrador()
    {
        var users = _userManager.Users.ToList();
        var listaAdministradores = users.Where(u => u.TypeOfUser == "Admin").ToList();

        List<UserViewModel> userList = new();

        foreach(var item in listaAdministradores)
        {
            userList.Add(new UserViewModel
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Cedula = item.Cedula,
                Email = item.Email,
                UserId = item.Id,
                UserName = item.UserName,
                IsActived = item.EmailConfirmed
            });
        }

        return userList;
    }
    public async Task<List<UserViewModel>> GetUsuariosDesarrollador()
    {
        var users = _userManager.Users.ToList();
        var listaAdministradores = users.Where(u => u.TypeOfUser == "Developer").ToList();

        List<UserViewModel> userList = new();

        foreach (var item in listaAdministradores)
        {
            userList.Add(new UserViewModel
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Cedula = item.Cedula,
                Email = item.Email,
                UserId = item.Id,
                UserName = item.UserName,
                IsActived = item.EmailConfirmed
            });
        }

        return userList;
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
            response.Error = $"El Nombre de Usuario ${request.UserName} ya esta registrado";

            return response;
        }


        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail != null)
        {
            response.HasError = true;
            response.Error = $"El Email ${request.Email} ya esta registrado";

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
            ImgUrl = "",
            PhoneNumber = request.PhoneNumber,
            Cedula = ""
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (request.file is not null)
        {
            if (user != null && user.Id != null)
            {
                user.ImgUrl = UploadFile(request.file, user.Id);
                await _userManager.UpdateAsync(user);
            }
        }
        

        var userId = await _userManager.FindByEmailAsync(request.Email);

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
        response.userId = userId.Id;
        return response;
    }

    public async Task<RegistrerResponse> RegistrerAdminUserAsync(RegistrerRequest request, string origin)
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
            TypeOfUser = request.TypeOfUser,
            IsActive = true,
            ImgUrl = "",
            PhoneNumber = request.PhoneNumber,
            Cedula = request.Cedula
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (request.file is not null)
        {
            if (user != null && user.Id != null)
            {
                user.ImgUrl = UploadFile(request.file, user.Id);
                await _userManager.UpdateAsync(user);
            }
        }

        var userId = await _userManager.FindByEmailAsync(request.Email);

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
        response.userId = userId.Id;
        return response;
    }
    public async Task EditarUsuario(UserPostViewModel vm)
    {
        var user = await _userManager.FindByIdAsync(vm.UserId);
        

        user.FirstName = vm.FirstName;
        user.LastName = vm.LastName;    
        user.Email = vm.Email;
        user.UserName = vm.UserName;
        user.Cedula = vm.Cedula;

        if(vm.Password == null)
        {
            user.PasswordHash = user.PasswordHash;
        }
        else
        {
            user.PasswordHash = await UpdatePassword(vm.UserId, vm.Password);
        }
    

        

        var result = await _userManager.UpdateAsync(user);   
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

    private async Task<string> SendVerificationEmailUrl(ApplicationUser user, string origin)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var route = "User/ConfirmEmail";

        var Uri = new Uri(string.Concat($"{origin}/", route));

        var verificationUrL = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
        verificationUrL = QueryHelpers.AddQueryString(verificationUrL, "token", code);

        return verificationUrL;
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
    public async Task EditUser(EditUserViewModel vm)
    {
        var user = await _userManager.FindByIdAsync(vm.Id);
        
        if (user != null) 
        {
            user.FirstName = vm.FirstName;  
            user.LastName = vm.LastName;    
            user.PhoneNumber = vm.PhoneNumber;  
            user.ImgUrl = user.ImgUrl = UploadFile(vm.file, user.Id);

         var result = await _userManager.UpdateAsync(user);
        }

    }
    private string UploadFile(IFormFile file, string Id, bool isEditMode = false, string imageURL = "")
    {
        if (isEditMode && file == null)
        {
            return imageURL;
        }

        //Get Directory Path
        string BasePath = $"/img/user/{Id}";
        string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{BasePath}");

        //Create Folder if no exits
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }


        //GetFilePath
        Guid guid = Guid.NewGuid();
        FileInfo fileInfo = new(file.FileName);
        string fileName = guid + fileInfo.Extension;

        string fileNameWithPath = Path.Combine(path, fileName);

        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        if (isEditMode)
        {
            string[] oldImage = imageURL.Split("/");
            string olImageName = oldImage[^1];
            string completeImageOldPath = Path.Combine(path, olImageName);

            if (System.IO.File.Exists(completeImageOldPath))
            {
                System.IO.File.Delete(completeImageOldPath);
            }

        };
        return $"{BasePath}/{fileName}";
    }

}
