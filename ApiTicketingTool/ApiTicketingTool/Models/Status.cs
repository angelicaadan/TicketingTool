using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketingTool.Models
{
    public class Status
    {
        public string statusID { get; set; }
        public string statusDescription { get; set; }

    }

    public class Contact
    {
        public string contactID { get; set; }
        public string name { get; set; }
        public string active { get; set; }

    }
    public class Customer
    {
        public string companyID { get; set; }
        public string companyDescription { get; set; }
    }
    public class Types
    {
        public string type { get; set; }
    }
    public class Group
    {
        public string groupID { get; set; }
        public string groupDescription { get; set; }
    }
    public class Priorities
    {
        public string priorityID { get; set; }
        public string priorityDescription { get; set; }
    }

    public class Agents
    {
        public string agentID { get; set; }
        public string agentDescription { get; set; }
    }

    public class Cotizador
    {
        public string quotationID { get; set; }
    }
}
