using System.Text.Json.Serialization;
using Flunt.Notifications;

namespace Domain.Records;

internal record BaseResponse
{
    internal int statuscode;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    internal string? message = string.Empty;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    internal List<Notification>? notifications = null!;

    internal BaseResponse(int statuscode, string message, List<Notification>? notifications = null)
    {
        this.message = message;
        this.statuscode = statuscode;
        this.notifications = notifications != null && notifications.Any() ? this.notifications : null;
    }
}
    