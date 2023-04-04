using MisMinistritosUNED.Models;
using Newtonsoft.Json;
using System.Text;

namespace MisMinistritosUNED.Connection
{
    public class ProductoConnection : IProducto
    {

        private string _baseurl;

        public ProductoConnection()
        {
            _baseurl = "http://localhost:5182";
        }

        public async Task<bool> AgregarProducto(ProductoModel productos)
        {
            bool respuesta = false;
            var producto = new HttpClient();
            producto.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(productos), Encoding.UTF8, "application/json");
            var response = await producto.PostAsync($"ProductoApi/Producto", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> ActualizarProducto(ProductoModel productos)
        {
            bool respuesta = false;
            var producto = new HttpClient();
            producto.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(productos), Encoding.UTF8, "application/json");
            var response = await producto.PutAsync($"ProductoApi/Producto", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<ProductoModel> BorrarProducto(string id)
        {
            ProductoModel productoEncontrado = null;

            var producto = new HttpClient();
            producto.BaseAddress = new Uri(_baseurl);

            var respuesta = await producto.DeleteAsync($"ProductoApi/Producto/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ProductoModel>(json_respuesta);
                productoEncontrado = resultado;
            }

            return productoEncontrado;
        }

        public async Task<ProductoModel> UnProducto(string id)
        {
            ProductoModel productoEncontrado = null;

            var producto = new HttpClient();
            producto.BaseAddress = new Uri(_baseurl);

            var respuesta = await producto.GetAsync($"ProductoApi/Producto/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ProductoModel>(json_respuesta);
                productoEncontrado = resultado;
            }

            return productoEncontrado;
        }

        public async Task<List<ProductoModel>> TodosProducto()
        {
            var listaTemporal = new List<ProductoModel>();

            var producto = new HttpClient();
            producto.BaseAddress = new Uri(_baseurl);

            var respuesta = await producto.GetAsync("ProductoApi/Producto");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<ProductoModel>>(json_respuesta);
                listaTemporal = resultado;
            }

            return listaTemporal;
        }

        public async Task<List<ProveedorModel>> TodosProveedor()
        {
            List<ProveedorModel> listaTemporal = new List<ProveedorModel>();

            var producto = new HttpClient();
            producto.BaseAddress = new Uri(_baseurl);

            var respuesta = await producto.GetAsync($"ProductoApi/Producto/getproveedor");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<ProveedorModel>>(json_respuesta);
                listaTemporal = resultado;
            }

            return listaTemporal;
        }

    }
}
