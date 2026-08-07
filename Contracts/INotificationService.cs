using NotificationDispatchKata.Domain;

namespace NotificationDispatchKata.Contracts;

public interface INotificationSender
{
    public NotificationChannel Channel { get; }
    void Send(Notification notification);
}