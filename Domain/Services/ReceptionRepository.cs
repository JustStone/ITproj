using System;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
	public class ReceptionRepository
	{
        private IReceptionRepository _db;
        private IDoctorRepository _doctorDB;
        public ReceptionRepository(IReceptionRepository db, IDoctorRepository doctorDB)
        {
            _db = db;
            _doctorDB = doctorDB;
        }

        public Result<Reception> AddToConcreteDate(Reception reception)
        {
            var doctor = _doctorDB.Get(reception.DoctorId);
            if (!_doctorDB.Exists(doctor.Id))
                return Result.Fail<Reception>("Такого доктора не существует");

            if (!_db.CheckFreeByDoctor(reception.StartRecept, doctor))
                return Result.Fail<Reception>("Данный доктор занят в эту дату");

            _db.Create(reception);
            return Result.Ok(reception);
        }

        public Result<Reception> AddToConcreteDate(DateTime dateTime, Specialization spec)
        {
            if (!_db.CheckFreeBySpec(dateTime, spec))
                return Result.Fail<Reception>("На это время нет свободных докторов");

            var reception = _db.CreateBySpec(dateTime, spec);
            return Result.Ok(reception);
        }

        public Result<IEnumerable<DateTime>> GetFreeBySpec(Specialization spec)
        {
            var list = _db.GetFreeBySpec(spec);
            return Result.Ok(list);
        }

        public Result<IEnumerable<DateTime>> GetFreeByDoctor(Doctor doctor)
        {
            if (!_doctorDB.Exists(doctor.Id))
                return Result.Fail<IEnumerable<DateTime>>("Такого доктора не существует");
            var list = _db.GetFreeByDoctor(doctor);
            return Result.Ok(list);
        }
    }
}

