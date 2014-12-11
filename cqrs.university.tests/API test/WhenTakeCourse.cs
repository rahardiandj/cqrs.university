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
    public class WhenTakeCourse
    {
        private string _code;
        private string _name;
        private int _credit;
        private string _lecturer;
        private string _description;
        private bool _isAdded;
        private string _studentId;

        public void SetCode(string code)
        {
            _code = code;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetCredit(int credit)
        {
            _credit = credit;
        }

        public void SetLecturer(string lecturer)
        {
            _lecturer = lecturer;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public void SetIsAdded(bool isAdded)
        {
            _isAdded = isAdded;
        }

        public void SetStudentId(string studentId)
        {
            _studentId = studentId;
        }

        public string Status()
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/CoursePlan/Take", Method.POST);
            request.RequestFormat = DataFormat.Json;
            CourseInfo course = new CourseInfo
            {
                Code = _code,
                Credit = _credit,
                Description = _description,
                IsAdded = _isAdded,
                Lecturer = _lecturer,
                Name = _name
            };
            List<CourseInfo> courseList = new List<CourseInfo>();
            courseList.Add(course);

            request.AddBody(new TakeCourse
            {
                StudentId = _studentId,
                Items = courseList
            });

            var result = client.Execute(request);
            return result.StatusCode.ToString();
        }
    }
}
