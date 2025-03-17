using System;
using Domain.Entities;
using Domain.Interfaces;
using Nest;

namespace Infrastructure.Repositories;

public class CategoryRepository : ElasticBaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ElasticClient client) 
        : base(client, "categories") { }
}
