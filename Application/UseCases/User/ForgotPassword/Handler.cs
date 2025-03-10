using System;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Records;
using MediatR;

namespace Application.UseCases.User.ForgotPassword;

public class Handler : IRequestHandler<Request, BaseResponse>
{
     private readonly IUserRepository _userRepository;
     private readonly IDbCommit _dbCommit;
     private readonly IEmailService _emailService;

     public Handler(IUserRepository userRepository,IDbCommit dbCommit, IEmailService emailService)
     {
          _userRepository = userRepository;
          _emailService = emailService;
          _dbCommit = dbCommit;
     }
     
     public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
     {
          var userFromDb = await _userRepository.GetByEmail(request.Email, cancellationToken);
          if (userFromDb == null)
          {
               return new BaseResponse(404,"User not found");
          }
          
          userFromDb.GenerateNewToken();
          _userRepository.Update(userFromDb);
          await _dbCommit.Commit(cancellationToken);
          
          //Envia email com código para ativação da alteração de senha
          await _emailService.SendEmailAsync(userFromDb.FullName.FirstName, userFromDb.Email.Address!, "Altere sua senha!",
               $"<strong> Seu código de Alteração de senha: {userFromDb.TokenActivate} <strong>", "ScoreBlog",
               Configuration.SmtpUser, cancellationToken);
          return new BaseResponse(201, "Password change activation email sent");
     }
}