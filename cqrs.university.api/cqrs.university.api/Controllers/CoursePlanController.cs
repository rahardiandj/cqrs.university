using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using university.courses;

namespace cqrs.university.api.Controllers
{
    [RoutePrefix("api/CoursePlan")]
    public class CoursePlanController : ApiController
    {
        //[Route("api/CoursePlan/{id}")]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetCoursePlanByStudentId(string id)
        {
            var coursePlan = Domain.OpenCourseQueries.GetCourseByStudentId(id);
            return Ok(coursePlan);
        }
        
        [HttpPost]
        public IHttpActionResult GetCoursePlanByStudentId(ModifyCoursePlan cmd)
        {
            cmd.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(cmd);
            return Ok();
        }

        [Route("Take")]
        [HttpPost]
        public IHttpActionResult TakeCourse(TakeCourse cmd)
        {
            //cmd.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(new TakeCourse(){
                Id = new Guid("08AD2A6C-F23A-47FA-99FF-EC9B6F7FF0C8"),
                Items = cmd.Items,
                StudentId = cmd.StudentId
            });
            return Ok();
        }
    }
}
