using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class DocTheSolveNetContext: DbContext
{
    public DbSet<Assistant> Assistants { get; set; }
    public DbSet<Incidence> Incidences { get; set; }
    public DbSet<Problem> Problems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("InMemory");
    }
}