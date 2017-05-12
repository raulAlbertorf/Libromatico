using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Libros.Testing
{
    [TestClass]
    public class Cuenta
    {
        [TestMethod]
        public void CRUD_Cuenta()
        {
            #region[Crear Cuenta]
            var cuenta = new Libros.Models.Cuenta() { Email = "correo@test.com", Contrasena = "test" };
            Assert.IsTrue(cuenta.Crear());
            #endregion  

            #region[Seleccionar Cuenta]
            cuenta = new Libros.Models.Cuenta();
            Assert.IsTrue(cuenta.Seleccionar("correo@test.com"));
            #endregion

            #region[Modificar Cuenta]
            Assert.IsTrue(cuenta.CambiarContrasena("test1"));
            #endregion

            #region[Eliminar Cuenta]
            Assert.IsTrue(cuenta.Eliminar());
            #endregion
        }
    }
}
