using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Propiedad
{
    public class ImgPropiedadViewModel
    {
        public string UrlImg { get; set; }

        public int PropieadId { get; set; }

        public PropiedadViewModel Propiedad { get; set; }
    }
}
