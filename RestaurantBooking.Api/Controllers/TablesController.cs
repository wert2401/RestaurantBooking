using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RestaurantBooking.Api.Models.Table;
using RestaurantBooking.Application.Services.RestaurantService;
using RestaurantBooking.Application.Services.TableService;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService tableService;
        private readonly IUserService userService;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper mapper;

        public TablesController(ITableService tableService, IUserService userService, IRestaurantService restaurantService, IMapper mapper)
        {
            this.tableService = tableService;
            this.userService = userService;
            this.restaurantService = restaurantService;
            this.mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<TableModel> GetTables()
        {
            return tableService.GetAll().ProjectTo<TableModel>(mapper.ConfigurationProvider);
        }

        [HttpGet("UserClaims")]
        [Authorize(Roles = "Member")]
        public IQueryable<TableClaimModel> GetClaimsByUser()
        {
            var user = userService.GetByEmail(User.Identity!.Name!);
            return tableService.GetAllClaims().Where(x => x.UserId == user.Id).ProjectTo<TableClaimModel>(mapper.ConfigurationProvider);
        }

        [HttpGet("RestaurantClaims")]
        [Authorize(Roles = "Admin")]
        public IQueryable<TableClaimModel> GetClaimsByRestaurant()
        {
            var restautant = restaurantService.GetByOwnerEmail(User.Identity!.Name!);
            return restautant.Tables.Select(t => t.TableClaims).AsQueryable().ProjectTo<TableClaimModel>(mapper.ConfigurationProvider);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(TableModelCreate modelCreate)
        {
            var restaurant = restaurantService.GetByOwnerEmail(User?.Identity?.Name!);

            if (restaurant == null)
                return Unauthorized();

            tableService.Add(restaurant.Id, mapper.Map<Table>(modelCreate));
            return Ok();
        }

        [HttpPost("Claim")]
        [Authorize]
        public IActionResult ClaimTable(int id, DateTime dateTime)
        {
            tableService.ClaimTable(id, User.Identity!.Name!, dateTime);
            return Ok();
        }
    }
}
