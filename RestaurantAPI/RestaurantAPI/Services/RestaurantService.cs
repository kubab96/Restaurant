using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, 
            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<PageResult<RestaurantDTO>> GetAll(RestaurantQuery query)
        {
            var baseQuery = _dbContext.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .Where(x => query.SearchPhrase == null ||
                (x.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || x.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), x => x.Name },
                    { nameof(Restaurant.Category), x => x.Category },
                    { nameof(Restaurant.Address.City), x => x.Address.City },
                };

                var selectedColumns = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumns) : baseQuery.OrderByDescending(selectedColumns);
            }

            var restaurants = await baseQuery
                .Skip(query.PageSize * (query.PageNumber -1))
                .Take(query.PageSize)
                .ToListAsync();

            var totalItems = baseQuery.Count();

            var restaurantsDTO = _mapper.Map<List<RestaurantDTO>>(restaurants);

            var result = new PageResult<RestaurantDTO>(restaurantsDTO, totalItems, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<RestaurantDTO> GetById(int id)
        {
            var restaurant = await _dbContext.Restaurants.Include(x => x.Address).Include(x => x.Dishes).FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDTO;
        }

        public async Task<Restaurant> Create(RestaurantCreateDTO restaurantCreateDTO)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantCreateDTO);
            restaurant.CreatedById = _userContextService.GetUserId;
            await _dbContext.Restaurants.AddAsync(restaurant);
            await _dbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task Delete(int id)
        {
            _logger.LogWarning($"Delete action invoke for restaurant id = {id}");

            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidenException("Delete method is allowed only for creator");
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public async Task Update(int id, RestaurantUpdateDTO restaurantUpdateDTO)
        {
            _logger.LogWarning($"Edit action invoke for restaurant id = {id}");

            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
            var address = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Restaurant.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            if (address is null)
            {
                throw new NotFoundException("Restaurant address not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidenException("Update method is allowed only for creator");
            }

            restaurant.Name = restaurantUpdateDTO.Name;
            restaurant.Description = restaurantUpdateDTO.Description;
            restaurant.Category = restaurantUpdateDTO.Category;
            restaurant.HasDelivery = restaurantUpdateDTO.HasDelivery;
            restaurant.ContactEmail = restaurantUpdateDTO.ContactEmail;
            restaurant.ContactNumber = restaurantUpdateDTO.ContactNumber;
            address.City = restaurantUpdateDTO.City;
            address.Street = restaurantUpdateDTO.Street;
            address.PostalCode = restaurantUpdateDTO.PostalCode;

            _dbContext.SaveChanges();
        }
    }
}
