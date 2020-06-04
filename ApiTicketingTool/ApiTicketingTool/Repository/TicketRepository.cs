using ApiTicketingTool.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketingTool.Repository
{
    public class TicketRepository
    {
        private readonly string _connectionString;
        public TicketRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("Context");
        }
        public async Task<object> GetAll()
        {
            
           
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetTicketsAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
                                //_ticket.tags = reader["tags"];
                                _ticket.surveysResult = reader["surveysResult"].ToString();
                                _ticket.companyID = reader["companyID"].ToString();
                                _ticket.customerCompany = reader["customerCompany"].ToString();
                                _ticket.projectNumber = reader["projectNumber"].ToString();
                                _ticket.sharePointID = reader["sharePointID"].ToString();
                                //_ticket.quatationID = reader["quatationID"] == DBNull.Value ? null : reader["quatationID"].ToString();
                                _ticket.customerEstimatedHours = (decimal)reader["customerEstimatedHours"];
                                _ticket.tmHoursWeek1 = reader["tmHoursWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek1"];
                                _ticket.tmHoursWeek2 = reader["tmHoursWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek2"];
                                _ticket.tmHoursWeek3 = reader["tmHoursWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek3"];
                                _ticket.tmHoursWeek4 = reader["tmHoursWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["tmHoursWeek4"];
                                _ticket.progressWeek1 = reader["progressWeek1"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek1"];
                                _ticket.progressWeek2 = reader["progressWeek2"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek2"];
                                _ticket.progressWeek3 = reader["progressWeek3"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek3"];
                                _ticket.progressWeek4 = reader["progressWeek4"] == DBNull.Value ? (decimal?)null : (decimal)reader["progressWeek4"];
                                _ticket.billingMonth = reader["billingMonth"].ToString();
                                _ticket.totalBillingHours = (decimal)reader["totalBillingHours"];
                                _ticket.totalProgress = (decimal)reader["totalProgress"];
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
                        return _list;
                    }
                }
            }
        }
    }
}
