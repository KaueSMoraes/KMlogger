using System;
using AutoMapper;
using Domain.Records;

namespace Application.UseCases.Log.Read.ReadById;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Domain.Entities.LogEnrty, BaseResponse>()
            .ForSourceMember(src => src.Id, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.DeletedDate, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.UpdatedDate, opt => opt.DoNotValidate());
    }
}
