using MisMinistritosUNED.Models.Combo;
using MisMinistritosUNED.Models;
using Newtonsoft.Json;
using System.Text;

namespace MisMinistritosUNED.Connection
{
    public class FacturaConnection : IFactura
    {
        private string _baseurl;

        public FacturaConnection()
        {
            _baseurl = "http://localhost:5182";
        }

        public async Task<bool> AgregarFactura(FacturaModel facturas)
        {
            bool respuesta = false;
            var factura = new HttpClient();
            factura.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(facturas), Encoding.UTF8, "application/json");
            var response = await factura.PostAsync($"FacturaApi/Factura", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> ActualizarFactura(FacturaModel facturas)
        {
            bool respuesta = false;
            var factura = new HttpClient();
            factura.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(facturas), Encoding.UTF8, "application/json");
            var response = await factura.PutAsync($"FacturaApi/Factura", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<FacturaModel> BorrarFactura(string id)
        {
            FacturaModel facturaEncontrado = null;

            var factura = new HttpClient();
            factura.BaseAddress = new Uri(_baseurl);

            var respuesta = await factura.DeleteAsync($"FacturaApi/Factura/{id}");


            return null;
        }

        public async Task<FacturaModel> UnFactura(string id)
        {
            FacturaModel facturaEncontrado = null;

            var factura = new HttpClient();
            factura.BaseAddress = new Uri(_baseurl);

            var respuesta = await factura.GetAsync($"FacturaApi/Factura/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<FacturaModel>(json_respuesta);
                facturaEncontrado = resultado;
            }

            return facturaEncontrado;
        }

        public async Task<List<FacturaModel>> TodosFactura()
        {
            List<FacturaModel> listaTemporal = new List<FacturaModel>();

            var factura = new HttpClient();
            factura.BaseAddress = new Uri(_baseurl);

            var respuesta = await factura.GetAsync("FacturaApi/Factura");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<FacturaModel>>(json_respuesta);
                listaTemporal = resultado;
            }

            return listaTemporal;
        }

    }
}
