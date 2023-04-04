//Universidad Estatal a Distancia 
//Fundamentos de Programación Web
//Cristian Law Arrieta
//Tercer cuatrimestre 2022

using Microsoft.AspNetCore.Mvc;
using MisMinistritosUNED.Models;
using MisMinistritosUNED.Connection;

namespace MisMinistritosUNED.Controllers
{
    public class ClienteController : Controller //Clase que hereda de la clase Controller 
    {
        private readonly ICliente _iCliente; //Constructor para las interfaces de conexión
        public ClienteController(ICliente connection)
        {
            _iCliente = connection;
        }


        //Ingresa y muestra el formulario del Ususario para añadir datos nuevos
        [HttpGet]
        public async Task<ActionResult> CreateCliente()
        {
            ViewBag.DatosReferencia = await _iCliente.ComboBoxCliente();  //Llama al combobox de "Donde se enteró de la página"
            return View(); //Muestra la vista del mismo nombre del método CreateCliente asociado
        }

        //Almacena lo que se registra en el formulario Create
        [HttpPost]
        public async Task<ActionResult> CreateCliente(ClienteModel cliente) //Recibe todos los valores del formulario
        {
            try
            {                  
                if (await _iCliente.AgregarCliente(cliente) == false)
                {
                    TempData["clientenocreado"] = "El cliente ya existe"; //Mensaje
                    ViewBag.DatosReferencia = await _iCliente.ComboBoxCliente();
                    return View();
                }
                TempData["clientecreado"] = "Cliente creado con éxito"; //Mensaje
                return View("ReadCliente", cliente); //Devuelve una vista con los datos del cliente 
            }
            catch
            {
                return RedirectToAction("CreateCliente");
            }
        }

        //Ingresa y muestra el formulario del Ususario para añadir datos modificados
        [HttpGet]
        public async Task<ActionResult> IrActualizar(ClienteModel clienteIrActualizar)
        {
            try
            {
                object cliente = await _iCliente.UnCliente(clienteIrActualizar.cedula);
                ViewBag.DatosReferencia = await _iCliente.ComboBoxCliente();
                return View("UpdateCliente", cliente);
            }
            catch
            {
                return View();
            }
        }


        //Almacena lo que se registra en el formulario Update
        [HttpPost]
        public async Task<ActionResult> UpdateCliente(ClienteModel cliente)
        {
            try
            {
                await _iCliente.ActualizarCliente(cliente);
                TempData["clientecreado"] = "Cliente actualizado con éxito";
                return View("ReadCliente", cliente); //Regresa a una vista de detalles del cliente
            }
            catch
            {
                return View();
            }
        }

        //[HttpGet]
        public ActionResult UpdateCliente()
        {
            return View();
        }


        //Lee los detalles del cliente para mostrar en una vista de detalles Read-----------------
        [HttpGet]
        public async Task<ActionResult> ReadCliente(ClienteModel leercliente)
        {
            object cliente = await _iCliente.UnCliente(leercliente.cedula);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult ReadCliente()
        {
            return View();
        }
        //-----------------------------------------------------------------------------------------


        //Ingresa a una vista Delete y muestra los detalles del Ususario antes de borrar
        [HttpGet]
        public async Task<ActionResult> IrBorrar(ClienteModel clienteIrBorrar)
        {
            try
            {
                object cliente = await _iCliente.UnCliente(clienteIrBorrar.cedula);
                return View("DeleteCliente", cliente);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult IrBorrar()
        {
            return View();
        }


        //Método que borra el usuario desde una vista Delete 
        [HttpPost]
        public async Task<ActionResult> DeleteCliente(ClienteModel cliente)
        {
            try
            {
                await _iCliente.BorrarCliente(cliente.cedula);
                TempData["clienteborrado"] = "CLIENTE BORRADO";
                return RedirectToAction("Login", "Login"); //Regresa al login cuando el cliente es borrado 
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCliente()
        {
            return View();
        }


        //Muestra una lista de clientes en una opción del Administrador
        public async Task<ActionResult> ListClientes()
        {
            try
            {
                var listaCliente = await _iCliente.TodosCliente();
                return View(listaCliente);
            }
            catch
            {
                return View();
            }
        }
    }
}



