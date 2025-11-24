using System;

using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.ValueObjects;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserAuthenticationQueryRepository : BaseRepository, IUserAuthenticationQueryRepository
{
    public UserAuthenticationQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<UserEntity>> GetUsers(Guid? id = null, string? code = null, byte[]? username = null)
    {
        var query = this._dbContext.t_users.AsQueryable();

        if (id != null)
            query = query.Where(x => x.id == id);

        if (!string.IsNullOrEmpty(code))
            query = query.Where(x => x.code == code);

        if (username != null)
            query = query.Where(x => x.username == username);

        var dbList = await query.ToListAsync();

        var domainList = dbList.Select(db => new UserEntity(
            new UserCodeValueObject(db.code),
            new UsernameValueObject(db.username),
            new AuthenticationTypeValueObject(db.authenticationType),
            db.id
        )).ToList();

        return domainList;
    }
}
