using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using RestSharp;

namespace cqrs.university.tests
{
    public class WhenGetCoursePlanByStudentId
    {
        private string _studentId;
        private List<string> _code;
        private CoursePlan plan;

        public void SetStudentId(string studentId)
        {
            _studentId = studentId;
        }

        public List<string> Code()
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest(string.Format("api/CoursePlan/{0}", _studentId), Method.GET);
            request.RequestFormat = DataFormat.Json;
            var result = client.Execute(request);
            CoursePlan coursePlan = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<CoursePlan>(result.Content);
            return coursePlan.CoursesCode;
        }

    }
}
