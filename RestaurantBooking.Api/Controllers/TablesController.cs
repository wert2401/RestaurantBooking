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
        public IQueryable<TableModel> Get()
        {
            return tableService.GetAll().ProjectTo<TableModel>(mapper.ConfigurationProvider);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(TableModelCreate modelCreate)
        {
            if (!IsOwner(modelCreate.RestaurantId))
                return Unauthorized();

            tableService.Add(mapper.Map<Table>(modelCreate));
            return Ok();
        }

        [HttpPost("Claim")]
        [Authorize]
        public IActionResult ClaimTable(int id, DateTime dateTime)
        {
            tableService.ClaimTable(id, User.Identity!.Name!, dateTime);
            return Ok();
        }

        private bool IsOwner(int restId)
        {
            var onwer = userService.GetByEmail(User.Identity!.Name!);

            var restaurant = restaurantService.GetById(restId);

            if (restaurant.OwnerUserId != onwer.Id)
                return false;
            else
                return true;
        }
    }
}
