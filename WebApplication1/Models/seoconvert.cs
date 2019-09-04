using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MevaPro.Models
{
    public class Seoconvert
    {
        public static string Recover(string title)
        {
            string url = string.Empty;
            string[] olds = { "Ğ", "ğ", "Ü", "ü", "Ş", "ş", "İ", "ı", "Ö", "ö", "Ç", "ç" };
            string[] news = { "G", "g", "U", "u", "S", "s", "I", "i", "O", "o", "C", "c" };

            for (int i = 0; i < olds.Length; i++)
            {
                title = title.Replace(olds[i], news[i]);
            }
            url = title.Replace(" ", "-").Replace(".", "").ToLowerInvariant().Trim();
            return url;
        }
    }
}