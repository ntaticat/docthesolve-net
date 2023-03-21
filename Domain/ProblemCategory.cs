namespace Domain;

public class ProblemCategory
{
    public Guid ProblemId { get; set; }
    public Guid CategoryId { get; set; }
    
    public Problem? Problem { get; set; }
    public Category? Category { get; set; }
}