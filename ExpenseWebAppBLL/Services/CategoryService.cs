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
        private readonly CategoryMapper _categoryMapper;

        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
            _categoryMapper = new CategoryMapper();
        }
        public async Task<IEnumerable<ICategoryTransferObject>?> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryDTOs = new List<ICategoryTransferObject>();
            foreach (var c in categories)
            {
                if(_categoryMapper.ToDTO(c) is CategoryDTO cDTO)
                    categoryDTOs.Add(cDTO);
            }

            return categoryDTOs;
        }
        public async Task<ICategoryTransferObject?> GetCategoryByIdAsync(int id)
        {
            if (id < 0) return null;

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return null;

            var categoryDTO = _categoryMapper.ToDTO(category);

            return categoryDTO;
        }

        public async Task<bool> AddCategoryAsync(ICategoryTransferObject categoryDTO)
        {
            if (categoryDTO.Id < 0 || string.IsNullOrEmpty(categoryDTO.Name))
                return false;

            var category = _categoryMapper.ToEntity(categoryDTO);

            await _categoryRepository.AddAsync(category);

            return true;
        }

        public async Task<bool> UpdateCategoryAsync(ICategoryTransferObject categoryDTO)
        {
            if (categoryDTO.Id < 0 || string.IsNullOrEmpty(categoryDTO.Name))
                return false;

            var category = _categoryMapper.ToEntity(categoryDTO);

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
