using System;
using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ILogRepository : IElasticBaseRepository<Log>
{
}