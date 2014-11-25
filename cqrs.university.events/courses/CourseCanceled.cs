﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cqrs.university.events.courses
{
    public class CourseCanceled
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public string Lecturer { get; set; }
        public string Description { get; set; }
    }
}
