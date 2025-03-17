using Domain.Records;
using MediatR;

namespace Application.UseCases.User.Activate;

public  record Request(string email,long token) : IRequest<BaseResponse>;