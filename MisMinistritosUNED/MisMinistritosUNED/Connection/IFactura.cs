using MisMinistritosUNED.Models.Combo;
using MisMinistritosUNED.Models;

namespace MisMinistritosUNED.Connection
{
    public interface IFactura
    {

        public Task<bool> AgregarFactura(FacturaModel factura);

        public Task<FacturaModel> UnFactura(string id);

        public Task<List<FacturaModel>> TodosFactura();

        public Task<bool> ActualizarFactura(FacturaModel factura);

        public Task<FacturaModel> BorrarFactura(string id);

    }
}
