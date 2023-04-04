using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MisMinistritosUNED.Models
{
    public class ProveedorModel
    {
        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[0]{1}[0-9]{1}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Digite la cédula en formato '0#-####-####'")]
        [StringLength(12)]
        public string cedula { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Se necesitan entre 10 y 100 caracteres")]
        public string descripcion { get; set; }


        [JsonIgnore]
        [NotMapped]
        public string? proveedor { get; set; }


    }
}
