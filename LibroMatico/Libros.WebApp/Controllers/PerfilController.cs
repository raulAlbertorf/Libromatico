using Libros.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace Libros.WebApp.Controllers
{
    public class PerfilController : Controller
    {

        [HttpPost]
        public ActionResult CargarImagen(Int64 Id)
        {
            string directory = System.AppDomain.CurrentDomain.BaseDirectory + @"Contents\img\";
            HttpPostedFileBase photo = Request.Files["photo"];
            var perfil = new Perfil();
            perfil.Seleccionar(Id);
            try
            {
                if (photo != null && photo.ContentLength > 0 && Utils.Validator.verificarExtension(Path.GetExtension(photo.FileName)))
                {
                    if (photo.FileName.Length > 30)
                    {
                        Utils.UIWarnings.SetError("Nombre de la imagen demasiado largo");
                        return RedirectToAction("Detalles", "Perfil", new { Id = Id });
                    }
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);

                    photo.SaveAs(Path.Combine(directory, fileName));
                    perfil.UrlImagen = fileName;
                    if (!perfil.CambiarImagen())
                    {
                        Utils.UIWarnings.SetError("No se pudo agregar la imagen");
                        return RedirectToAction("Detalles", "Perfil", new { Id = Id });
                    }
                    else
                    {
                        Utils.UIWarnings.SetInfo("Modificación realizada");
                        return RedirectToAction("Detalles", "perfil", new { Id = Id });
                    }
                }
                else
                {
                    Utils.UIWarnings.SetError("El archivo no corresponde a una imagen");
                    return RedirectToAction("Detalles", "perfil", new { Id = Id });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MisItems( int? page, long Id )
        {
            Cuenta cuenta = Utils.SessionManager.CuentaActiva( );
            Perfil perfilActivo = Utils.SessionManager.PerfilActivo( );
            Perfil p = new Perfil();

            if(cuenta!=null && perfilActivo != null  && p.Seleccionar(Id))
            {
                Dictionary<Libros.Models.Item , bool> items = p.MisItems( );
                int pageSize = 10;
                int pageNumber = ( page ?? 1 );
                return View( items.ToPagedList( pageNumber , pageSize ) );
            }
            return RedirectToAction( "Ingresar" , "Cuenta" );
        }

        // GET: Perfil
        /// <summary>
        /// Metodo que muestra los datos de un perfil en particular dado el id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Detalles(long Id )
        {
            Cuenta cuenta = Utils.SessionManager.CuentaActiva();
            Perfil perfilActivo = Utils.SessionManager.PerfilActivo();
            Perfil perfil = new Perfil();
            perfil.Seleccionar(Id);

            if (cuenta != null && perfilActivo == null)
            {
                return RedirectToAction("Perfiles", "Cuenta");
            }
            else if(cuenta != null && perfilActivo != null)
            {
                return View(perfil);
            }
            
            return RedirectToAction("Ingresar", "Cuenta");

        }

        [HttpPost]
        public ActionResult Crear(Perfil p, Cuenta c, String ciudad)
        {
            p.Cuenta = c;
            p.Ubicacion = Utils.GeoLocation.buscar(ciudad);

            if (p.Crear())
            {
                Utils.SessionManager.RegistarPerfil(p.Id);
                return RedirectToAction("Detalles", "Perfil", new { Id = p.Id });
            }
            return RedirectToAction("Perfiles", "Cuenta");
        }

        [HttpPost]
        public ActionResult Modificar(Perfil p, Cuenta cuenta, string ciudad)
        {
            p.Cuenta = cuenta;
            p.Ubicacion = Utils.GeoLocation.buscar(ciudad);

            var c = Utils.SessionManager.CuentaActiva();
            if (c == null || p.Cuenta.Email != c.Email)
            {
                Utils.UIWarnings.SetError("Usted no tiene los permisos para esta solicitud");
                return RedirectToAction("Ingresar", "Cuenta");
            }
            if (p.Modificar())
            {
                return RedirectToAction("ManipularPerfiles", "Cuenta");
            }
            Utils.UIWarnings.SetError("Perfil no pudo ser modificado");
            return RedirectToAction("ManipularPerfiles", "Cuenta");
        }

        public ActionResult Modificar(long id)
        {
            var c = Utils.SessionManager.CuentaActiva();
            var p = new Perfil();
            p.Seleccionar(id);
            if (c == null || p.Cuenta.Email != c.Email)
            {
                Utils.UIWarnings.SetError("Usted no tiene los permisos para esta solicitud");
                return RedirectToAction("Ingresar", "Cuenta");
            }

            return PartialView("_ModalModificarPerfil", p);
        }

        public ActionResult Eliminar(long id)
        {
            var c = Utils.SessionManager.CuentaActiva();

            var p = new Perfil();
            p.Seleccionar(id);

            if (c == null || p.Cuenta.Email != c.Email)
            {
                Utils.UIWarnings.SetError("Usted no tiene los permisos para esta solicitud");
                return RedirectToAction("Ingresar", "Cuenta");
            }
            return PartialView("_ModalEliminarPerfil", p);
        }

        [HttpPost]
        public ActionResult Eliminar(Perfil p, Cuenta cuenta)
        {
            Perfil perfil_activo = Utils.SessionManager.PerfilActivo();
            p.Cuenta = cuenta;
            var c = Utils.SessionManager.CuentaActiva();

            if (c == null || p.Cuenta.Email != c.Email)
            {
                Utils.UIWarnings.SetError("Usted no tiene los permisos para esta solicitud");
                return RedirectToAction("Ingresar", "Cuenta");
            }

            if (c.Perfiles().Count < 2)
            {
                Utils.UIWarnings.SetError("Una Cuenta no puede quedar sin un perfil");
                return RedirectToAction("ManipularPerfiles", "Cuenta");
            }
            if (p.Eliminar())
            {
                if(perfil_activo.Id == p.Id)
                {
                    Utils.SessionManager.SalirPerfil();
                }
                Utils.UIWarnings.SetInfo("Perfil eliminado exitosamente");
                return RedirectToAction("ManipularPerfiles", "Cuenta");
            }
            Utils.UIWarnings.SetError("Perfil no pudo ser eliminado");
            return RedirectToAction("ManipularPerfiles", "Cuenta");
        }

        [HttpPost]
        public JsonResult Disponibilidad(long Perfil_Id, long Item_Id, bool estado) //metodo modificar item, para cambiar disponibilidad
        {
            try
            {
                Perfil perfil = new Perfil();
                Item item = new Item();

                perfil.Id = Perfil_Id;
                item.Seleccionar(Item_Id);

                var disponibilidad = perfil.Disponibilidad(item, estado);
                return Json(disponibilidad, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}