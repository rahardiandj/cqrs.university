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
        private Guid testId;
        private Guid coursePlanId;
        private string courseCode;
        private string courseCode2;
        private CourseInfo testCourse1;
        private CourseInfo testCourse2;
        private List<CourseInfo> listCourses;
        private List<CourseInfo> listCourses2;
        private List<CourseInfo> listCourses3;

        [SetUp]
        public void SetUp()
        {
            coursePlanId = Guid.NewGuid();
            courseId = Guid.NewGuid();
            testId = Guid.NewGuid();
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
            listCourses = new List<CourseInfo>();
            listCourses.Add(testCourse1);

            listCourses2 = new List<CourseInfo>();
            listCourses2.Add(testCourse2);

            listCourses3 = new List<CourseInfo> { testCourse1, testCourse2 };
        }

        [Test]
        public void WhenOpenCourse()
        {
            Test(
                    Given(),
                    When(new OpenCourse
                    {
                        Code = courseCode,
                        Name = "Calculus",
                        Credit = 4,
                        Type = "Mandatory"
                    }),
                    Then(new CourseOpened
                    {
                        Code = courseCode,
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
                        Code = courseCode,
                        Credit = 4,
                        Name = "Calculus",
                        Type = "Mandatory"
                    }
                    ),
                    When(new TakeCourse
                    {
                        Id = testId,
                        StudentId = "1001201",
                        Items = listCourses
                    }),
                    Then(new CourseTaken
                    {
                        Id = testId,
                        StudentId = "1001201",
                        Items = listCourses
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
                        Id = testId,
                        StudentId = "1001201",
                        Items = listCourses
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
                        Code = courseCode,
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
                        Code = courseCode2,
                        Credit = 4,
                        Name = "Calculus",
                        Type = "Mandatory"
                    }
                    ),
                    When(new CancelCourse
                    {
                        Id = testId,
                        StudentId = "1001201",
                        Items = listCourses2
                    }),
                    Then(new CourseCanceled
                    {
                        Id = testId,
                        StudentId = "1001201",
                        Items = listCourses2
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
                        Id = testId,
                        Items = listCourses2
                    }),
                    ThenFailWith<CourseNotOpen>()
                );
        }

        [Test]
        public void CanModifyCourses()
        {
            Test(
                Given(new CourseOpened
                {
                    Code = courseCode,
                    Credit = 4,
                    Name = "Calculus",
                    Type = "Mandatory"
                }, new CourseOpened
                {
                    Code = courseCode2,
                    Credit = 4,
                    Name = "Calculus",
                    Type = "Mandatory"
                }
                
                ),
                When(new ModifyCoursePlan
                {
                    Id = testId,
                    StudentId = "1001201",
                    Items = new List<CourseInfo> { testCourse1, testCourse2 }
                }),
                Then(new CourseTaken
                {
                    StudentId = "1001201",
                    Items = listCourses
                },
                new CourseCanceled
                {
                    StudentId = "1001201",
                    Items = listCourses2
                }
                )
                );
        }
    }
}
