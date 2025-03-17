using System;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces;

public interface ICategoryRepository : IElasticBaseRepository<Category>
{

}
