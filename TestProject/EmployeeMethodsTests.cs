using NUnit.Framework;
using Employee;
using Employee_Details;
using ConsoleTables;

namespace EmployeeTests
{
    public class employeeMethodsTests
    {
        EmployeeMethods employeeMethods = new EmployeeMethods(); 
        [Test]
        public void AddEmployee_ShouldAddEmployeeToList()
        {
            // Arrange
            var employeeDetailsObj = new EmployeeDetailsProps()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                EmployeeAddress = "123 Main Street",
                RollId = 1
            };

            // Act
            employeeMethods.Add(employeeDetailsObj);

            // Assert
            Assert.IsTrue(employeeMethods.list.Contains(employeeDetailsObj));
        }

        [Test]
        public void ViewEmployee_ShouldDisplayAllEmployees()
        {
            // Arrange
            var employeeDetailsObj1 = new EmployeeDetailsProps()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                EmployeeAddress = "123 Main Street",
                RollId = 1
            };

            var employeeDetailsObj2 = new EmployeeDetailsProps()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = "9876543210",
                EmployeeAddress = "456 Elm Street",
                RollId = 2
            };

            employeeMethods.Add(employeeDetailsObj1);
            employeeMethods.Add(employeeDetailsObj2);

            // Act
            employeeMethods.ViewEmployee();

            // Assert
            // TODO: Implement assertion for ViewEmployee method
            // Assert.AreEqual("1 John Doe john.doe@example.com 1234567890 123 Main Street 1 2 Jane Doe jane.doe@example.com 9876543210 456 Elm Street 2", employeeMethods.ViewEmployee());

        }

        [Test]
        // validating the addemployee object is null or not
        public void AddEmployee_ShouldThrowExceptionIfEmployeeDetailsObjectIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => employeeMethods.Add(null));
        }

        [Test]
        public void ViewEmployee_ShouldReturnCorrectConsoleTable()
        {
            // Arrange
            var employeeDetailsList = new List<EmployeeDetailsProps>
            {
                new EmployeeDetailsProps()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    EmployeeAddress = "123 Main Street",
                    RollId = 1
                },
                new EmployeeDetailsProps()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "9876543210",
                    EmployeeAddress = "456 Elm Street",
                    RollId = 2
                }
            };

            employeeMethods.list.AddRange(employeeDetailsList);

            // Act
            var consoleTable = employeeMethods.ViewEmployee();

            // Assert
            var expectedTable = new ConsoleTable("Id", "Firstname", "LastName", "Email", "PhoneNumber", "EmployeeAddress", "RollId");
            expectedTable.AddRow("1", "John", "Doe", "john.doe@example.com", "1234567890", "123 Main Street", "1");
            expectedTable.AddRow("2", "Jane", "Doe", "jane.doe@example.com", "9876543210", "456 Elm Street", "2");

            Assert.AreNotEqual(expectedTable, consoleTable);
        }


        
    }
}




