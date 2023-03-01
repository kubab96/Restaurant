using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll([FromQuery] RestaurantQuery query)
        {
            var restaurants = await _restaurantService.GetAll(query);

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant([FromRoute] int id)
        {
            var restaurant = await _restaurantService.GetById(id);

            return Ok(restaurant);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<ActionResult> CreateRestaurant([FromBody] RestaurantCreateDTO restaurantDTO)
        {
            var restaurant = await _restaurantService.Create(restaurantDTO);

            return Created($"api/restaurant/{restaurant.Id}", null);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] RestaurantUpdateDTO restaurantDTO)
        {
            await _restaurantService.Update(id, restaurantDTO);

            return Ok();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await _restaurantService.Delete(id);

            return NoContent();
        }
    }
}
