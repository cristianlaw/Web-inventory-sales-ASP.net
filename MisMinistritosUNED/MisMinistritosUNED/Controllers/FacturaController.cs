using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisMinistritosUNED.Models;
using MisMinistritosUNED.Connection;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.ComponentModel;

namespace MisMinistritosUNED.Controllers
{
    public class FacturaController : Controller
    {
        private readonly IFactura _iFactura;
        public readonly IProducto _iProducto;
        public readonly IProveedor _iProveedor;
        private readonly ICliente _iCliente;
        private readonly ITour _iTour;

        public FacturaController(IFactura connection, IProducto connectionProd, IProveedor connectionProv, ICliente connectionCli, ITour iTour)
        {
            _iFactura = connection;
            _iProducto = connectionProd;
            _iProveedor = connectionProv;
            _iCliente = connectionCli;
            _iTour = iTour;
        }

        public static string cedulaVista;

        //Muestra la vista del formulario para crear el factura
        [HttpGet]
        public async Task<ActionResult> Create(string cedula)
        {
            try
            {
                var cliente = await _iCliente.UnCliente(cedula);
                cedulaVista = cedula;

                ViewBag.pro = await _iProveedor.TodosProveedor();

                return View(cliente);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<JsonResult> AutoCompleteProducto(string search)
        {
            try
            {
                List<Autocompletar> autocomplete = new List<Autocompletar>();
                List<ProductoModel> respuesta = new List<ProductoModel>();
                respuesta = await _iProducto.TodosProducto();

                autocomplete = respuesta.Where(x => string.Concat(x.id.ToUpper(), x.descripcion.ToUpper()).Contains(search.ToUpper()))
                    .Select(m => new Autocompletar
                    {
                        label = $"{m.id} - {m.descripcion}",
                        value = m.id
                    }).ToList();

                return Json(autocomplete);

            }
            catch (Exception)
            {

                return null;
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> AutoCompleteTour(string buscar)
        {
            try
            {
                List<Autocompletar> autocomplete = new List<Autocompletar>();
                List<TourModel> respuesta = new List<TourModel>();
                respuesta = await _iTour.TodosTour();

                autocomplete = respuesta.Where(x => string.Concat(x.id.ToUpper(), x.descripcion.ToUpper()).Contains(buscar.ToUpper()))
                    .Select(m => new Autocompletar
                    {
                        label = $"{m.id} - {m.descripcion}",
                        value = m.id
                    }).ToList();

                return Json(autocomplete);

            }
            catch (Exception)
            {

                return null;
            }

        }

        [HttpGet]
        public async Task<JsonResult> ObtenerProducto(string idproducto)
        {
            try
            {
                ProductoModel? oProducto = new ProductoModel();
                List<ProductoModel> respuesta = new List<ProductoModel>();
                respuesta = await _iProducto.TodosProducto();

                oProducto = respuesta.Where(x => x.id == idproducto).FirstOrDefault();

                return Json(oProducto);

            }
            catch (Exception)
            {

                return null;
            }            
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerTour(string idtour)
        {
            try
            {
                TourModel? tourTodos = new TourModel();
                List<TourModel> respuesta = new List<TourModel>();
                respuesta = await _iTour.TodosTour();

                tourTodos = respuesta.Where(x => x.id == idtour).FirstOrDefault();

                return Json(tourTodos);

            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarVenta([FromBody] FacturaModel body)
        {
            try
            {                
                body.clienteCedula = cedulaVista;
                body.clienteNombre = (await _iCliente.UnCliente(cedulaVista)).nombreCompleto;
                body.facturaId = aleatorio();

                if (await _iFactura.AgregarFactura(body) == false)
                {                   
                    return Json(new { respuesta = "error" });                    
                }
                return Json(new { respuesta = body.facturaId });


            }
            catch (Exception)
            {

                return null;
            }
            
        }


        //Muesta en una vista de todos los facturas
        public async Task<ActionResult> List(string cedula)
        {
            try
            {
                List<FacturaModel> respuesta2 = new List<FacturaModel>();
                List<FacturaModel> respuesta = new List<FacturaModel>();
                respuesta = await _iFactura.TodosFactura();

                foreach (FacturaModel ff in respuesta)
                {
                    if (ff.clienteCedula == cedulaVista)
                    {
                        respuesta2.Add(ff);
                    }
                }
                return View(respuesta2);
            }
            catch
            {
                return View();
            }
        }

        //Muesta en una vista de todos los facturas
        public async Task<ActionResult> ListFactura(string cedula)
        {
            try
            {                
                var respuesta = await _iFactura.TodosFactura();

                return View(respuesta);
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<ActionResult> Read(string facturaId)
        {
            try
            {
                var factura = await _iFactura.UnFactura(facturaId);

                ViewBag.cliente = factura.clienteNombre;
                ViewBag.cedula = factura.clienteCedula;
                ViewBag.idFactura = factura.facturaId;
                ViewBag.sub = factura.montoSubTotal;
                ViewBag.iva = factura.montoIVA;
                ViewBag.total = factura.montoTotal;

                return View(factura.productosFactura);

            }
            catch
            {
                return View();
            }

        }

        //Borrar el producto
        [HttpGet]
        public async Task<ActionResult> Delete(FacturaModel facturaBorrar)
        {
            try
            {
                await _iFactura.BorrarFactura(facturaBorrar.facturaId);
                var listaFactura = await _iFactura.TodosFactura();
                return RedirectToAction("List", listaFactura);
            }
            catch
            {
                return View();
            }
        }



        private string aleatorio()
        {
            Random tam = new Random();
            int n = tam.Next(10, 15);
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var salida = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < n; i++)
            {
                salida.Append(chars[random.Next(chars.Length)]);
            }

            return salida.ToString();
        }


    }
}
