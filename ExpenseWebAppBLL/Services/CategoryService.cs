using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppBLL.Mappers;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }
        public async Task<IEnumerable<ICategoryTransferObject>?> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            foreach (var c in categories)
            {
                categoryDTOs.Add(CategoryMapper.ToDTO(c));
            }

            return categoryDTOs;
        }
        public async Task<ICategoryTransferObject?> GetCategoryByIdAsync(int id)
        {
            if (id < 0) return null;

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return null;

            CategoryDTO categoryDTO = CategoryMapper.ToDTO(category);

            return categoryDTO;
        }

        public async Task<bool> AddCategoryAsync(ICategoryTransferObject categoryDTO)
        {
            if (categoryDTO.Id < 0 || string.IsNullOrEmpty(categoryDTO.Name))
                return false;

            Category category = CategoryMapper.ToEntity(categoryDTO);

            await _categoryRepository.AddAsync(category);

            return true;
        }

        public async Task<bool> UpdateCategoryAsync(ICategoryTransferObject categoryDTO)
        {
            if (categoryDTO.Id < 0 || string.IsNullOrEmpty(categoryDTO.Name))
                return false;

            Category category = CategoryMapper.ToEntity(categoryDTO);

            await _categoryRepository.UpdateAsync(category);

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id < 0) return false;

            await _categoryRepository.DeleteAsync(id);

            return true;
        }
    }
}
