using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //No copy of Id. Mind using tracked update method
        internal static Category ToEntity(ICategoryTransferObject categoryDTO)
        {
            return new Category
            {
                Name = categoryDTO.Name
            };
        }
    }
}
