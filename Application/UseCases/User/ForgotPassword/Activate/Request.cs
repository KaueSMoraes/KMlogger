using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ForgotPassword.Activate;

internal record Request(string email, long token, string newPassword) : IRequest<BaseResponse>;
