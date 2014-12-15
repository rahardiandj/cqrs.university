using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using RestSharp;
using university.courses;

namespace cqrs.university.tests
{
    public class CourseDoFixtureTest : fitlibrary.DoFixture
    {
        public void OpenCourseWithCreditAndType(string code, string name, string description, int credit, string type)
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/Courses", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new OpenCourse
            {
                Code = code,
                Credit = credit,
                Name = name,
                Type = type
            });
            client.Execute(request);
        }

        public void Opencourse(string code)
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/Courses", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new OpenCourse
            {
                Code = code,
                Credit = 3,
                Name = "3",
                Type = "test"
            });
            client.Execute(request);
        }

        public void StudentTakeCourse(string studentId, string code)
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/CoursePlan/Take", Method.POST);
            request.RequestFormat = DataFormat.Json;
            CourseInfo course = new CourseInfo
            {
                Code = code,
                Credit = 4,
                Description = "Fundamental",
                IsAdded = true,
                Lecturer = "Mr Choi",
                Name = "Calculus"
            };
            List<CourseInfo> courseList = new List<CourseInfo>();
            courseList.Add(course);

            request.AddBody(new TakeCourse
            {
                StudentId = studentId,
                Items = courseList
            });

            client.Execute(request);
        }

        public bool IsActive(string code)
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/courses", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var result = client.Execute(request);
            List<CourseInfo> courses = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<CourseInfo>>(result.Content);
            return (courses.First().Code.ToString() == code);
        }

        public List<string> AllCourse()
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/courses/All", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var result = client.Execute(request);
            List<CourseInfo> courses = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<CourseInfo>>(result.Content);
            return (courses.Select(x=>x.Code).ToList());
        }

    }
}
