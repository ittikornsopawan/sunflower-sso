using System;

namespace Domain.Interfaces.Repository;

public interface IUserQueryRepository
{
    Task<int> GetExistUser();
}
