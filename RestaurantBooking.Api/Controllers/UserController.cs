using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantBooking.Api.Models.Restaurant;
using RestaurantBooking.Api.Models.User;
using RestaurantBooking.Application.Services.RestaurantService;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IRestaurantService restaurantService, IMapper mapper)
        {
            this.userService = userService;
            this.restaurantService = restaurantService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserModel> Get()
        {
            var email = User.Identity?.Name;

            if (email == null)
                return Unauthorized();

            var userModel = mapper.Map<UserModel>(userService.GetByEmail(email));

            return Ok(userModel);
        }

        [HttpPatch]
        [Authorize]
        public ActionResult<UserModel> Patch(UserEditModel model)
        {
            model.Email = User.Identity!.Name!;
            userService.Patch(mapper.Map<User>(model));
            return Ok(model);
        }

        [HttpGet("Favorites")]
        [Authorize]
        public ActionResult<ICollection<RestaurantModel>> GetFavorites()
        {
            var userId = userService.GetByEmail(User.Identity!.Name!).Id;
            return Ok(mapper.Map<ICollection<RestaurantModel>>(restaurantService.GetFavoritesbyUserId(userId)));
        }

        [HttpPost("AddToFavorites")]
        [Authorize]
        public IActionResult AddToFavorites(int restaurantId)
        {
            var user = userService.GetByEmail(User.Identity!.Name!);
            restaurantService.AddToFavorites(user.Id, restaurantId);
            return Ok();
        }

        [HttpPost("RemoveFromFavorites")]
        [Authorize]
        public IActionResult RemoveFromFavorites(int restaurantId)
        {
            var user = userService.GetByEmail(User.Identity!.Name!);
            restaurantService.RemoveFromFavorites(user.Id, restaurantId);
            return Ok();
        }
    }
}
