//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisMinistritosAPI.Models.ModelDb
{
    public class ProductoDbModel
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string id { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string descripcion { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int ingreso { get; set; }

        public virtual ProveedorDbModel? proveedorDbModel { get; set; }

        [Required]
        [Column(TypeName = "varchar(12)")]
        [ForeignKey("cedula")] 
        public string proveedor { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int precioCompra { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int utilidad { get; set; }

        

        



    }
}
