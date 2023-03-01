using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        Task<PageResult<RestaurantDTO>> GetAll(RestaurantQuery query);
        Task<RestaurantDTO> GetById(int id);
        Task<Restaurant> Create(RestaurantCreateDTO restaurantCreateDTO);
        Task Update(int id, RestaurantUpdateDTO restaurantUpdateDTO);
        Task Delete(int id);
    }
}