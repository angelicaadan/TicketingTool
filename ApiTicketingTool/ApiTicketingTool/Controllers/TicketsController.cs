using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiTicketingTool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTicketingTool.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly Context _context;
        private string _connectionString;

        public TicketsController(Context context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetValue<string>("Context");

        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("tickets/{status}")]
        public async Task<ActionResult> GetTicket(string status)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetTicket", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@status", status));
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    int x = -1;
                    List<TicketFreshDesk> _list = new List<TicketFreshDesk>();
                    TicketFreshDesk _ticket = new TicketFreshDesk();
                    {
                        List<string> tags = new List<string>();

                        while (await reader.ReadAsync())
                        {

                            if ((int)reader["ticketID"] != x)
                            {
                                if (x != -1)
                                {
                                    _list.Add(_ticket);
                                }
                                x = (int)reader["ticketID"];
                                _ticket = new TicketFreshDesk();
                                _ticket.TicketID = (int)reader["ticketID"];
                                _ticket.group_id = Convert.ToInt64(reader["groupID"]);
                                _ticket.priority = Convert.ToInt32(reader["priority"]);
                                _ticket.requester_id = Convert.ToInt64(reader["customerID"]);
                                _ticket.responder_id = Convert.ToInt64(reader["agentID"].ToString().Trim(), 16);
                                _ticket.source = Convert.ToInt32(reader["source"]);
                                _ticket.status = Convert.ToInt32(reader["status"]);
                                _ticket.subject = reader["subject"].ToString().Trim();
                                _ticket.type = reader["type"].ToString().Trim();
                                _ticket.description = reader["description"].ToString().Trim();
                                _ticket.custom_fields.cliente = Convert.ToString(reader["customerCompany"]).Trim();
                                _ticket.custom_fields.id_cotizador = Convert.ToString(reader["quotationID"]).Trim();
                                _ticket.custom_fields.proyecto = Convert.ToInt32(reader["projectNumber"]); 
                                _ticket.custom_fields.diseo = reader["name"].ToString().Trim();
                                _ticket.custom_fields.horas_estimadas_por_cliente = Convert.ToDouble(reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_inicio = Convert.ToString(reader["estimatedStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedStartDate"]).Trim();
                                _ticket.custom_fields.porcentaje_de_avance = Convert.ToInt32(reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"]);
                                _ticket.custom_fields.horas_tampm_semana_2 = Convert.ToDouble(reader["tmHoursWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek2"]);
                                _ticket.custom_fields.horas_tampm_semana_1 = Convert.ToDouble(reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_entrega = Convert.ToString(reader["estimatedEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedEndDate"]).Trim();
                                _ticket.custom_fields.horas_tampm_semana_3 = Convert.ToDouble(reader["tmHoursWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek3"]);
                                _ticket.custom_fields.cf_fecha_de_real_inicio = Convert.ToString(reader["realStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realStartDate"]).Trim();
                                _ticket.custom_fields.horas_tampm_semana_4 = Convert.ToDouble(reader["tmHoursWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek4"]);
                                _ticket.custom_fields.cf_fecha_de_real_entrega = Convert.ToString(reader["realEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realEndDate"]).Trim();
                                _ticket.custom_fields.avance_semana_2 = Convert.ToDouble(reader["progressWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek2"]);
                                _ticket.custom_fields.avance_semana_3 = Convert.ToDouble(reader["progressWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek3"]);
                                _ticket.custom_fields.avance_semana_4 = Convert.ToDouble(reader["progressWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek4"]);
                                _ticket.custom_fields.horas_garanta = Convert.ToDouble(reader["guaranteeHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["guaranteeHours"]);
                                _ticket.custom_fields.cf_horas_estimadas_por_agente = Convert.ToInt64(reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"]);
                                _ticket.custom_fields.mes_facturacin = reader["billingMonth"].ToString().Trim();
                                _ticket.tags = reader["tags"].ToString().Trim();

                            }
                        }
                        if (_ticket.TicketID != 0)
                        {
                            _list.Add(_ticket);
                        }
                        return Ok(_list);
                    }
                }
            }
        }
        
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("ticket/{ticketID}")]
        public async Task<ActionResult> GetTicketID(int ticketID)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetTicketID", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ticketID", ticketID));

                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    int x = -1;
                    List<TicketFreshDesk> _list = new List<TicketFreshDesk>();
                    TicketFreshDesk _ticket = new TicketFreshDesk();
                    {

                        while (await reader.ReadAsync())
                        {

                            if ((int)reader["TicketID"] != x)
                            {
                                if (x != -1)
                                {
                                    _list.Add(_ticket);
                                }
                                x = (int)reader["TicketID"];
                                _ticket = new TicketFreshDesk();

                                _ticket.TicketID = (int)reader["ticketID"];
                                _ticket.group_id = Convert.ToInt64(reader["groupID"]);
                                _ticket.priority = Convert.ToInt32(reader["priority"]);
                                _ticket.requester_id = Convert.ToInt64(reader["customerID"]);
                                _ticket.responder_id = Convert.ToInt64(reader["agentID"].ToString().Trim());
                                _ticket.source = Convert.ToInt32(reader["source"]);
                                _ticket.status = Convert.ToInt32(reader["status"]);
                                _ticket.subject = reader["subject"].ToString().Trim();
                                _ticket.type = reader["type"].ToString().Trim();
                                _ticket.description = reader["description"].ToString().Trim();
                                _ticket.custom_fields.cliente = Convert.ToString(reader["customerCompany"]).Trim();
                                _ticket.custom_fields.id_cotizador = Convert.ToString(reader["quotationID"]).Trim();
                                //_ticket.custom_fields.proyecto = reader["projectNumber"].ToString().Trim();
                                _ticket.custom_fields.proyecto = Convert.ToInt32(reader["projectNumber"]);
                                _ticket.custom_fields.diseo = reader["name"].ToString().Trim();
                                _ticket.custom_fields.horas_estimadas_por_cliente = Convert.ToDouble(reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_inicio = Convert.ToString(reader["estimatedStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedStartDate"]).Trim();
                                _ticket.custom_fields.porcentaje_de_avance = Convert.ToInt16(reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"]);
                                _ticket.custom_fields.horas_tampm_semana_2 = Convert.ToDouble(reader["tmHoursWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek2"]);
                                _ticket.custom_fields.horas_tampm_semana_1 = Convert.ToDouble(reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_entrega = Convert.ToString(reader["estimatedEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedEndDate"]).Trim();
                                _ticket.custom_fields.horas_tampm_semana_3 = Convert.ToDouble(reader["tmHoursWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek3"]);
                                _ticket.custom_fields.cf_fecha_de_real_inicio = Convert.ToString(reader["realStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realStartDate"]).Trim();
                                _ticket.custom_fields.horas_tampm_semana_4 = Convert.ToDouble(reader["tmHoursWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek4"]);
                                _ticket.custom_fields.cf_fecha_de_real_entrega = Convert.ToString(reader["realEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realEndDate"]).Trim();
                                _ticket.custom_fields.avance_semana_2 = Convert.ToDouble(reader["progressWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek2"]);
                                _ticket.custom_fields.avance_semana_3 = Convert.ToDouble(reader["progressWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek3"]);
                                _ticket.custom_fields.avance_semana_4 = Convert.ToDouble(reader["progressWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek4"]);
                                _ticket.custom_fields.horas_garanta = Convert.ToDouble(reader["guaranteeHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["guaranteeHours"]);
                                _ticket.custom_fields.cf_horas_estimadas_por_agente = Convert.ToInt64(reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"]);
                                _ticket.custom_fields.mes_facturacin = reader["billingMonth"].ToString().Trim();
                                _ticket.tags = reader["tags"].ToString().Trim();

                            }
                        }
                        if (_ticket.TicketID != 0)
                        {
                            _list.Add(_ticket);
                        }
                        return Ok(_list);
                    }
                }
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("tickets/{idTicket}/users")]
        public async Task<String> PostTicketsAsync(IEnumerable<PostTicketFreshDesk> data, string idTicket)
        {
            {
                List<String> ticketID = new List<String>() { idTicket };
                String resultPut = String.Empty;

                try
                {
                    foreach (var it in data)
                    {
                        var dataAsString = JsonConvert.SerializeObject(it);
                        var httpClient = new HttpClient();
                        string yourusername = "UNGq0cadwojfirXm6U7o";
                        string yourpwd = "X";


                        httpClient.DefaultRequestHeaders.Authorization =
                          new AuthenticationHeaderValue(
                              "Basic", Convert.ToBase64String(
                                  System.Text.ASCIIEncoding.ASCII.GetBytes(
                                     $"{yourusername}:{yourpwd}")));

                        httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                        var url = "https://tmconsulting.freshdesk.com/api/v2/tickets";
                        var content = new StringContent(dataAsString, Encoding.UTF8, "application/json"); 
                        var result = await httpClient.PostAsync(url, content);

                        if (result.IsSuccessStatusCode)
                        {
                            var jsonPuro = await result.Content.ReadAsStringAsync();
                            dynamic jsonDesarializado = JsonConvert.DeserializeObject(jsonPuro);
                            ticketID.Add(Convert.ToString(jsonDesarializado["id"]));
                        }
                    }

                    


                    foreach (var id in ticketID)
                    {
                        foreach (var item in ticketID)
                        {
                            PutStringTicket jsonPut = new PutStringTicket();

                            List<String> _listID = new List<String>();

                            foreach (var item2 in ticketID)
                            {
                                if (item != item2)
                                {
                                    _listID.Add(item2);
                                }
                            }
                            jsonPut.custom_fields.tickets_relacionados = String.Join(",", _listID);
                            resultPut = await PutTicketsAsync(item, jsonPut);
                        }

                    }
                    return resultPut;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("tickets/usersPut/{ticketID}")]
        public async Task<String> PutTicketsAsync(string ticketID, [FromBody] PutStringTicket data)
        {
            String jsonResponse = String.Empty;

            try
            {
                //var csvString = String.Join(",", data.custom_fields.tickets_relacionados);

                //PutStringTicket obj = new PutStringTicket();
                //obj.custom_fields.tickets_relacionados = csvString;

                var dataAsString = JsonConvert.SerializeObject(data);

                HttpClient httpClient = new HttpClient();
                string yourusername = "UNGq0cadwojfirXm6U7o";
                string yourpwd = "X";


                httpClient.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue(
                      "Basic", Convert.ToBase64String(
                          System.Text.ASCIIEncoding.ASCII.GetBytes(
                             $"{yourusername}:{yourpwd}")));

                httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var url = "https://tmconsulting.freshdesk.com/api/v2/tickets/" + ticketID;
                var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonPuro = await response.Content.ReadAsStringAsync();
                    dynamic jsonDesarializado = JsonConvert.DeserializeObject(jsonPuro);
                    jsonResponse = jsonDesarializado.ToString(); ;

                }
            
                    return jsonResponse;
        }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("status")]
        public async Task<ActionResult> GetStatus()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("spGetStatus", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Status> _list = new List<Status>();
                    Status _Status = new Status();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Status = new Status();
                            _Status.statusID = reader["statusID"].ToString().Trim();
                            _Status.statusDescription = reader["statusDescription"].ToString().Trim();
                            _list.Add(_Status);

                        }
                    }
                    return Ok(_list);
                }
            }
        }
    }
}