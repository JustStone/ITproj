using Domain.Models;
using Domain.Interfaces;
using Domain.Services;
using Moq;

namespace ITprojectTests;



public class UserTests
{
    private readonly UserRepository _userRepository;
    private readonly Mock<IUserRepository> _db;

    public UserTests()
    {
        _db = new Mock<IUserRepository>();
        _userRepository = new UserRepository(_db.Object);
    }

    public User GetUser(string x)
    {
        return new User(x, "12345678", 1, "88005553535", "Иван Иванов", Role.Patient);
    }

    [Fact]
    public void CreateEmptyUser()
    {
        _db.Setup(repository => repository.IsValid(It.Is<User>(user => string.IsNullOrEmpty(user.Username))))
            .Returns(false);

        var response = _userRepository.CreateUser(GetUser(""));

        Assert.False(response.Success);
        Assert.Equal("Неподходящие входные данные", response.Error);
    }

    [Fact]
    public void UserAlreadyExists()
    {
        _db.Setup(repository => repository.FindByLogin(It.Is<string>(s => s == "Jigan")))
            .Returns(true);

        _db.Setup(repository => repository.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userRepository.CreateUser(GetUser("Jigan"));

        Assert.False(response.Success);
        Assert.Equal("Пользователь с таким именем уже существует", response.Error);
    }

    [Fact]
    public void SucefullCreate()
    {
        _db.Setup(repository => repository.FindByLogin(It.IsAny<string>()))
            .Returns(false);
        _db.Setup(repository => repository.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userRepository.CreateUser(GetUser("Jigan"));

        Assert.True(response.Success);
    }


    [Fact]
    public void LogEmpty()
    {
        var response = _userRepository.GetByLogin(string.Empty);
        Assert.False(response.Success);
        Assert.Equal("Введите логин", response.Error);
    }

    [Fact]
    public void LogFound()
    {
        _db.Setup(repository => repository.FindByLogin(It.Is<string>(s => s == "Jigan")))
            .Returns(true);
        _db.Setup(repository => repository.GetByLogin(It.Is<string>(s => s == "Jigan")))
            .Returns(GetUser("Jigan"));

        var response = _userRepository.GetByLogin("Jigan");

        Assert.True(response.Success);
    }

    [Fact]
    public void LogNotFound()
    {
        _db.Setup(repository => repository.GetByLogin(It.IsAny<string>()))
            .Returns(() => null);

        var response = _userRepository.GetByLogin("Jigan");

        Assert.False(response.Success);
        Assert.Equal("Пользователя с таким именем не существует", response.Error);
    }

    [Fact]
    public void EmptyLogOrPass()
    {
        var response = _userRepository.CheckExist("", "");
        Assert.False(response.Success);
        Assert.Equal("Какое-то из полей пустое", response.Error);
    }

    [Fact]
    public void ExistLoginAndPasswordOk()
    {
        _db.Setup(repository => repository.IsExist(
                It.Is<string>(x => x == "Jigan"),
                It.Is<string>(y => y == "88005553535")
            )
        ).Returns(true);

        var response = _userRepository.CheckExist("Jigan", "88005553535");

        Assert.True(response.Success);
    }


}
