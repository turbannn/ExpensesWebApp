using ExpenseWebAppBLL.DTOs.CategoryDTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppDAL.Entities;

namespace ExpenseWebAppBLL.Mappers
{
    internal class CategoryMapper : IMapper<ICategoryTransferObject, Category>
    {
        public ICategoryTransferObject ToReadDTO(Category category)
        {
            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public Category ToEntity(ICategoryTransferObject categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };
        }
    }
}
