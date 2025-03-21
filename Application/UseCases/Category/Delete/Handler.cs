using System;
using Domain.Interfaces;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Category.Delete;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    public Handler(ICategoryRepository repository)
    {
        _categoryRepository = repository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.id, cancellationToken);
        if (category is null)
            return new BaseResponse(400, "Category not found");

        await _categoryRepository.DeleteAsync(category.Id, cancellationToken);
        return new BaseResponse(201, "Category deleted successfully");
    }
}
