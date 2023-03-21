namespace Domain;

public class Solution
{
    public Guid SolutionId { get; set; }
    public string? Content { get; set; }
    
    public Guid? ProblemId { get; set; }
    public Problem? Problem { get; set; }
    public Guid? AgentId { get; set; }
    public Agent? Agent { get; set; }
}