using NUnit.Framework;
using AddEmployeToProject;
using AddEmployeeToProjectProps;

namespace AddEmployeeToProjectTests
{
    [TestFixture]
    public class AddEmployeeToProjectMethodsTests
    {
        [Test ,Order(1)]
        public void AddEmployeeToProject_ShouldAddEmployeeToProjectsList()
        {
            // Arrange
            var addEmployeeToProjectObj = new AddEmployeeToProjectProperties
            {
                ProjectId = 1,
                EmployeeId = 2,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            AddEmployeeToProjectMethods.AddEmployeeToProject(addEmployeeToProjectObj);

            // Assert
            Assert.AreEqual(1, AddEmployeeToProjectMethods.addEmployeeToProjectslist.Count);
            Assert.IsTrue(AddEmployeeToProjectMethods.addEmployeeToProjectslist.Contains(addEmployeeToProjectObj));

        }

        [Test,Order(2)]
        public void ViewEmployeesOfProject_ShouldDisplayEmployeesOfProject()
        {
            // Arrange
            AddEmployeeToProjectProperties obj = new AddEmployeeToProjectProperties()
            {
                ProjectId = 1,
                EmployeeId = 2,
                FirstName = "John",
                LastName = "Doe"
            };
            // Add an employee to the list
            AddEmployeeToProjectMethods.addEmployeeToProjectslist.Add(obj);

            // Assert that the first element of the list is equal to 1
            Assert.That(AddEmployeeToProjectMethods.addEmployeeToProjectslist[0],Is.EqualTo(obj));


        }


        [Test,Order(3)]
        public void RemoveEmployeeFromProject_ShouldRemoveEmployeeFromProjectsList()
        {
            // Arrange
            AddEmployeeToProjectMethods.addEmployeeToProjectslist.Add(new AddEmployeeToProjectProperties
            {
                ProjectId = 1,
                EmployeeId = 2,
                FirstName = "John",
                LastName = "Doe"
            });

            // Act
            AddEmployeeToProjectMethods.RemoveEmployeeFromProject(2, 1);

            // Assert
            Assert.AreEqual(0, AddEmployeeToProjectMethods.addEmployeeToProjectslist.Count);
            Assert.IsFalse(AddEmployeeToProjectMethods.addEmployeeToProjectslist.Contains(new AddEmployeeToProjectProperties
            {
                ProjectId = 1,
                EmployeeId = 2,
                FirstName = "John",
                LastName = "Doe"
            }));
        }
    }
}
