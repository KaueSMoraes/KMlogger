using Flunt.Br;

namespace Domain.ValueObjects;

internal class Address : BaseValueObject
{
    internal string? Road { get; private set; }
    internal string? NeighBordHood { get; private set; }
    internal long? Number { get; private set; }
    internal string? Complement  { get; private set; }

    private Address(){}
    internal Address(long? number, string? neighBordHood, string? road, string? complement)
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotNull(number, Key, "Number cannot be null") 
                .IsGreaterThan(number != null ? (double)number : double.MinValue, 0.0, Key, "Number must be greater than 0")
                .IsNotNullOrEmpty(road ?? string.Empty, Key, "Road is required") 
                .IsNotNullOrEmpty(neighBordHood ?? string.Empty, Key, "Neighborhood is required") 
                .IsLowerThan(complement != null ? complement.Length : int.MaxValue, 100, Key, "Complement must not exceed 100 characters") 
        );

        Number = number;
        NeighBordHood = neighBordHood;
        Road = road;
        Complement = complement;
    }
}