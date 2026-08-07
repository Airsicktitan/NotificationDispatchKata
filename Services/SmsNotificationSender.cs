using NotificationDispatchKata.Domain;
using NotificationDispatchKata.Contracts;

namespace NotificationDispatchKata.Services;

public class SmsNotificationSender : INotificationSender
{
    public NotificationChannel Channel => NotificationChannel.SMS;
    public void Send(Notification notification)
    {   
        ArgumentNullException.ThrowIfNull(notification, nameof(notification));
        
        if(notification.Channel != NotificationChannel.SMS)
            throw new ArgumentException("SmsNotification can only send SMS notifications.", nameof(notification));

        Console.WriteLine($"Sending notification: {notification.Message} to {notification.Recipient}");
    }
}