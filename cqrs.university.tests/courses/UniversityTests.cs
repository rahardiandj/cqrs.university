using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cqrs.university.events.courses;
using Edument.CQRS;
using NUnit.Framework;
using university.courses;

namespace cqrs.university.tests.courses
{
    [TestFixture]
    public class UniversityTests : BDDTest<CourseAggregate>
    {
        private Guid courseId;

        [SetUp]
        public void SetUp()
        {
            courseId = Guid.NewGuid();
        }

        [Test]
        public void WhenOpenCourse()
        {
            Test(
                    Given(),
                    When(new OpenCourse
                    {
                        Id = courseId,
                        Name = "Calculus",
                        Credit = 4,
                        Type = "Mandatory"
                    }),
                    Then(new CourseOpened
                    {
                        Id = courseId,
                        Name = "Calculus",
                        Credit = 4,
                        Type = "Mandatory"
                    }
                    )
                );
        }
    }
}
