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
        static void Main(string[] args)
        {
            string html = string.Empty;
            string url = "https://www.anm.ro/nomenclator/medicamente";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "POST";

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    html = reader.ReadToEnd();
            //}

            //Console.WriteLine(html);

            //GetRequest(url);
            PostRequest(url);

            Console.ReadLine();
        }

        async static void GetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string myContent = await content.ReadAsStringAsync();
                Console.WriteLine(myContent);
            }
        }

        async static void PostRequest(string url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("form[CIM]", "W43451001")
            };

            HttpContent q = new FormUrlEncodedContent(queries);

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.PostAsync(url, q))
            using (HttpContent content = response.Content)
            {
                string myContent = await content.ReadAsStringAsync();
                Console.WriteLine(myContent);
            }
        }
    }
}
