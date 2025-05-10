using AutoMapper;
using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppBLL.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>()
                .ForMember(dest => dest.TotalExpensesCount, opt =>
                {
                    opt.MapFrom(src => src.EntityMetadata.UserExpensesTotalCount);
                });

            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();

            CreateMap<UserCreateDTO, UserReadDTO>();
        }
    }
}
