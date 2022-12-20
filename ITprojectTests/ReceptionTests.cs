using System;
using Moq;

namespace ITprojectTests
{
    public class ReceptionTests
    {
        private readonly Mock<IDoctorRepository> _doctorDB;
        private readonly Mock<IReceptionRepository> _db;
        private readonly ReceptionRepository _receptionRepository;

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

        public Reception GetReceptionMin()
        {
            return new Reception(1, DateTime.Now, DateTime.Now.AddMinutes(30), 1, 1);
        }

        public Reception GetReceptionBetween()
        {
            return new Reception(1, DateTime.Now.AddMinutes(15), DateTime.Now.AddMinutes(45), 1, 1);
        }

        public Reception GetReceptionMax()
        {
            return new Reception(1, DateTime.Now.AddMinutes(60), DateTime.Now.AddMinutes(90), 1, 1);
        }



        [Fact]
        public void AddByDoctorNotExists_ShouldFail()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);
            _doctorDB.Setup(repository => repository.Get(It.Is<int>(id => id == 1)))
                .Returns(GetDoctor());

            var x = _receptionRepository.AddToConcreteDate(GetReceptionMin());

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);
        }

        [Fact]
        public void AddByDoctorSucefull_ShouldSucess()
        {
            List<Reception> testList = new()
            {
                GetReceptionMin(), GetReceptionBetween()
            };

            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);
            _doctorDB.Setup(repository => repository.Get(It.Is<int>(id => id == 1)))
                .Returns(GetDoctor());
            _db.Setup(repository => repository.GetReceptsByDoctor(It.IsAny<Doctor>())).Returns(() => testList);

            var x = _receptionRepository.AddToConcreteDate(GetReceptionMax());

            Assert.True(x.Success);
        }

        [Fact]
        public void AddByDoctorFail_ShouldFail()
        {
            List<Reception> testList = new()
            {
                GetReceptionMin(), GetReceptionMax()
            };

            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);
            _doctorDB.Setup(repository => repository.Get(It.Is<int>(id => id == 1)))
                .Returns(GetDoctor());
            _db.Setup(repository => repository.GetReceptsByDoctor(It.IsAny<Doctor>())).Returns(() => testList);

            var x = _receptionRepository.AddToConcreteDate(GetReceptionBetween());

            Assert.False(x.Success);
            Assert.Equal("Данный доктор занят в эту дату", x.Error);
        }

        [Fact]
        public void AddBySpecSucess_ShouldSucess()
        {
            List<Reception> testList = new()
            {
                GetReceptionMin(), GetReceptionBetween()
            };

            _db.Setup(repository => repository.GetReceptsBySpec(It.IsAny<Specialization>())).Returns(() => testList);

            var x = _receptionRepository.AddToConcreteDate(GetReceptionMax().StartRecept, GetSpecialization());

            Assert.True(x.Success);
        }

        [Fact]
        public void AddBySpecFail_ShouldFail()
        {
            List<Reception> testList = new()
            {
                GetReceptionMin(), GetReceptionMax()
            };

            _db.Setup(repository => repository.GetReceptsBySpec(It.IsAny<Specialization>())).Returns(() => testList);

            var x = _receptionRepository.AddToConcreteDate(GetReceptionBetween().StartRecept, GetSpecialization());

            Assert.False(x.Success);
            Assert.Equal("На это время нет свободных докторов", x.Error);
        }

        [Fact]
        public void GetFreeBySpecSucefull()
        {
            var x = _receptionRepository.GetFreeBySpec(GetSpecialization());
            Assert.True(x.Success);
        }

    }
}
