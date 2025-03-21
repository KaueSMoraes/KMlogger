using System;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Read.ReadByInterval;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ILogRepository _logRepository;

    public Handler(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var logs = await _logRepository.GetByIntervalAsync(request.DateInitial, request.DateFinal, cancellationToken);
        if(logs is null) return new BaseResponse(404, "Logs not found");
        return new BaseResponse(200, "Logs retrieved successfully", null, logs);
    }
}
