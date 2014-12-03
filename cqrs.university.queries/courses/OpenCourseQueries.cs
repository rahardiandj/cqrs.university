using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using cqrs.university.queries.models;
using cqrs.university.queries.stab;
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


        private List<CoursePlan> coursePlanList = new List<CoursePlan>();

        public List<CourseItem> GetAllCourse()
        {
            return StaticData.Courses.ToList();
        }

        public List<CourseItem> GetAllActiveCourse()
        {
            lock (activeCourseList)
                return (activeCourseList).OrderBy(i => i).ToList();
        }

        public CourseItem GetCourseDetail(string code)
        {
            return StaticData.Courses.Where(i => i.Code == code).FirstOrDefault();
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
            var alreadyTaken = coursePlanList.Where(x => x.StudentId == e.StudentId).FirstOrDefault();
            coursePlanList.Remove(alreadyTaken);
            if (alreadyTaken != null)
            {
                foreach (var course in e.Items)
                    alreadyTaken.CoursesCode.Add(course.Code);
                coursePlanList.Add(alreadyTaken);
            }
            else
                coursePlanList.Add(new CoursePlan
                {
                    StudentId = e.StudentId,
                    CoursesCode = e.Items.Select(i => i.Code).ToList()      
                });
        }

        public void Handle(CourseClosed e)
        {
            throw new NotImplementedException();
        }

        public void Handle(CourseCanceled e)
        {
            throw new NotImplementedException();
        }

        public CoursePlan GetCourseByStudentId(string studentId)
        {
            lock (coursePlanList)
                return coursePlanList.Where(x => x.StudentId == studentId).FirstOrDefault();
        }
    }
}
