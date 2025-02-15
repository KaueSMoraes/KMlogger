using MediatR;

namespace Application.UseCases.User.Login;
internal record Request(string email, string password) : IRequest<Response>;