using System;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using ITproject.Views;
using Microsoft.AspNetCore.Mvc;

namespace ITproject.Controllers
{
    [ApiController]
    [Route("reception")]
    public class ReceptionController : ControllerBase
    {
        private readonly ReceptionRepository _service;
        private readonly SheduleRepository _serviceSched;



        public ReceptionController (ReceptionRepository service, SheduleRepository serviceSched)
        {
            _service = service;
            _serviceSched = serviceSched;

        }

        [HttpPost("add")]
        public ActionResult<ReceptionSearchView> SaveReception(ReceptionSearchView x)
        {
            var result = new Reception(
                x.Id,
                x.StartRecept,
                x.EndRecept,
                x.PatientId,
                x.DoctorId
            );
            var temp = _service.AddToConcreteDate(result);
            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            return Ok(x);
        }

        [HttpPost("add_by_spec")]
        public ActionResult<ReceptionSearchView> SaveReceptionBySpec(DateTime dateTime, Specialization spec)
        {
            var temp = _service.AddToConcreteDate(dateTime, spec);
            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            var result = new ReceptionSearchView
            {
                DoctorId = temp.Value.DoctorId,
                Id = temp.Value.Id,
                StartRecept = temp.Value.StartRecept,
                EndRecept = temp.Value.EndRecept
            };
            return Ok(result);
        }

        [HttpGet("get_free_by_spec")]
        public ActionResult<IEnumerable<DateTime>> GetFreeBySpec(Specialization spec, [FromQuery] Shedule shedule)
        {
            var temp = _service.GetFreeBySpec(spec, shedule);
            if (!temp.Success)
                return Problem(statusCode: 404, detail: temp.Error);

            return Ok(temp.Value);

        }
    }
}
