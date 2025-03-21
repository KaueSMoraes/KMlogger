using System;
using Domain.Interfaces;
using Domain.Records;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Category.Create;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ICategoryRepository   _categoryRepository;
    public Handler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        if(await _categoryRepository.GetByNameAsync(request.name, cancellationToken) is null) 
            return new BaseResponse(400, "Category already exists", null, null);

        await _categoryRepository.SaveAsync(new Domain.Entities.Category
            (new UniqueName(request.name), true), cancellationToken);

        return new BaseResponse(200, "Category created successfully", null, null);
    }
}