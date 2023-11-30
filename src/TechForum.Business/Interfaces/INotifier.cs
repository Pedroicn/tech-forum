using TechForum.Business.Notifications;

namespace TechForum.Business.Interfaces;

public interface INotifier
{
  bool HasNotification();
  List<Notification> GetNotifications();
  void Handle(Notification notification);
}
