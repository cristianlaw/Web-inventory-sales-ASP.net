using MisMinistritosUNED.Models;
using System.Text;
using Newtonsoft.Json;
using MisMinistritosUNED.Models.Combo;

namespace MisMinistritosUNED.Connection
{
    public class ClienteConnection : ICliente
    {
        private string _baseurl;

        public ClienteConnection()
        {
            _baseurl = "http://localhost:5182";
        }

        public async Task<bool> AgregarCliente(ClienteModel clientes)
        {
            bool respuesta = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(clientes), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"ConectarApi/Cliente", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> ActualizarCliente(ClienteModel clientes)
        {
            bool respuesta = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var contenido = new StringContent(JsonConvert.SerializeObject(clientes), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"ConectarApi/Cliente", contenido);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<ClienteModel> BorrarCliente(string cedula)
        {
            ClienteModel clienteEncontrado = null;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var respuesta = await cliente.DeleteAsync($"ConectarApi/Cliente/{cedula}");

            
            return null;
        }

        public async Task<ClienteModel> UnCliente(string cedula)
        {
            ClienteModel clienteEncontrado = null;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var respuesta = await cliente.GetAsync($"ConectarApi/Cliente/{cedula}");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ClienteModel>(json_respuesta);
                clienteEncontrado = resultado;
            }

            return clienteEncontrado;
        }

        public async Task<List<ClienteModel>> TodosCliente()
        {
            List<ClienteModel> listaTemporal = new List<ClienteModel>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var respuesta = await cliente.GetAsync("ConectarApi/Cliente");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<ClienteModel>>(json_respuesta);
                listaTemporal = resultado;
            }

            return listaTemporal;
        }

        public async Task<List<Referencia>> ComboBoxCliente()
        {
            List<Referencia> referenciaTemporal = new List<Referencia>();

            var referencia = new HttpClient();
            referencia.BaseAddress = new Uri(_baseurl);

            var respuesta = await referencia.GetAsync("ComboApi/ComboBox");

            if (respuesta.IsSuccessStatusCode)
            {
                var json_respuesta = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Referencia>>(json_respuesta);
                referenciaTemporal = resultado;
            }
            return referenciaTemporal;
        }

       
    }
}