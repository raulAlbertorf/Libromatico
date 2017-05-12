using Microsoft.VisualStudio.TestTools.UnitTesting;
using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libros.Models.Tests
{
    [TestClass( )]
    public class Perfil
    {
        [TestMethod( )]
        public void CRUD_Perfil( )
        {
            Cuenta c = new Cuenta( );
            Assert.IsTrue( c.Seleccionar( "email1@mail.cl" ) );

            #region [Crear Perfil]
            var nombre = "Pepito";
            var nac = "Chile";
            var imagen = "PICON_029.png";
            var ub = new Ubicacion( ) { lat= (float)-35.4333 , lon=(float) -71.6668 };
            Models.Perfil p = new Models.Perfil( )
            {
                Nombre = nombre ,
                Cuenta = c ,
                Nacionalidad = nac ,
                Ubicacion = ub ,
                UrlImagen = imagen
            };

            Assert.IsTrue( p.Crear( ) );
            Assert.IsTrue( p.Id > 0 );
            var Id = p.Id;
            #endregion

            #region [Modificar Perfil]
            p.Nombre = "Juanito";
            p.Nacionalidad = "Brasil";
            p.Ubicacion = new Ubicacion( ) { lat = ( float ) 19.431885 , lon = ( float ) -99.133492 , Perfil_Id=Id };
            p.UrlImagen = "PICON_028.png";

            Assert.IsTrue( p.Modificar() );
            Assert.IsTrue( p.Id > 0 );
            Assert.AreEqual( p.Id , Id );
            Assert.AreNotSame( p.Nombre , nombre);
            Assert.AreNotSame( p.Nacionalidad , nac );
            Assert.AreNotSame( p.Ubicacion.lat , ub.lat );
            Assert.AreNotSame( p.Ubicacion.lon , ub.lon );
            Assert.AreNotSame( p.UrlImagen , imagen );
            #endregion

            #region [Seleccionar Perfil]
            Models.Perfil p2 = new Models.Perfil( );
           
            Assert.IsTrue( p2.Seleccionar( Id ) );
            Assert.IsTrue( p2.Id > 0 );
            Assert.AreEqual( p2.Id , Id );
            Assert.AreEqual( p.Nombre , p2.Nombre );
            Assert.AreEqual( p.Nacionalidad , p2.Nacionalidad );
            Assert.AreEqual( p.Ubicacion.lat.ToString() , p2.Ubicacion.lat.ToString() );
            Assert.AreEqual( p.Ubicacion.lon.ToString( ) , p2.Ubicacion.lon.ToString( ) );
            Assert.AreEqual( p.UrlImagen , p2.UrlImagen );
            #endregion

            #region [Eliminar Perfil]
            Assert.IsTrue( p2.Eliminar( ) );
            Assert.IsFalse( p2.Seleccionar(Id) );
            #endregion
        }
    }
}