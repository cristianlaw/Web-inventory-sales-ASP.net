using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MisMinistritosAPI.Models.ModelDb
{
    public class VentaDbModel
    {
        internal object facturaId;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = ("int"))]
        public int id { get; set; }
        
        [Column(TypeName = "varchar(15)")]
        public string idDatoF { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string descripcionDatoF { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal productoPrecio { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int cantidadProducto { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal totalProducto { get; set; }
    }
}
