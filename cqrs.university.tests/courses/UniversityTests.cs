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
        private string courseCode;

        [SetUp]
        public void SetUp()
        {
            courseId = Guid.NewGuid();
            courseCode = "CS5032";
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

        [Test]
        public void WhenTakeCourse()
        {
            Test(
                    Given(new CourseOpened
                    {
                        Id = courseId,
                        Credit = 4,
                        Name = "Calculus",
                        Type = "Mandatory"
                    }
                    ),
                    When(new TakeCourse
                    {
                        Code = courseCode,
                        Name = "Calculus",
                        Credit = 4,
                        Description = "Foundation of Calculus",
                        Lecturer = "David Teece"
                    }),
                    Then(new CourseTaken
                    {
                        Code = courseCode,
                        Name = "Calculus",
                        Credit = 4,
                        Description = "Foundation of Calculus",
                        Lecturer = "David Teece"
                    }
                    )
                );
        }

        [Test]
        public void WhenTakeClosedCourse()
        {
            Test(
                    Given(
                    ),
                    When(new TakeCourse
                    {
                        Code = courseCode,
                        Name = "Calculus",
                        Credit = 4,
                        Description = "Foundation of Calculus",
                        Lecturer = "David Teece"
                    }),
                    ThenFailWith<CourseNotOpen>()
                );
        }
    }
}
