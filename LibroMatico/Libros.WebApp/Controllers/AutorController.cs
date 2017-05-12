using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.WebApp.Controllers
{
    public class AutorController : Controller
    {
        public ActionResult AgregarExistente(Libros.Models.Autor autor, Int64 Id_Item)
        {
            try
            {
                var item = new Libros.Models.Item();
                item.Seleccionar(Id_Item);
                autor.Seleccionar(autor.Id);
                foreach (var a in item.Autores())
                {
                    if (a.Nombre.Equals(autor.Nombre))
                    {
                        Utils.UIWarnings.SetError("Autor No Agregado");
                        return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
                    }

                }
                item.Crear(autor);
                Utils.UIWarnings.SetInfo("Autor Agregado");
                return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Autor/Agregar/5
        [HttpPost]
        public ActionResult Agregar(FormCollection collection, Int64 Id_Item)
        {
            try
            {
                string nombre = Request.Form["Nombre"];
                if (string.IsNullOrEmpty(nombre))
                {
                    Utils.UIWarnings.SetError("Campo vacío");
                    return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
                }
                var autor = new Libros.Models.Autor();
                var item = new Libros.Models.Item();
                item.Seleccionar(Id_Item);

                autor.Nombre = nombre;
                foreach (var a in item.Autores())
                    if (a.Nombre.Equals(nombre))
                    {
                        Utils.UIWarnings.SetError("Este autor ya se encuentra en su item");
                        return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
                    }

                if (!autor.Crear())
                {
                    Utils.UIWarnings.SetError("Autor No Agregado");
                    return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
                }
                item.Crear(autor);
                Utils.UIWarnings.SetInfo("Autor Nuevo Agregado");
                return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpGet]
        public ActionResult Eliminar(Int64 Id_Item, Int64 Id_Autor)
        {
            var item = new Models.Item();
            var autor = new Models.Autor();
            item.Seleccionar(Id_Item);
            autor.Seleccionar(Id_Autor);
            item.Eliminar(autor);
            return RedirectToAction("Detalles", "Item", new { Id = item.Id });
        }

    }
}
