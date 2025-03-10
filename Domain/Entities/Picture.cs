using System;
using Domain.ValueObjects;
using Flunt.Br;

namespace Domain.Entities;

public class Picture : Entity
{
    public AppFile File { get; private set; } = null!;
    public UniqueName Name { get; private set; } = null!;
    public string AwsKey { get;  private set; } = null!;
    public DateTime UrlExpired { get; private set; }
    public string UrlTemp { get; private set; } = null!;
    public bool Ativo { get; private set; }
    
    private Picture(){}
    public Picture(AppFile file, UniqueName name, string awsKey, DateTime urlExpired, string urlTemp, bool ativo)
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