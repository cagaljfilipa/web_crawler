using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Core.StaticData
{
    public static class CategoriesNames
    {
        public static string Category1 = "BOJILA";
        public static string Category2= "KONZERVANSI";
        public static string Category3 = "ANTIOKSIDANSI, REGULATORI KISELOSTI...";
        public static string Category4= "STABILIZATORI, EMULGATORI,ZGUŠNJIVAČI, TVARI ZA ZADRŽAVANJE VLAGE...";
        public static string Category5 = "REGULATORI KISELOSTI, TVARI ZA SPREČAVANJE ZGRUDNJAVANJA.";
        public static string Category6 = "POJAČIVAČI OKUSA";
        public static string Category7 = "UMJETNA SLADILA, STABILIZATORI, ZGUŠNJIVAČI ...";


        public static List<string> CategoriesList()
        {
            var list = new List<string>();
            list.Add(Category1);
            list.Add(Category2);
            list.Add(Category3);
            list.Add(Category4);
            list.Add(Category5);
            list.Add(Category6);
            list.Add(Category7);
            return list;
        }



    }

  
}
