using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cqrs.university.events.courses
{
    public class CourseCanceled
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public List<CourseInfo> Items { get; set; }
    }
}
