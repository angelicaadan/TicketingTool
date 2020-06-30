using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ApiTicketingTool.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.custom_fields = new custom_fields();
        }
        public int TicketID { get; set; }
        public string name { get; set; }//name
        public string requester_id { get; set; }//requester_id 
        public string email { get; set; } //email
        public string phone { get; set; }//phone
        public string subject { get; set; }//subject 
        public string type { get; set; }//type
        public string status { get; set; }//status
        public string priority { get; set; }//priority
        public string description { get; set; }//description
        public Int64 responder_id { get; set; }//responder_id
        public string cc_emails { get; set; }//cc_emails
        public custom_fields custom_fields { get; set; }
        public DateTime due_by { get; set; }
        public int? email_config_id { get; set; }
        public DateTime fr_due_by { get; set; }
        public string group_id { get; set; }//group_id
        public int? product_id { get; set; }
        public string source { get; set; }
        public string tags { get; set; }//tags
    }

    public class custom_fields
    {
        public string diseo { get; set; }
        public string proyecto { get; set; }
        public decimal horas_estimadas_por_cliente { get; set; }
        public Int64 cf_horas_estimadas_por_agente { get; set; }
        public decimal horas_tampm_semana_1 { get; set; }
        public decimal porcentaje_de_avance { get; set; }
        [JsonIgnore]
        public string se_incluyeron_pruebas_unitarias { get; set; }
        [JsonIgnore]
        public string se_incluyeron_objetos_de_seguridad { get; set; }
        public string mes_facturacin { get; set; }
    }
}
