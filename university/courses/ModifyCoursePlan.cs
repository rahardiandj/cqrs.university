﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;

namespace university.courses
{
    public class ModifyCoursePlan
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public List<CourseInfo> Items { get; set; }
    }
}
