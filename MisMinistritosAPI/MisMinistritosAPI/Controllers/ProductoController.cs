using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisMinistritosAPI.Data;
using MisMinistritosAPI.Models;
using MisMinistritosAPI.Models.ModelDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MisMinistritosAPI.Controllers
{
    [Route("ProductoApi/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly ContextDb _contextDb;

        public ProductoController(ContextDb contextDb) //Constructor para el controller.
        {
            _contextDb = contextDb;
        }


        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<List<ProductoModel>> GetListaCompleta()
        {
            try
            {
                var dato = await(from t in _contextDb.ProductoDbModel
                                 select new ProductoModel
                                 {
                                     id= t.id,
                                     descripcion = t.descripcion,   
                                     ingreso = t.ingreso,
                                     precioCompra = t.precioCompra,
                                     utilidad = t.utilidad,
                                     proveedor= t.proveedor,
                                 }
                    ).ToListAsync();
                return dato;
            }
            catch
            {
                return null;
            }
        }
                

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDbModel>> GetUnProducto(string id)
        {
            try
            {
                var dato = _contextDb.ProductoDbModel.FirstOrDefault(c => c.id == id);
                return dato;
            }
            catch (Exception)
            {

                return null;
            }
        }

        // POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductoModel producto)
        {
            try
            {
                if ((_contextDb.ProductoDbModel.Any(c => c.id == producto.id)) == true) return BadRequest("El producto ya existe");

                ProductoDbModel productoDb = new ProductoDbModel();
                productoDb.id = producto.id;
                productoDb.descripcion = producto.descripcion;
                productoDb.ingreso = producto.ingreso;
                productoDb.precioCompra = producto.precioCompra;
                productoDb.utilidad = producto.utilidad;
                productoDb.proveedor = producto.proveedor;                
                _contextDb.ProductoDbModel.Add(productoDb);
                await _contextDb.SaveChangesAsync();

                return Ok(productoDb);
            }
            catch
            {
                return null;
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public async Task PutAsync([FromBody] ProductoDbModel producto)
        {
            try
            {
                var dato = await(from t in _contextDb.ProductoDbModel where t.id == producto.id select t).FirstOrDefaultAsync();

                dato.id = producto.id;
                dato.descripcion = producto.descripcion;
                dato.ingreso = producto.ingreso;
                dato.precioCompra = producto.precioCompra;
                dato.utilidad = dato.utilidad;
                dato.proveedor = producto.proveedor;
                await _contextDb.SaveChangesAsync();
            }
            catch
            {

            }
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            try
            {
                var dato = await(from t in _contextDb.ProductoDbModel where t.id == id select t).FirstOrDefaultAsync();
                _contextDb.ProductoDbModel.Remove(dato);
                await _contextDb.SaveChangesAsync();
            }
            catch
            {

            }
        }
    }
}
