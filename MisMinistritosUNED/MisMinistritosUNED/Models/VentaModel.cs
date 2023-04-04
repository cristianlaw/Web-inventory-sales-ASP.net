using MisMinistritosUNED.Connection;

namespace MisMinistritosUNED.Models
{
    public class VentaModel
    {
        public string idDatoF { get; set; }
        public string descripcionDatoF { get; set; }
        public decimal productoPrecio { get; set; }
        public int cantidadProducto { get; set; }
        public decimal totalProducto { get; set; }
    }
}
