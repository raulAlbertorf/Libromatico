using Libros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Estanteria estanteria = new Estanteria();
            estanteria.BuscarPorPropiedad("Valor 10", 1, 10);
            var perfil = new Perfil();
            perfil.Seleccionar(1);
            var item = new Item();
            item.Seleccionar(perfil.MisItems().ElementAt(0).Key.Id);
            perfil.SeleccionarPrestamo(perfil.Id, item.Id);



        }
    }
}
