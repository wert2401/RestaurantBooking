using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RestaurantBooking.Api.Models.Restaurant;
using RestaurantBooking.Api.Models.Review;
using RestaurantBooking.Application.Services.ImagesService;
using RestaurantBooking.Application.Services.RestaurantService;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService restaurantService;
        private readonly IUserService userService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public RestaurantsController(IRestaurantService restaurantService, IUserService userService, IImageService imageService, IMapper mapper)
        {
            this.restaurantService = restaurantService;
            this.userService = userService;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<RestaurantModel> Get()
        {
            return restaurantService.GetAll().ToList().AsQueryable().ProjectTo<RestaurantModel>(mapper.ConfigurationProvider);
        }

        [HttpGet("Details")]
        public ActionResult<RestaurantModelDetailed> Get([FromBody(EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Disallow)] int id)
        {
            return Ok(mapper.Map<RestaurantModelDetailed>(restaurantService.GetById(id)));
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public IActionResult PatchInfo(RestaurantModelEdit editedRestaurant)
        {
            var rest = restaurantService.GetByOwnerEmail(User.Identity!.Name!);

            if (rest == null)
                return Unauthorized();

            restaurantService.Patch(rest, mapper.Map<Restaurant>(editedRestaurant));
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromForm] RestaurantModelCreate model)
        {
            var user = userService.GetByEmail(User.Identity!.Name!);

            if (userService.HasUserRestaurant(user.Id))
                return BadRequest("User already has restaurant");

            Restaurant newRest = mapper.Map<Restaurant>(model);

            if (model.MainPhoto != null)
            {
                string pathToImage = imageService.SaveImage(model.MainPhoto);
                newRest.MainPhoto = pathToImage;
            }

            newRest.OwnerUserId = user.Id;

            restaurantService.Add(newRest);
            return Ok();
        }

        [HttpPost("ChangeImage")]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeImage(IFormFile image)
        {
            var rest = restaurantService.GetByOwnerEmail(User.Identity!.Name!);

            if (rest == null)
                return Unauthorized();

            string pathToImage = imageService.SaveImage(image);

            rest.MainPhoto = pathToImage;

            restaurantService.Patch(rest, rest);

            return Ok(new { NewImgUrl = pathToImage });
        }

        [HttpPost("Grade")]
        [Authorize]
        public IActionResult Grade(ReviewModelCreate modelCreate)
        {
            var review = mapper.Map<Review>(modelCreate);

            review.UserId = userService.GetByEmail(User.Identity!.Name!).Id;

            restaurantService.Rate(review);

            return Ok();
        }
    }
}
