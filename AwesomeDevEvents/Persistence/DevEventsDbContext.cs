using AwesomeDevEvents.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.Api.Persistence
{
    public class DevEventsDbContext : DbContext
    {
        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {
            
        }
        public DbSet<DevEvents> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeaker { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<DevEvents>(e =>
            {
                e.HasKey(de => de.Id);

                e.Property(e => e.Title).IsRequired(false);

                e.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                e.Property(e => e.StartDate)
                    .HasColumnName("Sart_Date");

                e.Property(e => e.EndDate)
                    .HasColumnName("End_Date");

                e.HasMany(de => de.Speakers)
                    .WithOne()
                    .HasForeignKey(s => s.DevEventId);

            });

            builder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }
      
    }
    
}
