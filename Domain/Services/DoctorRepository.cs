using System;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
	public class DoctorRepository
    {
        private readonly IDoctorRepository _db;

        public DoctorRepository(IDoctorRepository db)
        {
            _db = db;
        }

        public Result<IEnumerable<Doctor>> GetAll()
        {
            return Result.Ok(_db.List());
        }

        public Result<Doctor> GetById(int id)
        {
            if (!_db.Exists(id))
                return Result.Fail<Doctor>("Такого доктора не существует");

            return Result.Ok<Doctor>(_db.Get(id));
        }

        public Result<IEnumerable<Doctor>> GetBySpec(Specialization spec)
        {
            return Result.Ok(_db.GetBySpec(spec));
        }


        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            if (string.IsNullOrEmpty(doctor.FullName))
                return Result.Fail<Doctor>("Неправильно введено имя доктора");

            if (_db.Exists(doctor.Id))
                return Result.Fail<Doctor>("Такой доктор уже существует");

            _db.Create(doctor);
            return Result.Ok(doctor);
        }

        public Result DeleteDoctor(int id)
        {
            if (!_db.Exists(id))
                return Result.Fail<Doctor>("Такого доктора не существует");

            _db.Delete(id);
            return Result.Ok();
        }


    }
}

