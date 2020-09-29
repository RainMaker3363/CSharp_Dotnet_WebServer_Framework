using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RankingApp.Data.Models;

namespace RankingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<GameResult> GameResults { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
