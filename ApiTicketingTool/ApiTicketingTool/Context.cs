﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTicketingTool.Models;

namespace ApiTicketingTool
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
               : base(options)
        {
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<PostTicketFreshDesk> PostTicketFreshDesk { get; set; }
        public DbSet<PutTicket> PutTicket { get; set; }

        public DbSet<Status> Status { get; set; }

    }
}
