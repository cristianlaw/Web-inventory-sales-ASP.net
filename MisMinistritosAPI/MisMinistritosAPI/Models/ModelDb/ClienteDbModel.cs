using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisMinistritosAPI.Models.ModelDb
{
    public class ClienteDbModel
    {
        
        [Key]
        [Column(TypeName = "varchar(12)")]
        public string cedula { get; set; }
        
        [Required]
        [Column(TypeName ="varchar(60)")]
        public string nombreCompleto { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int fechaNacimiento { get; set; }

        [Required]
        [Column(TypeName = "varchar(9)")]
        public string telefono { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string profesion { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string correoElectronico { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string comboReferencia { get; set; }

        [Required]
        [Column(TypeName = "varchar(16)")]
        public string contrasena { get; set; }

    }
}
