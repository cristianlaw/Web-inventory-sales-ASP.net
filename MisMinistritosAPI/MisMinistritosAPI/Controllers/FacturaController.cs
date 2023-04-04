using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using MisMinistritosAPI.Data;
using MisMinistritosAPI.Models;
using MisMinistritosAPI.Models.ModelDb;
using System.Collections;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MisMinistritosAPI.Controllers
{
    [Route("FacturaApi/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {

        private readonly ContextDb _contextDb;

        public FacturaController(ContextDb contextDb) //Constructor para el controller.
        {
            _contextDb = contextDb;
        }


        // GET: api/<FacturaController>
        [HttpGet]
        public async Task<List<FacturaModel>> GetListaCompleta()
        {
            try                
            {               
                var dato = await (from t in _contextDb.FacturaDbModel
                                  select new FacturaModel
                                  {
                                      facturaId = t.facturaId,
                                      clienteCedula = t.clienteCedula,
                                      clienteNombre = t.clienteNombre,
                                      montoSubTotal = t.montoSubTotal,
                                      montoIVA = t.montoIVA,
                                      montoTotal = t.montoTotal,
                                  }
                                  ).ToListAsync();
                return dato;

            }
            catch
            {
                return null;
            }
        }


        //GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDbModel>> GetUnFactura(string id)
        {
            try
            {                
                var factura = _contextDb.FacturaDbModel.FirstOrDefault(c => c.facturaId == id);
                factura.productosFactura = await _contextDb.FacturaDbModel.Where(x => x.facturaId == id).Select(x => x.productosFactura).FirstOrDefaultAsync();
                return factura;
            }
            catch (Exception)
            {

                return null;
            }
        }

        // POST api/<FacturaController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] FacturaDbModel factura)
        {
            try
            {                
                _contextDb.FacturaDbModel.Add(factura);
                await _contextDb.SaveChangesAsync();
                return Ok(factura);
            }
            catch
            {
                return null;
            }
        }


        // DELETE 
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            try
            {
                var dato = await (from t in _contextDb.FacturaDbModel where t.facturaId == id select t).FirstOrDefaultAsync();
                dato.productosFactura = await _contextDb.FacturaDbModel.Where(x => x.facturaId == id).Select(x => x.productosFactura).FirstOrDefaultAsync();

                _contextDb.FacturaDbModel.Remove(dato);
                _contextDb.VentaDbModel.RemoveRange(dato.productosFactura);
                await _contextDb.SaveChangesAsync();
            }
            catch
            {

            }
        }


    }



}
