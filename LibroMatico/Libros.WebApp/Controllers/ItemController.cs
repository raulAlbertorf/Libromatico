using Libros.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Libros.WebApp.Controllers
{

    public class ItemController : Controller
    {

        [HttpPost]
        public ActionResult Eliminar(Int64 Perfil_Id, Int64 Item_Id)
        {
            try
            {
                var perfil = new Perfil();
                perfil.Seleccionar(Perfil_Id);
                var item = new Libros.Models.Item();
                item.Seleccionar(Item_Id);
                if(perfil.HistorialPrestamista().Any(p => p.Item.Id == item.Id && p.Estado != EstadoPrestamo.Cancelado))
                {
                    Utils.UIWarnings.SetError("Este item se encuentra en prestamo");
                    return RedirectToAction("Detalles", "Perfil", new { Id = Perfil_Id });
                }
                if (!perfil.Remover(item))
                {
                    Utils.UIWarnings.SetError("No se pudo eliminar");
                    return RedirectToAction("Detalles", "Perfil", new { Id = Perfil_Id });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            Utils.UIWarnings.SetInfo("Eliminado correctamente");
            return RedirectToAction("Detalles", "Perfil", new { Id = Perfil_Id });
        }

        [HttpPost]
        public ActionResult CargarImagen(Int64 Id)
        {
            string directory = System.AppDomain.CurrentDomain.BaseDirectory + @"Contents\img\";
            HttpPostedFileBase photo = Request.Files["photo"];
            var item = new Item();
            item.Seleccionar(Id);
            try
            {
                if (photo != null && photo.ContentLength > 0 && Utils.Validator.verificarExtension(Path.GetExtension(photo.FileName)))
                {
                    if(photo.FileName.Length > 30)
                    {
                        Utils.UIWarnings.SetError("Nombre de la imagen demasiado largo");
                        return RedirectToAction("Detalles", "Item", new { Id = Id });
                    }
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);
                   
                    photo.SaveAs(Path.Combine(directory, fileName));
                    item.UrlImagen = fileName;
                    if (!item.CambiarImagen())
                    {
                        Utils.UIWarnings.SetError("No se pudo agregar la imagen");
                        return RedirectToAction("Detalles", "Item", new { Id = Id });
                    }
                    else
                    {
                        Utils.UIWarnings.SetInfo("Modificación realizada");
                        return RedirectToAction("Detalles", "Item", new { Id = item.Id });
                    }
                }
                else
                {
                    Utils.UIWarnings.SetError("El archivo no corresponde a una imagen");
                    return RedirectToAction("Detalles", "Item", new { Id = item.Id });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Item
        public ActionResult Detalles(Int64 Id)
        {
            Cuenta cuenta_activa = Utils.SessionManager.CuentaActiva();
            Perfil perfil_activo = Utils.SessionManager.PerfilActivo();

            if (cuenta_activa != null && perfil_activo == null)
            {
                return RedirectToAction("Perfiles", "Cuenta");
            }
            else if (cuenta_activa == null && perfil_activo == null)
            {
                Utils.UIWarnings.SetError("Debes estar autenticado para ver este item. Ingrese <a href='"+@Url.Action("Ingresar","Cuenta")+"'>aquí</a>");
                return RedirectToAction("Index", "Home");
            }

            Item item = new Models.Item();
            item.Seleccionar(Id);
            return View(item);
        }

        // POS: Item/Modificar/5
        public ActionResult Modificar(Libros.Models.Item item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Titulo) || string.IsNullOrEmpty(item.Resumen))
                {
                    Utils.UIWarnings.SetError("Se encontraron campos vacios");
                    return RedirectToAction("Detalles", new { Id = item.Id });
                }
                item.Modificar();
                return RedirectToAction("Detalles", new { Id = item.Id });
            }
            catch
            {
                return RedirectToAction("Indice", "Home");
            }

        }

        // GET: Item/Create
        public ActionResult Crear()
        {
            if (Utils.SessionManager.CuentaActiva() != null)
            {
                return View();
            }
            return RedirectToAction("Index","Home");
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Crear(Libros.Models.Item item)
        {
            Perfil p = Libros.WebApp.Utils.SessionManager.PerfilActivo();

            if (string.IsNullOrEmpty(item.Titulo) || string.IsNullOrEmpty(item.Resumen))
            {

                Utils.UIWarnings.SetError("Faltan campos por llenar");
                return View();
            }
            try
            {
                item.VecesVisto = 0;
                item.UrlImagen = "notPicture.jpg";
                p.Agregar(item);
                return RedirectToAction("Detalles", "Item", new { id = item.Id });
            }
            catch
            {
                Utils.UIWarnings.SetError("Debes estar autenticado para crear un item. Ingrese <a href='" + @Url.Action("Ingresar", "Cuenta") + "'>aquí</a>");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public void Buscar(FormCollection collection)
        {
            string nombre = Request.Form["target"];
            if (nombre == null)
            {
                nombre = "";
            }
            List<Libros.Models.Autor> autores = new Libros.Models.Autor().Buscar_Autores(nombre);
            ViewData["autores"] = autores;
        }
    }
}
