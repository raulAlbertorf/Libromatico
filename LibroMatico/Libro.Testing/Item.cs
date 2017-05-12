using Libros.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Testing
{
    [TestClass()]
    public class Item
    {

        [TestMethod()]
        public void CRUD_Item()
       { 

            var perfil = new Perfil();
            perfil.Seleccionar(1);
            
            #region[Crear Item]
            var titulo = "Test titulo 1";
            var resumen = "Test resumen 1";
            var imagen = "Url.jpg";
            var prop = perfil;
            var visto = 0;
            var ub = new Ubicacion() { lat = (float)19.431885, lon = (float)-99.133492, Perfil_Id = perfil.Id };

            Libros.Models.Item item = new Libros.Models.Item()
            {
                Titulo = titulo,
                Resumen = resumen,
                UrlImagen = imagen,
                Propietario = prop,
                VecesVisto = visto
            };
            var Id = item.Id;
            Assert.IsTrue(item.Crear());

            Assert.IsTrue(item.Id > 0);
            #endregion

            #region[Seleccionar Item]

            Libros.Models.Item item2 = new Libros.Models.Item();
            Assert.IsTrue(item2.Seleccionar(item.Id));

            Assert.AreEqual(item.Id, item2.Id);
            Assert.AreEqual(item.Propietario.Id, item2.Propietario.Id);
            Assert.AreEqual(item.Resumen, item2.Resumen);
            Assert.AreEqual(item.Titulo, item2.Titulo);
            Assert.AreEqual(item.UrlImagen, item2.UrlImagen);
            Assert.AreEqual(item.VecesVisto, item2.VecesVisto);
            #endregion

            #region[Modificar Item]
            Perfil perfil2 = new Perfil();
            perfil2.Seleccionar(2);

            item2.Titulo = "Titulo 2";
            item2.Resumen = "Resumen 2";
            item2.UrlImagen = "Imagen.jpg";
            item2.Propietario = perfil2;
            item2.VecesVisto = 1;

            Assert.AreEqual(item.Id, item2.Id);
            Assert.AreNotEqual(item.Propietario.Id, item2.Propietario.Id);
            Assert.AreNotEqual(item.Resumen, item2.Resumen);
            Assert.AreNotEqual(item.Titulo, item2.Titulo);
            Assert.AreNotEqual(item.UrlImagen, item2.UrlImagen);
            Assert.AreNotEqual(item.VecesVisto, item2.VecesVisto);
            #endregion

            #region[Eliminar Item]
            Assert.IsTrue(item.Eliminar());
            Assert.IsFalse(item2.Seleccionar(Id));
            #endregion

           
        }
    }
}
