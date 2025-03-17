using System;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Read.ReadById;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ILogRepository _logRepository;
    private readonly IMapper _mapper;

    public Handler(ILogRepository logRepository, IMapper mapper)
    {
        _logRepository = logRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var log = await _logRepository.GetByIdAsync(request.Id.ToString(), cancellationToken);
        if(log is null) return new BaseResponse(404, "Log not found");
        return _mapper.Map<BaseResponse>(log);
    }
}
