using System;
using Domain.Models;


namespace Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    User? GetByLogin(string login);

    bool ExistUser(string login, string password);

    bool ExistUser(string login);
}

