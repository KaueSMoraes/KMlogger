using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ResendCode;

public record Request(string email,long token) : IRequest<BaseResponse>;