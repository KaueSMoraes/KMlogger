using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Nest;

namespace Infrastructure.Repositories;

public class LogRepository(ElasticClient client) 
    : ClickHouseClient<LogEnrty>(client, "logs"), ILogRepository;