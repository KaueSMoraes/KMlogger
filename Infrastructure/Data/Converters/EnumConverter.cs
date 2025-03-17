using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Converters;

public  class EnumConverter : ValueConverter<Enum, string>
{
    public EnumConverter() 
        : base(
            v => v.ToString(),  
            v => (Enum)Enum.Parse(typeof(Enum), v))
    {
    }
}