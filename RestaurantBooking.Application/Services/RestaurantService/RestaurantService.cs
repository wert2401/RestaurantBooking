using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.RestaurantService
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;

        public RestaurantService(ApplicationDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public Restaurant GetById(int id)
        {
            var rest = dbContext.Restaurants.AsNoTracking()
                .Include(r => r.Reviews)
                .Include(r => r.Tables)
                    .ThenInclude(t => t.TableClaims)
                .FirstOrDefault(r => r.Id == id);

            if (rest == null)
                throw new Exception("Restaurant is not found");

            return rest;
        }

        public Restaurant GetByOwnerEmail(string email)
        {
            var owner = userService.GetByEmail(email);

            if (owner == null)
                throw new Exception("Owner was not found");

            var rest = dbContext.Restaurants.AsNoTracking()
                .Include(r => r.Reviews)
                .Include(r => r.Tables)
                    .ThenInclude(t => t.TableClaims)
                .FirstOrDefault(r => r.OwnerUserId == owner.Id);

            if (rest == null)
                throw new Exception("Restaurant was not found");

            return rest;
        }

        public IQueryable<Restaurant> GetAll()
        {
            return dbContext.Restaurants.AsNoTracking()
                .Include(r => r.Reviews)
                .Include(r => r.Tables)
                    .ThenInclude(t => t.TableClaims);
        }

        public void Patch(Restaurant oldRestaurant, Restaurant newRestaurantModel)
        {
            newRestaurantModel.Id = oldRestaurant.Id;
            newRestaurantModel.OwnerUserId = oldRestaurant.OwnerUserId;

            dbContext.Restaurants.Update(newRestaurantModel);

            dbContext.SaveChanges();
        }

        public void Add(Restaurant newModel)
        {
            dbContext.Restaurants.Add(newModel);

            dbContext.SaveChanges();
        }

        public void Rate(Review review)
        {
            var rest = dbContext.Restaurants.Find(review.RestaurantId);

            if (rest == null)
                throw new Exception("Restaurant is not found");

            rest.Reviews.Add(review);

            dbContext.SaveChanges();
        }

        public ICollection<Restaurant> GetFavoritesbyUserId(int userId)
        {
            return dbContext.Restaurants.AsNoTracking()
                .Include(r => r.FavoritedBy)
                .Include(r => r.Reviews)
                .Include(r => r.Tables)
                    .ThenInclude(t => t.TableClaims)
                .Where(r => r.FavoritedBy.Any(u => u.Id ==  userId)).ToList();
        }

        public void AddToFavorites(int userId, int restaurantId)
        {
            var rest = dbContext.Restaurants.Find(restaurantId);

            if (rest == null)
                throw new Exception("Restaurant is not found");

            rest.FavoritedBy.Add(dbContext.Users.Find(userId)!);
            
            dbContext.SaveChanges();
        }

        public void RemoveFromFavorites(int userId, int restaurantId)
        {
            var rest = dbContext.Restaurants.Find(restaurantId);

            if (rest == null)
                throw new Exception("Restaurant is not found");

            rest.FavoritedBy.Remove(dbContext.Users.Find(userId)!);

            dbContext.SaveChanges();
        }

        
    }
}
