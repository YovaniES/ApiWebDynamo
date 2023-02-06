using ApiWebDynamo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebDynamo.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("usuarios");
            //modelBuilder.Entity<Personas>().ToTable("personas");
        }
    }
}
