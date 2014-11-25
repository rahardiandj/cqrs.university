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
        IHandleCommand<CloseCourse>,
        IHandleCommand<CancelCourse>,
        IHandleCommand<ModifyCoursePlan>,
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

        public IEnumerable Handle(CloseCourse c)
        {
            if (!open)
                throw new CourseNotOpen();
            yield return new CourseClosed
            {
                Id = c.Id,
                Credit = c.Credit,
                Name = c.Name,
                Type = c.Type
            };
        }

        public IEnumerable Handle(CancelCourse c)
        {
            if (!open)
                throw new CourseNotOpen();
            yield return new CourseCanceled
            {
                Code = c.Code,
                Credit = c.Credit,
                Name = c.Name,
                Description = c.Description,
                Lecturer = c.Lecturer
            };
        }

        public IEnumerable Handle(ModifyCoursePlan c)
        {
            var added = c.Items.Where(i => i.IsAdded).ToList();
            if (added.Any())
            {
                yield return new CourseTaken
                {
                    Code= added.FirstOrDefault().Code,
                    Credit = added.FirstOrDefault().Credit,
                    Description = added.FirstOrDefault().Description,
                    Lecturer = added.FirstOrDefault().Lecturer,
                    Name = added.FirstOrDefault().Name
                };
            }
            var canceled = c.Items.Where(i => !i.IsAdded).ToList();
            if (canceled.Any())
                yield return new CourseCanceled
                {
                    Code = canceled.FirstOrDefault().Code,
                    Credit = canceled.FirstOrDefault().Credit,
                    Description = canceled.FirstOrDefault().Description,
                    Lecturer = canceled.FirstOrDefault().Lecturer,
                    Name = canceled.FirstOrDefault().Name
                };
        }
    }
}
