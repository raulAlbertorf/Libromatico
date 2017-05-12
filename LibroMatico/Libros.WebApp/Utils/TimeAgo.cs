using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libros.WebApp.Utils
{
    public static class TimeAgo
    {
        public static string timeNow( DateTime date )
        {

            TimeSpan timeSince = DateTime.Now.Subtract( date );
            if ( timeSince.TotalSeconds < 10 )
                return "ahora";
            if ( timeSince.TotalSeconds < 60 )
                return string.Format( "hace {0} segundos" , timeSince.Seconds );
            if ( timeSince.TotalMinutes < 2 )
                return "hace 1 minuto";
            if ( timeSince.TotalMinutes < 60 )
                return string.Format( "hace {0} minutos" , timeSince.Minutes );
            if ( timeSince.TotalMinutes < 120 )
                return "hace 1 hora";
            if ( timeSince.TotalHours < 24 )
                return string.Format( "hace {0} horas" , timeSince.Hours );
            if ( timeSince.TotalDays < 2 )
                return "ayer";
            if ( timeSince.TotalDays < 7 )
                return string.Format( "hace {0} días" , timeSince.Days );
            if ( timeSince.TotalDays < 14 )
                return "hace 1 semana";
            if ( timeSince.TotalDays < 21 )
                return "2 semanas atrás";
            if ( timeSince.TotalDays < 28 )
                return "3 semanas atrás";
            if ( timeSince.TotalDays < 60 )
                return "hace 1 mes";
            if ( timeSince.TotalDays < 365 )
                return string.Format( "hace {0} meses" , Math.Round( timeSince.TotalDays / 30 ) );
            if ( timeSince.TotalDays < 730 )
                return "hace 1 año ";
            return string.Format( "hace {0} años" , Math.Round( timeSince.TotalDays / 365 ) );

        }
    }
}