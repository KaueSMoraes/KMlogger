using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Read.ReadById;

public record  Request(
    Guid Id
) : IRequest<BaseResponse>;

