﻿using Cursos_API.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Cursos_API.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
    }
}
