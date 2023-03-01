using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Runtime.CompilerServices;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DishService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public DishService(RestaurantDbContext dbContext, IMapper mapper, ILogger<DishService> logger, IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<DishDTO>> GetAll(int restaurantId)
        {
            await ChceckIfRestaurantExists(restaurantId);

            var dishes = await GetDishes(restaurantId);

            var dishesDTO = _mapper.Map<List<DishDTO>>(dishes);

            return dishesDTO;
        }

        public async Task<DishDTO> GetById(int restaurantId, int dishId)
        {
            await ChceckIfRestaurantExists(restaurantId);

            var dish = await GetDish(dishId, restaurantId);

            var dishDTO = _mapper.Map<DishDTO>(dish);

            return dishDTO;
        }

        public async Task<Dish> CreateDish(int restaurantId, DishUpsertDTO dishUpsertDTO)
        {
            await ChceckIfRestaurantExists(restaurantId);

            var dish = _mapper.Map<Dish>(dishUpsertDTO);

            dish.RestaurantId = restaurantId;

            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidenException("Create method is allowed only for creator");
            }

            await _dbContext.AddAsync(dish);
            await _dbContext.SaveChangesAsync();

            return dish;
        }

        public async Task Update(int restaurantId, int dishId, DishUpsertDTO dishUpsertDTO)
        {
            _logger.LogWarning($"Edit action invoke for dish id = {dishId}");

            await ChceckIfRestaurantExists(restaurantId);

            var dish = await GetDish(dishId, restaurantId);

            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidenException("Update method is allowed only for creator");
            }

            dish.Name = dishUpsertDTO.Name;
            dish.Price = dishUpsertDTO.Price;

            if(dishUpsertDTO.RestaurantId != 0)
            {
                dish.RestaurantId = dishUpsertDTO.RestaurantId;
            }
            
            _dbContext.SaveChanges();
        }

        public async Task DeleteAll(int restaurantId)
        {
            _logger.LogWarning($"Delete all dishes action invoke for restaurant id = {restaurantId}");

            await ChceckIfRestaurantExists(restaurantId);

            var dishes = await GetDishes(restaurantId);

            _dbContext.RemoveRange(dishes);
            _dbContext.SaveChanges();
        }

        public async Task Delete(int restaurantId, int dishId)
        {
            _logger.LogWarning($"Delete dish id = {dishId} action invoke for restaurant id = {restaurantId}");

            await ChceckIfRestaurantExists(restaurantId);

            var dish = await GetDish(dishId, restaurantId);

            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidenException("Delete method is allowed only for creator");
            }

            _dbContext.Remove(dish);
            _dbContext.SaveChanges();
        }

        private async Task ChceckIfRestaurantExists(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);

            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant not found");
            }
        }

        private async Task<Dish> GetDish(int dishId, int restaurantId)
        {
            var dish = await _dbContext.Dishes.Where(x => x.RestaurantId == restaurantId).FirstOrDefaultAsync(x => x.Id == dishId);

            if (dish == null)
            {
                throw new NotFoundException("Dish not found");
            }

            return await Task.FromResult(dish);
        }

        private async Task<IEnumerable<Dish>> GetDishes(int restaurantId)
        {
            var dishes = await _dbContext.Dishes.Where(x => x.RestaurantId == restaurantId).ToListAsync();

            if (dishes.Count == 0)
            {
                throw new NotFoundException("This restaurant do not contain any dishes");
            }

            return await Task.FromResult(dishes);
        }
    }
}
