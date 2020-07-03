using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketingTool.Models
{
    public class TicketFreshDesk
    {

        public TicketFreshDesk()
        {
            this.custom_fields = new Json();
        }
        public int TicketID { get; set; }
        public int? email_config_id { get; set; }
        public Int64 group_id { get; set; }
        public int priority { get; set; }
        public Int64 requester_id { get; set; }
        public Int64 responder_id { get; set; }
        public int source { get; set; }
        public int status { get; set; }//status
        public string subject { get; set; }
        public int? product_id { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public Json custom_fields { get; set; }
        public string tags { get; set; }
    }
    public class PostTicketFreshDesk
    {
        public int? email_config_id { get; set; }
        public Int64 group_id { get; set; }
        public int priority { get; set; }
        public Int64 requester_id { get; set; }
        public Int64 responder_id { get; set; }
        public int source { get; set; }
        public int status { get; set; }//status
        public string subject { get; set; }
        public int? product_id { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public Json custom_fields { get; set; }
        public List<string> tags { get; set; }
    }
    public class Json
    {
        public string cliente { get; set; }
        public string id_cotizador { get; set; }
        public int? proyecto { get; set; }
        public string diseo { get; set; }
        public double? horas_estimadas_por_cliente { get; set; }
        public string cf_fecha_de_estimada_inicio { get; set; }
        public int? porcentaje_de_avance { get; set; }
        public double? horas_tampm_semana_2 { get; set; }
        public double? horas_tampm_semana_1 { get; set; }
        public string tickets_relacionados { get; set; }
        public string cf_fecha_de_estimada_entrega { get; set; }
        public double? horas_tampm_semana_3 { get; set; }
        public string cf_fecha_de_real_inicio { get; set; }
        public double? horas_tampm_semana_4 { get; set; }
        public string cf_fecha_de_real_entrega { get; set; }
        public double? avance_semana_2 { get; set; }
        public double? avance_semana_3 { get; set; }
        public double? avance_semana_4 { get; set; }
        public double? horas_garanta { get; set; }
        public Int64? cf_horas_estimadas_por_agente { get; set; }
        public string mes_facturacin { get; set; }
        public string se_incluyeron_pruebas_unitarias { get; set; }
        public string se_incluyeron_objetos_de_seguridad { get; set; }

    }

    public class JsonPut
    {
        public List<string> tickets_relacionados { get; set; }

    }
    public class PutTicketFreshDesk
    {
        public JsonPut custom_fields { get; set; }
    }
<<<<<<< HEAD
=======
    public class JsonPut2
    {
        public string tickets_relacionados { get; set; }

    }
    public class Put2TicketFreshDesk
    {
        public Put2TicketFreshDesk()
        {
            this.custom_fields = new JsonPut2();
        }
        public JsonPut2 custom_fields { get; set; }
    }
>>>>>>> d2c910e6077c98307c3e9b3598a7f60d7f157a79
}
