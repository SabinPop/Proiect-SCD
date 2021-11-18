using AutoMapper;
using LocationWebApp.Models;
using LocationWebApp.Models.ViewModels;
using System.Collections.Generic;

namespace LocationWebApp.AutoMapper
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationViewModel>().ReverseMap();
            CreateMap<IEnumerable<Location>, IEnumerable<LocationViewModel>>().ReverseMap();
        }
    }
}
