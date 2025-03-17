using System;
using Domain.Interfaces.Repositories;
using Domain.Records;
using MediatR;

namespace Application.UseCases.User.Activate;

public  class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IDbCommit _dbCommit;
    
    public Handler(IUserRepository userRepository, IDbCommit dbCommit)
    {
        _dbCommit = dbCommit;
        _userRepository = userRepository;
    }
    
    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.ActivateUserAsync(request.email, request.token, cancellationToken);
        if (!(user is null) || request.token.Equals(0)) return new BaseResponse(400, "User or token invalid");
        await _dbCommit.Commit(cancellationToken);
        return new BaseResponse(200, "User activated!");
    }
}
