using System;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
