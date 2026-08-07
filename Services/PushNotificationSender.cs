using NotificationDispatchKata.Domain;
using NotificationDispatchKata.Contracts;

namespace NotificationDispatchKata.Services;

public class PushNotificationSender : INotificationSender
{
    public NotificationChannel Channel => NotificationChannel.Push;
    public void Send(Notification notification)
    {   
        ArgumentNullException.ThrowIfNull(notification, nameof(notification));
        
        if(notification.Channel != NotificationChannel.Push)
            throw new ArgumentException("PushNotification can only send push notifications.", nameof(notification));

        Console.WriteLine($"Sending notification: {notification.Message} to {notification.Recipient}");
    }
}