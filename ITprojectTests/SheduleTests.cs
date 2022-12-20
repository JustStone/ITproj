using System;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace ITprojectTests
{
	public class SheduleTests
	{
        private readonly SheduleRepository _scheduleRepository;
        private readonly Mock<IDoctorRepository> _doctorDB;
        private readonly Mock<ISheduleRepository> _sheduleDB;

        public SheduleTests()
        {
            _doctorDB = new Mock<IDoctorRepository>();
            _sheduleDB = new Mock<ISheduleRepository>();
            _scheduleRepository = new SheduleRepository(_sheduleDB.Object, _doctorDB.Object);
        }

        Doctor GetDoctor()
        {
            return new Doctor(1, "Иван Иванов", new Specialization(1, "Какой-то доктор"));
        }

        Shedule GetSchedule()
        {
            return new Shedule(1, 1, new DateTime(2022, 08, 08, 12, 30, 0), new DateTime(2022, 08, 08, 13, 00, 0));
        }

        [Fact]

        public void DoctorNotExist()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            _doctorDB.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _scheduleRepository.GetByDoctor(GetDoctor(), new DateOnly());

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);
        }

        [Fact]

        public void DeleteNotExists()
        {
            _sheduleDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            var x = _scheduleRepository.Delete(GetSchedule());
            Assert.False(x.Success);
            Assert.Equal("Такого расписания не существует", x.Error);
        }



        [Fact]
        public void DoctorNotValid()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            _doctorDB.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(false);

            var x = _scheduleRepository.GetByDoctor(GetDoctor(), new DateOnly());

            Assert.False(x.Success);
            Assert.Equal("Доктор недействителен", x.Error);
        }

        [Fact]
        public void SheduleAddAlreadyExists()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            _sheduleDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            var x = _scheduleRepository.Add(GetSchedule());

            Assert.False(x.Success);
            Assert.Equal("Такое расписание уже существует", x.Error);
        }


        [Fact]
        public void SheduleAddDoctorIsNotExists()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);
            _doctorDB.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _scheduleRepository.Add(GetSchedule());

            Assert.False(x.Success);
            Assert.Equal("Такого доктора не существует", x.Error);

        }

        [Fact]
        public void DoctorSucefull()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            _doctorDB.Setup(repository => repository.IsValid(It.IsAny<Doctor>()))
                .Returns(true);

            var x = _scheduleRepository.GetByDoctor(GetDoctor(), new DateOnly());

            Assert.True(x.Success);
        }



        [Fact]
        public void AddScheduleSucefull()
        {
            _doctorDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            _sheduleDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            var x = _scheduleRepository.Add(GetSchedule());
            Assert.True(x.Success);
        }



        [Fact]
        public void DeleteSucefull()
        {
            _sheduleDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            var x = _scheduleRepository.Delete(GetSchedule());
            Assert.True(x.Success);
        }

        [Fact]
        public void ScheduleUpdateSucefull()
        {
            _sheduleDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(true);

            var x = _scheduleRepository.Update(GetSchedule());
            Assert.True(x.Success);
        }

        [Fact]
        public void ScheduleUpdateIsNotExists()
        {
            _sheduleDB.Setup(repository => repository.Exists(It.Is<int>(id => id == 1)))
                .Returns(false);

            var x = _scheduleRepository.Update(GetSchedule());
            Assert.False(x.Success);
            Assert.Equal("Такого расписания не существует", x.Error);
        }


    }
}

