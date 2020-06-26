using ApiTicketingTool.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiTicketingTool
{
    class Conexion
    {
        private const string urlApi = "https://tmconsulting.freshdesk.com/api/v2";

        public static string GetConnectionString(ExecutionContext context)
        {

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();

            return config.GetConnectionString("SQLConnectionString");
        }

        public static HttpResponseMessage GetApi(string route, string urlParameters = "")
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(urlApi + route);

            string yourusername = "UNGq0cadwojfirXm6U7o";
            string yourpwd = "X";


            client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue(
                  "Basic", Convert.ToBase64String(
                      System.Text.ASCIIEncoding.ASCII.GetBytes(
                         $"{yourusername}:{yourpwd}")));


            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;

            return response;

        }

    
        public static HttpResponseMessage GetTickets(string urlParameters = "")
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(urlApi + "/tickets?per_page=1");

            string yourusername = "UNGq0cadwojfirXm6U7o";
            string yourpwd = "X";


            client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue(
                  "Basic", Convert.ToBase64String(
                      System.Text.ASCIIEncoding.ASCII.GetBytes(
                         $"{yourusername}:{yourpwd}")));


            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            var response =  client.GetAsync(urlParameters).Result;
            return response;

        }

        
    }
}
