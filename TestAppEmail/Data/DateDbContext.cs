using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppEmail.Models;

namespace TestAppEmail.Data
{
    public class DateDbContext : DbContext
    {
        public DateDbContext(DbContextOptions<DateDbContext> options)
            : base(options)
        {
        }

        public DbSet<DateModel> SystemParamaterTest { get; set; }

        public DbSet<SendEmailModel> SystemParamaterTestEmail { get; set; }

    }
}
