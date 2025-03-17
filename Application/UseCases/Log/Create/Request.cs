using Domain.Records;
using MediatR;

using Environment = Domain.Enums.Environment;

namespace Application.UseCases.Log.Create;

public record Request(
    string Message,
    string Level,
    string? StackTrace,
    string? Source,
    string? TargetSite,
    Environment Environment
) : IRequest<BaseResponse>;
