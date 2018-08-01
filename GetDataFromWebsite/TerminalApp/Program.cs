using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Win32;

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

        static void GetRequestAsync(string url, string cim)
        {
            string searchUrl = url + "?cim=" + cim;

            var htmlContent = HtmlDocumentUtility.GetHtmlContent(searchUrl);

            if (!string.IsNullOrEmpty(htmlContent))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                var drugDetails = doc.DocumentNode.SelectNodes("//button[@data-target='#detailsModal']");

                if (drugDetails.Count == 1 && drugDetails[0].InnerHtml.ToLower() == "detalii")
                {
                    var rcp = drugDetails[0].GetAttributeValue("data-linkrcp", "not found");
                    var prospect = drugDetails[0].GetAttributeValue("data-linkpro", "not found");
                    //Console.WriteLine($"RCP: {rcp}\nProspect: {prospect}");
                    if (rcp != "not found" || prospect != "not found")
                    {
                        Console.WriteLine(rcp);
                        Console.WriteLine(prospect);
                    }

                    Process.Start(rcp);
                    Process.Start(prospect);

                }
            }

        }
        static bool IsAcrobatReaderInstalled()
        {
            try
            {
                Microsoft.Win32.RegistryKey _mainKey = Microsoft.Win32.Registry.CurrentUser;
                _mainKey = _mainKey.OpenSubKey(@"Software\Adobe\Acrobat Reader");
                if (_mainKey == null)
                {
                    _mainKey = Microsoft.Win32.Registry.LocalMachine;
                    _mainKey = _mainKey.OpenSubKey(@"Software\Adobe\Acrobat Reader");
                    if (_mainKey == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        static string GetDefaultBrowserPath()
        {

            string key = @"htmlfile\shell\open\command";
            RegistryKey registryKey =
            Registry.ClassesRoot.OpenSubKey(key, false);
            // get default browser path

            return ((string)registryKey.GetValue(null, null)).Split('"')[1];

        }

        static void WriteHtmlToDisk(string html)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\index.html";

            File.WriteAllText(path, html);
        }
    }
}
