using System;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
    public class SheduleRepository
    {
        private ISheduleRepository _db;
        private IDoctorRepository _doctorDB;

        public SheduleRepository(ISheduleRepository db, IDoctorRepository doctorDB)
        {
            _db = db;
            _doctorDB = doctorDB;
        }

        public Result<Shedule> Update(Shedule schedule)
        {
            if (!_db.Exists(schedule.Id))
                return Result.Fail<Shedule>("Такого расписания не существует");

            _db.Update(schedule);
            return Result.Ok<Shedule>(schedule);
        }

        public Result<Shedule> Delete(Shedule schedule)
        {
            if (!_db.Exists(schedule.Id))
                return Result.Fail<Shedule>("Такого расписания не существует");

            _db.Delete(schedule.Id);
            return Result.Ok<Shedule>(schedule);
        }

        public Result<Shedule> Add(Shedule schedule)
        {
            if (!_doctorDB.Exists(schedule.DoctorId))
                return Result.Fail<Shedule>("Такого доктора не существует");

            if (_db.Exists(schedule.Id))
                return Result.Fail<Shedule>("Такое расписание уже существует");

            _db.Create(schedule);
            return Result.Ok<Shedule>(schedule);
        }

        public Result<IEnumerable<Shedule>> GetByDoctor(Doctor doctor, DateOnly date)
        {
            if (!_doctorDB.Exists(doctor.Id))
                return Result.Fail<IEnumerable<Shedule>>("Такого доктора не существует");
            if (!_doctorDB.IsValid(doctor))
                return Result.Fail<IEnumerable<Shedule>>("Доктор недействителен");

            return Result.Ok<IEnumerable<Shedule>>(_db.GetSheduleByDate(doctor, date));
        }

    }
}
