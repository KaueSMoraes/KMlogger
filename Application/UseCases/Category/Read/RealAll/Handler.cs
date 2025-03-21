using System;
using Domain.Interfaces;
using Domain.Records;
using MediatR;

namespace Application.UseCases.Category.Read.RealAll;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    public Handler(ICategoryRepository repository)
    {
        _categoryRepository = repository;
    }

    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(request.Skip, request.Take, cancellationToken);
        return new BaseResponse(200, "Categories retrieved successfully", null, categories);
    }
}
