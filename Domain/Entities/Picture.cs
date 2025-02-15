using System;
using Domain.ValueObjects;
using Flunt.Br;

namespace Domain.Entities;

internal class Picture : Entity
{
    internal AppFile File { get; private set; } = null!;
    internal UniqueName Name { get; private set; } = null!;
    internal string AwsKey { get;  private set; } = null!;
    internal DateTime UrlExpired { get; private set; }
    internal string UrlTemp { get; private set; } = null!;
    internal bool Ativo { get; private set; }
    
    private Picture(){}
    internal Picture(AppFile file, UniqueName name, string awsKey, DateTime urlExpired, string urlTemp, bool ativo)
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotNullOrEmpty(name.ToString(), "Picture.Name", "Name cannot be null or empty")
                .IsNotNullOrEmpty(awsKey, "Picture.AwsKey", "AwsKey cannot be empty")
                .IsNotNullOrEmpty(urlTemp, "Picture.UrlTemp", "UrlTemp cannot be null or empty")
                .IsGreaterThan(urlExpired, DateTime.Now, "Picture.UrlExpired", "UrlExpired cannot be in the past")
        );
        
        AddNotificationsFromValueObjects(file, name);
        if (!IsValid) return;
        
        File = file;
        Name = name;
        AwsKey = awsKey;
        UrlExpired = urlExpired;
        UrlTemp = urlTemp;
        Ativo = ativo;
    }
}