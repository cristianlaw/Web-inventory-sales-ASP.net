using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisMinistritosAPI.Models.ModelDb
{
    public class TourDbModel
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string id { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string descripcion { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string verImagen { get; set; }

        [Required]
        [Column(TypeName = "varchar(25)")]
        public string diasTour { get; set; }

        public virtual ProveedorDbModel proveedorDbModel { get; set; }

        [Required]
        [Column(TypeName = "varchar(12)")]
        [ForeignKey("cedula")]
        public string proveedor { get; set; }
        

        [Required]
        [Column(TypeName = "int")]
        public int precioTour { get; set; }


       
        



    }
}
