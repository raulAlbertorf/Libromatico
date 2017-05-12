using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
   public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hola Mundo");
            Cuenta cuenta = new Cuenta();
            cuenta.Seleccionar("email6@mail.cl");
            cuenta.Perfiles();
            Perfil perfil = new Perfil();
            perfil.Seleccionar(1);
            
            Item item = new Item();
            //item.Crear();
            item.Seleccionar(1);
            perfil.Disponibilidad(item, false);
            item.Autores();
            Prestamo prestamo = new Prestamo();
            item.Titulo = "222";
            prestamo.Seleccionar(1);
            Comentario comentario = new Comentario() { Texto = "Texto N", Perfil = perfil, Prestamo = prestamo };
            comentario.Crear();
            comentario.Texto = "Texto NxN";
            comentario.Modificar();
            comentario.Eliminar();
            Comentario comentario2 = new Comentario(); 
            comentario2.Seleccionar(1);
            perfil.HistorialPrestamista(0, 10);
            perfil.HistorialPrestamista(0, 10);
            perfil.MisItems();
            Ubicacion ubicacion = new Ubicacion();
            ubicacion.Seleccionar(1);
            Estanteria estant = new Estanteria();
            estant.Buscar("Titulo 8", ubicacion, 1, 10, 2749);
            estant.LosMasCabrones(5);
            //prestamo.Eliminar_Comentario(comentario);
            //Console.WriteLine(string.Format("ID: {0}", item.Crear()));
            //item.Titulo = "2323";
            //item.Modificar();
            item.Agregar("perro","Hola");
            item.Propiedades();
            item.Modificar("anio", "1111");
            item.Remove("anio");
            Console.WriteLine(string.Format("ENUM {0}",(int) EstadoPrestamo.Enviado));
            //item.Eliminar();
            //item.Seleccionar(1);
            //Console.WriteLine(string.Format("ID: {0}, Titutlo: {1}", item.Id, item.Titulo));
            //item.Autores();
            //Autor autor = new Autor() { Nombre = "Nuevo" };
            //autor.Crear();
            //item.Autor_item_crear(autor);
            //item.Autor_item_eliminar(autor);
            //item.Remove("anio");
            //autor.Nombre = "PEDRO";
            //autor.Modificar();
            //autor.Eliminar();
            //perfil2.Eliminar();
            //cuenta.Eliminar();
            Console.ReadLine();

            }
    }
}
