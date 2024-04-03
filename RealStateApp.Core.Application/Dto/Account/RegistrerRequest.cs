using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dto.Account
{
    public class RegistrerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeOfUser { get; set; }
        public string ImgUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email {get; set;}
        public IFormFile file {  get; set;}    
    }
}
