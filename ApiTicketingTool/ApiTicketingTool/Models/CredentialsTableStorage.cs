using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketingTool.Models
{
    public class CredentialsTableStorage : TableEntity
    {
        public string token { get; set; }

        public string username { get; set; }

    }
}
