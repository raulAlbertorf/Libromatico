using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(Utils.SessionManager.CuentaActiva() != null && Utils.SessionManager.PerfilActivo() == null)
            {
                return RedirectToAction("Perfiles", "Cuenta");
            }
            else
            {
                Estanteria est = new Estanteria();
                return View(est);
            }
        }

        public ActionResult Busqueda( string buscar , int page =1 , int cantResult =10, int filtro=1  )
        {
            Estanteria e = new Estanteria( );
            if ( String.IsNullOrEmpty( buscar ) )
            {
                buscar = "";
            }
            var result = new List<Item> ();
            switch ( filtro )
            {
                case 1: //titulo
                    result = e.Buscar( buscar , page , cantResult );
                    break;
                case 2: //Autor
                    result = e.BuscarPorAutor( buscar , page , cantResult );
                    break;
                case 3: //Propiedad
                    result = e.BuscarPorPropiedad( buscar , page , cantResult );
                    break;
                case 4: //Ubicacion    usamos radio de 40.
                    try
                    {
                        var p = Utils.SessionManager.PerfilActivo( );
                        Ubicacion u = p.Ubicacion;
                        var radio = 40;
                        result = e.Buscar( buscar , u , page , cantResult , radio );
                    }
                    catch ( Exception ex )
                    { }
                    break;
                case 5: //All
                    result = e.BuscarPorAll( buscar , page , cantResult );
                    break;
                default:
                    result = e.Buscar( buscar , page , cantResult );
                    break;
            }
             

            this.ViewBag.Page = page;
            this.ViewBag.Results = cantResult;
            this.ViewBag.Termino = buscar;
            this.ViewBag.Filtro = filtro;

            if ( result.Count == cantResult )
            {
                this.ViewBag.More = 1;
            }
            else
            {
                this.ViewBag.More = 0;
            }
            
            return View( result );
        }

        public ActionResult MasVistos( )
        {
            if ( Utils.SessionManager.CuentaActiva( ) != null && Utils.SessionManager.PerfilActivo( ) == null )
            {
                return RedirectToAction( "Perfiles" , "Cuenta" );
            }
            else
            {
                Estanteria est = new Estanteria( );
                return View( est );
            }
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}