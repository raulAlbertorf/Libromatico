using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.WebApp.Controllers
{
    public class CuentaController : Controller
    {
        [HttpPost]
        public ActionResult Crear(Cuenta c, Perfil p)
        {
            if (!Utils.Validator.isNullOrEmptyOrWhiteSpace(new List<String>() { c.Email, c.Contrasena, p.Nombre, p.Nacionalidad }) && Utils.Validator.esValido(c.Email))
            {
                if (c.Crear())
                {
                    Utils.SessionManager.Ingresar(c.Email);
                    p.Cuenta = c;
                    p.UrlImagen = "PICON_029.png"; //imagen por default
                    p.Ubicacion = Utils.GeoLocation.buscar(p.Nacionalidad);
                    p.Crear();
                    Utils.SessionManager.RegistarPerfil(p.Id);

                    String body = "Hola,<br> Bienvenido a Libromatico,<br> Esperamos que pronto puedas comenzar a compartir o leer nuevos libros<br><br>Saludos";

                    Utils.Email.SendEmail("Bienvenido a Libromatico", c.Email, body);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Utils.UIWarnings.SetError("Ya existe una cuenta usando este Email");
                    return RedirectToAction("Ingresar", "Cuenta");
                }
            }
            Utils.UIWarnings.SetError("Campos invalidos");
            return RedirectToAction("Ingresar", "Cuenta");
        }

        [HttpPost]
        public ActionResult OlvidePassword( Cuenta p )
        {
            string nuevopassword = p.CreatePassword( 8 );
            if(p.CambiarContrasena( nuevopassword ) )
            {
                string subject = "Nueva contraseña Libromatico";
                string mensaje = string.Format("Hola, <br><br> Tu nueva contraseña es: {0}. <br><br>Saludos<br><br>Libromatico", nuevopassword);

                Utils.Email.SendEmail(subject, p.Email, mensaje);
                Utils.UIWarnings.SetInfo( "Su nueva contraseña ha sido enviada al correo: " + p.Email );
                return RedirectToAction( "Ingresar" , "Cuenta" );
            }
            Utils.UIWarnings.SetError( "Lo sientimos, No se pudo recuperar la contraseña." );
            return RedirectToAction( "Ingresar" , "Cuenta" );
        }

        [HttpPost]
        public ActionResult CambiarContrasena( Cuenta p , string confirm_password )
        {
            if(p!=null)
            {
                if ( p.Contrasena == confirm_password )
                {
                    if ( p.CambiarContrasena( p.Contrasena ) )
                    {
                        Utils.UIWarnings.SetInfo( "Se ha cambiado su contraseña Exitosamente" );
                        return RedirectToAction( "Index" , "Home" );
                    }
                }
                Utils.UIWarnings.SetError( "Lo sientimos, No se pudo cambiar la contraseña." );
                return RedirectToAction( "Index" , "Home" );
            }
            Utils.UIWarnings.SetError( "Usted no tiene los permisos para cambiar una contraseña." );
            return RedirectToAction( "Index" , "Home" );
        }

        public ActionResult CambiarPassword(  )
        {
            Cuenta c = Utils.SessionManager.CuentaActiva( );
            if ( c != null )
            {
                return View( c );
            }
            Utils.UIWarnings.SetError( "Debe estar autenticado para cambiar su contraseña." );
            return RedirectToAction( "Ingresar" , "Cuenta" );
        }
        

        // GET: Cuenta
        public ActionResult Ingresar()
        {
            Cuenta c = Utils.SessionManager.CuentaActiva();
            if(c != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Ingresar(Cuenta c)
        {
            if (!Utils.Validator.isNullOrEmptyOrWhiteSpace(new List<String>() { c.Email, c.Contrasena }) && c.InicioSesion())
            {
                Utils.SessionManager.Ingresar(c.Email);
                Utils.SessionManager.RegistarPerfil(c.Perfiles().First().Id);
                return RedirectToAction("Perfiles", "Cuenta");
            }
            Utils.UIWarnings.SetError("Su Email o Contraseña es incorrecto");
            return View();
        }

        public ActionResult Salir()
        {
            Utils.SessionManager.Salir();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Perfiles()
        {
            Cuenta c = Utils.SessionManager.CuentaActiva( );
            if(c!=null)
            {
                return View( c );
            }
            Utils.UIWarnings.SetError("Debes estar autenticado para ver tus perfiles");
            return RedirectToAction( "Ingresar" , "Cuenta" );
        }

        public ActionResult Seleccionar(long Id)
        {
            Cuenta c = Utils.SessionManager.CuentaActiva();
            if (c != null && c.Perfiles().Where(p => p.Id == Id).Count() == 1)
            {
                Utils.SessionManager.RegistarPerfil(Id);
                return RedirectToAction("Detalles", "Perfil", new { Id = Id });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Eliminar(Cuenta c)
        {
            if (c.Eliminar())
            {
                Utils.SessionManager.Salir();
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }

        public ActionResult ManipularPerfiles( )
        {
            var c = Utils.SessionManager.CuentaActiva( );
            if ( c != null )
            {
                return View( c );
            }
            Utils.UIWarnings.SetError("Debes estar autenticado para manipular tus perfiles");
            return RedirectToAction( "Ingresar" , "Cuenta" );
        }
    }
}