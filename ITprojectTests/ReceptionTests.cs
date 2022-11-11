using System;
using Moq;

namespace ITprojectTests
{
	public class ReceptionTests
	{
        private readonly ReceptionRepository _receptionRepository;
        private readonly Mock<IDoctorRepository> _doctorDB;
        private readonly Mock<IReceptionRepository> _db;


        public ReceptionTests()
        {
            _db = new Mock<IReceptionRepository>();
            _doctorDB = new Mock<IDoctorRepository>();
            _receptionRepository = new ReceptionRepository(_db.Object, _doctorDB.Object);
        }

        public Specialization GetSpecialization()
        {
            return new Specialization(1, "Какой-то доктор");
        }

        public Doctor GetDoctor(string name = "Иван иванов")
        {

            return new Doctor(1, name, new Specialization(1, "Какой-то доктор"));
        }

        public Reception GetReception()
        {
            return new Reception(1, DateTime.Now, DateTime.Now, 1, 1);
        }

        [Fact]
        public void AddBySpecBusy()
        {
            _db.Setup(repository => repository.CheckFreeBySpec(It.IsAny<DateTime>(), It.IsAny<Specialization>()))
                .Returns(false);

            var x = _receptionRepository.AddToConcreteDate(DateTime.Now, GetSpecialization());

            Assert.False(x.Success);
            Assert.Equal("На это время нет свободных докторов", x.Error);
        }

        [Fact]
        public void GetFreeByDoctorSucefull()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            var x = _receptionRepository.GetFreeByDoctor(GetDoctor());

            Assert.True(x.Success);
        }

        [Fact]
        public void AddByDoctorTaken()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);
            _doctorDB.Setup(repository => repository.Get(It.Is<int>(id => id == 1)))
                .Returns(GetDoctor());
            _db.Setup(repository => repository.CheckFreeByDoctor(It.IsAny<DateTime>(), It.Is<Doctor>(doctor => doctor.Id == 1)))
                .Returns(false);

            var x = _receptionRepository.AddToConcreteDate(GetReception());
            Assert.False(x.Success);
            Assert.Equal("Данный доктор занят в эту дату", x.Error);
        }

        [Fact]
        public void AddBySpecSucefull()
        {
            _db.Setup(repository => repository.CheckFreeBySpec(It.IsAny<DateTime>(), It.IsAny<Specialization>()))
                .Returns(true);

            var x = _receptionRepository.AddToConcreteDate(DateTime.Now, GetSpecialization());

            Assert.True(x.Success);
        }

        [Fact]
        public void GetFreeBySpecSucefull()
        {
            var x = _receptionRepository.GetFreeBySpec(GetSpecialization());
            Assert.True(x.Success);
        }

        [Fact]
        public void AddByDoctorNotExists()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);
            _doctorDB.Setup(repository => repository.Get(It.Is<int>(id => id == 1)))
                .Returns(GetDoctor());

            var x = _receptionRepository.AddToConcreteDate(GetReception());

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);
        }

        [Fact]
        public void AddByDoctorSucefull()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);
            _doctorDB.Setup(repository => repository.Get(It.Is<int>(id => id == 1)))
                .Returns(GetDoctor());
            _db.Setup(repository => repository.CheckFreeByDoctor(It.IsAny<DateTime>(), It.Is<Doctor>(doctor => doctor.Id == 1)))
                .Returns(true);
            var x = _receptionRepository.AddToConcreteDate(GetReception());

            Assert.True(x.Success);
        }

        [Fact]
        public void GetFreeByDoctorNotExists()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            var x = _receptionRepository.GetFreeByDoctor(GetDoctor());

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);
        }
    }
}

