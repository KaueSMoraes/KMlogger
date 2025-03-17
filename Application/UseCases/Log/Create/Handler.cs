using System;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Log.Create;

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
        var log = _mapper.Map<LogEnrty>(request);
        await _logRepository.CreateAsync(log, cancellationToken);
        return new BaseResponse(201, "Log created successfully");
    }
}
