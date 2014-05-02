using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;

namespace WebScrapping
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            var result = client.DownloadString("http://www.asp.net//");


            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.OptionAutoCloseOnEnd = true;
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.OptionWriteEmptyNodes = true;
            htmlDoc.LoadHtml(result);

            var selectedItems = from articulos in htmlDoc.DocumentNode
                                    .QuerySelectorAll("div.col-center div.common-post")
                                select new
                                {
                                    p = articulos.QuerySelector("p.excerpt").InnerText
                                };

            foreach (var item in selectedItems)
	        {
                Console.WriteLine();
                Console.WriteLine(item.p);
                Console.WriteLine();
	        }

            
            Console.ReadLine();

           
        }
    }
}
