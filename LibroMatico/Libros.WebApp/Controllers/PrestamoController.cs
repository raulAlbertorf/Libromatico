using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.WebApp.Controllers
{
    public class PrestamoController : Controller
    {

        // GET: Prestamo
        public ActionResult MisPrestamos(int? page, int? cantResult)
        {
            var cant = (cantResult ?? 4);
            var pagina = (page ?? 1);
            var perfil_activo = Utils.SessionManager.PerfilActivo();
            if (perfil_activo == null)
                return RedirectToAction("Index", "Home");
            List<Libros.Models.Prestamo> misprestamos = perfil_activo.HistorialPrestamista(pagina, cant);
            this.ViewBag.Page = pagina;
            this.ViewBag.Results = cant;
            if (misprestamos.Count == cant && perfil_activo.HistorialPrestamista().Count != cant)
            {
                this.ViewBag.More = 1;
            }
            else
            {
                this.ViewBag.More = 0;
            }

            return View(misprestamos);
        }

        // GET: Prestamo
        public ActionResult MisSolicitudes(int? page, int? cantResult)
        {
            var cant = (cantResult ?? 4);
            var pag = (page ?? 1);
            var perfil_activo = Utils.SessionManager.PerfilActivo();
            if (perfil_activo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Libros.Models.Prestamo> misSolicitados = perfil_activo.HistorialReceptor(pag, cant);
            this.ViewBag.Solicitudes = misSolicitados.Count;
            this.ViewBag.Page = pag;
            this.ViewBag.Results = cant;
            if (misSolicitados.Count == cant && perfil_activo.HistorialReceptor().Count != cant)
            {
                this.ViewBag.More = 1;
            }
            else
            {
                this.ViewBag.More = 0;
            }

            return View(misSolicitados);
        }

        // GET: Prestamo
        /// <summary>
        /// Metodo que muestra los datos de un prestamo en particular dado el id
        /// </summary>
        /// (sflores)
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Detalles(long Id)
        {
            var perfil_activo = Libros.WebApp.Utils.SessionManager.PerfilActivo();
            if (perfil_activo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Prestamo p = new Prestamo();
            if (p.Seleccionar(Id) && (p.Receptor.Id == perfil_activo.Id || p.Prestamista.Id == perfil_activo.Id))
                return View(p);
            else
            {
                Utils.UIWarnings.SetError("No existen registros de este prestamo");
                return RedirectToAction("Index", "Home");
            }
        }

        // Post: Editar
        /// <summary>
        /// Metodo que permite editar el estado de un prestamo.
        /// </summary>
        /// (sflores)
        /// <param name="Id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editar(long Id, Libros.Models.EstadoPrestamo estado)
        {
            Prestamo p = new Prestamo();
            p.Seleccionar(Id);
            Perfil perfil = Utils.SessionManager.PerfilActivo();
            Item item = new Item();

            if (perfil != null)
            {
                item = p.Item;
                if(p.Receptor.Id == perfil.Id && p.Estado != EstadoPrestamo.Solicitado && p.Estado != EstadoPrestamo.Aceptado && estado == EstadoPrestamo.Cancelado)
                {
                    Utils.UIWarnings.SetError("Ya es muy tarde para cancelar directamente el prestamo");
                    return RedirectToAction("Detalles", "Prestamo", new { id = Id });
                }
                p.Estado = estado;
                p.FechaUltimaModificacion = DateTime.Now;
                p.Modificar();
                if (p.Estado == Libros.Models.EstadoPrestamo.Aceptado)
                {
                    p.FechaEnvio = DateTime.Now;
                    p.Modificar();
                    perfil.Disponibilidad(item, false);
                    foreach (var prestamo in perfil.HistorialPrestamista())
                    {
                        if (prestamo.Item.Id == p.Item.Id && prestamo.Estado != Libros.Models.EstadoPrestamo.Aceptado)
                        {
                            prestamo.Eliminar();
                        }
                    }
                }

                if (p.Estado == Libros.Models.EstadoPrestamo.Entregado)
                {
                    p.Item.Propietario = p.Receptor;
                    p.FechaRecepcion = DateTime.Now;
                    p.Item.Modificar();
                    p.Receptor.Disponibilidad(p.Item, false);
                    p.Modificar();
                    var Item_Id = p.Item.Id;
                    Utils.UIWarnings.SetInfo("Item Entregado");
                    return RedirectToAction("Detalles", "Perfil", new { Id = perfil.Id });
                }
                Utils.UIWarnings.SetInfo("Solicitud Modificada Exitosamente");
                return RedirectToAction("Detalles", "Prestamo", new { id = Id });
            }

            Utils.UIWarnings.SetError("No se pudo modificar este Prestamo");
            return RedirectToAction("Detalles", "Prestamo", new { id = Id });
        }

        // Post: Crear
        /// <summary>
        /// Metodo que permite crear un prestamo.
        /// </summary>
        /// (sflores)
        /// <param name="Item_Id"></param>
        /// <param name="Prestamista_Id"></param>
        /// <param name="Receptor_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Crear(long Item_Id, long Prestamista_Id, long Receptor_Id)
        {
            Prestamo p = new Prestamo();

            Item item = new Item() { Id = Item_Id };

            Perfil prestamista = new Perfil();
            prestamista.Seleccionar(Prestamista_Id);

            Perfil receptor = new Perfil();
            receptor.Seleccionar(Receptor_Id);
            //bool estaSolicitado = receptor.SeleccionarPrestamo(Receptor_Id, Item_Id);

            if (prestamista.Id != receptor.Id)
            {
                //if (receptor.HistorialReceptor().Any(pre => pre.Item.Id == Item_Id && p.Estado != EstadoPrestamo.Cancelado))
                //{
                //    Utils.UIWarnings.SetError("Ya has solicitado este item anteriormente");
                //    return RedirectToAction("Detalles", "Item", new { id = Item_Id });
                //}

                p.Item = item;
                p.Receptor = receptor;
                p.Estado = EstadoPrestamo.Solicitado;
                p.FechaEnvio = DateTime.Now;
                p.FechaExpiracion = DateTime.Now.AddMonths(2);
                p.FechaRecepcion = DateTime.Now;
                p.FechaUltimaModificacion = DateTime.Now;
                prestamista.Agregar(p);

                Utils.UIWarnings.SetInfo("Solicitud Creada Exitosamente");
                return RedirectToAction("Detalles", "Prestamo", new { id = p.Id });
            }

            Utils.UIWarnings.SetError("No puede solicitar su propio item");
            return RedirectToAction("Detalles", "Item", new { id = Item_Id });
        }

        // Post: AgregarComentario
        /// <summary>
        /// Metodo que permite agregar un comentario a un prestamo.
        /// </summary>
        /// (sflores)
        /// <param name="Comentario"></param>
        /// <param name="Prestamo_id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgregarComentario(Comentario c, string Prestamo_id)
        {
            if (String.IsNullOrEmpty(c.Texto.Trim()))
            {
                Utils.UIWarnings.SetError("No se encontró el comentario");
                return RedirectToAction("Detalles", "Prestamo", new { id = long.Parse(Prestamo_id) });
            }
            Perfil p = Utils.SessionManager.PerfilActivo();
            Prestamo prestamo = new Prestamo() { Id = long.Parse(Prestamo_id) };
            if (p != null)
            {
                c.Perfil = p;
                c.FechaCreacion = DateTime.Now;
                if (prestamo.Agregar(c))
                {
                    return RedirectToAction("Detalles", "Prestamo", new { id = c.Prestamo.Id });
                }
                Utils.UIWarnings.SetError("No se pudo publicar su comentario");
                return RedirectToAction("Detalles", "Prestamo", new { id = prestamo.Id });
            }
            Utils.UIWarnings.SetError("Debe estar autenticado para comentar");
            return RedirectToAction("Detalles", "Prestamo", new { id = prestamo.Id });

        }

        // GET: EliminarComentario
        /// <summary>
        /// Metodo que permite eliminar un comentario
        /// </summary>
        /// (sflores)
        /// <param name="Id"></param>
        /// <param name="prestamo" type="long"></param>
        /// <returns></returns>
        public ActionResult EliminarComentario(long Id, long prestamo)
        {
            Comentario c = new Comentario();
            c.Seleccionar(Id);
            Perfil p = Utils.SessionManager.PerfilActivo();
            Prestamo pre = new Prestamo() { Id = prestamo };

            if (p != null && p.Id == c.Perfil.Id)
            {
                if (pre.Eliminar(c))
                {
                    return RedirectToAction("Detalles", "Prestamo", new { id = prestamo });
                }
                Utils.UIWarnings.SetError("No se pudo eliminar su comentario");
                return RedirectToAction("Detalles", "Prestamo", new { id = prestamo });
            }
            Utils.UIWarnings.SetError("Debe estar autenticado para eliminar su comentario");
            return RedirectToAction("Detalles", "Prestamo", new { id = prestamo });
        }

        //<summary>
        //Permite calificar de 1 a 5 al prestamista
        //</summary>
        //(chernandez)
        [HttpPost]
        public ActionResult CalificarPrestamista(Int64 Prestamo_Id, int valoracion)
        {
            Prestamo prestamo = new Prestamo();
            prestamo.Seleccionar(Prestamo_Id);

            Perfil perfil = Utils.SessionManager.PerfilActivo();

            if (perfil != null && perfil.Id == prestamo.Receptor.Id)
            {
                prestamo.CalificacionAlPrestamista = valoracion;

                if (prestamo.Modificar())
                {
                    return RedirectToAction("Detalles", "Prestamo", new { id = Prestamo_Id });
                }
                Utils.UIWarnings.SetError("No hemos podido enviar tu valoración");
                return RedirectToAction("Detalles", "Prestamo", new { id = prestamo });
            }
            return RedirectToAction("Ingresar", "Cuenta");
        }

        //<summary>
        //Permite calificar de 1 a 5 al receptor (solicitante)
        //</summary>
        //(chernandez)
        [HttpPost]
        public ActionResult CalificarSolicitante(Int64 Prestamo_Id, int valoracion)
        {
            Prestamo prestamo = new Prestamo();
            prestamo.Seleccionar(Prestamo_Id);

            Perfil perfil = Utils.SessionManager.PerfilActivo();

            if (perfil != null && perfil.Id == prestamo.Prestamista.Id)
            {
                prestamo.CalificacionAlReceptor = valoracion;

                if (prestamo.Modificar())
                {
                    return RedirectToAction("Detalles", "Prestamo", new { id = Prestamo_Id });
                }
                Utils.UIWarnings.SetError("No hemos podido enviar tu valoración");
                return RedirectToAction("Detalles", "Prestamo", new { id = prestamo });
            }
            return RedirectToAction("Ingresar", "Cuenta");
        }
    }
}