using System.ComponentModel.DataAnnotations.Schema;
using Flunt.Notifications;

namespace Domain.ValueObjects;

public abstract class BaseValueObject : Notifiable<Notification>
{
    protected string  Key { get; }
    [NotMapped]
    public IReadOnlyCollection<Notification> Notifications => base.Notifications;
    protected BaseValueObject()
    {
        Key = this.GetType().Name;
    }
}