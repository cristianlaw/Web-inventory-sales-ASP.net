using MisMinistritosUNED.Models;
using MisMinistritosUNED.Models.Combo;


namespace MisMinistritosUNED.Connection
{
    public interface ICliente
    {
        public Task<bool> AgregarCliente(ClienteModel cliente);

        public Task<ClienteModel> UnCliente(string cedula);

        public Task<List<ClienteModel>> TodosCliente();

        public Task<bool> ActualizarCliente(ClienteModel cliente);

        public Task<ClienteModel> BorrarCliente(string cedula);

        public Task<List<Referencia>> ComboBoxCliente();

        

    }
}
