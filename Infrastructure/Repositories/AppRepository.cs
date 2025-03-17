using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Nest;

namespace Infrastructure.Repositories;

public class AppRepository : ElasticBaseRepository<App>, IAppRepository
{
    public AppRepository(ElasticClient client) : base(client, "apps") { }
}