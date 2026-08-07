using NotificationDispatchKata.Domain;
using NotificationDispatchKata.Contracts;

namespace NotificationDispatchKata.Services;

public class EmailNotificationSender : INotificationSender
{
    public NotificationChannel Channel => NotificationChannel.Email;
    public void Send(Notification notification)
    {   
        ArgumentNullException.ThrowIfNull(notification, nameof(notification));
        
        if(notification.Channel != NotificationChannel.Email)
            throw new ArgumentException("EmailNotification can only send email notifications.", nameof(notification));

        Console.WriteLine($"Sending notification: {notification.Message} to {notification.Recipient}");
    }
}