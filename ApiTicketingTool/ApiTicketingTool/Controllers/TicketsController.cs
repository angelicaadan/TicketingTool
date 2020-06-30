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
                        List<Ticket> _list = new List<Ticket>();
                        Ticket _ticket = new Ticket() ;
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
                                _ticket = new Ticket();

                                _ticket.TicketID = (int)reader["TicketID"];
                                _ticket.name = reader["name"].ToString();
                                _ticket.requester_id = reader["customerID"].ToString();
                                _ticket.email = reader["email"].ToString();
                                _ticket.phone = reader["phoneNumberRequester"].ToString();
                                _ticket.subject = reader["subject"].ToString();
                                _ticket.type = reader["type"].ToString();
                                _ticket.status = reader["status"].ToString();
                                _ticket.priority =reader["priority"].ToString();
                                _ticket.description = reader["description"].ToString();
                                _ticket.responder_id = Convert.ToInt64(reader["agentID"].ToString(), 16);
                                _ticket.cc_emails= reader["cc_emails"].ToString();
                                _ticket.due_by= (DateTime)reader["expirationDate"];
                                _ticket.fr_due_by = (DateTime)reader["creationDate"];
                                _ticket.group_id = reader["groupID"].ToString();
                                _ticket.source = reader["source"].ToString();
                                _ticket.tags = reader["tags"].ToString();
                                _ticket.custom_fields.diseo= reader["name"].ToString();
                                _ticket.custom_fields.proyecto = reader["projectNumber"].ToString();
                                _ticket.custom_fields.horas_estimadas_por_cliente = Convert.ToDecimal(reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"]);
                                _ticket.custom_fields.cf_horas_estimadas_por_agente = Convert.ToInt64(reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"]);
                                _ticket.custom_fields.horas_tampm_semana_1 = Convert.ToDecimal(reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"]);
                                _ticket.custom_fields.porcentaje_de_avance = Convert.ToDecimal(reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"]);
                                _ticket.custom_fields.mes_facturacin = reader["billingMonth"].ToString();
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
                    List<Ticket> _list = new List<Ticket>();
                    Ticket _ticket = new Ticket();
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
                                _ticket = new Ticket();

                                _ticket.TicketID = (int)reader["TicketID"];
                                _ticket.name = reader["name"].ToString();
                                _ticket.requester_id = reader["customerID"].ToString();
                                _ticket.email = reader["email"].ToString();
                                _ticket.phone = reader["phoneNumberRequester"].ToString();
                                _ticket.subject = reader["subject"].ToString();
                                _ticket.type = reader["type"].ToString();
                                _ticket.status = reader["status"].ToString();
                                _ticket.priority = reader["priority"].ToString();
                                _ticket.description = reader["description"].ToString();
                                _ticket.responder_id = Convert.ToInt64(reader["agentID"].ToString(), 16);
                                _ticket.cc_emails = reader["cc_emails"].ToString();
                                _ticket.due_by = (DateTime)reader["expirationDate"];
                                _ticket.fr_due_by = (DateTime)reader["creationDate"];
                                _ticket.group_id = reader["groupID"].ToString();
                                _ticket.source = reader["source"].ToString();
                                _ticket.tags = reader["tags"].ToString();
                                _ticket.custom_fields.diseo = reader["name"].ToString();
                                _ticket.custom_fields.proyecto = reader["projectNumber"].ToString();
                                _ticket.custom_fields.horas_estimadas_por_cliente = Convert.ToDecimal(reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"]);
                                _ticket.custom_fields.cf_horas_estimadas_por_agente = Convert.ToInt64(reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"]);
                                _ticket.custom_fields.horas_tampm_semana_1 = Convert.ToDecimal(reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"]);
                                _ticket.custom_fields.porcentaje_de_avance = Convert.ToDecimal(reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"]);
                                _ticket.custom_fields.mes_facturacin = reader["billingMonth"].ToString();
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

        [HttpPost]
        public async Task<HttpResponseMessage> PostTickets(TicketFreshdesk data)
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("tickets/users")]
        public async Task<IActionResult> PostTicketsAsync([FromBody] TicketFreshDesk data)
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
                        }
                        _list.Add(_Status);
                    }
                    return Ok(_list);
                }
            }
        }
    }
}