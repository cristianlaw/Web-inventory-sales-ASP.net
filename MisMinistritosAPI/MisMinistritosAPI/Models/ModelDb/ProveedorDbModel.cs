using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisMinistritosAPI.Models.ModelDb
{
    public class ProveedorDbModel
    {
        [Key]
        [Column(TypeName = "varchar(12)")]
        public string cedula { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string descripcion { get; set; }

    }
}
