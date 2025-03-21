using System.Text.Json.Serialization;
using Flunt.Notifications;

namespace Domain.Records;

public record BaseResponse
{
    public int statuscode;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? message = string.Empty;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<Notification>? notifications = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public dynamic Response { get; private set; } = null!;

    public BaseResponse(int statuscode, string message, List<Notification>? notifications = null, dynamic response = null)
    {
        this.message = message;
        this.statuscode = statuscode;
        this.notifications = notifications != null && notifications.Any() ? notifications : null;
        Response = response;
    }
}
    