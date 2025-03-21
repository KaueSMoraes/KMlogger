using Domain.Records;
using MediatR;

namespace Application.UseCases.Category.Delete;

public record Request(Guid id) 
    : IRequest<BaseResponse>;

