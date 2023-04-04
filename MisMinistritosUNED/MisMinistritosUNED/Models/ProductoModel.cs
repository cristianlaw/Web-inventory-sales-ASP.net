using System.ComponentModel.DataAnnotations;

namespace MisMinistritosUNED.Models
{
    public class ProductoModel 
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Se necesitan entre 10 y 150 caracteres")]
        //[RegularExpression(@"^[a-zA-ZñÑáéíóúàèìòùÁÉÍÓÚÀÈÌÒÙüÜ0-9\s,]*$", ErrorMessage = "Digite una descripción válida")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Digite un año valido")]
        [Range(1900, 2022, ErrorMessage = "Digite un año valido")]
        public int ingreso { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[0-9\s,][0-9\s,]*$", ErrorMessage = "Digite un precio entero valido")]
        [Range(0, 10000000, ErrorMessage = "Digite un precio inferior a 10.000.000")]
        public int precioCompra { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[1-9\s,][0-9\s,]*$", ErrorMessage = "Digite un porcentaje entero y mayor a 0")]
        public int utilidad { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string proveedor { get; set; }

    }
}