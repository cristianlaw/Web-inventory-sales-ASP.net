using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MisMinistritosAPI.Models.ModelDb;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MisMinistritosAPI.Models
{
    public class FacturaModel
    {
        public string facturaId { get; set; }


        public string clienteCedula { get; set; }
        public string clienteNombre { get; set; }



        public decimal montoSubTotal { get; set; }
        public decimal montoIVA { get; set; }
        public decimal montoTotal { get; set; }
        public List<VentaModel>? productosFactura { get; set; }


       


    }
}
