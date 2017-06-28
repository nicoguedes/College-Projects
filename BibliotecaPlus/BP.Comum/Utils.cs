using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.Comum
{
    public abstract class Utils
    {
        public static string DiminuirString(string text, int limite) 
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return text.Length <= limite ? text : text.Substring(0, limite);
        }
    }
}
