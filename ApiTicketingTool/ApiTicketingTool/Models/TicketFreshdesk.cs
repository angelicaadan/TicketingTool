using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketingTool.Models
{
    public class TicketFreshdesk
    {
        public string name { get; set; }//name
        public int requester_id { get; set; }//requester_id 
        public string email { get; set; } //email
        public string phone { get; set; }//phone
        public string subject { get; set; }//subject 
        public string type { get; set; }//type
        public int status { get; set; }//status
        public int priority { get; set; }//priority
        public string description { get; set; }//description
        public string responder_id { get; set; }//responder_id
        public List<string> cc_Emails { get; set; }//cc_emails
        public Json custom_fields { get; set; }
        public DateTime due_by { get; set; }
        public int email_config_id { get; set; }
        public DateTime fr_due_by { get; set; }
        public int groupID { get; set; }//group_id
        public int product_id { get; set; }
        public int source { get; set; }
        public List<string> tags { get; set; }//tags


    }

    // [JsonConverter(typeof(JsonPathConverter))]
    public class Json
    {
        public string diseo { get; set; }
        public int proyecto { get; set; }
        public double horas_estimadas_por_cliente { get; set; }
        public double cf_horas_estimadas_por_agente { get; set; }
        public int horas_tampm_semana_1 { get; set; }
        public int porcentaje_de_avance { get; set; }
        public bool se_incluyeron_pruebas_unitarias { get; set; }
        public bool se_incluyeron_objetos_de_seguridad { get; set; }
        public string mes_facturacin { get; set; }

    }
}
