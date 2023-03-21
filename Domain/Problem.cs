namespace Domain;

public class Problem
{
    public Guid ProblemId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public Guid AgentId { get; set; }
    public Agent? Agent { get; set; }

    public ICollection<Solution>? Solutions { get; set; }
    public ICollection<ProblemCategory>? Categories { get; set; }
    public ICollection<TicketProblem>? Tickets { get; set; }
}
