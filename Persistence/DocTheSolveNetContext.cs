using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DocTheSolveNetContext: DbContext
{
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<ProblemCategory> ProblemCategories { get; set; }
    public DbSet<TicketCategory> TicketCategories { get; set; }
    public DbSet<TicketNotification> TicketNotifications { get; set; }
    public DbSet<TicketProblem> TicketProblems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("InMemory");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Agent
        modelBuilder.Entity<Agent>()
            .HasMany(a => a.Problems)
            .WithOne(p => p.Agent)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Agent>()
            .HasMany(a => a.Solutions)
            .WithOne(a => a.Agent)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Agent>()
            .HasMany(a => a.Tickets)
            .WithOne(t => t.Agent)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Customer
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Tickets)
            .WithOne(t => t.Customer)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Problem
        modelBuilder.Entity<Problem>()
            .HasMany(p => p.Solutions)
            .WithOne(s => s.Problem)
            .OnDelete(DeleteBehavior.Cascade);
        
        // ProblemCategories
        modelBuilder.Entity<ProblemCategory>()
            .HasKey(pc => new { pc.ProblemId, pc.CategoryId });
        modelBuilder.Entity<ProblemCategory>()
            .HasOne(pc => pc.Problem)
            .WithMany(p => p.Categories)
            .HasForeignKey(pc => pc.ProblemId);
        modelBuilder.Entity<ProblemCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.Problems)
            .HasForeignKey(pc => pc.CategoryId);
        
        // TicketCategories
        modelBuilder.Entity<TicketCategory>()
            .HasKey(tc => new { tc.TicketId, tc.CategoryId });
        modelBuilder.Entity<TicketCategory>()
            .HasOne(tc => tc.Ticket)
            .WithMany(t => t.Categories)
            .HasForeignKey(tc => tc.TicketId);
        modelBuilder.Entity<TicketCategory>()
            .HasOne(tc => tc.Category)
            .WithMany(c => c.Tickets)
            .HasForeignKey(tc => tc.CategoryId);
        
        // TicketNotifications
        modelBuilder.Entity<TicketNotification>()
            .HasKey(tn => new { tn.TicketId, tn.NotificationId });
        modelBuilder.Entity<TicketNotification>()
            .HasOne(tn => tn.Ticket)
            .WithMany(t => t.Notifications)
            .HasForeignKey(tn => tn.TicketId);
        modelBuilder.Entity<TicketNotification>()
            .HasOne(tn => tn.Notification)
            .WithMany(n => n.Tickets)
            .HasForeignKey(tn => tn.NotificationId);
        
        // TicketProblems
        modelBuilder.Entity<TicketProblem>()
            .HasKey(tp => new { tp.TicketId, tp.ProblemId });
        modelBuilder.Entity<TicketProblem>()
            .HasOne(tp => tp.Ticket)
            .WithMany(t => t.Problems)
            .HasForeignKey(tp => tp.TicketId);
        modelBuilder.Entity<TicketProblem>()
            .HasOne(tp => tp.Problem)
            .WithMany(p => p.Tickets)
            .HasForeignKey(tp => tp.ProblemId);
        
    }
}