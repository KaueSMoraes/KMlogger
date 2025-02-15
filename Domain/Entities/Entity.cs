using Flunt.Notifications;

namespace Domain.Entities;

internal abstract class Entity : Notifiable<Notification>
{
    internal Guid Id { get; private set; }
    internal DateTime? CreatedDate { get;  private set; }
    internal DateTime? UpdatedDate { get;  private set; }
    internal DateTime? DeletedDate { get;  private set; }
    
    protected void AddNotificationsFromValueObjects(params List<Notifiable<Notification>> valueObjects)
    {
        foreach (var valueObject in valueObjects)
        {
            AddNotifications(valueObject.Notifications);
        }
    }
    internal void SetValuesUpdate() => UpdatedDate = DateTime.Now;
    internal void SetValuesDelete() => DeletedDate = DateTime.Now;
    internal void SetValuesCreate() => CreatedDate = UpdatedDate = DateTime.Now;
    
}