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
        IHandleCommand<OpenCourse>,
        IHandleCommand<TakeCourse>,
        IApplyEvent<CourseOpened>
    {
        private bool open;

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

        public IEnumerable Handle(TakeCourse c)
        {
            if (!open)
                throw new CourseNotOpen();
            yield return new CourseTaken
            {
                Code = c.Code,
                Credit = c.Credit,
                Name = c.Name,
                Description = c.Description,
                Lecturer = c.Lecturer
            };
        }

        public void Apply(CourseOpened e)
        {
            open = true;
        }
    }
}
