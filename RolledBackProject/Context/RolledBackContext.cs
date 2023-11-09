using Microsoft.EntityFrameworkCore;
using RolledBackProject.Models;

namespace RolledBackProject.Context
{
    public class RolledBackContext:DbContext
    {
        public RolledBackContext(DbContextOptions<RolledBackContext> context):base(context) 
        {
                
        }
        public DbSet<Person> People { get; set; }
    }
}
