using ExpenseWebAppBLL.DTOs.CategoryDTOs;
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
using FluentValidation;

namespace ExpenseWebAppBLL.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryMapper _categoryMapper;
        private readonly IValidator<ICategoryTransferObject> _validator;

        public CategoryService(ICategoryRepository repository, IValidator<ICategoryTransferObject> validator)
        {
            _categoryRepository = repository;
            _validator = validator;
            _categoryMapper = new CategoryMapper();
        }
        public async Task<IEnumerable<ICategoryTransferObject>?> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryDTOs = new List<ICategoryTransferObject>();
            foreach (var c in categories)
            {
                if(_categoryMapper.ToReadDTO(c) is CategoryDTO cDTO)
                    categoryDTOs.Add(cDTO);
            }

            return categoryDTOs;
        }
        public async Task<ICategoryTransferObject?> GetCategoryByIdAsync(int id)
        {
            if (id < 0) return null;

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return null;

            var categoryDTO = _categoryMapper.ToReadDTO(category);

            return categoryDTO;
        }

        public async Task<bool> AddCategoryAsync(ICategoryTransferObject categoryDTO)
        {
            var validationResult = await _validator.ValidateAsync(categoryDTO);
            if (!validationResult.IsValid) return false;

            var category = _categoryMapper.ToEntity(categoryDTO);

            await _categoryRepository.AddAsync(category);

            return true;
        }

        public async Task<bool> UpdateCategoryAsync(ICategoryTransferObject categoryDTO)
        {
            var validationResult = await _validator.ValidateAsync(categoryDTO);
            if (!validationResult.IsValid) return false;

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
