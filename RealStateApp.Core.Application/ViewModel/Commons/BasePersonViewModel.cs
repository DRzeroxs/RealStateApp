using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Commons
{
    public abstract class BasePersonViewModel
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
    }
}
