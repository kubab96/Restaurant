using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        Task<IEnumerable<DishDTO>> GetAll(int restaurantId);
        Task<Dish> CreateDish(int restaurantId, DishUpsertDTO dishUpsertDTO);
        Task<DishDTO> GetById(int restaurantId, int id);
        Task Update(int restaurantId, int dishId, DishUpsertDTO dishUpsertDTO);
        Task DeleteAll(int restaurantId);
        Task Delete(int restaurantId, int dishId);
    }
}