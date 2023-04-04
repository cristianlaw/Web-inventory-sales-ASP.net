using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MisMinistritosAPI.Data;
using MisMinistritosAPI.Models;
using MisMinistritosAPI.Models.Combo;
using MisMinistritosAPI.Models.ModelDb;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MisMinistritosAPI.Controllers
{
    [Route("TourApi/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ContextDb _contextDb;

        public TourController(ContextDb contextDb) //Constructor para el controller.
        {
            _contextDb = contextDb;
        }


        // GET: api/<TourController>
        [HttpGet]
        public async Task<List<TourModel>> GetListaCompleta()
        {
            try
            {
                //var dato = _contextDb.TourDbModel.ToListAsync();

                var dato = await (from t in _contextDb.TourDbModel
                                  select new TourModel
                                  {
                                      id = t.id,
                                      descripcion = t.descripcion,
                                      verImagen = t.verImagen,
                                      diasTour = t.diasTour,
                                      precioTour = t.precioTour,
                                      proveedor = t.proveedor,
                                  }
                    ).ToListAsync();

                return dato;
            }
            catch
            {
                return null;
            }
        }

        // GET api/<TourController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourDbModel>> GetUnTour(string id)
        {
            try
            {
                var dato = _contextDb.TourDbModel.FirstOrDefault(c => c.id == id);
                return dato;
            }
            catch (Exception)
            {

                return null;
            }
        }

        // POST api/<TourController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TourModel tour)
        {
            try
            {
                if ((_contextDb.TourDbModel.Any(c => c.id == tour.id)) == true) return BadRequest("El tour ya existe");

                TourDbModel tourDb = new TourDbModel();
                tourDb.id = tour.id;
                tourDb.descripcion = tour.descripcion;
                tourDb.verImagen = tour.verImagen;
                tourDb.diasTour = tour.diasTour;
                tourDb.precioTour = tour.precioTour;
                tourDb.proveedor = tour.proveedor;               
               
                _contextDb.TourDbModel.Add(tourDb);
                await _contextDb.SaveChangesAsync();

                return Ok(tour);
            }
            catch
            {
                return null;
            }
        }

        // PUT api/<TourController>/5
        [HttpPut]
        public async Task PutAsync([FromBody] TourModel tour)
        {
            try
            {
                var tourDb = await (from t in _contextDb.TourDbModel where t.id == tour.id select t).FirstOrDefaultAsync();

                tourDb.id = tour.id;
                tourDb.descripcion = tour.descripcion;
                tourDb.verImagen = tour.verImagen;
                tourDb.diasTour = tour.diasTour;
                tourDb.precioTour = tour.precioTour;
                tourDb.proveedor = tour.proveedor;
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
                var dato = await (from t in _contextDb.TourDbModel where t.id == id select t).FirstOrDefaultAsync();
                _contextDb.TourDbModel.Remove(dato);
                await _contextDb.SaveChangesAsync();
            }
            catch
            {

            }
        }
    }
}
