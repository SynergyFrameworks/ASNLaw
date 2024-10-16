using System.Threading.Tasks;

namespace Infrastructure.PushNotifications
{
    public interface IPushNotificationManager
    {
        void Send(PushNotification notification);
        Task SendAsync(PushNotification notification);
        PushNotificationSearchResult SearchNotifies(string userId, PushNotificationSearchCriteria criteria);

    }
}
