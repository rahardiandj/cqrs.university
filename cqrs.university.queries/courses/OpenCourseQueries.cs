using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using cqrs.university.queries.models;
using Edument.CQRS;

namespace cqrs.university.queries.courses
{
    public class OpenCourseQueries : IOpenCourseQueries,
        ISubscribeTo<CourseOpened>,
        ISubscribeTo<CourseTaken>,
        ISubscribeTo<CourseClosed>,
        ISubscribeTo<CourseCanceled>
    {

        private class Courses
        {
            public int TableNumber;
            public List<CourseItem> Active;
            public List<CourseItem> Inactive;
        }

        private Courses courseList = new Courses();
        private List<CourseItem> activeCourseList = new List<CourseItem>();
        public List<CourseItem> GetAllCourseCode()
        {
            throw new NotImplementedException();
        }

        public List<CourseItem> GetAllActiveCourseCode()
        {
            lock (activeCourseList)
                return (activeCourseList).OrderBy(i => i).ToList();
        }

        public CourseInfo GetCourseDetail(string code)
        {
            throw new NotImplementedException();
        }

        public void Handle(CourseOpened e)
        {
            lock (activeCourseList)
                activeCourseList.Add(new CourseItem
                {
                    Code = e.Code,
                    Name = e.Name,
                    Description = e.Name
                });
        }

        public void Handle(CourseTaken e)
        {
            throw new NotImplementedException();
        }

        public void Handle(CourseClosed e)
        {
            throw new NotImplementedException();
        }

        public void Handle(CourseCanceled e)
        {
            throw new NotImplementedException();
        }
    }
}
