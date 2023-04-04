using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisMinistritosAPI.Data;
using MisMinistritosAPI.Models;
using MisMinistritosAPI.Models.ModelDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MisMinistritosAPI.Controllers
{
    [Route("ProveedorApi/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ContextDb _contextDb;

        public ProveedorController(ContextDb contextDb) //Constructor para el controller.
        {
            _contextDb = contextDb;
        }


        // GET: api/<ProveedorController>
        [HttpGet]
        public async Task<List<ProveedorModel>> GetListaCompleta()
        {
            try
            {
                agregarProvedores();
                var dato = await(from t in _contextDb.ProveedorDbModel
                                    select new ProveedorModel
                                    {
                                        cedula = t.cedula,
                                        descripcion = t.descripcion,                                        
                                    }
                    ).ToListAsync();

                return dato;
            }
            catch
            {
                return null;
            }
        }

        // GET api/<ProveedorController>/5
        [HttpGet("{cedula}")]
        public async Task<ActionResult<ProveedorDbModel>> GetUnProveedor(string cedula)
        {
            try
            { 
                var dato = _contextDb.ProveedorDbModel.FirstOrDefault(c => c.cedula == cedula);                
                return dato;
            }
            catch (Exception)
            {

                return null;
            }
        }

        // POST api/<ProveedorController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProveedorDbModel proveedor)
        {
            try
            {  
                if ((_contextDb.ProveedorDbModel.Any(c => c.cedula == proveedor.cedula)) == true) return BadRequest("El proveedor ya existe");               
                _contextDb.ProveedorDbModel.Add(proveedor);
                await _contextDb.SaveChangesAsync();

                return Ok(proveedor);
            }
            catch
            {
                return null;
            }
        }

        // PUT api/<ProveedorController>/5
        [HttpPut]
        public async Task PutAsync([FromBody] ProveedorModel proveedor)
        {
            try
            {
                var dato = await(from t in _contextDb.ProveedorDbModel where t.cedula == proveedor.cedula select t).FirstOrDefaultAsync();
                dato.cedula = proveedor.cedula;
                dato.descripcion = proveedor.descripcion;        
                await _contextDb.SaveChangesAsync();
            }
            catch
            {

            }
        }

        // DELETE 
        [HttpDelete("{cedula}")]
        public async Task DeleteAsync(string cedula)
        {
            try
            {
                var dato = await(from t in _contextDb.ProveedorDbModel where t.cedula == cedula select t).FirstOrDefaultAsync();
                _contextDb.ProveedorDbModel.Remove(dato);
                await _contextDb.SaveChangesAsync();
            }
            catch
            {

            }
        }

        private void agregarProvedores()
        {
            var proveedor = _contextDb.ProveedorDbModel.FirstOrDefault(c => c.cedula == "02-2222-2222");

            if (proveedor == null)
            {
                ProveedorDbModel prov = new ProveedorDbModel();
                prov.cedula = "02-2222-2222";
                prov.descripcion = "Viajes Turisticos S.A 02";
                _contextDb.ProveedorDbModel.Add(prov);
                _contextDb.SaveChanges();
            }
        }
    }
}
