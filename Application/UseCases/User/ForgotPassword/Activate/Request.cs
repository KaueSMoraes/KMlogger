using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ForgotPassword.Activate;

public  record Request(string email, long token, string newPassword) : IRequest<BaseResponse>;
