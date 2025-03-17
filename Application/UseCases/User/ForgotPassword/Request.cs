using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ForgotPassword;

public  record Request(string Email) : IRequest<BaseResponse>;
