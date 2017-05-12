using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.WebApp.Controllers
{
    public class PropiedadesController : Controller
    {
        [HttpPost]
        public ActionResult Agregar(FormCollection collection, Int64 Id_Item)
        {
            try
            {
               
                string Key = Request.Form["Key"];
                string Value = Request.Form["Value"];
                if (String.IsNullOrEmpty(Key) || String.IsNullOrEmpty(Value))
                {
                    Utils.UIWarnings.SetError("Campos vacios");
                    return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
                }
                    var item = new Libros.Models.Item();
                item.Seleccionar(Id_Item);
                if(!item.Agregar(Key, Value))
                {
                    Utils.UIWarnings.SetError("Propiedad Existente");
                    return RedirectToAction("Detalles", "Item", new { Id = Id_Item });
                }
                Utils.UIWarnings.SetInfo("Propiedad Agregada");
                return RedirectToAction("Detalles","Item", new {Id = Id_Item});
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
            
        }

        [HttpGet]
        public ActionResult Eliminar(string key, Int64 Id)
        {
            var item = new Models.Item();
            item.Seleccionar(Id);
            item.Remove(key);
            return RedirectToAction("Detalles", "Item", new { Id = Id });
        }
    }
}