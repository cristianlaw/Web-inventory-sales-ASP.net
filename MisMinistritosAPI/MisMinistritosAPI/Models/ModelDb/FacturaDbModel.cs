using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MisMinistritosAPI.Models.ModelDb
{
    public class FacturaDbModel
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string facturaId { get; set; }




        [Required]
        [Column(TypeName = "varchar(12)")]
        public string clienteCedula { get; set; }
        [Required]
        [Column(TypeName = "varchar(60)")]
        public string clienteNombre { get; set; }




        [Required]
        [Column(TypeName = "decimal")]
        public decimal montoSubTotal { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal montoIVA { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal montoTotal { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        public List<VentaDbModel>? productosFactura { get; set; }
        
    }
}
