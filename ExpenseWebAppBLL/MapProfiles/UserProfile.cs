using AutoMapper;
using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppBLL.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>();

            CreateMap<UserReadDTO, User>();
            CreateMap<UserCreateDTO, User>();

            CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserUpdateDTO>();

            CreateMap<UserCreateDTO, UserReadDTO>();
        }
    }
}
