namespace Domain;

public class TicketProblem
{
    public Guid TicketId { get; set; }
    public Guid ProblemId { get; set; }
    
    public Ticket? Ticket { get; set; }
    public Problem? Problem { get; set; }
}