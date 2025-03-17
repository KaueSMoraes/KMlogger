using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Api;
using Presentation.Middlewares;

using LoginRequest = Application.UseCases.User.Login.Request;
using RegisterRequest = Application.UseCases.User.Register.Request;
using ActivateRequest = Application.UseCases.User.Activate.Request;
using ActivatePasswordRequest = Application.UseCases.User.ForgotPassword.Activate.Request;
using ForgotRequest = Application.UseCases.User.ForgotPassword.Request;
using ResendCodeRequest = Application.UseCases.User.ResendCode.Request;

using System.Net.Mail;

namespace Presentation.Controllers;

[ApiController]
[Route("user")]
public  class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [ApiKey]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, response);
        }
        catch (Exception e) 
        {
            return StatusCode(500 , new {message = e.Message});
        }
    }

    [HttpPost("register")]
    [ApiKey]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest();
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, new { response.statuscode, response.message, response.notifications});
        }
        catch (SmtpException e)
        {
            return BadRequest( new {message = e.Message});
        }
        catch (Exception e) 
        {
            return StatusCode(500 , new {message = e.Message});
        }
    }
    
    [HttpPut("activate")]
    [ApiKey]
    public async Task<IActionResult> Activate([FromQuery] string code, [FromQuery] string email, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest();   
        try
        {
            var response = await mediator.Send(new ActivateRequest(email, long.TryParse(code, out var tokenlong) ? tokenlong : 0), cancellationToken);
            return StatusCode(response.statuscode, response);
        }
        catch (Exception e) 
        {
            return StatusCode(500 , new {message = e.Message});
        }
    }

    [HttpPut("forgot-password")]
    [ApiKey]
    public async Task<IActionResult> ForgotPassword(ForgotRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, new { response.statuscode, response.message, response.notifications });
        }
        catch(Exception e)
        {
           return StatusCode(500 , new {message = e.Message});
        }
    }

    [HttpPut("resend-code")]
    [ApiKey]
    public async Task<IActionResult> ResendCode(ResendCodeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, new { response.statuscode, response.message, response.notifications });
        }
        catch(Exception e)
        {
           return StatusCode(500 , new {message = e.Message});
        }
    }
    
    
    [HttpPut("forgot-password/activate")]
    [ApiKey]    
    public async Task<IActionResult> ForgotPasswordActivate(ActivatePasswordRequest
        request, CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid) return BadRequest();
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, new { response.message, response.notifications });
        }
        catch(Exception e)
        {
           return StatusCode(500 , new {message = e.Message});
        }
    }
}
