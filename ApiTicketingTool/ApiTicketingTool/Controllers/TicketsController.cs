using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTicketingTool.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTicketingTool.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly Context _context;
        private readonly TicketRepository _repository;
        private string _connectionString;

        public TicketsController(Context context, TicketRepository repository, IConfiguration configuration)
        {
            _context = context;
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _connectionString = configuration.GetValue<string>("connectionString");

        }
        // GET: Tickets
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _repository.GetAll();
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }
    }
}