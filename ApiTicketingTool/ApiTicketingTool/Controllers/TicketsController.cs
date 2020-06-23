using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiTicketingTool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace ApiTicketingTool.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public async Task<ActionResult> GetAllTicket(string status)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetTicketsAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@status", status));

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        int x = -1;
                        List<Ticket> _list = new List<Ticket>();
                        Ticket _ticket = new Ticket();
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
                                _ticket.customerID = reader["customerID"].ToString();
                                _ticket.subject = reader["subject"].ToString();
                                _ticket.description = reader["description"].ToString();
                                _ticket.status = reader["status"].ToString();
                                _ticket.priority = reader["priority"].ToString();
                                _ticket.source = reader["source"].ToString();
                                _ticket.type = reader["type"].ToString();
                                _ticket.email = reader["email"].ToString();
                                _ticket.phoneNumberRequester = reader["phoneNumberRequester"].ToString();
                                _ticket.IDFacebookProfile = reader["IDFacebookProfile"].ToString();
                                _ticket.agentID = reader["agentID"].ToString();
                                _ticket.groupID = reader["groupID"].ToString();
                                _ticket.creationDate = (DateTime)reader["creationDate"];
                                _ticket.expirationDate = (DateTime)reader["expirationDate"];
                                _ticket.resolvedDate = (reader["resolvedDate"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["resolvedDate"]);
                                _ticket.closedDate = (reader["closedDate"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["closedDate"]);
                                _ticket.lastUpdateDate = (DateTime)reader["lastUpdateDate"];
                                _ticket.fistResponseRequestDate = (reader["fistResponseRequestDate"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["fistResponseRequestDate"]);
                                _ticket.agentInteractions = reader["agentInteractions"].ToString();
                                _ticket.customerIntearction = reader["customerIntearction"].ToString();
                                _ticket.resolutionStatus = reader["resolutionStatus"].ToString();
                                _ticket.firstResponseStatus = reader["firstResponseStatus"].ToString();
                                _ticket.tags = reader["tags"].ToString();
                                _ticket.surveysResult = reader["surveysResult"].ToString();
                                _ticket.companyID = reader["companyID"].ToString();
                                _ticket.customerCompany = reader["customerCompany"].ToString();
                                _ticket.projectNumber = reader["projectNumber"].ToString();
                                _ticket.sharePointID = reader["sharePointID"].ToString();
                                _ticket.quotationID = reader["quotationID"].ToString();
                                _ticket.customerEstimatedHours = reader["customerEstimatedHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["customerEstimatedHours"];
                                _ticket.tmHoursWeek1 = reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"];
                                _ticket.tmHoursWeek2 = reader["tmHoursWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek2"];
                                _ticket.tmHoursWeek3 = reader["tmHoursWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek3"];
                                _ticket.tmHoursWeek4 = reader["tmHoursWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek4"];
                                _ticket.progressWeek1 = reader["progressWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek1"];
                                _ticket.progressWeek2 = reader["progressWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek2"];
                                _ticket.progressWeek3 = reader["progressWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek3"];
                                _ticket.progressWeek4 = reader["progressWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek4"];
                                _ticket.billingMonth = reader["billingMonth"].ToString();
                                _ticket.totalBillingHours = reader["totalBillingHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalBillingHours"];
                                _ticket.totalProgress = reader["totalProgress"] == DBNull.Value ? (decimal?)null : (decimal)reader["totalProgress"];
                                _ticket.estimatedStartDate = reader["estimatedStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedStartDate"];
                                _ticket.estimatedEndDate = reader["estimatedEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["estimatedEndDate"];
                                _ticket.realStartDate = reader["realStartDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realStartDate"];
                                _ticket.realEndDate = reader["realEndDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["realEndDate"];
                                _ticket.estimatedHourAgent = reader["estimatedHourAgent"] == DBNull.Value ? (decimal?)null : (decimal)reader["estimatedHourAgent"];
                                _ticket.guaranteeHours = reader["guaranteeHours"] == DBNull.Value ? (decimal?)null : (decimal)reader["guaranteeHours"];
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
        {
            string route = "/tickets";
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://tmconsulting.freshdesk.com/api/v2" + route);

                string yourusername = "UNGq0cadwojfirXm6U7o";
                string yourpwd = "X";


                client.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue(
                      "Basic", Convert.ToBase64String(
                          System.Text.ASCIIEncoding.ASCII.GetBytes(
                             $"{yourusername}:{yourpwd}")));


                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync(client.BaseAddress, data).Result;
            return response;
        }
        //[HttpPost]
        //public async Task Post2()
        //{
        //    string fdDomain = "tmconsulting"; // your freshdesk domain
        //    string apiKey = "UNGq0cadwojfirXm6U7o";
        //    string apiPath = "/api/v2/tickets"; // API path
        //    string json = "{ \"name\":\"Prueba2\",\"requester_id\" : 8022623444,\"email\": \"rodolfo.sanchez@cuamoc.com\", \"phone\":\"0000\",\"subject\": \"Support Needed...\", \"type\":\"Requerimiento\",\"status\": 2, \"priority\": 1,\"description\": \"prueba...\",\"responder_id\":8000138444,\"cc_emails\": [\"ram@freshdesk.com\",\"diana@freshdesk.com\"],\"custom_fields\": { \"diseo\":\"test\", \"proyecto\":1, \"horas_estimadas_por_cliente\":18, \"cf_horas_estimadas_por_agente\":15, \"horas_tampm_semana_1\":12, \"porcentaje_de_avance\":6, \"se_incluyeron_pruebas_unitarias\":\"NO\", \"se_incluyeron_objetos_de_seguridad\":\"NO\", \"mes_facturacin\":\"Julio 2020\"},\"due_by\" : \"2020-08-14T13:08:06Z\",\"email_config_id\" : 0,\"fr_due_by\" : \"2020-08-10T13:08:06Z\",\"group_id\" : 8000074542,\"product_id\" : 0,\"source\":2,\"tags\" : [] }";
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + fdDomain + ".freshdesk.com" + apiPath);
        //    //HttpWebRequest class is used to Make a request to a Uniform Resource Identifier (URI).  
        //    request.ContentType = "application/json";
        //    // Set the ContentType property of the WebRequest. 
        //    request.Method = "POST";
        //    byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //    // Set the ContentLength property of the WebRequest. 
        //    request.ContentLength = byteArray.Length;
        //    string authInfo = apiKey + ":X"; // It could be your username:password also.
        //    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        //    request.Headers["Authorization"] = "Basic " + authInfo;

        //    //Get the stream that holds request data by calling the GetRequestStream method. 
        //    Stream dataStream = request.GetRequestStream();
        //    // Write the data to the request stream. 
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    // Close the Stream object. 
        //    dataStream.Close();
        //    try
        //    {
        //        Console.WriteLine("Submitting Request");
        //        WebResponse response = request.GetResponse();
        //        // Get the stream containing content returned by the server.
        //        //Send the request to the server by calling GetResponse. 
        //        dataStream = response.GetResponseStream();
        //        // Open the stream using a StreamReader for easy access. 
        //        StreamReader reader = new StreamReader(dataStream);
        //        // Read the content. 
        //        string Response = reader.ReadToEnd();
        //        //return status code
        //        Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
        //        //return location header
        //        Console.WriteLine("Location: {0}", response.Headers["Location"]);
        //        //return the response 
        //        Console.Out.WriteLine(Response);
        //    }
        //    catch (WebException ex)
        //    {
        //        Console.WriteLine("API Error: Your request is not successful. If you are not able to debug this error properly, mail us at support@freshdesk.com with the follwing X-Request-Id");
        //        Console.WriteLine("X-Request-Id: {0}", ex.Response.Headers["X-Request-Id"]);
        //        Console.WriteLine("Error Status Code : {1} {0}", ((HttpWebResponse)ex.Response).StatusCode, (int)((HttpWebResponse)ex.Response).StatusCode);
        //        using (var stream = ex.Response.GetResponseStream())
        //        using (var reader = new StreamReader(stream))
        //        {
        //            Console.Write("Error Response: ");
        //            Console.WriteLine(reader.ReadToEnd());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("ERROR");
        //        Console.WriteLine(ex.Message);

        //    }
        //}
    }
}