using Domain.Records;
using Flunt.Notifications;

namespace Application.UseCases.User.Login;

public  record Response(int statuscode, string? message = null!,List<Notification>? notifications = null!, string? Token = null!) 
    : BaseResponse(statuscode,message, notifications);