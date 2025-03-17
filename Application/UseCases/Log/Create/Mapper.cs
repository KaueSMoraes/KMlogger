using System;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Log = Domain.Entities.LogEnrty;

namespace Application.UseCases.Log.Create
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Request, LogEnrty>()
                .ConstructUsing(request => new LogEnrty(
                    request.Environment,
                    ParseLevel(request.Level), 
                    new Description(request.Message),
                    new Description(request.StackTrace)
                ));
        }

        private static Level ParseLevel(string level)
        {
            return Enum.TryParse<Level>(level, true, out var parsedLevel) 
                ? parsedLevel 
                : Level.Information;
        }
    }
}
