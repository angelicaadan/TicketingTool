using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ApiTicketingTool.Models;

namespace ApiTicketingTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private const string url = "https://tmconsulting.freshdesk.com/api/v2";
        [HttpPost("login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult Login(LoginCredentials credentials)
        {
            try
            {
                //InstanciaHttpClient
                HttpClient client = new HttpClient();

                //AsignacionDeURL
                client.BaseAddress = new Uri(url);

                //Credenciales del API de FreshDesk
                string yourusername = credentials.Username;
                string yourpwd = credentials.Password;

                //Authorization al API


                client.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue(
                      "Basic", Convert.ToBase64String(
                          System.Text.ASCIIEncoding.ASCII.GetBytes(
                             $"{yourusername}:{yourpwd}")));



                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(url + "/tickets/?include=stats").Result;

                if (response.IsSuccessStatusCode == true)
                {
                    var datos = new LoginResults()
                    {
                        status = "Autorizado"
                    };
                    return Ok(datos);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized user, check your credentials");
                }


            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, "AuthController Register", DateTime.UtcNow);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
