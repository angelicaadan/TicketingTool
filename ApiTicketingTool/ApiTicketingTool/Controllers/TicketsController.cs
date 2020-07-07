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
                                _ticket.group_id = Convert.ToInt64(reader["groupID"].ToString());
                                _ticket.priority = Convert.ToInt32(reader["priority"].ToString());
                                _ticket.requester_id = Convert.ToInt64(reader["customerID"].ToString());
                                _ticket.responder_id = Convert.ToInt64(reader["agentID"].ToString(), 16);
                                _ticket.source = Convert.ToInt32(reader["source"].ToString());
                                _ticket.status = Convert.ToInt32(reader["status"].ToString());
                                _ticket.subject = reader["subject"].ToString();
                                _ticket.type = reader["type"].ToString();
                                _ticket.description = reader["description"].ToString();
                                _ticket.custom_fields.cliente = Convert.ToString(reader["customerCompany"].ToString());
                                _ticket.custom_fields.id_cotizador = Convert.ToString(reader["quotationID"].ToString());
                                _ticket.custom_fields.proyecto = Convert.ToInt32(reader["projectNumber"]);
                                _ticket.custom_fields.diseo = reader["name"].ToString();
                                _ticket.custom_fields.horas_estimadas_por_cliente = Convert.ToDouble(reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_inicio = Convert.ToString(reader["estimatedStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedStartDate"]);
                                _ticket.custom_fields.porcentaje_de_avance = Convert.ToInt32(reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"]);
                                _ticket.custom_fields.horas_tampm_semana_2 = Convert.ToDouble(reader["tmHoursWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek2"]);
                                _ticket.custom_fields.horas_tampm_semana_1 = Convert.ToDouble(reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_entrega = Convert.ToString(reader["estimatedEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedEndDate"]);
                                _ticket.custom_fields.horas_tampm_semana_3 = Convert.ToDouble(reader["tmHoursWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek3"]);
                                _ticket.custom_fields.cf_fecha_de_real_inicio = Convert.ToString(reader["realStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realStartDate"]);
                                _ticket.custom_fields.horas_tampm_semana_4 = Convert.ToDouble(reader["tmHoursWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek4"]);
                                _ticket.custom_fields.cf_fecha_de_real_entrega = Convert.ToString(reader["realEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realEndDate"]);
                                _ticket.custom_fields.avance_semana_2 = Convert.ToDouble(reader["progressWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek2"]);
                                _ticket.custom_fields.avance_semana_3 = Convert.ToDouble(reader["progressWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek3"]);
                                _ticket.custom_fields.avance_semana_4 = Convert.ToDouble(reader["progressWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek4"]);
                                _ticket.custom_fields.horas_garanta = Convert.ToDouble(reader["guaranteeHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["guaranteeHours"]);
                                _ticket.custom_fields.cf_horas_estimadas_por_agente = Convert.ToInt64(reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"]);
                                _ticket.custom_fields.mes_facturacin = reader["billingMonth"].ToString();
                                _ticket.tags = reader["tags"].ToString();

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
                                _ticket.group_id = Convert.ToInt64(reader["groupID"].ToString());
                                _ticket.priority = Convert.ToInt32(reader["priority"].ToString());
                                _ticket.requester_id = Convert.ToInt64(reader["customerID"].ToString());
                                _ticket.responder_id = Convert.ToInt64(reader["agentID"].ToString());
                                _ticket.source = Convert.ToInt32(reader["source"].ToString());
                                _ticket.status = Convert.ToInt32(reader["status"].ToString());
                                _ticket.subject = reader["subject"].ToString();
                                _ticket.type = reader["type"].ToString();
                                _ticket.description = reader["description"].ToString();
                                _ticket.custom_fields.cliente = Convert.ToString(reader["customerCompany"].ToString());
                                _ticket.custom_fields.id_cotizador = Convert.ToString(reader["quotationID"].ToString());
                                _ticket.custom_fields.proyecto = Convert.ToInt16(reader["projectNumber"].ToString());
                                _ticket.custom_fields.diseo = reader["name"].ToString();
                                _ticket.custom_fields.horas_estimadas_por_cliente = Convert.ToDouble(reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_inicio = Convert.ToString(reader["estimatedStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedStartDate"]);
                                _ticket.custom_fields.porcentaje_de_avance = Convert.ToInt32(reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"]);
                                _ticket.custom_fields.horas_tampm_semana_2 = Convert.ToDouble(reader["tmHoursWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek2"]);
                                _ticket.custom_fields.horas_tampm_semana_1 = Convert.ToDouble(reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"]);
                                _ticket.custom_fields.cf_fecha_de_estimada_entrega = Convert.ToString(reader["estimatedEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedEndDate"]);
                                _ticket.custom_fields.horas_tampm_semana_3 = Convert.ToDouble(reader["tmHoursWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek3"]);
                                _ticket.custom_fields.cf_fecha_de_real_inicio = Convert.ToString(reader["realStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realStartDate"]);
                                _ticket.custom_fields.horas_tampm_semana_4 = Convert.ToDouble(reader["tmHoursWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek4"]);
                                _ticket.custom_fields.cf_fecha_de_real_entrega = Convert.ToString(reader["realEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realEndDate"]);
                                _ticket.custom_fields.avance_semana_2 = Convert.ToDouble(reader["progressWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek2"]);
                                _ticket.custom_fields.avance_semana_3 = Convert.ToDouble(reader["progressWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek3"]);
                                _ticket.custom_fields.avance_semana_4 = Convert.ToDouble(reader["progressWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek4"]);
                                _ticket.custom_fields.horas_garanta = Convert.ToDouble(reader["guaranteeHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["guaranteeHours"]);
                                _ticket.custom_fields.cf_horas_estimadas_por_agente = Convert.ToInt64(reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"]);
                                _ticket.custom_fields.mes_facturacin = reader["billingMonth"].ToString();
                                _ticket.tags = reader["tags"].ToString();

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
        [HttpPost("tickets/users")]
        public async Task<IActionResult> PostTicketsAsync([FromBody] PostTicketFreshDesk data)
        {

            try
            {
                var dataAsString = JsonConvert.SerializeObject(data);

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
                
                return Ok(result.StatusCode);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("tickets/usersPut/{ticketID}")]
        public async Task<IActionResult> PutTicketsAsync(string ticketID, [FromBody] PutTicketFreshDesk data)
        {
            try
            {
                var csvString = String.Join(",", data.custom_fields.tickets_relacionados);

                Put2TicketFreshDesk obj = new Put2TicketFreshDesk();
                obj.custom_fields.tickets_relacionados = csvString;

                var dataAsString = JsonConvert.SerializeObject(obj);

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
                var url = "https://tmconsulting.freshdesk.com/api/v2/tickets/" + ticketID;
                var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
                var result = await httpClient.PutAsync(url, content);

                return Ok(result.StatusCode);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
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
                            _Status.statusID = reader["statusID"].ToString();
                            _Status.statusDescription = reader["statusDescription"].ToString();
                            _list.Add(_Status);

                        }
                    }
                    return Ok(_list);
                }
            }
        }
    }
}