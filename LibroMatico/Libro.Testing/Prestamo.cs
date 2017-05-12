using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Libros.Models;

namespace Libro.Testing
{
    [TestClass]
    public class Prestamo
    {
        [TestMethod]
        public void Prestamo_CRUD()
        {
            #region[Crear_Prestamo]
            var id = 1;
            var prestamista = new Libros.Models.Perfil();
            var receptor = new Libros.Models.Perfil();
            var item = new Libros.Models.Item();
            prestamista.Seleccionar(1);
            receptor.Seleccionar(2);
            item.Seleccionar(1);
            var fechaEnvio = new DateTime(2016, 11, 1);
            var fechaRecep = new DateTime(2016, 11, 20);
            var estado = (EstadoPrestamo)1;
            var fechaExp = new DateTime(2016, 11, 21);
            var fechaUltim = new DateTime(2016, 11, 10);
            var califRecep = 5;
            var califPrest = 5;

            var prestamo = new Libros.Models.Prestamo() { Id = id, Item = item, FechaEnvio = fechaEnvio, FechaRecepcion = fechaRecep,
                                                          Prestamista = prestamista, Receptor = receptor, Estado = estado,
                                                          FechaExpiracion = fechaExp, FechaUltimaModificacion = fechaUltim,
                                                          CalificacionAlReceptor = califRecep, CalificacionAlPrestamista = califPrest};

            Assert.IsTrue(prestamo.Crear());
            Assert.IsTrue(prestamo.Id > 0);
            #endregion

            #region[Seleccionar_Prestamo]

            Libros.Models.Prestamo prestamo2 = new Libros.Models.Prestamo();
            Assert.IsTrue(prestamo2.Seleccionar(prestamo.Id));

            Assert.AreEqual(prestamo.Id, prestamo2.Id);
            Assert.AreEqual(prestamo.Item.Id, prestamo2.Item.Id);
            Assert.AreEqual(prestamo.Prestamista.Id, prestamo2.Prestamista.Id);
            Assert.AreEqual(prestamo.Receptor.Id, prestamo2.Receptor.Id);
            Assert.AreEqual(prestamo.FechaEnvio, prestamo2.FechaEnvio);
            Assert.AreEqual(prestamo.FechaRecepcion, prestamo2.FechaRecepcion);
            Assert.AreEqual(prestamo.Estado, prestamo2.Estado);
            Assert.AreEqual(prestamo.FechaExpiracion, prestamo2.FechaExpiracion);
            Assert.AreNotEqual(prestamo.FechaUltimaModificacion, prestamo2.FechaUltimaModificacion);
            Assert.AreEqual(prestamo.CalificacionAlReceptor, prestamo2.CalificacionAlReceptor);
            Assert.AreEqual(prestamo.CalificacionAlPrestamista, prestamo2.CalificacionAlPrestamista);
            #endregion

            #region[Modificar]

            Perfil prestamista2 = new Perfil();
            Perfil receptor2 = new Perfil();
            Libros.Models.Item item2 = new Libros.Models.Item();
            prestamista2.Seleccionar(2);
            receptor2.Seleccionar(3);

            prestamo2.Item = item2;
            prestamo2.Prestamista = prestamista2;
            prestamo2.Receptor = receptor2;
            prestamo2.FechaEnvio = new DateTime(2016, 9, 15);
            prestamo2.FechaExpiracion = new DateTime(2016, 9, 20);
            prestamo2.FechaRecepcion = new DateTime(2016, 9, 17);
            prestamo2.Estado = (EstadoPrestamo)2;
            prestamo2.CalificacionAlPrestamista = 4;
            prestamo2.CalificacionAlReceptor = 1;

            Assert.AreEqual(prestamo.Id, prestamo2.Id);
            Assert.AreNotEqual(prestamo.Item.Id, prestamo2.Item.Id);
            Assert.AreNotEqual(prestamo.Prestamista.Id, prestamo2.Prestamista.Id);
            Assert.AreNotEqual(prestamo.Receptor.Id, prestamo2.Receptor.Id);
            Assert.AreNotEqual(prestamo.FechaEnvio, prestamo2.FechaEnvio);
            Assert.AreNotEqual(prestamo.FechaExpiracion, prestamo2.FechaExpiracion);
            Assert.AreNotEqual(prestamo.FechaRecepcion, prestamo2.FechaRecepcion);
            Assert.AreNotEqual(prestamo.Estado, prestamo2.Estado);
            Assert.AreNotEqual(prestamo.CalificacionAlPrestamista, prestamo2.CalificacionAlPrestamista);
            Assert.AreNotEqual(prestamo.CalificacionAlReceptor, prestamo2.CalificacionAlReceptor);

            #endregion

            #region[Eliminar Prestamo]

            Assert.IsTrue(prestamo.Eliminar());
            Assert.IsFalse(prestamo.Seleccionar(prestamo.Id));

            #endregion

        }

    }
}
