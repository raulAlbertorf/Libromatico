using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Libros.WebApp.Utils
{
    public class Validator
    {
        public static Boolean isNullOrEmptyOrWhiteSpace(List<String> list)
        {
            foreach(String input in list)
            {
                if (String.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    return true;
                }
            }
            return false;
        }

        public static Boolean esValido(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        public static bool verificarExtension(String ext)
        {
            if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png"))
            {
                return true;
            }
            return false;
        }
    }
}