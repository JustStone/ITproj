using System;
using Domain.Models;


namespace Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{

    User GetByLogin(string login);
    bool IsValid(User user);
    bool IsExist(string login, string password);
    bool FindByLogin(string login);

}

