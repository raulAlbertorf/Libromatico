using System.ComponentModel.DataAnnotations;

namespace Libros.Models
{
    public enum EstadoPrestamo
    {
        [Display(Name = "Cancelado")]
        Cancelado,

        [Display(Name = "Solicitado")]
        Solicitado,

        [Display( Name = "Aceptado" )]
        Aceptado,

        [Display( Name = "En Transito" )]
        EnTransito,

        [Display( Name = "Entregado" )]
        Entregado
        
    }
}