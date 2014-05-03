using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using WebScrapping.Class;

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

            CloudTable table = inicializarStorage("Postaspnet");

            foreach (var item in selectedItems)
            {
                PostEntity post = new PostEntity(item.p);
                TableOperation insertOperation = TableOperation.Insert(post);
                table.Execute(insertOperation);    
            }
			
			
			Console.Write("Guardo");
		}

		private static CloudTable inicializarStorage(string nombreTabla)
		{
			// Retrieve the storage account from the connection string.
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(("DefaultEndpointsProtocol=https;AccountName=testhefesoft;AccountKey=u5djE00wXULc+KMtSB+k1+ZrV8q4JOEHbCrq2Qn+YalVwlQ7JUj2m/VMhgxJSVfkz+QG4cxs+A1OdBXorcn/CQ=="));

			// Create the table client.
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

			// Create the CloudTable object that represents the "people" table.
			CloudTable table = tableClient.GetTableReference(nombreTabla);
			try
			{
				table.CreateIfNotExists();
			}
			catch { }
			return table;
		}
	}
}
