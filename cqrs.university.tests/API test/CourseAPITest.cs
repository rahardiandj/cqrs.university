using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using university.courses;

namespace cqrs.university.tests
{
    public class CourseAPITest
    {
        //private int _cash;
        //private int _pintsOfMilkRemaining;
        //private string _useCreditCard;

        //public void SetCashInWallet(int cash)
        //{
        //    _cash = cash;
        //}

        //public void SetCreditCard(string useCreditCard)
        //{
        //    _useCreditCard = useCreditCard;
        //}

        //public void SetPintsOfMilkRemaining(int pints)
        //{
        //    _pintsOfMilkRemaining = pints;
        //}

        //public string GoToStore()
        //{
        //    if (_cash > 0 || _useCreditCard.Equals("yes"))
        //        return "yes";
        //    return "no";
        //}

        private string _status;
        private string _code;
        private string _name;
        private int _credit;
        private string _type;

        public CourseAPITest()
        {
        }

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

        public void SetType(string type)
        {
            _type = type;
        }

        public string Status()
        {
            var client = new RestClient("http://localhost:42299/");
            var request = new RestRequest("api/Courses", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new OpenCourse
            {
                Code = _code,
                Credit = _credit,
                Name = _name,
                Type = _type
            });
            var result = client.Execute(request);
            return result.StatusCode.ToString();
        }
    }
}
