using System.ComponentModel.DataAnnotations.Schema;
using Flunt.Br;

namespace Domain.ValueObjects;

internal class AppFile : BaseValueObject 
{
    [NotMapped]
    internal Stream File { get; private set; } = null!;
    internal string FileName { get; private set; } = null!;
    internal long FileSize { get; private set; }

    private AppFile(){}
    internal AppFile(Stream file, string fileName)
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotNull(file, Key, "File cannot be null")
                .IsLowerThan(file.Length, 10_000_000, Key, "File size must be less than 10MB")  // Example size validation
                .IsNotNullOrEmpty(fileName, Key, "File name cannot be null or empty")
        );

        if (!IsValid) return;
        File = file;
        FileName = fileName;
        FileSize = file.Length;
    }
}
