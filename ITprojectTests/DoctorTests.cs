using System;
using Moq;

namespace ITprojectTests
{
	public class DoctorTests
	{
        private readonly DoctorRepository _doctorRepository;
        private readonly Mock<IDoctorRepository> _db;

        public DoctorTests()
        {
            _db = new Mock<IDoctorRepository>();
            _doctorRepository = new DoctorRepository(_db.Object);
        }

        public Doctor GetDoctor(string name = "Иван Иванов")
        {
            return new Doctor(1, name, new Specialization(1, "Какой-то доктор"));
        }

        [Fact]
        public void GetAllOk()
        {
            var x = _doctorRepository.GetAll();
            Assert.True(x.Success);
        }

        [Fact]
        public void CreateNameMistake()
        {
            Doctor doctor = GetDoctor(string.Empty);
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            var response = _doctorRepository.CreateDoctor(doctor);
            Assert.False(response.Success);
        }

        [Fact]
        public void CreateSucefull()
        {
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            _db.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _doctorRepository.CreateDoctor(GetDoctor());

            Assert.True(x.Success);
        }

        [Fact]
        public void GetByIdSucefull()
        {
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            var x = _doctorRepository.GetById(1);

            Assert.True(x.Success);
        }
        [Fact]
        public void GetBySpecSucefull()
        {
            var x = _doctorRepository.GetBySpec(new Specialization(1, "Какой-то доктор"));
            Assert.True(x.Success);
        }

        [Fact]
        public void DeleteNotExists()
        {
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            _db.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _doctorRepository.DeleteDoctor(GetDoctor().Id);

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);

        }

        [Fact]
        public void DeleteSucefull()
        {
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            _db.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _doctorRepository.DeleteDoctor(GetDoctor().Id);

            Assert.True(x.Success);
        }

        [Fact]
        public void CreateAlreadyExists()
        {
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            _db.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _doctorRepository.CreateDoctor(GetDoctor());

            Assert.False(x.Success);
            Assert.Equal("Такой доктор уже существует", x.Error);
        }

        [Fact]
        public void GetByIdNotExists()
        {
            _db.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            var x = _doctorRepository.GetById(1);

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);
        }


    }
}

