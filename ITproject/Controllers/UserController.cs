using System;
using Domain.Models;
using Domain.Services;
using ITproject.Views;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для работы с пользователем
/// </summary>
namespace ITproject.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _service;
        public UserController(UserRepository service)
        {
            _service = service;
        }

        [HttpGet("login")]
        public ActionResult<UserSearchView> GetUserByLogin(string x)
        {
            if (x == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан логин");

            var temp = _service.GetByLogin(x);

            if (!temp.Success)
            {
                return Problem(statusCode: 404, detail: temp.Error);
            }
            return Ok(new UserSearchView
            {
                Id = temp.Value.Id,
                Username = temp.Value.Username,
                Password = temp.Value.Password,
                FullName = temp.Value.FullName,
                PhoneNumber = temp.Value.PhoneNumber,
                Role = temp.Value.Role
            });
        }



        [HttpPost("CreateUser")]
        public ActionResult<UserSearchView> CreateUser(UserSearchView x)
        {
            if (string.IsNullOrEmpty(x.Username))
                return Problem(statusCode: 404, detail: "Введите логин");
            if (string.IsNullOrEmpty(x.Password))
                return Problem(statusCode: 404, detail: "Введите пароль");

            var user = new User(
                x.Username,
                x.Password,
                x.Id,
                x.PhoneNumber,
                x.FullName,
                x.Role);

            var temp = _service.CreateUser(user);

            if (!temp.Success)
                return Problem(statusCode: 400, detail: temp.Error);

            return Ok(x);
        }

        [HttpGet("exists")]
        public ActionResult<UserSearchView> IsUserExists(string login, string password)
        {
            var temp = _service.CheckExist(login, password);
            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);
            return Ok(temp.Value);
        }
    }
}

