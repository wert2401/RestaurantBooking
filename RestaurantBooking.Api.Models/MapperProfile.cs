using AutoMapper;
using RestaurantBooking.Api.Models.Restaurant;
using RestaurantBooking.Api.Models.Review;
using RestaurantBooking.Api.Models.Table;
using RestaurantBooking.Api.Models.User;
using RestaurantBooking.Application.Services.Authentication.Models;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Api.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Data.Entities.Restaurant, RestaurantModel>();
            CreateMap<Data.Entities.Restaurant, RestaurantModelDetailed>();
            CreateMap<RestaurantModelEdit, Data.Entities.Restaurant>();
            CreateMap<RestaurantModelCreate, Data.Entities.Restaurant>();

            CreateMap<Data.Entities.Table, TableModel>()
                .ForMember(d => d.Restaurant, opt => opt.MapFrom(s => s.Restaurant.Name))
                .ReverseMap()
                .ForMember(d => d.Restaurant, opt => opt.Ignore());

            CreateMap<TableModelCreate, Data.Entities.Table>();

            CreateMap<TableClaim, TableClaimModel>()
                .ReverseMap();

            CreateMap<Data.Entities.User, UserModel>().ReverseMap();

            CreateMap<RegisterModel, UserModel>();

            CreateMap<Role, RoleModel>().ReverseMap();

            CreateMap<ReviewModelCreate, Data.Entities.Review>();
        }
    }
}
