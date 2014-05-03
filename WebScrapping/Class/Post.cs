using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapping.Class
{
    public class PostEntity : TableEntity
    {
        public PostEntity(string parrafo)
        {
            this.PartitionKey = DateTime.Now.Ticks.ToString();
            this.RowKey = DateTime.Now.ToShortTimeString().ToString();
            this.Parrafo = parrafo;
        }

        public PostEntity() { }

        public string Parrafo { get; set; }
    }
}
