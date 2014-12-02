using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cqrs.university.api.Controllers
{
    [RoutePrefix("api/CoursePlan")]
    public class CoursePlanController : ApiController
    {
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetCoursePlanByStudentId(string studentId)
        {
            var coursePlan = Domain.OpenCourseQueries.GetCourseByStudentId(studentId);
            return Ok(coursePlan);
        }
    }
}
