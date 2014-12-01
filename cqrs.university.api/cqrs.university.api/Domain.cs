using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cqrs.university.queries.courses;
using Edument.CQRS;
using university.courses;

namespace cqrs.university.api
{
    public class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IOpenCourseQueries OpenCourseQueries;

        public static void Setup()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());

            Dispatcher.ScanInstance(new CourseAggregate());

            OpenCourseQueries = new OpenCourseQueries();
            Dispatcher.ScanInstance(OpenCourseQueries);

        }
    }
}