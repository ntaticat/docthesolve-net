namespace Domain;

public class TicketNotification
{
    public Guid TicketId { get; set; }
    public Guid NotificationId { get; set; }

    public Ticket? Ticket { get; set; }
    public Notification? Notification { get; set; }
}