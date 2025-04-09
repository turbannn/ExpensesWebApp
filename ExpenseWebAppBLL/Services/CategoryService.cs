using ExpenseWebAppBLL.DTOs.CategoryDTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using AutoMapper;
using FluentValidation;

namespace ExpenseWebAppBLL.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<ICategoryTransferObject> _validator;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository,
            IValidator<ICategoryTransferObject> validator,
            IMapper mapper)
        {
            _categoryRepository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTO>?> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);

            return categoryDTOs;
        }
        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            if (id < 0) return null;

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null) return null;

            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var validationResult = await _validator.ValidateAsync(categoryDTO);
            if (!validationResult.IsValid) return false;

            var category = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.AddAsync(category);

            return true;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var validationResult = await _validator.ValidateAsync(categoryDTO);
            if (!validationResult.IsValid) return false;

            var category = _mapper.Map<Category>(categoryDTO);

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
