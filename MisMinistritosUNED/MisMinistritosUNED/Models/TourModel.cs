using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MisMinistritosUNED.Models
{
    public class TourModel
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Se necesitan entre 10 y 100 caracteres")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string verImagen { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string diasTour { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string proveedor { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[0-9\s,][0-9\s,]*$", ErrorMessage = "Digite un precio entero valido")]
        [Range(0, 10000000, ErrorMessage = "Digite un precio inferior a 10.000.000")]
        public int precioTour { get; set; }

        
        [JsonIgnore]
        [NotMapped]
        public IFormFile imagen { get; set; } = default!;

    }
}
