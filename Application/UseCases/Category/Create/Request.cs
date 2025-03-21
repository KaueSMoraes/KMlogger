using Domain.Records;
using MediatR;

namespace Application.UseCases.Category.Create;

public record Request(
    string name
) 
: IRequest<BaseResponse>;
