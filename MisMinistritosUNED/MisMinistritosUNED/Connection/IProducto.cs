using MisMinistritosUNED.Models;

namespace MisMinistritosUNED.Connection
{
    public interface IProducto
    {
        public Task<bool> AgregarProducto(ProductoModel cliente);

        public Task<ProductoModel> UnProducto(string cedula);

        public Task<List<ProductoModel>> TodosProducto();

        public Task<bool> ActualizarProducto(ProductoModel cliente);

        public Task<ProductoModel> BorrarProducto(string cedula);

        public Task<List<ProveedorModel>> TodosProveedor();
        
    }
}
