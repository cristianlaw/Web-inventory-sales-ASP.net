using System.ComponentModel.DataAnnotations;

namespace MisMinistritosAPI.Models
{
    public class ClienteModel
    {

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[0]{1}[0-9]{1}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Digite la cédula en formato '0#-####-####'")]
        [StringLength(12)]
        public string cedula { get; set; }


        
        public string nombreCompleto { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Digite un año valido, mayor a 18 años")]
        [Range(1900, 2004, ErrorMessage = "Digite un año valido")]
        public int fechaNacimiento { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[1-9][0-9]{3}-[0-9]{4}$", ErrorMessage = "Formato aceptado ####-####")]
        [StringLength(9)]
        public string telefono { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Se necesitan entre 10 y 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZñÑáéíóúàèìòùÁÉÍÓÚÀÈÌÒÙüÜ0-9\s,]*$", ErrorMessage = "Solo nombres validos")]
        public string profesion { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Se necesitan entre 10 y 100 caracteres")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{2,3}\.[0-9]{2,3}\.[0-9]{2,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{2,3})(\]?)$", ErrorMessage = "Solo caracteres y formato valido")]
        public string correoElectronico { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        public string comboReferencia { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[@#$%]).{8,16})$", ErrorMessage = "Debe contener letras, números y caracteres especiales y estar entre 8 y 16 caracteres")]
        public string contrasena { get; set; }
    }
}
