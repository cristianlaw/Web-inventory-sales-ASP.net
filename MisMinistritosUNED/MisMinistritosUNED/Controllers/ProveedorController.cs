using Microsoft.AspNetCore.Mvc;
using MisMinistritosUNED.Connection;
using MisMinistritosUNED.Models;
using System.Collections.Immutable;

namespace MisMinistritosUNED.Controllers
{
    public class ProveedorController : Controller //Clase que hereda de la clase Controller 
    {
        public readonly IProducto _iProducto;
        public readonly IProveedor _iProveedor;
        public readonly ITour _iTour;
        public ProveedorController(IProducto connection, IProveedor connectionP, ITour connectionT)
        {
            _iProducto = connection;
            _iProveedor = connectionP;
            _iTour = connectionT;
        }


        //Muestra la vista del formulario para crear el proveedor
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }


        //Almacena lo que se registra en el formulario de crear proveedor
        [HttpPost]
        public async Task<ActionResult> Create(ProveedorModel proveedor)
        {
            try
            {  
                if (await _iProveedor.AgregarProveedor(proveedor) == false)
                {
                    TempData["provexiste"] = "El proveedor ya existe"; //Mensaje
                    return View();
                }
                var listaProvedor = await _iProveedor.TodosProveedor();
                return View("List", listaProvedor); //Devuelve una vista con los datos del cliente
            }
            catch
            {
                return RedirectToAction("CreateProveedor");
            }
        }





        //Actualiza los datos del proveedor
        [HttpPost]
        public async Task<ActionResult> Update(ProveedorModel proveedor)
        {
            try
            {
                await _iProveedor.ActualizarProveedor(proveedor);
                TempData["provactualizado"] = "Proveedor actualizado con éxito";
                return View("Read", proveedor); //Regresa a una vista de detalles del proveedor
            }
            catch
            {
                return View();
            }
        }

        //Lee los datos del proveedor seleccionado y muestra el formulario de Update
        [HttpGet]
        public async Task<ActionResult> Update(string cedula)
        {
            try
            {
                object proveedor = await _iProveedor.UnProveedor(cedula);
                return View("Update", proveedor);
            }
            catch
            {
                return View("List");
            }
        }



        //Vista con los datos del proveedor 
        [HttpGet]
        public async Task<ActionResult> Read(ProveedorModel proveedorLeer)
        {
            try
            {
                object proveedor = await _iProveedor.UnProveedor(proveedorLeer.cedula);
                return View(proveedor);
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


        //Borrar el proveedor
        [HttpPost]
        public async Task<ActionResult> Delete(ProveedorModel proveedorBorrar)
        {
            try
            {   

                await _iProveedor.BorrarProveedor(proveedorBorrar.cedula);
                var listaProvedor = await _iProveedor.TodosProveedor();
                TempData["ActionProvedor"] = "Proveedor borrado";
                return RedirectToAction("List", listaProvedor);
               
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Replace(string cedula, string proveedor)
        {
            try
            {
                var producto = await _iProducto.TodosProducto();
                foreach (var item in producto)
                {
                    if (item.proveedor == cedula)
                    {
                        item.proveedor = proveedor; //Añade el nuevo proveedor
                        await _iProducto.BorrarProducto(item.id); //Borrar los productos 
                        await _iProducto.AgregarProducto(item); //Añade los productos
                    }
                }

                var tour = await _iTour.TodosTour();
                foreach (var item in tour)
                {
                    if (item.proveedor == cedula)
                    {
                        item.proveedor = proveedor; //Añade el nuevo tour
                        await _iTour.BorrarTour(item.id); //Borrar los tour
                        await _iTour.AgregarTour(item); //Añade los tour
                    }
                }

                await _iProveedor.BorrarProveedor(cedula); //Borra el proveedor
                var listaProvedor = await _iProveedor.TodosProveedor();
                TempData["ActionProvedor"] = "El proveedor ha sido remplazado en los productos y tours.";
                return RedirectToAction("List", listaProvedor);

                return View("List");
                return null;
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Replace(string cedula)
        {
            try
            {

                ViewBag.pro = await _iProveedor.TodosProveedor();
                var proveedor = await _iProveedor.UnProveedor(cedula);
                return View("Replace", proveedor);
                return null;
            }
            catch
            {

                return View();
            }
        }


        //Vista con los datos del proveedor antes de borrar
        [HttpGet]
        public async Task<ActionResult> Delete(string cedula)
        {
            try
            {
                List<ProveedorModel> lista = new List<ProveedorModel>();
                bool irReplace = false;

                var todosProveedor = await _iProveedor.TodosProveedor();
                var producto = await _iProducto.TodosProducto();
                foreach (var item in producto) 
                {
                    if (item.proveedor == cedula) //Si existe un producto con el proveedor a borrar, entra
                    {
                        irReplace = true;                       
                        foreach (var todosP in todosProveedor)
                        {
                            bool siLista = false;
                            if (todosP.cedula != cedula) //Descarta el proveedor actual en el combobox 
                                foreach (var newP in lista)
                                {
                                    if (newP.cedula == todosP.cedula)
                                    {
                                        siLista = true;
                                    }
                                }
                            if (siLista == false && todosP.cedula != cedula)
                            {
                                lista.Add(todosP);
                            }
                        }
                    }
                }

                var tour = await _iTour.TodosTour();
                foreach (var item in tour)
                {
                    if (item.proveedor == cedula) //Si existe un producto con el proveedor a borrar, entra
                    {
                        irReplace = true;
                        foreach (var todosP in todosProveedor)
                        {
                            bool siLista = false;
                            if (todosP.cedula != cedula) //Descarta el proveedor actual en el combobox 
                            {
                                foreach(var newP in lista)
                                {
                                    if (newP.cedula == todosP.cedula)
                                    {
                                        siLista = true;
                                    }
                                }
                                if (siLista == false)
                                {
                                    lista.Add(todosP);
                                }                                
                            }
                        }
                    }
                }
                if(irReplace == true)
                {
                    if (lista.Count < 1) //Si solo existe un proveedor
                    {
                        TempData["ActionProvedor"] = "Debe existir más de un Proveedor para borrar";
                        var todosProveedor2 = await _iProveedor.TodosProveedor();
                        return View("List", todosProveedor2);
                    }
                    ViewBag.pro = lista; //Carga el combo box
                    object proveedor2 = await _iProveedor.UnProveedor(cedula);
                    return View("Replace", proveedor2);
                }
   

                //Si el proveedor no tiene asociado productos, se borra
                object proveedor = await _iProveedor.UnProveedor(cedula);
                return View(proveedor);
            }
            catch
            {
                return View();
            }
        }

        //Muesta en una vista de todos los proveedores
        public async Task<IActionResult> List()
        {
            try
            {    
                object listaTemporal = await _iProveedor.TodosProveedor();
                return View (listaTemporal);
            }
            catch
            {
                return View();
            }

        }

        
    }
}
