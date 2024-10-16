using System.Threading.Tasks;

namespace Infrastructure.PushNotifications
{
    public interface IPushNotificationStorage
    {
        void SavePushNotification(PushNotification notification);
        Task SavePushNotificationAsync(PushNotification notification);
        PushNotificationSearchResult SearchPushNotifications(string userId, PushNotificationSearchCriteria criteria);
    }
}
