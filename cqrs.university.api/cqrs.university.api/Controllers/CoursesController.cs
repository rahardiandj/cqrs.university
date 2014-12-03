using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using university.courses;

namespace cqrs.university.api.Controllers
{
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {      
        [HttpPost]
        public IHttpActionResult Open(OpenCourse cmd)
        {
            cmd.Id = new Guid("08AD2A6C-F23A-47FA-99FF-EC9B6F7FF0C8");
            Domain.Dispatcher.SendCommand(cmd);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Active()
        {
            var x = Domain.OpenCourseQueries.GetAllActiveCourse();
            return Ok(x);
        }

        [Route("All")]
        [HttpGet]
        public IHttpActionResult All()
        {
            var x = Domain.OpenCourseQueries.GetAllCourse();
            return Ok(x);
        }
    }
}
