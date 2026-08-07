using NotificationDispatchKata.Services;
using NotificationDispatchKata.Domain;
using NotificationDispatchKata.Contracts;

namespace NotificationDispatchKata;

class Program
{
    static void Main(string[] args)
    {
        var pushSender = new PushNotificationSender();
        var smsSender = new SmsNotificationSender();
        var emailSender = new EmailNotificationSender();

        INotificationSender[] notificationSenders =
        [
            pushSender,
            smsSender,
            emailSender
        ];

        var dispatcher = new NotificationDispatcher(notificationSenders);

        var pushNotification = new Notification(1, "Test User 1", "Hello Push", NotificationChannel.Push, NotificationPriority.Normal);
        var smsNotification = new Notification(2, "Test User 2", "Hello SMS", NotificationChannel.SMS, NotificationPriority.Normal);
        var emailNotification = new Notification(3, "Test User 3", "Hello Email", NotificationChannel.Email, NotificationPriority.Normal);

        dispatcher.Dispatch(pushNotification);
        dispatcher.Dispatch(smsNotification);
        dispatcher.Dispatch(emailNotification);

        Console.WriteLine($"\nStatus of push notification: {pushNotification.Status}");
        Console.WriteLine($"Status of SMS notification: {smsNotification.Status}");
        Console.WriteLine($"Status of email notification: {emailNotification.Status}\n");

        try
        {
            dispatcher.Dispatch(emailNotification);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\n\nError: {ex.Message}\n\n");
        }

    }
}
