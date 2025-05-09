using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppBLL.Interfaces;
using FluentValidation;
using AutoMapper;
using System;
using System.Security.Cryptography;

namespace ExpenseWebAppBLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<IUserTransferObject> _validator;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailHandler _emailHandler;

        public UserService(IUserRepository expenseRepository,
            IValidator<IUserTransferObject> expenseValidator,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IEmailHandler emailHandler)
        {
            _userRepository = expenseRepository;
            _validator = expenseValidator;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _emailHandler = emailHandler;
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
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserAsync(UserUpdateDTO userUpdateDto)
        {
            var validationResult = await _validator.ValidateAsync(userUpdateDto);
            if (!validationResult.IsValid) return false;

            var user = _mapper.Map<User>(userUpdateDto);
            try
            {
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
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
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetUserPasswordAsync(string email)
        {
            await _userRepository.BeginTransactionAsync();

            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                if (user is null) return false;

                var newPassword = GenerateRandomString(15);
                user.Password = _passwordHasher.HashPassword(newPassword);

                string body = $"Your reset password for: {user.Username}\n{newPassword}";

                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();

                await _emailHandler.SendEmail(email, "Expense Tracker password reset", body);
                await _userRepository.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                Console.WriteLine("ERROR:" + ex);
                return false;
            }
        }

        private string GenerateRandomString(int length)
        {
            if (length < 0) throw new IndexOutOfRangeException();

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var bytes = RandomNumberGenerator.GetBytes(length);
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }
    }
}
