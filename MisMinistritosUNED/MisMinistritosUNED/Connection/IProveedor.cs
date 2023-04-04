using MisMinistritosUNED.Models.Combo;
using MisMinistritosUNED.Models;
using Microsoft.AspNetCore.Mvc;

namespace MisMinistritosUNED.Connection
{
    public interface IProveedor
    {
        public Task<bool> AgregarProveedor(ProveedorModel cliente);

        public Task<ProveedorModel> UnProveedor(string cedula);

        public Task<List<ProveedorModel>> TodosProveedor();

        public Task<bool> ActualizarProveedor(ProveedorModel cliente);

        public Task<ProveedorModel> BorrarProveedor(string cedula);
       
    }
}
