using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TerminalApp
{
    class Program
    {
        private static string _htmlContent = string.Empty;

        static void Main(string[] args)
        {
            string url = "https://www.anm.ro/nomenclator/medicamente";

            GetRequestAsync(url, "W43451001");

            Console.ReadLine();
        }

        async static void GetRequestAsync(string url, string cim)
        {
            string html = string.Empty;

            string searchUrl = url + "?cim=" + cim;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(searchUrl))
            using (HttpContent content = response.Content)
            {
                _htmlContent = await content.ReadAsStringAsync();

                if (_htmlContent == null)
                {
                    _htmlContent = string.Empty;
                }

                Console.WriteLine(_htmlContent);
            }
        }
    }
}
