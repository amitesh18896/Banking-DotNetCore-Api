using Banking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Data
{
    public class cardDbContext:DbContext
    {
        public cardDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Card> cards { get; set; }
    }
}
