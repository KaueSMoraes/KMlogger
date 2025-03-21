using Domain.Records;
using MediatR;

namespace Application.UseCases.Category.Read.RealAll;
public record Request(
    int Skip,
    int Take
) 
: IRequest<BaseResponse>; 
