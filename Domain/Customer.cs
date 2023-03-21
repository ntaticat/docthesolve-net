namespace Domain;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    
    public ICollection<Ticket>? Tickets { get; set; }
    
}