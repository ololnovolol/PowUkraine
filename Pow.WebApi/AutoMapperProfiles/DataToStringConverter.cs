using System;
using AutoMapper;

namespace Pow.WebApi.AutoMapperProfiles
{
    public class DataToStringConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return System.Convert.ToString(source.ToString("dddd, dd MMMM yyyy"));
        }
    }
}
