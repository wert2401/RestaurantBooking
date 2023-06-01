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
