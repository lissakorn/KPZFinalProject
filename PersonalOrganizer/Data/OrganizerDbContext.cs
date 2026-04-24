using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Models;

namespace PersonalOrganizer.Data
{
    public class OrganizerDbContext : DbContext
    {
        public OrganizerDbContext(DbContextOptions<OrganizerDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}