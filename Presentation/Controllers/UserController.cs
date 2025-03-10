using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Api;
using Presentation.Middlewares;

using LoginRequest = Application.UseCases.User.Login.Request;
using RegisterRequest = Application.UseCases.User.Register.Request;
using ActivateRequest = Application.UseCases.User.Activate.Request;
using ForgotRequest = Application.UseCases.User.ForgotPassword.Request;
using ResendCodeRequest = Application.UseCases.User.ResendCode.Request;

using System.Net.Mail;

namespace Presentation.Controllers;

[ApiController]
[Route("user")]
internal class UserController(IMediator mediator) : InternalControllerBase
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
             return StatusCode(500 ,e.Message);
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
            return BadRequest("Email does not exist or is invalid");
        }
        catch (Exception e) 
        {
            return StatusCode(500 ,e.Message);
        }
    }
    
    [HttpPut("activate")]
    [ApiKey]
    public async Task<IActionResult> Activate([FromQuery] long token, [FromQuery] string email, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest();   
        try
        {
            var response = await mediator.Send(new ActivateRequest(email, token), cancellationToken);
            return StatusCode(response.statuscode, response);
        }
        catch (Exception e) 
        {
            return StatusCode(500 ,e.Message.ToString());
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
            return StatusCode(500, e.Message);
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
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpPut("forgot-password/activate")]
    [ApiKey]    
    public async Task<IActionResult> ForgotPassword(ActivateRequest
        request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, new { response.message, response.notifications });
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
