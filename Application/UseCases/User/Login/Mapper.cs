using System;
using AutoMapper;
using Domain.ValueObjects;

namespace Application.UseCases.User.Login;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Request, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password(src.password, true)))
            .ConstructUsing(request => new Domain.Entities.User(
                new Email(request.email),
                new Password(request.password, true) 
            ));
    }
}