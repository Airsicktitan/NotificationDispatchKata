namespace NotificationDispatchKata.Domain;

public class Notification
{
    public int Id { get; init; }
    public string Recipient { get; init; }
    public string Message { get; init; }
    public NotificationChannel Channel { get; init; }
    public NotificationPriority Priority { get; init; }
    public NotificationStatus Status { get; private set; }

    public Notification(int id, string recipient, string message, NotificationChannel channel, NotificationPriority priority)
    {
        if(id <= 0)
            throw new ArgumentException("Id must be a positive integer.", nameof(id));
        
        if(string.IsNullOrWhiteSpace(recipient))
            throw new ArgumentException("Recipient cannot be null or empty.", nameof(recipient));

        if(string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be null or empty.", nameof(message));

        Id = id;
        Recipient = recipient;
        Message = message;
        Channel = channel;
        Priority = priority;
        Status = NotificationStatus.Pending;

    }

    public void MarkSent()
    {
        if(Status != NotificationStatus.Pending)
            throw new InvalidOperationException("Cannot update status to Sent");

        Status = NotificationStatus.Sent;
    }

    public void MarkFailed()
    {
        if (Status != NotificationStatus.Pending)
            throw new InvalidOperationException("Cannot update status to Failed");

        Status = NotificationStatus.Failed;
    }
}