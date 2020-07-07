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
    [Route("api/loadcombos")]
    [ApiController]
    public class LoadComboBoxsController : ControllerBase
    {
        private readonly Context _context;
        private string _connectionString;

        public LoadComboBoxsController(Context context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetValue<string>("Context");

        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("contact")]
        public async Task<ActionResult> GetContact()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetContact", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Contact> _list = new List<Contact>();
                    Contact _Contact = new Contact();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Contact = new Contact();
                            _Contact.contactID = reader["contactID"].ToString();
                            _Contact.name = reader["name"].ToString();
                            _Contact.active = reader["active"].ToString();
                            _list.Add(_Contact);
                        }
                    }
                    return Ok(_list);
                }
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("customer")]
        public async Task<ActionResult> GetCustomer()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("spGetCustomer", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Customer> _list = new List<Customer>();
                    Customer _Customer = new Customer();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Customer = new Customer();
                            _Customer.companyID = reader["companyID"].ToString();
                            _Customer.companyDescription = reader["companyDescription"].ToString();
                            _list.Add(_Customer);
                        }
                    }
                    return Ok(_list);
                }
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("types")]
        public async Task<ActionResult> GetTypes()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetTypes", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Types> _list = new List<Types>();
                    Types _Types = new Types();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Types = new Types();
                            _Types.type = reader["type"].ToString();
                            _list.Add(_Types);
                        }
                    }
                    return Ok(_list);
                }
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("groups")]
        public async Task<ActionResult> GetGroups()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetGroup", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Group> _list = new List<Group>();
                    Group _Group = new Group();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Group = new Group();
                            _Group.groupID = reader["groupID"].ToString();
                            _Group.groupDescription = reader["groupDescription"].ToString();
                            _list.Add(_Group);
                        }
                    }
                    return Ok(_list);
                }
            }
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("priorities")]
        public async Task<ActionResult> GetPriorities()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetPriorities", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Priorities> _list = new List<Priorities>();
                    Priorities _Priorities = new Priorities();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Priorities = new Priorities();
                            _Priorities.priorityID = reader["priorityID"].ToString();
                            _Priorities.priorityDescription = reader["priorityDescription"].ToString();
                            _list.Add(_Priorities);
                        }
                    }
                    return Ok(_list);
                }

            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("agents")]
        public async Task<ActionResult> GetAgents()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAgents", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Agents> _list = new List<Agents>();
                    Agents _Agent = new Agents();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Agent = new Agents();
                            _Agent.agentID = reader["agentID"].ToString();
                            _Agent.agentDescription = reader["agentDescription"].ToString();
                            _list.Add(_Agent);
                        }
                    }
                    return Ok(_list);
                }

            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("cotizador")]
        public async Task<ActionResult> GetCotizador()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetQuotation", sql))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await sql.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<Cotizador> _list = new List<Cotizador>();
                    Cotizador _Cotizador = new Cotizador();
                    {

                        while (await reader.ReadAsync())
                        {
                            _Cotizador = new Cotizador();
                            _Cotizador.quotationID = reader["quotationID"].ToString();
                            _list.Add(_Cotizador);
                        }
                    }
                    return Ok(_list);
                }

            }
        }
    }
}