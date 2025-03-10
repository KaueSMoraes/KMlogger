using Domain.Records;
using MediatR;

namespace Application.UseCases.User.Activate;

internal record Request(string email,long token) : IRequest<BaseResponse>;