using System;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Nest;

namespace Infrastructure.Repositories;

public class CategoryRepository
    : ClickHouseBaseRepository<Category>,
     ICategoryRepository;