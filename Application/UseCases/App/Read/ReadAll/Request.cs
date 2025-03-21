using Domain.Records;
using MediatR;

namespace Application.UseCases.App.Read.ReadAll;

public record  Request
(
    int skip,
    int take
) : IRequest<BaseResponse>;
