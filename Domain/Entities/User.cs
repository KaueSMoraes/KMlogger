using System.ComponentModel.DataAnnotations.Schema;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : Entity
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public Password Password { get; private set; } 
    public bool Active { get; private set; }
    
    public IList<Role> Roles { get; private set; }
    public long TokenActivate { get; private set; }
    
    [NotMapped]
    public string Token { get; private set; }
    
    private User(){}
    
    public User(FullName? fullName, Email? email, Address? address, bool active, Password? password)
    {
        AddNotificationsFromValueObjects(fullName, email, address, password);
        FullName = fullName;
        Email = email;
        Address = address;
        Active = active;
        Password = password;
        TokenActivate = Random.Shared.Next(1000, 10000);
    }
    
    public User(Email email, Password password)
    {
        AddNotificationsFromValueObjects(email, password);
        Password = password;
        Email = email;
    }

    public void GenerateNewToken()
        => TokenActivate = Random.Shared.Next(1000, 10000);

    public void UpdatePassword(Password password)
    {
        AddNotificationsFromValueObjects(password);
        Password = password;
    }
    
    public void AssignToken(string token) => Token = token;

    public void AssignActivate(bool isActivate)
    {
        Active = isActivate;
        TokenActivate = 0;
    } 
}