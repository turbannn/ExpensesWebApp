using AutoMapper;
using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppBLL.MapProfiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile() //From -> To
        {
            CreateMap<Expense, ExpenseReadDTO>()
                .ForMember(dest => dest.Categories,
                    opt =>
                    {
                        opt.MapFrom(src => src.CategoriesList != null && src.CategoriesList.Count > 0 
                            ? string.Join("; ", src.CategoriesList) 
                            : "No categories");
                    });

            CreateMap<ExpenseCreateDTO, Expense>();
            CreateMap<ExpenseUpdateDTO, Expense>();
        }
    }
}
