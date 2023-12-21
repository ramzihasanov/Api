using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  {}
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Catagories { get; set; }
    }
}