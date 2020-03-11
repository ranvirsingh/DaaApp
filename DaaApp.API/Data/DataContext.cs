using DaaApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DaaApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Value> Values { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }

}