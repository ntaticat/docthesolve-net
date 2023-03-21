namespace Domain;

public class Notification
{
    public Guid NotificationId { get; set; }
    public string? Content { get; set; }
    
    public ICollection<TicketNotification>? Tickets { get; set; }
}