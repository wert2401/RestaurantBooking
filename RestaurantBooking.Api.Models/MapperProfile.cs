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
                .ForMember(r => r.VacantTablesCount, opt => opt.MapFrom(s => s.Tables.Where(t => t.VacantFrom < DateTime.UtcNow.AddDays(1)).Count()))
                .ForMember(r => r.Rating, opt => opt.MapFrom(s => s.Reviews.Count() > 0 ? s.Reviews.Average(r => r.Grade) : 0))
                .ForMember(r => r.SchemeImage, opt => opt.MapFrom(s => s.SchemeImage == null ? null : serverUri + s.SchemeImage))
                .ForMember(r => r.MenuPath, opt => opt.MapFrom(s => s.MenuPath == null ? null : serverUri + s.MenuPath));

            CreateMap<Data.Entities.Restaurant, RestaurantModelDetailed>()
                .ForMember(r => r.VacantTablesCount, opt => opt.MapFrom(s => s.Tables.Where(t => t.VacantFrom < DateTime.UtcNow.AddDays(1)).Count()))
                .ForMember(r => r.Rating, opt => opt.MapFrom(s => s.Reviews.Count() > 0 ? s.Reviews.Average(r => r.Grade) : 0))
                .AfterMap((s, d, context) =>
                {
                    if (context.Items.TryGetValue("serverUri", out object? uri))
                    {
                        if (d.SchemeImage is not null)
                            d.SchemeImage = (string)uri + d.SchemeImage;
                        if (d.MenuPath is not null)
                            d.MenuPath = (string)uri + d.MenuPath;
                    }
                });

            CreateMap<RestaurantModelEdit, Data.Entities.Restaurant>();
            CreateMap<RestaurantModelCreate, Data.Entities.Restaurant>();

            CreateMap<Data.Entities.Table, TableModel>()
                .ForMember(d => d.Restaurant, opt => opt.MapFrom(s => s.Restaurant.Name));

            CreateMap<TableModelCreate, Data.Entities.Table>();

            CreateMap<TableClaim, TableClaimModel>()
                .ForMember(d => d.TableNumber, opt => opt.MapFrom(s => s.Table.TableNumber))
                .ForMember(d => d.Restaurant, opt => opt.MapFrom(s => s.Table.Restaurant.Name))
                .ForMember(d => d.RestaurantId, opt => opt.MapFrom(s => s.Table.RestaurantId))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.Name))
                .ForMember(d => d.UserPhoneNumber, opt => opt.MapFrom(s => s.User.Phone))
                .ForMember(d => d.RestaurantPhoneNumber, opt => opt.MapFrom(s => s.Table.Restaurant.PhoneNumber))
                .ForMember(d => d.IsExpired, opt => opt.MapFrom(s => s.ClaimToDate < DateTime.UtcNow));

            CreateMap<TableClaimRequestModel, TableClaim>();

            CreateMap<Data.Entities.User, UserModel>().ReverseMap();

            CreateMap<UserEditModel, Data.Entities.User>();

            CreateMap<RegisterModel, UserModel>();

            CreateMap<Role, RoleModel>().ReverseMap();

            CreateMap<ReviewModelCreate, Data.Entities.Review>();
        }
    }
}
