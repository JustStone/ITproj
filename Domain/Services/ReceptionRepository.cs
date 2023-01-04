using System;
using System.Numerics;
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

        //К конкретному свободному врачу
        public Result<Reception> AddToConcreteDate(Reception reception)
        {
            var doctor = _doctorDB.Get(reception.DoctorId);
            if (!_doctorDB.Exists(doctor.Id))
                return Result.Fail<Reception>("Такого доктора не существует");

            DateTime startR = reception.StartRecept;
            DateTime endR = reception.EndRecept;
            var list = _db.GetReceptsByDoctor(doctor);

            if (list.Any(x => startR < x.EndRecept && endR > x.StartRecept ))
                return Result.Fail<Reception>("Данный доктор занят в эту дату");

            _db.Create(reception);
            return Result.Ok(reception);
        }

        //К любому свободному врачу указанной спец
        public Result<Reception> AddToConcreteDate(DateTime dateTime, Specialization spec)
        {
            var list = _db.GetReceptsBySpec(spec);
            if (list.Any(x => dateTime < x.EndRecept && dateTime.AddMinutes(30) > x.StartRecept ))
                return Result.Fail<Reception>("На это время нет свободных докторов");

            var reception = _db.CreateBySpec(dateTime, spec);
            _db.Create(reception);
            return Result.Ok(reception);
        }

        public Result<IEnumerable<DateTime>> GetFreeBySpec(Specialization spec, Shedule shedule)
        {
            var list = _db.GetFreeReceptsBySpec(spec, shedule);
            return Result.Ok(list);
        }
    }
}