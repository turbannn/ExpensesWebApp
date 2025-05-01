using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppBLL.Interfaces;
using FluentValidation;
using AutoMapper;

namespace ExpenseWebAppBLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<IUserTransferObject> _validator;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository expenseRepository,
            IValidator<IUserTransferObject> expenseValidator,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _userRepository = expenseRepository;
            _validator = expenseValidator;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = _mapper.Map<List<UserReadDTO>>(users);

            return userDtos;
        }
        public async Task<UserReadDTO?> GetUserByIdAsync(int id)
        {
            if (id < 0) return null;

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            var userReadDto = _mapper.Map<UserReadDTO>(user);

            return userReadDto;
        }

        public async Task<UserReadDTO?> GetUserByNameAndPasswordAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user is null) return null;

            var isPasswordValid = _passwordHasher.VerifyPassword(password, user.Password);
            if (!isPasswordValid) return null;

            return _mapper.Map<UserReadDTO>(user);
        }

        public async Task<bool> AddUserAsync(UserCreateDTO userCreateDto)
        {
            var validationResult = await _validator.ValidateAsync(userCreateDto);
            if (!validationResult.IsValid) return false;

            var user = _mapper.Map<User>(userCreateDto);

            var passwordHash = _passwordHasher.HashPassword(userCreateDto.Password);

            user.Password = passwordHash;
            user.Role = userCreateDto.Role;

            await _userRepository.AddAsync(user);

            return true;
        }

        public async Task<bool> UpdateUserAsync(UserReadDTO userReadDto)
        {
            var validationResult = await _validator.ValidateAsync(userReadDto);
            if (!validationResult.IsValid) return false;

            var user = _mapper.Map<User>(userReadDto);
            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine("ERROR: User NOT FOUND");
                Console.WriteLine(exception.ToString());
                return false;
            }
            
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {

            if (id < 0) return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
