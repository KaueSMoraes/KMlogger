using System;
using Bogus;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Test.Repositories;

namespace Test.Red.Infrastructure.Repositories;

public class UserRepositoryTests
{
    private readonly Faker _faker = new();
    private readonly FakeUserRepository _fakeRepository = new();

    [Fact]
    public async Task Should_Not_Authenticate_User_With_Invalid_Password()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var correctPassword = _faker.Internet.Password(8, true);
        var wrongPassword = _faker.Internet.Password(8, true);

        var user = new User(
            fullName: new FullName(_faker.Name.FirstName(), _faker.Name.LastName()),
            email: new Email(email),
            address: new Address(long.TryParse(_faker.Address.BuildingNumber(), out var number) ? number : 0  , 
            _faker.Address.City(), _faker.Address.ZipCode(), _faker.Address.SecondaryAddress()),
            active: true,
            password: new Password(correctPassword)
        );
        await _fakeRepository.CreateAsync(user, CancellationToken.None);
        var loginUser = new User(new Email(email), new Password(wrongPassword, true));

        // Act
        var isAuthenticated = await _fakeRepository.Authenticate(loginUser, CancellationToken.None);

        // Assert
        isAuthenticated.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Not_Activate_User_With_Invalid_Token()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var invalidToken = _faker.Random.Long(1000, 9999);

        var user = new User(
            fullName: new FullName(_faker.Name.FirstName(), _faker.Name.LastName()),
            email: new Email(email),
            address: new Address(long.TryParse(_faker.Address.BuildingNumber(), out var number) ? number : 0 , _faker.Address.City(), _faker.Address.ZipCode(), _faker.Address.SecondaryAddress()),
            active: false,
            password: new Password(_faker.Internet.Password(8, true))
        );

        await _fakeRepository.CreateAsync(user, CancellationToken.None);

        // Act
        var isActivated = await _fakeRepository.ActivateUserAsync(user.Email.Address!, invalidToken, CancellationToken.None);

        // Assert
        isActivated.Active.Should().BeFalse();
        var updatedUser = await _fakeRepository.GetById(user.Id, CancellationToken.None);
        updatedUser!.Active.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Not_Authenticate_Nonexistent_User()
    {
        // Arrange
        var loginUser = new User(new Email(_faker.Internet.Email()), new Password(_faker.Internet.Password(8, true)));

        // Act
        var isAuthenticated = await _fakeRepository.Authenticate(loginUser, CancellationToken.None);

        // Assert
        isAuthenticated.Should().BeFalse();
    }
    
    [Fact]
    public async Task GetByEmail_ShouldReturnFalse_WhenEmailDoesNotExist()
    {
        // Arrange
        var fakeRepo = new FakeUserRepository();
        // Act
        var exists = await fakeRepo.GetByEmail("nonexistent@example.com", CancellationToken.None);
        // Assert
        Assert.False(exists != null);
    }
}