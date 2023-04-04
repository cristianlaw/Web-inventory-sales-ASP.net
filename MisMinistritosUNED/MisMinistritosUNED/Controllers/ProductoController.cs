using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using MisMinistritosUNED.Connection;
using MisMinistritosUNED.Controllers;
using MisMinistritosUNED.Models;
using System.Text;

namespace MisMinistritosUNED.Controllers
{
    public class ProductoController : Controller
    {
        
        public readonly IProducto _iProducto;
        public readonly IProveedor _iProveedor;
        public ProductoController(IProducto connection, IProveedor connectionP)
        {
            _iProducto = connection;
            _iProveedor = connectionP;
        }
  
        //Muestra la vista del formulario para crear el producto
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewBag.pro = await _iProveedor.TodosProveedor(); 
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductoModel producto)
        {
            try
            {                                
                producto.id = aleatorio();

                await _iProducto.AgregarProducto(producto);
                return RedirectToAction("List"); //Devuelve una vista con los datos del cliente
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }


        //Actualiza los datos del producto
        [HttpPost]
        public async Task<ActionResult> Update(ProductoModel producto)
        {
            try
            {
                await _iProducto.ActualizarProducto(producto);
                TempData["prodactualizado"] = "Producto actualizado con éxito";
                return View("Read", producto); //Regresa a una vista de detalles del producto
            }
            catch
            {
                return View();
            }
        }

        //Lee los datos del producto seleccionado y muestra el formulario de Update
        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            try
            {
                ViewBag.pro = await _iProveedor.TodosProveedor();
                object producto = await _iProducto.UnProducto(id);
                return View("Update", producto);
            }
            catch
            {
                return View("List");
            }
        }



        //Vista con los datos del producto 
        [HttpGet]
        public async Task<ActionResult> Read(ProductoModel productoLeer)
        {
            try
            {
                object producto = await _iProducto.UnProducto(productoLeer.id);
                return View(producto);
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


        //Borrar el producto
        [HttpPost]
        public async Task<ActionResult> Delete(ProductoModel productoBorrar)
        {
            try
            {
                await _iProducto.BorrarProducto(productoBorrar.id);
                var listaProvedor = await _iProducto.TodosProducto();
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
                var producto = await _iProducto.UnProducto(id); 
                return View(producto);
            }
            catch
            {

                return View();
            }
        }

        //Muesta en una vista de todos los productos
        public async Task<ActionResult> List()
        {
            try
            {
                var listaProducto = await _iProducto.TodosProducto();                
                return View(listaProducto);
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
