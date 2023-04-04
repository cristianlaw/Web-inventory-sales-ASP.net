using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MisMinistritosAPI.Data;
using MisMinistritosAPI.Models.Combo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MisMinistritosAPI.Controllers
{
    [Route("ComboApi/[controller]")]
    [ApiController]
    public class ComboBoxController : ControllerBase
    {
        private static readonly IMemoryCache _memoriaCache = new MemoryCache(new MemoryCacheOptions());


        // GET: api/<ComboBoxController>
        [HttpGet]
        public List<Referencia> ComboBoxCliente() //Retorna toda la lista 
        {
            try
            {
                List<Referencia> referenciaTemporal = new List<Referencia>();
                referenciaTemporal.Add(new Referencia() { idReferencia = 1, nombreReferencia = "Por un Amigo" });
                referenciaTemporal.Add(new Referencia() { idReferencia = 2, nombreReferencia = "Por redes sociales" });
                referenciaTemporal.Add(new Referencia() { idReferencia = 3, nombreReferencia = "Ya había comprado antes" });

                return referenciaTemporal;
            }
            catch
            {
                return null;
            }
        }


    }
}
