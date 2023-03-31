namespace Domain;

public class Ticket
{
    public Guid TicketId { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public int Watchers { get; set; }
    public bool SelectedByAgent { get; set; }
    public bool Solved { get; set; }
    
    // public Guid? AgentId { get; set; }  // Sustituir por Identity Role
    // public ApplicationUser? Agent { get; set; }  // Sustituir por Identity Role
    // public Guid CustomerId { get; set; } // Sustituir por Identity Role
    // public Customer? Customer { get; set; } // Sustituir por Identity Role

    public ICollection<TicketCategory>? Categories { get; set; }
    public ICollection<TicketNotification>? Notifications { get; set; }
    public ICollection<TicketProblem>? Problems { get; set; }
}
