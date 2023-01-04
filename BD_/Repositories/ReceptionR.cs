using System;
using BD_.Convert;
using BD_.Models;
using Domain.Models;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace BD_.Repositories
{
    public class ReceptionR : IReceptionRepository
    {
        private readonly ApplicationContext _bd;

        public ReceptionR(ApplicationContext bd)
        {
            _bd = bd;
        }

        public Reception Create(Reception x)
        {
            _bd.Receptions.Add(x.ToModel());
            return x;
        }

        public Reception Update(Reception x)
        {
            _bd.Receptions.Update(x.ToModel());
            return x;
        }

        public Reception? Get(int id)
        {
            return _bd.Receptions.FirstOrDefault(t => t.Id == id).ToDomain();
        }

        public bool Exists(int id)
        {
            return _bd.Receptions.Any(t => t.Id == id);
        }

        public bool Delete(int id)
        {
            var x = _bd.Receptions.FirstOrDefault(t => t.Id == id);
            if (x == default)
                return false; // not deleted
            _bd.Receptions.Remove(x);
            return true;
        }

        public bool IsValid(Reception x)
        {
            if (x.Id < 0)
                return false;

            if (x.StartRecept >= x.EndRecept)
                return false;

            return true;
        }

        public IEnumerable<Reception> List()
        {
            return _bd.Receptions.Select(x => x.ToDomain()).ToList();
        }

        IEnumerable<Reception> IReceptionRepository.GetReceptsByDoctor(Doctor doctor)
        {
            return _bd.Receptions.Where(t => t.DoctorId == doctor.Id)
                .Select(t => t.ToDomain())
                .ToList();
        }

        IEnumerable<Reception> IReceptionRepository.GetReceptsBySpec(Specialization spec)
        {
            var arr_docs = _bd.Doctors.Where(t => t.Specialization.Id == spec.Id);

            return _bd.Receptions.Where(t => arr_docs
            .Any(z => t.DoctorId == z.Id))
            .Select(t => t.ToDomain())
            .ToList();
        }


        IEnumerable<DateTime> IReceptionRepository.GetFreeReceptsBySpec(Specialization spec, Shedule shedule)
        {
            var arr_docs = _bd.Doctors.Where(t => t.Specialization.Id == spec.Id && t.Id == shedule.DoctorId);
            var arr_recepts = _bd.Receptions.Where(t => arr_docs.Any(z => z.Id == t.DoctorId)).Select(t => t.StartRecept);
            var result_arr = new List<DateTime>();

            for (DateTime i = shedule.StartTime; i < shedule.EndTime; i.AddMinutes(30))
            {
                if (arr_recepts.All(t => t != i))
                    result_arr.Append(i);
            }
            return result_arr;
        }


        public Reception CreateBySpec(DateTime dateTime, Specialization spec)
        {
            ////1)Берем всех врачей указанной специализации
            //var arr_docs1 = _bd.Doctors.Where(doc => doc.Specialization.Id == spec.Id);
            //var arr_shedules1 = _bd.Shedules;

            //var arr_docs2 = new List<DoctorM>();
            //var arr_shedules2 = new List<SheduleM>();

            ////2)Оставляем только тех врачей, расписание которых подходит нашей дате.
            //foreach (DoctorM doc in arr_docs1)
            //    foreach(SheduleM shed in arr_shedules1)
            //        if (doc.Id == shed.DoctorId &&
            //            (
            //            dateTime >= shed.StartTime &&
            //            dateTime.AddMinutes(30) <= shed.EndTime
            //            )
            //           )
            //        {
            //            arr_docs2.Add(doc);
            //            arr_shedules2.Add(shed);
            //        }
            ////Оставляем врачей, которые могут принять в эту дату.
            ////......

            ////Потом надо будет создать любой Reception и вернуть его,
            ////но пока непонятно откуда брать idшник пациента для этого, 
            ////поэтому пока отложу.

            //https://learn.microsoft.com/ru-ru/dotnet/api/system.notimplementedexception?view=net-7.0
            throw new NotImplementedException();
        }

    }
}
