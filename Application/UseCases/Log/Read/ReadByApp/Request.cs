using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Read.ReadByApp;
public record Request(
    Guid AppId,
    DateTime StartDate,
    DateTime EndDate
) :
 IRequest<BaseResponse>;
