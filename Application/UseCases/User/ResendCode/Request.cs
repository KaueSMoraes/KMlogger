using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ResendCode;

internal record Request(string email,long token) : IRequest<BaseResponse>;