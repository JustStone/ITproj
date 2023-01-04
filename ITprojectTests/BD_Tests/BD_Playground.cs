using System;
using Moq;
using Domain.Models;
using BD_;
using BD_.Convert;
using BD_.Models;
using BD_.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITprojectTests.BD_Tests
{
	public class BD_Playground
	{
        private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

        public BD_Playground()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql(
                $"Host=localhost;Port=5432;Database=ITproject;Username=ruslan;Password=ruslan_password");
            _optionsBuilder = optionsBuilder;
        }

        public Specialization GetSpecialization()
        {
            return new Specialization(77, "Какой-то доктор77");
        }

        //[Fact]
        //public void PlaygroundMethod1()
        //{
        //    using var context = new ApplicationContext(_optionsBuilder.Options);
        //    context.Doctors.Add(new DoctorM
        //    {
        //        Id = 66,
        //        FullName = "Тест66",
        //        Specialization = GetSpecialization().ToModel()
        //    }) ;

        //    context.SaveChanges(); // сохранили в БД

        //    Assert.True(context.Doctors.Any(u => u.FullName == "Тест66")); // проверим, нашло ли в нашей бд

        //}

        //[Fact]
        //public void PlaygroundMethod2()
        //{
        //    using var context = new ApplicationContext(_optionsBuilder.Options);
        //    context.Specializations.Add(new SpecializationM
        //    {
        //        Id = 7,
        //        Name = "Хирург2"
        //    });

        //    context.SaveChanges(); // сохранили в БД

        //    Assert.True(context.Specializations.Any(u => u.Name == "Хирург2")); // проверим, нашло ли в нашей бд

        //}

        //[Fact]
        //public void PlaygroundMethod2()
        //{
        //    using var context = new ApplicationContext(_optionsBuilder.Options);
        //    context.Specializations.Add(new SpecializationM
        //    {
        //        Id = 7,
        //        Name = "Хирург2"
        //    });

        //    context.SaveChanges(); // сохранили в БД

        //    Assert.True(context.Specializations.Any(u => u.Name == "Хирург2")); // проверим, нашло ли в нашей бд

        //}

        [Fact]
        public void PlaygroundMethod3()
        {
            using var context = new ApplicationContext(_optionsBuilder.Options);
            context.Shedules.Add(new SheduleM
            {
                Id = 1,
                DoctorId = 333,
                StartTime = new DateTime(2022, 12, 15, 15, 0, 0, 0),
                EndTime = new DateTime(2022, 12, 15, 15, 30, 0, 0)
            }) ; 

            context.SaveChanges(); // сохранили в БД

            Assert.True(context.Shedules.Any(u => u.DoctorId == 333)); // проверим, нашло ли в нашей бд

        }

    }
}

