using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Data;

namespace RestaurantAPI.Controllers
{
    [Route("/api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAll([FromRoute] int restaurantId)
        {
            var dishes = await _dishService.GetAll(restaurantId);

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDTO>> GetById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await _dishService.GetById(restaurantId, dishId);

            return Ok(dish);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<ActionResult> CreateDish([FromRoute] int restaurantId, DishUpsertDTO dishUpsertDTO)
        {
            var dish = await _dishService.CreateDish(restaurantId, dishUpsertDTO);

            return Created($"api/restaurant/{dish.RestaurantId}/dish/{dish.Id}", null);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{dishId}")]
        public async Task<ActionResult> UpdateDish([FromRoute] int restaurantId, [FromRoute] int dishId, DishUpsertDTO dishUpsertDTO)
        {
            await _dishService.Update(restaurantId, dishId, dishUpsertDTO);

            return Ok(); 
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAllDishes([FromRoute] int restaurantId)
        {
            await _dishService.DeleteAll(restaurantId);

            return NoContent();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{dishId}")]
        public async Task<ActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await _dishService.Delete(restaurantId, dishId);

            return NoContent();
        }
    }
}
