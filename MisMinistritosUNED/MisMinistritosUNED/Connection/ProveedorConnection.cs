using Microsoft.AspNetCore.Mvc;
using MisMinistritosUNED.Models;
using Newtonsoft.Json;
using System.Text;

namespace MisMinistritosUNED.Connection
{
    public class ProveedorConnection : IProveedor
    {

        private string _baseurl;

        public ProveedorConnection()
        {
            _baseurl = "http://localhost:5182";
        }

        public async Task<bool> AgregarProveedor(ProveedorModel proveedors)
        {
            bool respuesta = false;
            var proveedor = new HttpClient();
            proveedor.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(proveedors), Encoding.UTF8, "application/json");
            var response = await proveedor.PostAsync($"ProveedorApi/Proveedor", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> ActualizarProveedor(ProveedorModel proveedors)
        {
            bool respuesta = false;
            var proveedor = new HttpClient();
            proveedor.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(proveedors), Encoding.UTF8, "application/json");
            var response = await proveedor.PutAsync($"ProveedorApi/Proveedor", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<ProveedorModel> BorrarProveedor(string cedula)
        {
            ProveedorModel proveedorEncontrado = null;

            var proveedor = new HttpClient();
            proveedor.BaseAddress = new Uri(_baseurl);

            var respuesta = await proveedor.DeleteAsync($"ProveedorApi/Proveedor/{cedula}");


            return null;
        }

        public async Task<ProveedorModel> UnProveedor(string cedula)
        {
            ProveedorModel proveedorEncontrado = null;

            var proveedor = new HttpClient();
            proveedor.BaseAddress = new Uri(_baseurl);

            var respuesta = await proveedor.GetAsync($"ProveedorApi/Proveedor/{cedula}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ProveedorModel>(json_respuesta);
                proveedorEncontrado = resultado;
            }

            return proveedorEncontrado;
        }

        public async Task<List<ProveedorModel>> TodosProveedor()
        {
            List<ProveedorModel> listaTemporal = new List<ProveedorModel>();

            var proveedor = new HttpClient();
            proveedor.BaseAddress = new Uri(_baseurl);

            var respuesta = await proveedor.GetAsync("ProveedorApi/Proveedor");

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
