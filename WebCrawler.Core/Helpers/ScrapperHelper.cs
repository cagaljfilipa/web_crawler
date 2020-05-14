using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebCrawler.Core.StaticData;

namespace WebCrawler.Core.Helpers
{
    public static class ScrapperHelper
    {

        public static string ConvertImageTo64(string url)
        {
            string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(url));

            using (var client = new WebClient())
            {
                byte[] dataBytes = client.DownloadData(new Uri(url));
                string encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                return encodedFileAsBase64;
            }
        }


        public static bool IsAllUpper(string input)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            input = rgx.Replace(input, "");

            for (int i = 0; i < input.Length; i++)
            {

                if (input[i] == ' ')
                {
                    continue;
                }
                if (!Char.IsUpper(input[i]))
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }

        public static string CleanInnerText(string text)
        {

            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var cleanInnerText = rgx.Replace(text, "");
            cleanInnerText = cleanInnerText.Trim();
            return cleanInnerText;

        }

        public static string FindColor(string colorCode)
        {
            if (colorCode == ColorsCodes.Brown)
            {
                return "Brown";
            }

            else if (colorCode == ColorsCodes.Green)
            {
                return "Green";
            }

            else if (colorCode == ColorsCodes.Red)
            {
                return "Red";
            }

            else
            {
                return "";
            }
        }

    }

}
