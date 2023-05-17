using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Data;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.RestaurantService
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext dbContext;

        public RestaurantService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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

        public IQueryable<Restaurant> GetAll()
        {
            return dbContext.Restaurants.AsNoTracking()
                .Include(r => r.Reviews)
                .Include(r => r.Tables)
                    .ThenInclude(t => t.TableClaims);
        }

        public void Patch(Restaurant newRestaurantModel)
        {
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
            return dbContext.Restaurants.AsNoTracking().Include(r => r.FavoritedBy).Where(r => r.FavoritedBy.Any(u => u.Id ==  userId)).ToList();
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
