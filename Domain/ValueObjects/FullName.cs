using Flunt.Br;

namespace Domain.ValueObjects;

public class FullName : BaseValueObject
{
    public string FirstName { get; private set; }
    public string LastName  { get; private set; }

    public FullName(string firstName, string lastName)
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotNullOrEmpty(firstName, Key, "First name cannot be null or empty")
                .IsNotNullOrEmpty(lastName, Key, "Last name cannot be null or empty")
                .IsLowerThan(firstName.Length, 100.0, Key, "First name cannot be longer than 100 characters")
                .IsLowerThan(lastName.Length, 100.0, Key, "Last name cannot be longer than 100 characters")
        );
        FirstName = firstName;
        LastName = lastName;
    }
    private FullName(){}
}