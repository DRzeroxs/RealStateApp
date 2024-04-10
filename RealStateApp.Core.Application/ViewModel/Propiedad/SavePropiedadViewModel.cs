using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Propiedad
{
    public class SavePropiedadViewModel 
    {
        public int Id { get; set; }
        public int Identifier { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Text)]
        public string Size { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un Valor valido")]
        public double Precio { get; set; }

        public string Descripcion { get; set; }

        [Range(1, 100, ErrorMessage = "Ingrese un Valor valido")]
        public int NumHabitaciones { get; set; }

        [Range(1, 100, ErrorMessage = "Ingrese un Valor valido")]
        public int NumAceados { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un Valor valido")]
        public int TipoPropiedadId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un Valor valido")]
        public int TipoVentaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un Valor valido")]
        public int AgenteId { get; set; }

        [MaxLength(4, ErrorMessage = "Solo puede agregar 4 imagenes")]
        [MinLength(1, ErrorMessage = "Debe agregar por lo menos una imagen")]
        public List<IFormFile> Files { get; set; }

        [MaxLength(4, ErrorMessage = "Solo puede agregar 4 imagenes")]
        [MinLength(1, ErrorMessage = "Debe agregar por lo menos una imagen")]
        public List<string> ImgUrls { get; set; }

    }
}
