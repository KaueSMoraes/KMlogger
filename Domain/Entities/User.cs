using System.ComponentModel.DataAnnotations.Schema;
using Domain.ValueObjects;

namespace Domain.Entities;

internal class User : Entity
{
    internal FullName FullName { get; private set; }
    internal Email Email { get; private set; }
    internal Address Address { get; private set; }
    internal Password Password { get; private set; } 
    internal bool Active { get; private set; }
    
    internal IList<Role> Roles { get; private set; }
    internal long TokenActivate { get; private set; }
    
    [NotMapped]
    internal string Token { get; private set; }
    
    private User(){}
    
    internal User(FullName? fullName, Email? email, Address? address, bool active, Password? password)
    {
        AddNotificationsFromValueObjects(fullName, email, address, password);
        FullName = fullName;
        Email = email;
        Address = address;
        Active = active;
        Password = password;
        TokenActivate = Random.Shared.Next(1000, 10000);
    }
    
    internal User(Email email, Password password)
    {
        AddNotificationsFromValueObjects(email, password);
        Password = password;
        Email = email;
    }

    internal void GenerateNewToken()
        => TokenActivate = Random.Shared.Next(1000, 10000);

    internal void UpdatePassword(Password password)
    {
        AddNotificationsFromValueObjects(password);
        Password = password;
    }
    
    internal void AssignToken(string token) => Token = token;

    internal void AssignActivate(bool isActivate)
    {
        Active = isActivate;
        TokenActivate = 0;
    } 
}