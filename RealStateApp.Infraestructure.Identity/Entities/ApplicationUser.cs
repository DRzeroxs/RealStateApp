using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string ImgUrl { get; set; }
        public bool IsActive { get; set; }
        public string TypeOfUser { get; set; }  
        public string Cedula {  get; set; }
    }
}
