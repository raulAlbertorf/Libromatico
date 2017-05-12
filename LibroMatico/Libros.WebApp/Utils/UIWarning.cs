using System.Collections.Generic;
using System.Web;

namespace Libros.WebApp.Utils
{
    public static class UIWarnings
    {
        public static string UltimoError( )
        {
            var emess = HttpContext.Current.Session[ "ERROR_MESSAGE" ];
            if ( emess != null )
            {
                var message = emess.ToString( );
                HttpContext.Current.Session.Remove( "ERROR_MESSAGE" );
                return message;
            }
            return string.Empty;
        }

        public static void SetError( List<string> validaciones )
        {
            var mensaje = "Encontramos algunos errores:<br /><ul>";
            foreach ( string validacion in validaciones )
            {
                mensaje += "<li>" + validacion + "</li>";
            }
            mensaje += "</ul>";
            HttpContext.Current.Session.Add( "ERROR_MESSAGE" , mensaje );
        }

        public static void SetError( string mensaje )
        {
            HttpContext.Current.Session.Add( "ERROR_MESSAGE" , mensaje );
        }

        public static string UltimaInfo( )
        {
            var emess = HttpContext.Current.Session[ "INFO_MESSAGE" ];
            if ( emess != null )
            {
                var message = emess.ToString( );
                HttpContext.Current.Session.Remove( "INFO_MESSAGE" );
                return message;
            }
            return string.Empty;
        }

        public static void SetInfo( string mensaje )
        {
            HttpContext.Current.Session.Add( "INFO_MESSAGE" , mensaje );
        }


    }
}