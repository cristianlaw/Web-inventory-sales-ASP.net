using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using MisMinistritosUNED.Connection;
using MisMinistritosUNED.Models;
using System.IO;
using System.Text;

namespace MisMinistritosUNED.Controllers
{
    public class TourController : Controller //Clase que hereda de la clase Controller 
    {
        

        public readonly ITour _iTour;
        public readonly IProveedor _iProveedor;
        public TourController(ITour connection, IProveedor connectionP)
        {
            _iTour = connection;
            _iProveedor = connectionP;
        }

        //Muestra la vista del formulario para crear el tour
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewBag.pro = await _iProveedor.TodosProveedor();
                ViewBag.DatosTour = await _iTour.ComboBoxTour();
                return View();
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(TourModel tour)
        {
            try
            {
                
                tour.id = aleatorio();

                byte[] bytes = null;
                if (tour.imagen != null)
                {
                    using (Stream fs = tour.imagen.OpenReadStream())
                    {
                        using(BinaryReader br = new BinaryReader(fs))
                        {
                            bytes=br.ReadBytes((Int32)fs.Length);
                        }
                    }
                    tour.verImagen = Convert.ToBase64String(bytes,0,bytes.Length);
                }


                await _iTour.AgregarTour(tour);
                return RedirectToAction("List"); //Devuelve una vista con los datos del cliente
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }



//Actualiza los datos del tour
[HttpPost]
        public async Task<ActionResult> Update(TourModel tour)
        {
            try
            {

                byte[] bytes = null;
                if (tour.imagen != null)
                {
                    using (Stream fs = tour.imagen.OpenReadStream())
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            bytes = br.ReadBytes((Int32)fs.Length);
                        }
                    }
                    tour.verImagen = Convert.ToBase64String(bytes, 0, bytes.Length);
                }

                await _iTour.ActualizarTour(tour);
                TempData["touractualizado"] = "Tour actualizado con éxito";
                return View("Read", tour); //Devuelve una vista con los datos del tour
            }
            catch
            {
                return View();
            }
        }

        //Lee los datos del tour seleccionado y muestra el formulario de Update
        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            try
            {
                ViewBag.pro = await _iProveedor.TodosProveedor();
                ViewBag.DatosTour = await _iTour.ComboBoxTour();
                object tour = await _iTour.UnTour(id);
                return View("Update", tour);
            }
            catch
            {
                return View("List");
            }
        }



        //Vista con los datos del tour 
        [HttpGet]
        public async Task<ActionResult> Read(TourModel tourLeer)
        {
            try
            {
                object tour = await _iTour.UnTour(tourLeer.id);
                return View(tour);
            }
            catch
            {
                return View("List");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Read()
        {
            return View();
        }


        //Borrar el tour
        [HttpPost]
        public async Task<ActionResult> Delete(TourModel tourBorrar)
        {
            try
            {
                await _iTour.BorrarTour(tourBorrar.id);
                var listaProvedor = await _iTour.TodosTour();
                return RedirectToAction("List", listaProvedor);
                
            }
            catch
            {
                return View();
            }
        }


        //Vista con los datos del prpveedor antes de borrar
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                
                var listaTour = await _iTour.UnTour(id);
                return View(listaTour);
            }
            catch
            {

                return View();
            }
        }

        //Muesta en una vista de todos los tours
        public async Task<ActionResult> List()
        {
            try
            {
                var listaTour = await _iTour.TodosTour();
                return View(listaTour);
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
