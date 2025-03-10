using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ForgotPassword;

internal record Request(string Email) : IRequest<BaseResponse>;
