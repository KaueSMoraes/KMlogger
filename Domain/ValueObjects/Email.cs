using Flunt.Br;

namespace Domain.ValueObjects;

public class Email : BaseValueObject
{
    public string? Address { get; private set; }
    public Email(string? address)
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsEmail(address, Key, "Email invalid")
        );
        Address = address;
    }
    private Email(){}
}