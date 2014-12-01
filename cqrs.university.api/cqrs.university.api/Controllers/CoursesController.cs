using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using university.courses;

namespace cqrs.university.api.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Open(OpenCourse cmd)
        {
            cmd.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(cmd);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Active()
        {
            var x = Domain.OpenCourseQueries.GetAllActiveCourseCode();
            return Ok(x);
        }
    }
}
