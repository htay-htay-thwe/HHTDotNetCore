using Microsoft.EntityFrameworkCore;
using HHTDotNetCore.MvcAPI.Models;

namespace HHTDotNetCore.MvcAPI.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //}
        //public DbSet<BlogModel> Blogs { get; set; }

    }
}
