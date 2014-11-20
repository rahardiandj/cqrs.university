using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using Edument.CQRS;

namespace university.courses
{
    public class CourseAggregate : Aggregate,
        IHandleCommand<OpenCourse>
    {

        public IEnumerable Handle(OpenCourse c)
        {
            yield return new CourseOpened
            {
                Id = c.Id,
                Credit = c.Credit,
                Name = c.Name,
                Type = c.Type
            };
        }
    }
}
