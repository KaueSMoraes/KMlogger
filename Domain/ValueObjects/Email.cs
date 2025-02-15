using Flunt.Br;

namespace Domain.ValueObjects;

internal class Email : BaseValueObject
{
    internal string? Address { get; private set; }
    internal Email(string? address)
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