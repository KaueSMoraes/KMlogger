using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Read.ReadByInterval;

public record Request(
    DateTime DateFinal,
    DateTime DateInitial
) : IRequest<BaseResponse>;