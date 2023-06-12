using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantBooking.Api.Models.User;
using RestaurantBooking.Application.Services.Authentication;
using RestaurantBooking.Application.Services.Authentication.Models;

namespace RestaurantBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMapper mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            this.authenticationService = authenticationService;
            this.mapper = mapper;
        }

        [HttpPost]
        public ActionResult<AuthenticatedModel> Login(LoginModel loginModel)
        {
            var authModel = authenticationService.AuthUser(loginModel);

            return Ok(authModel);
        }

        [HttpPost]
        public ActionResult<UserModel> Register(RegisterModel registerModel)
        {
            var regModel = authenticationService.RegisterUser(registerModel);

            return Ok(mapper.Map<UserModel>(regModel));
        }

        [HttpPost]
        public ActionResult<UserModel> RegisterAdmin(RegisterModel registerModel)
        {
            var regModel = authenticationService.RegisterUser(registerModel, true);

            return Ok(mapper.Map<UserModel>(regModel));
        }

        [HttpPost]
        public ActionResult<AuthenticatedModel> RefreshToken(TokenRefreshModel tokenRefreshModel)
        {
            var authModel = authenticationService.RefreshToken(tokenRefreshModel);

            return Ok(authModel);
        }
    }
}