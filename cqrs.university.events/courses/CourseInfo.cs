using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cqrs.university.events.courses
{
    public class CourseInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public string Lecturer { get; set; }
        public string Description { get; set; }
        public bool IsAdded { get; set; }
    }

    public class CoursePlan
    {
        public string StudentId { get; set; }
        public List<string> CoursesCode { get; set; }
    }
}
