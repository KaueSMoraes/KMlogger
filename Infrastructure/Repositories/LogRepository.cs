using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Nest;

namespace Infrastructure.Repositories;

public class LogRepository : ClickHouseBaseRepository<LogEnrty>, ILogRepository;