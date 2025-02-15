using System;
using System.Reflection;
using Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal class KMLoggerDbContext : DbContext
{
    internal KMLoggerDbContext(DbContextOptions<KMLoggerDbContext> options) : base(options) { }

    internal DbSet<User> Users { get; init; }
    internal DbSet<Role> Roles { get; init; }
    internal DbSet<Picture> Pictures { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
