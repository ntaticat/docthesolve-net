namespace Domain;

public class TicketCategory
{
    public Guid TicketId { get; set; }
    public Guid CategoryId { get; set; }
    
    public Ticket? Ticket { get; set; }
    public Category? Category { get; set; }
}