using System;
using BD_.Convert;
using BD_.Models;
using Domain.Interfaces;
using Domain.Models;

namespace BD_.Repositories
{
    public class UserR : IUserRepository
    {
        private readonly ApplicationContext _bd;

        public UserR(ApplicationContext bd)
        {
            _bd = bd;
        }

        public User Create(User x)
        {
            _bd.Users.Add(x.ToModel());
            return x;
        }

        public User Update(User x)
        {
            _bd.Users.Update(x.ToModel());
            return x;
        }

        public User? Get(int id)
        {
            var x = _bd.Users.FirstOrDefault(t => t.Id == id);
            return x?.ToDomain();
        }

        public bool Exists(int id)
        {
            return _bd.Users.Any(t => t.Id == id);
        }

        public bool Delete(int id)
        {
            var x = _bd.Users.FirstOrDefault(t => t.Id == id);
            if (x == default)
            {
                return false;
            }
            _bd.Users.Remove(x);
            return true;
        }

        public bool IsValid(User x)
        {
            if (string.IsNullOrEmpty(x.Username)
               ||string.IsNullOrEmpty(x.Password))
                return false;

            if (string.IsNullOrEmpty(x.PhoneNumber)
               || string.IsNullOrEmpty(x.FullName))
                return false;

            if (x.Id < 0)
                return false;

            return true;
        }

        public IEnumerable<User> List()
        {
            return _bd.Users.Select(t => t.ToDomain()).ToList();
        }

        public User? GetByLogin(string login)
        {
            var user = _bd.Users.FirstOrDefault(user => user.Username == login);
            return user?.ToDomain();
        }

        public bool ExistUser(string login)
        {
            return _bd.Users.Any(user => user.Username == login);
        }

        public bool ExistUser(string login, string password)
        {
            return _bd.Users.Any(user => user.Username == login && user.Password == password);
        }


    }
}

