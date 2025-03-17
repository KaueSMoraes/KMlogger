using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Nest;

namespace Infrastructure.Repositories;

public class LogRepository : ElasticBaseRepository<Log>, ILogRepository
{
    public LogRepository(ElasticClient client) : base(client, "logs") { }
}