using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libros.WebApp.Utils
{
    public class SessionManager
    {
        public static void Ingresar(String Email)
        {
            HttpCookie _sessionCookie = new HttpCookie("_sessionCookie");
            _sessionCookie.Values["Email"] = Email;
            _sessionCookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(_sessionCookie);
        }

        public static void SalirPerfil()
        {
            HttpCookie _sessionCookie = HttpContext.Current.Request.Cookies["_sessionCookie"];
            if(!String.IsNullOrEmpty(_sessionCookie.Values["Perfil"]))
                _sessionCookie.Values["Perfil"] = string.Empty;
            HttpContext.Current.Response.Cookies.Add(_sessionCookie);
        }

        public static void Salir()
        {
            HttpCookie _sessionCookie = HttpContext.Current.Request.Cookies["_sessionCookie"];
            _sessionCookie.Expires = DateTime.Now.AddDays(-1);
            _sessionCookie.Values["Email"] = String.Empty;
            _sessionCookie.Values["Perfil"] = string.Empty;
            HttpContext.Current.Response.Cookies.Add(_sessionCookie);
        }

        public static Cuenta CuentaActiva()
        {
            Cuenta cuenta = null;
            HttpCookie _session = HttpContext.Current.Request.Cookies["_sessionCookie"];

            if (_session != null &&
                !string.IsNullOrEmpty(_session.Values["Email"]))
            {
                cuenta = new Cuenta() { Email = _session.Values["Email"] };
                return cuenta;
            }

            return cuenta;
        }

        public static Perfil PerfilActivo()
        {
            Perfil perfil = null;
            HttpCookie _session = HttpContext.Current.Request.Cookies["_sessionCookie"];

            if (_session != null &&
                !string.IsNullOrEmpty(_session.Values["Email"]) &&
                !string.IsNullOrEmpty(_session.Values["Perfil"]))
            {
                perfil = new Perfil();
                perfil.Seleccionar(Convert.ToInt64(_session.Values["Perfil"]));
                return perfil;
            }

            return perfil;
        }

        public static void RegistarPerfil(long Id)
        {
            HttpCookie _session = HttpContext.Current.Request.Cookies["_sessionCookie"];

            if (_session != null && !string.IsNullOrEmpty(_session.Values["Email"]))
            {
                _session.Values["Perfil"] = "" + Id;
                HttpContext.Current.Response.Cookies.Add(_session);
            }
        }
    }
}