using Domain.Records;
using Flunt.Notifications;

namespace Application.UseCases.User.Register;

public  record Response(int statuscode, string message,List<Notification> notifications)   
    : BaseResponse(statuscode, message ,notifications);