using NotificationDispatchKata.Contracts;
using NotificationDispatchKata.Domain;

namespace NotificationDispatchKata.Services;

public class NotificationDispatcher
{
    private readonly IReadOnlyCollection<INotificationSender> _notificationSenders;

    public NotificationDispatcher(IReadOnlyCollection<INotificationSender> notificationSenders)
    {
        ArgumentNullException.ThrowIfNull(notificationSenders, nameof(notificationSenders));

        _notificationSenders = notificationSenders;
    }

    public void Dispatch(Notification notification)
    {
        ArgumentNullException.ThrowIfNull(notification, nameof(notification));
        
        var sender = _notificationSenders.FirstOrDefault(s => s.Channel == notification.Channel);

        if(notification.Status != NotificationStatus.Pending)
            throw new InvalidOperationException("Notification has already been sent or failed.");

        if(sender == null)
            throw new InvalidOperationException($"No sender found for channel {notification.Channel}");
        
        
        try
        {
            sender.Send(notification);
        }
        catch (Exception ex)
        {
            notification.MarkFailed();
            ex.Data.Add("NotificationId", notification.Id);
            throw;
        }

        notification.MarkSent();
    }
}