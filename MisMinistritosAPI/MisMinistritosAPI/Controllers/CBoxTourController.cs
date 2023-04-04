using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MisMinistritosAPI.Data;
using MisMinistritosAPI.Models.Combo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MisMinistritosAPI.Controllers
{
    [Route("CBoxTourApi/[controller]")]
    [ApiController]
    public class CBoxTourController : ControllerBase
    {
        private static readonly IMemoryCache _memoriaCache = new MemoryCache(new MemoryCacheOptions());

        // GET: api/<DiasTourController>
        [HttpGet]
        public List<DiasTour> CBoxTour() //Retorna toda la lista 
        {
            try
            {
                List<DiasTour> referenciaTemp = new List<DiasTour>();
                referenciaTemp.Add(new DiasTour() { idTour = 1, nombreTour = "Lunes a viernes" });
                referenciaTemp.Add(new DiasTour() { idTour = 2, nombreTour = "Sábado y domingo" });
                referenciaTemp.Add(new DiasTour() { idTour = 3, nombreTour = "Solo sábado" });
                referenciaTemp.Add(new DiasTour() { idTour = 4, nombreTour = "Solo Domingo" });

                return referenciaTemp;
            }
            catch
            {
                return null;
            }
        }
    }
}
