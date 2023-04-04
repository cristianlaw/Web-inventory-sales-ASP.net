

namespace MisMinistritosUNED.Models
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
