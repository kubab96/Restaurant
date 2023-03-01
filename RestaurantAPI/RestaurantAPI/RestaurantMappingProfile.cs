using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>().ForMember(x => x.City, c => c.MapFrom(s => s.Address.City))
            .ForMember(x => x.Street, c => c.MapFrom(s => s.Address.Street))
            .ForMember(x => x.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<RestaurantCreateDTO, Restaurant>().ForMember(x => x.Address, c => c.MapFrom(x => new Address()
            {
                City = x.City,
                Street = x.Street,
                PostalCode = x.PostalCode
            }));

            CreateMap<Dish, DishDTO>();
            CreateMap<DishUpsertDTO, Dish>();
            CreateMap<RegisterUserDTO, User>();
        }
    }
}
