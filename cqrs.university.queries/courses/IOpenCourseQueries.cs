using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using cqrs.university.queries.models;

namespace cqrs.university.queries.courses
{
    public interface IOpenCourseQueries
    {
        List<CourseItem> GetAllCourseCode();
        List<CourseItem> GetAllActiveCourseCode();
        CourseInfo GetCourseDetail(string code);
    }
}
