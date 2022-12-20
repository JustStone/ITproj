using System;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
    public class UserRepository
    {
        private readonly IUserRepository _db;

        public UserRepository(IUserRepository db)
        {
            _db = db;
        }

        public Result<User> GetByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Введите логин");

            if (!_db.FindByLogin(login))
                return Result.Fail<User>("Пользователя с таким именем не существует");

            return Result.Ok<User>(_db.GetByLogin(login));

        }

        public Result<User> CreateUser(User user)
        {
            if (!_db.IsValid(user))
                return Result.Fail<User>("Неподходящие входные данные");

            if (_db.FindByLogin(user.Username))
                return Result.Fail<User>("Пользователь с таким именем уже существует");

            return Result.Ok<User>(user);
        }

        public Result<bool> CheckExist(string login, string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(login))
                return Result.Fail<bool>("Какое-то из полей пустое");

            return Result.Ok<bool>(_db.IsExist(login, password));
        }

    }
}
