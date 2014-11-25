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
        private Guid coursePlanId;
        private string courseCode;
        private string courseCode2;
        private CourseInfo testCourse1;
        private CourseInfo testCourse2;

        [SetUp]
        public void SetUp()
        {
            coursePlanId = Guid.NewGuid();
            courseId = Guid.NewGuid();
            courseCode = "CS5032";
            courseCode2 = "IF3045";
            testCourse1 = new CourseInfo
            {
                Code = courseCode,
                Name = "Calculus",
                Credit = 4,
                Description = "Foundation of Calculus",
                Lecturer = "David Teece",
                IsAdded = true,
            };
            testCourse2 = new CourseInfo
            {
                Code = courseCode2,
                Name = "Algorithm Design",
                Credit = 2,
                Description = "Introduction of Algorithm",
                Lecturer = "Michael Page",
                IsAdded = false,
            };
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

        [Test]
        public void WhenCloseCourse()
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
                    When(new CloseCourse
                    {
                        Id = courseId,
                        Credit = 4,
                        Name = "Calculus",
                        Type = "Mandatory"
                    }),
                    Then(new CourseClosed
                    {
                        Id = courseId,
                        Credit = 4,
                        Name = "Calculus",
                        Type = "Mandatory"
                    }
                    )
                );
        }

        [Test]
        public void WhenCloseCourseNotOpen()
        {
            Test(
                    Given(
                    ),
                    When(new CloseCourse
                    {
                        Id = courseId,
                        Credit = 4,
                        Name = "Calculus",
                        Type = "Mandatory"
                    }),
                    ThenFailWith<CourseNotOpen>()
                );
        }

        [Test]
        public void WhenCancelCourse()
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
                    When(new CancelCourse
                    {
                        Code = courseCode,
                        Name = "Calculus",
                        Credit = 4,
                        Description = "Foundation of Calculus",
                        Lecturer = "David Teece"
                    }),
                    Then(new CourseCanceled
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
        public void WhenCancelCourseNotOpened()
        {
            Test(
                    Given(
                    ),
                    When(new CancelCourse
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

        [Test]
        public void CanPlaceFoodAndDrinkOrder()
        {
            Test(
                Given(new CourseOpened
                {
                    Id = courseId,
                    Credit = 4,
                    Name = "Calculus",
                    Type = "Mandatory"
                }),
                When(new ModifyCoursePlan
                {
                    Id = coursePlanId,
                    Items = new List<CourseInfo> { testCourse1, testCourse2 }
                }),
                Then(new CourseTaken
                {
                    Code = courseCode,
                    Name = "Calculus",
                    Credit = 4,
                    Description = "Foundation of Calculus",
                    Lecturer = "David Teece",
                },
                new CourseCanceled
                {
                    Code = courseCode2,
                    Name = "Algorithm Design",
                    Credit = 2,
                    Description = "Introduction of Algorithm",
                    Lecturer = "Michael Page",
                }
                )
                );
        }
    }
}
