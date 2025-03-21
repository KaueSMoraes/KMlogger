using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Read.ReadByApp;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ILogRepository _logRepository;
    public Handler(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var logs = await _logRepository.GetByAppAsync(request.AppId, request.StartDate, request.EndDate, cancellationToken);
        if(logs is null) return new BaseResponse(404, "Logs not found");
        return new BaseResponse(200, "Logs found", null, logs);
    }
}
