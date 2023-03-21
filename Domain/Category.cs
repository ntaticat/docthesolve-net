namespace Domain;

public class Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ICollection<ProblemCategory>? Problems { get; set; }
    public ICollection<TicketCategory>? Tickets { get; set; }
}