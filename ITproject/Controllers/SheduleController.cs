using System;
using Domain.Models;
using Domain.Services;
using ITproject.Views;
using Microsoft.AspNetCore.Mvc;

namespace ITproject.Controllers
{
    [ApiController]
    [Route("schedule")]
    public class SheduleController : ControllerBase
    {
        private readonly SheduleRepository _service;

        public SheduleController(SheduleRepository service)
        {
            _service = service;
        }

        [HttpPost("update")]
        public ActionResult<ScheduleSearchView> UpdateSchedule(ScheduleSearchView x)
        {
            var result = new Shedule(
                x.Id,
                x.DoctorId,
                x.StartTime,
                x.EndTime
            );

            var temp = _service.Update(result);

            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            return Ok(x);
        }

        [HttpDelete("delete")]
        public ActionResult<ScheduleSearchView> DeleteSchedule(ScheduleSearchView x)
        {
            var result = new Shedule(
                x.Id,
                x.DoctorId,
                x.StartTime,
                x.EndTime
            );

            var temp = _service.Delete(result);

            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            return Ok(x);
        }

        [HttpPost("add")]
        public ActionResult<ScheduleSearchView> AddSchedule(ScheduleSearchView x)
        {
            var result = new Shedule(
                x.Id,
                x.DoctorId,
                x.StartTime,
                x.EndTime
            );

            var temp = _service.Add(result);

            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            return Ok(x);
        }

        [HttpGet("get_by_doctor")]
        public ActionResult<List<ScheduleSearchView>> GetByDoctor(DoctorSearchView doc, DateOnly date)
        {

            var some = new Doctor(doc.DoctorId, doc.Name, doc.Specialization);

            var temp = _service.GetByDoctor(some, date);

            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            List<ScheduleSearchView> result_arr = new List<ScheduleSearchView>();

            foreach (var x in temp.Value)
            {
                result_arr.Add(new ScheduleSearchView
                {
                    Id = x.Id,
                    DoctorId = x.DoctorId,
                    EndTime = x.EndTime,
                    StartTime = x.StartTime
                });
            }

            return Ok(result_arr);
        }




    }
}

