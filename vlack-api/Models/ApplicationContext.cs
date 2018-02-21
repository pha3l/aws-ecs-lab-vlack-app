using Microsoft.EntityFrameworkCore;

namespace VlackApi.Models
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {}

    public DbSet<Channel> Channels { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Channel>().ToTable("Channel");
      modelBuilder.Entity<Message>().ToTable("Message");
    }
  }
}