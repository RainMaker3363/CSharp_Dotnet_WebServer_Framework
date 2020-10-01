using Microsoft.EntityFrameworkCore;
using SharedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class ApplicationDbCotext : DbContext
    {
        public DbSet<GameResult> GameResults { get; set; }

        public ApplicationDbCotext(DbContextOptions<ApplicationDbCotext> options) : base(options)
        {

        }
    }
}
