using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Data;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.UserService
{
    internal class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(User newModel)
        {
            dbContext.Users.Add(newModel);

            dbContext.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            var user = getByEmail(email);
            if (user == null)
                throw new Exception("User was not found");
            return user;
        }

        public bool IsExist(string email)
        {
            return dbContext.Users.Any(u => u.Email == email);
        }

        public void Patch(User editModel)
        {
            User? user = getByEmail(editModel.Email);

            if (user == null)
                throw new InvalidOperationException("Patch user error: user does not exist");

            user.Phone = editModel.Phone;
            user.Name = editModel.Name;

            user.Roles = new List<Role>();

            dbContext.Users.Update(user);
            dbContext.SaveChanges();
        }

        public void ChangePassword(string email, string password)
        {
            User? user = getByEmail(email);

            if (user == null)
                throw new InvalidOperationException("Change password error: user does not exist");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            user.Roles = new List<Role>();

            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public ICollection<Restaurant> GetFavoritesbyUserId(int userId)
        {
            return dbContext.Users.Where(u => u.Id == userId).AsNoTracking()
                .Include(r => r.FavoriteRestaurants)
                    .ThenInclude(r => r.Tables)
                        .ThenInclude(t => t.TableClaims)
                .Include(r => r.FavoriteRestaurants)
                    .ThenInclude(r => r.Reviews)
                .Select(u => u.FavoriteRestaurants).First().ToList();
        }

        public void AddToFavorites(int userId, int restaurantId)
        {
            var rest = dbContext.Restaurants.Find(restaurantId);

            if (rest == null)
                throw new Exception("Restaurant was not found");

            var user = dbContext.Users.Include(u => u.FavoriteRestaurants).First(u => u.Id == userId);

            if (user == null)
                throw new Exception("user was not found");

            if (user.FavoriteRestaurants.Any(r => r.Id == restaurantId))
                throw new Exception("User has already favorited this restaurant");

            user.FavoriteRestaurants.Add(rest);

            dbContext.SaveChanges();
        }

        public void RemoveFromFavorites(int userId, int restaurantId)
        {
            var user = dbContext.Users.Include(u => u.FavoriteRestaurants).First(u => u.Id == userId);

            if (user == null)
                throw new Exception("user was not found");

            var rest = user.FavoriteRestaurants.Find(r => r.Id == restaurantId);

            if (rest == null)
                throw new Exception("Restaurant was not found");

            user.FavoriteRestaurants.Remove(rest);

            dbContext.SaveChanges();
        }

        public void SetRefreshToken(int id, string refreshToken, DateTime expiringTime)
        {
            User? user = dbContext.Users.Find(id);

            if (user == null)
                throw new InvalidOperationException("Refresh token error: user does not exist");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiring = expiringTime;

            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public void SetRefreshToken(string email, string refreshToken, DateTime expiringTime)
        {
            User? user = getByEmail(email);

            if (user == null)
                throw new InvalidOperationException("Refresh token error: user does not exist");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiring = expiringTime;

            dbContext.Update(user);
            dbContext.SaveChanges();
        }

        public bool HasUserRestaurant(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user == null)
                throw new Exception("User was not found");

            return dbContext.Restaurants.Any(r => r.OwnerUserId == user.Id);
        }

        private User? getByEmail(string email) => dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Email == email);

        
    }
}
