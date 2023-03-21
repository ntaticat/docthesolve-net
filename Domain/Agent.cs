namespace Domain;

public class Agent
{
    public Guid AgentId { get; set; }
    public string? AgentName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    
    public ICollection<Solution>? Solutions { get; set; }
    public ICollection<Problem>? Problems { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
}