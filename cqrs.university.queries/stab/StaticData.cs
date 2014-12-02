using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.queries.models;

namespace cqrs.university.queries.stab
{
    public static class StaticData
    {
        public static List<CourseItem> Courses = new List<CourseItem>
        {
            new CourseItem()
            {
                Code = "IF3232",
                Name = "Algebra",
                Description = "FOundation of Algebra"
            },
            new CourseItem()
            {
                Code = "CS3232",
                Name = "Data Science",
                Description = "FOundation of Data"
            },
            new CourseItem()
            {
                Code = "IF3292",
                Name = "Risk Management",
                Description = "Risk ANalysis"
            },
            new CourseItem()
            {
                Code = "IF4432",
                Name = "Strategic IT",
                Description = "FOundation of IT Strategy"
            }
        };
    }
}
