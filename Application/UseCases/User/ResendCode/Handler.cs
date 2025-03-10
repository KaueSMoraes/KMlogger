using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Records;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace Application.UseCases.User.ResendCode;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IDbCommit _dbCommit;
    
    public Handler(IUserRepository userRepository, IDbCommit dbCommit,
         IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _dbCommit = dbCommit;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.email, cancellationToken);
        user?.AddNotifications(
            new Contract<Notifiable<Notification>>()
                .Requires()
                .IsTrue(await _userRepository.GetByEmail(request.email, cancellationToken)
                     != null, "Email", "Email not registered")
        );

        if (user.Notifications.Any())
            return new BaseResponse(404, "Request invalid",user.Notifications.ToList());

        user.AssignToken(new Random().Next(1000, 9999).ToString());
        await Task.Run(() => _userRepository.Update(user), cancellationToken);
        await _dbCommit.Commit(cancellationToken);

        await _emailService.SendEmailAsync(user.FullName.FirstName, user.Email.Address!, "Reenvio de Código de Ativação",
            $"<strong> Seu código de Ativação da Conta: {user.TokenActivate} <strong>", "KMLogger",
            Configuration.SmtpUser, cancellationToken);

        return new BaseResponse(200, "Code sent successfully", null);
    }
}
