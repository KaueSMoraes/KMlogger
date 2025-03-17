using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public  class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KMLoggerDbContext>
{
    public KMLoggerDbContext CreateDbContext(string[] args)
    {
        var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Infrastructure\dbkmlogger.db");
        try
        {
            if (string.IsNullOrEmpty(databasePath))
                throw new Exception("A connection string must be provided.");
                
            var builder = new DbContextOptionsBuilder<KMLoggerDbContext>();
            builder.UseSqlite(Path.GetFullPath(databasePath));
            var context = new KMLoggerDbContext(builder.Options);
            return context;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}
