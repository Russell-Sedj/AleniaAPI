using AleniaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AleniaAPI.Data
{
    public class AleniaContext : DbContext
    {
        public AleniaContext(DbContextOptions<AleniaContext> options) : base(options) { }

        public DbSet<Missions> Missions { get; set; }
        public DbSet<Etablissements> Etablissements { get; set; }
    }
}
