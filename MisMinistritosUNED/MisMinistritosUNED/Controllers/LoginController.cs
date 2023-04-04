using Microsoft.AspNetCore.Mvc;
using MisMinistritosUNED.Connection;
using MisMinistritosUNED.Models;

namespace MisMinistritosUNED.Controllers
{
    public class LoginController : Controller //Clase que hereda de la clase Controller 
    {
        private readonly ICliente _iCliente;
        public LoginController(ICliente connection)
        {
            _iCliente = connection;
        }

        public async Task<IActionResult> LoginAsync() //Despues del Constructor se muestra la Vista Login
        {
            await _iCliente.TodosCliente();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(ClienteModel clienteLogin) //Vericación el login, recibe los datos ingresados
        {
            try
            {
                ClienteModel cliente = await _iCliente.UnCliente(clienteLogin.cedula);                         

                if(cliente == null)
                {
                    ViewBag.message = "Login Incorrecto";
                    return View("Login");
                }

                if (cliente.cedula == "1-1111-1111" && clienteLogin.contrasena == "123") //Ingreso de cliente administrador
                {
                    return View("LoginAdmin"); //Vista para las opciones del administrador
                }
                else if (cliente.cedula == clienteLogin.cedula && cliente.contrasena == clienteLogin.contrasena)
                {
                    return View("LoginCliente", cliente);
                }
                ViewBag.message = "Login Incorrecto";
                return View("Login");
            }
            catch
            {
                ViewBag.message = "Login Incorrecto";
                return View("Login");
            }
            
        }


        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }
    }
}
