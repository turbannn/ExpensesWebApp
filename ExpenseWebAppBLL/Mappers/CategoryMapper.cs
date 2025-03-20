using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppBLL.Mappers
{
    internal static class CategoryMapper
    {
        internal static CategoryDTO ToDTO(Category category)
        {
            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        internal static Category ToEntity(ICategoryTransferObject categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };
        }
    }
}
