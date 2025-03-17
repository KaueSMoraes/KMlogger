using System;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace Application.UseCases.User.Register;

public  class Handler : IRequestHandler<Request, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IDbCommit _dbCommit;
    
    public Handler(IUserRepository userRepository, IDbCommit dbCommit,
        IMapper mapper, IEmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailService = emailService;
        _dbCommit = dbCommit;
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request);
        user.AddNotifications(
            new Contract<Notifiable<Notification>>()
                .Requires()
                .IsFalse(await _userRepository.GetByEmail(request.Email, cancellationToken) != null, "Email", "Email already registered")
        );
            
        if (user.Notifications.Any())
            return new Response(404, "Request invalid",user.Notifications.ToList());
        
        var activationLink = $"{Configuration.FrontendUrl}/activate-account?email={Uri.EscapeDataString(user.Email.Address)}&token={Uri.EscapeDataString(user.TokenActivate.ToString())}";
        await _emailService.SendEmailAsync(user.FullName.FirstName, user.Email.Address!, "Ative sua Conta!",
            $"<strong> Clique no link para ativar sua conta: <a href='{activationLink}'>Ativar Conta</a> <strong>", "KMLogger",
            Configuration.SmtpUser, cancellationToken);

        await _userRepository.CreateAsync(user, cancellationToken);
        await _dbCommit.Commit(cancellationToken);
        return _mapper.Map<Response>(user);
    }
}