namespace Domain;

public class Incidence
{
    public Guid IncidenceId { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public int Watchers { get; set; }
    public bool SelectedByAssistant { get; set; }
    public bool Solved { get; set; }
}
