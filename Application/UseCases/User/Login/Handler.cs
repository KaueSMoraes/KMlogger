using System;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;

namespace Application.UseCases.User.Login;

public  class Handler : IRequestHandler<Request, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public Handler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request);
        var isAuthenticated = await _userRepository.Authenticate(user, cancellationToken);

        if (!isAuthenticated || user.Notifications.Any())
            return new Response(403, "Invalid password or user" ,user.Notifications.ToList());
        
        var token = _tokenService.GenerateToken(user);
        user.AssignToken(token); 
        return new Response(200, "Login Successful",[], token);
    }
}