using System;
using Domain.Entities;

namespace Domain.Interfaces.Services;

internal interface ITokenService
{
    string GenerateToken(User user);
}
