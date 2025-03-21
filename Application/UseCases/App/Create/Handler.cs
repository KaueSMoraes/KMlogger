using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Records;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.App.Create;

public class Handler : IRequestHandler<Request, BaseResponse>
{
    private readonly IAppRepository _appRepository;
    private readonly ICategoryRepository _categoryRepository;

    public Handler(IAppRepository appRepository, ICategoryRepository categoryRepository)
    {
        _appRepository = appRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<BaseResponse> Handle(Request request, CancellationToken cancellationToken)
    {
        if(await _appRepository.GetByName(request.name) is null) return new BaseResponse(400,"App already exists");
        var category = await _categoryRepository.GetByIdAsync(request.category, cancellationToken);

        if(category is null) return new BaseResponse(400,"Category not found");

        await _appRepository.SaveAsync(new Domain.Entities.App(
            new UniqueName(request.name),
            category,
            request.environment,
            null, true), cancellationToken);
            
        return new BaseResponse(200,"App created successfully");
    }
}
