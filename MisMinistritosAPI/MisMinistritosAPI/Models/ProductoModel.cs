using System.ComponentModel.DataAnnotations;

namespace MisMinistritosAPI.Models
{
    public class ProductoModel
    {
        public string id { get; set; }

        public string descripcion { get; set; }

        public int ingreso { get; set; }

        public int precioCompra { get; set; }

        public int utilidad { get; set; }

       public string proveedor { get; set; }


    }
}
