using AutoMapper;
using ExpenseWebAppBLL.DTOs.CategoryDTOs;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppBLL.MapProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            
            CreateMap<CategoryDTO, Category>();
        }
    }
}
