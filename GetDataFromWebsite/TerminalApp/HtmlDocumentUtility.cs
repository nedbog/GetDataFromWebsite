using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TerminalApp
{
    public class HtmlDocumentUtility
    {
        public static string GetHtmlContent(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            }

            return "";
        }
    }
}
