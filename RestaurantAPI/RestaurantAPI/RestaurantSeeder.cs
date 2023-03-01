using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast food",
                    Description = "Just KFC",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "12312312",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Fillet Tower Burger",
                            Price = 6.99M  ,
                            Description = "Сhicken Fillet Tower Burger – the original fillet burger is simple and palatable. However, there’s always room for improvement, so we decided to make it even better. Three more ingredients turned this item into a new dish that was no less tasty."
                        },
                        new Dish()
                        {
                            Name = "Zinger Tower Burger",
                            Price = 6.99M,
                            Description = "Zinger Tower Burger – everyone loves Zinger, and everyone loves freshly-cooked hashbrowns and melted cheese. With that knowledge, a new burger was made. "
                        },
                        new Dish()
                        {
                            Name = "Vegan Burger",
                            Price = 5.99M  ,
                            Description = "Vegetarian Burger – now even those who don’t eat meat and animal products can enjoy a proper meal at KFC! The cultured meat used in this burger is nearly impossible to distinguish from a real chicken fillet!"
                        },
                    },
                    Address = new Address()
                    {
                        City = "Rzeszów",
                        Street = "Nowa 52",
                        PostalCode = "31-555"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald's",
                    Category = "Fast food",
                    Description = "Just McDonald's",
                    ContactEmail = "contact@mcdonald.com",
                    ContactNumber = "111223231",
                    HasDelivery = false,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "McCrispy",
                            Price = 6.30M  ,
                            Description = "100% chicken breast fillet in a crispy, crunchy coating. Served with iceberg lettuce, black pepper mayo and a delicious sourdough-style sesame topped bun."
                        },
                        new Dish()
                        {
                            Name = "Cheesy Garlic Bites",
                            Price = 3.30M  ,
                            Description = "A portion of 5 bites made with a blend of mozzarella and Emmental cheese, in a garlic herb coating. Served with a rich tomato dip. Nutrition and allergen information do not include dips."
                        },
                        new Dish()
                        {
                            Name = "Double McPlant",
                            Price = 10.30M  ,
                            Description = "A vegan burger made with two juicy plant-based patties co-developed with Beyond Meat® featuring vegan sandwich sauce, ketchup, mustard, onion, pickles, lettuce, tomato, and a vegan alternative to cheese in a sesame seed bun. Vegan certified"
                        },
                    },
                    Address = new Address()
                    {
                        City = "Krosno",
                        Street = "Lwowska 56",
                        PostalCode = "38-400"
                    }
                },
            };
            return restaurants;
        }
    }
}
