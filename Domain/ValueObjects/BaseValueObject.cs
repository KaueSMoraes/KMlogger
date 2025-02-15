using System.ComponentModel.DataAnnotations.Schema;
using Flunt.Notifications;

namespace Domain.ValueObjects;

internal abstract class BaseValueObject : Notifiable<Notification>
{
    protected string  Key { get; }
    [NotMapped]
    internal IReadOnlyCollection<Notification> Notifications => base.Notifications;
    protected BaseValueObject()
    {
        Key = this.GetType().Name;
    }
}