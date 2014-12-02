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
        IApplyEvent<CourseOpened>,
        IApplyEvent<CourseTaken>
    {
        private bool open;
        private List<CourseInfo> coursePlan = new List<CourseInfo>();
        private List<string> courseopenedcode = new List<string>();
        private List<CoursePlan> courseplans = new List<CoursePlan>();
        public IEnumerable Handle(OpenCourse c)
        {
            yield return new CourseOpened
            {
                Code = c.Code,
                Credit = c.Credit,
                Name = c.Name,
                Type = c.Type
            };
        }

        public IEnumerable Handle(TakeCourse c)
        {
            if (!isAllOpen(c.Items))
                throw new CourseNotOpen();
            yield return new CourseTaken
            {
                Id = c.Id,
                StudentId = c.StudentId,
                Items = c.Items
            };
        }

        public void Apply(CourseOpened e)
        {
            courseopenedcode.Add(e.Code);
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
            if (!isAllOpen(c.Items))
                throw new CourseNotOpen();
            yield return new CourseCanceled
            {
                Id = c.Id,
                StudentId = c.StudentId,
                Items = c.Items
            };
        }

        public IEnumerable Handle(ModifyCoursePlan c)
        {
            var added = c.Items.Where(i => i.IsAdded).ToList();
            if (!isAllOpen(c.Items))
                throw new CourseNotOpen();
            if (added.Any())
            {
                yield return new CourseTaken
                {
                    StudentId = c.StudentId,
                    Items = added
                };
            }
            var canceled = c.Items.Where(i => !i.IsAdded).ToList();
            if (canceled.Any())
                yield return new CourseCanceled
                {
                    StudentId = c.StudentId,
                    Items = canceled
                };
        }

        public void Apply(CourseTaken e)
        {
            coursePlan.AddRange(e.Items);
        }

        private void updateCoursePlan(ModifyCoursePlan e)
        {
            var code = e.Items.Select(x => x.Code).ToList();
            courseplans.Add(new CoursePlan { StudentId = e.StudentId, CoursesCode = code });
        }

        private bool isAllOpen(List<CourseInfo> course)
        {
            var code = course.Select(e => e.Code).ToList();
            var courses = code.Except(courseopenedcode);
            if ((courses.Any())||(!courseopenedcode.Any()))
                return false;
            else
                return true;
        }
    }
}
