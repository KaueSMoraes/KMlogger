using Flunt.Br;

namespace Domain.ValueObjects;

internal class Description : BaseValueObject
{
    internal string Text { get; private set; }

    internal Description(string text)
    {
        AddNotifications(
            new Contract().Requires().IsNotNullOrEmpty(text, Key, "Description cannot be null or empty")
                .IsLowerThan(text.Length, 1000, Key, "Description cannot be longer than 1000 characters")
        );
        Text = text;
    }
    private Description(){}
}