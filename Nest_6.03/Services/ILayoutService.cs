using System;
using Nest_6._03.Dtos.AppUserDtos;

namespace Nest_6._03.Services
{
    public interface ILayoutService
    {
        Task<UserGetDto> GetUser();
    }
}

