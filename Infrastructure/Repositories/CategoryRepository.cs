using System;
using Domain.Entities;
using Domain.Interfaces;
using Nest;

namespace Infrastructure.Repositories;

public class CategoryRepository(ElasticClient client)
    : ClickHouseClient<Category>(client, "categories"), ICategoryRepository;