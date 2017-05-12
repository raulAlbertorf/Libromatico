using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libros.Models
{
    class Logs
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger("Libromatico");

        public static void IniciaMetodo(string desde, string parametros)
        {
            String info = String.Format("Info: Comienza Metodo: {0} - Parametros: {1}", desde, parametros ?? string.Empty);
            logger.Info(info);
        }

        public static void InfoResult(string desde, string parametros)
        {
            string info = String.Format("Info: {0} - Result: {1}", desde, parametros);
            logger.Info(info);
        }

        public static void Info(string desde, string parametros)
        {
            String info = String.Format("Info: {0} - Parametros: {1}", desde, parametros ?? string.Empty);
            logger.Info(info);
        }

        public static void SalirMetodo(string desde)
        {
            String info = String.Format("Info: Termina Metodo: {0}", desde);
            logger.Info(info);
        }

        public static void Error(Exception ex)
        {
            string error = String.Format("  Error: {0}", ex);
            logger.Error(error);
        }
    }
}
