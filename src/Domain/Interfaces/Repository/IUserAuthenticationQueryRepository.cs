using System;
using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserAuthenticationQueryRepository
{
    Task<List<UserEntity>> GetUsers(Guid? id = null, string? code = null, byte[]? username = null);
}
