using System;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;
using Shared.DTOs;

namespace Infrastructure.Repository;

public class UserCommandRepository : BaseRepository, IUserCommandRepository
{
    public UserCommandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserDTO> Create(UserEntity userEntity)
    {
        var item = new t_users
        {
            id = userEntity.id,
            code = userEntity.code.ToString(),
            username = userEntity.username.ToBytes(),
            authenticationType = userEntity.authenticationType.ToString(),
            createdById = Guid.Empty,
            createdAt = DateTime.UtcNow
        };

        var entityEntry = await this._dbContext.t_users.AddAsync(item);
        await this._dbContext.SaveChangesAsync();

        if (entityEntry.Entity == null) return default!;

        var user = new UserDTO
        {
            id = entityEntry.Entity.id,
            code = entityEntry.Entity.code,
            username = entityEntry.Entity.username!.ToString()!,
            authenticationType = entityEntry.Entity.authenticationType
        };

        return user;
    }
}
