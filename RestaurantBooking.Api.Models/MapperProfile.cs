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
            string serverUri = "";
            CreateMap<Data.Entities.Restaurant, RestaurantModel>()
                .ForMember(r => r.SchemeImage, opt => opt.MapFrom(s => s.SchemeImage == null ? null : serverUri + s.SchemeImage))
                .ForMember(r => r.MenuPath, opt => opt.MapFrom(s => s.MenuPath == null ? null : serverUri + s.MenuPath));

            CreateMap<Data.Entities.Restaurant, RestaurantModelDetailed>().AfterMap((s, d, context) =>
            {
                if (context.Items.TryGetValue("serverUri", out object? uri))
                {
                    if (d.SchemeImage is not null)
                        d.SchemeImage = (string)uri + d.SchemeImage;
                    if (d.MenuPath is not null)
                        d.MenuPath = (string)uri + d.SchemeImage;
                }
            });

            CreateMap<RestaurantModelEdit, Data.Entities.Restaurant>();
            CreateMap<RestaurantModelCreate, Data.Entities.Restaurant>();

            CreateMap<Data.Entities.Table, TableModel>()
                .ForMember(d => d.Restaurant, opt => opt.MapFrom(s => s.Restaurant.Name))
            .ReverseMap()
                .ForMember(d => d.Restaurant, opt => opt.Ignore());

            CreateMap<TableModelCreate, Data.Entities.Table>();

            CreateMap<TableClaim, TableClaimModel>();

            CreateMap<TableClaimRequestModel, TableClaim>();

            CreateMap<Data.Entities.User, UserModel>().ReverseMap();

            CreateMap<UserEditModel, Data.Entities.User>();

            CreateMap<RegisterModel, UserModel>();

            CreateMap<Role, RoleModel>().ReverseMap();

            CreateMap<ReviewModelCreate, Data.Entities.Review>();
        }
    }
}
