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
            return dbContext.Restaurants;
        }

        public void Patch(Restaurant oldRestaurant, Restaurant newRestaurantModel)
        {
            newRestaurantModel.Id = oldRestaurant.Id;
            newRestaurantModel.OwnerUserId = oldRestaurant.OwnerUserId;

            dbContext.Attach(newRestaurantModel);
            dbContext.Entry(newRestaurantModel).Collection(r => r.Tables).Load();

            if (newRestaurantModel.TablesCount < newRestaurantModel.Tables.Count)
            {
                newRestaurantModel.Tables.Where(t => t.TableNumber > newRestaurantModel.TablesCount).ToList().ForEach(t => newRestaurantModel.Tables.Remove(t));
            }

            if (newRestaurantModel.TablesCount > newRestaurantModel.Tables.Count)
            {
                for (int i = newRestaurantModel.Tables.Count; i < newRestaurantModel.TablesCount; i++)
                {
                    newRestaurantModel.Tables.Add(new Table { TableNumber = i + 1 });
                }
            }

            dbContext.Restaurants.Update(newRestaurantModel);

            dbContext.SaveChanges();
        }

        public void Add(Restaurant newModel)
        {
            newModel.Tables = new List<Table>();

            for (int i = 0; i < newModel.TablesCount; i++)
            {
                newModel.Tables.Add(new Table { TableNumber = i + 1 });
            }

            dbContext.Restaurants.Add(newModel);

            dbContext.SaveChanges();
        }

        public void Rate(Review review)
        {
            var rest = dbContext.Restaurants.Find(review.RestaurantId);

            if (rest == null)
                throw new Exception("Restaurant was not found");

            rest.Reviews.Add(review);

            dbContext.SaveChanges();
        }
    }
}
