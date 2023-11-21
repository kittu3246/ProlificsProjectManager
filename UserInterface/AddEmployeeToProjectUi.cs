

using System;
using ProjectModel;
using Project;
using Employee;
using AddEmployeeToProjectProps;
using AddEmployeToProject;
using System.Data.SqlClient; // Import the necessary namespace for SqlConnection

namespace AddEmployee_To_Project
{
    public class AddEmployee_ToProject
    {
        private static EmployeeMethods employeeMethods = new EmployeeMethods();
        private static ProjectMethods projectMethods = new ProjectMethods();

        //  database configuration
        private static string connectionString = "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Secrity = SSPI;";

        /// Adds an employee to a project in a console application.
        public static void AddEmployeeToProjectUI()
        {
            int projectId;

            while (true)
            {
                Console.WriteLine("Enter ProjectId:");

                if (!int.TryParse(Console.ReadLine(), out projectId))
                {
                    Console.WriteLine("Enter Proper ProjectId");
                    continue;
                }

                if (!IsProjectIdValid(projectId))
                {
                    Console.WriteLine("Enter Valid Project Id");
                    continue;
                }

                int employeeId;

                while (true)
                {
                    Console.Write("Enter Employee Id: ");

                    if (!int.TryParse(Console.ReadLine(), out employeeId))
                    {
                        Console.WriteLine("Enter Proper Employee Id");
                        continue;
                    }

                    if (!IsEmployeeIdValid(employeeId))
                    {
                        Console.WriteLine("Enter a valid Employee Id");
                        continue;
                    }

                    var empObj = employeeMethods.GetEmployeeById(employeeId);

                    var addEmployeeToProject = new AddEmployeeToProjectProperties
                    {
                        ProjectId = projectId,
                        EmployeeId = employeeId,
                        FirstName = empObj.FirstName,
                        LastName = empObj.LastName
                    };

                    AddEmployeeToProjectMethods.AddEmployeeToProject(addEmployeeToProject);
                    Console.WriteLine("Employee added to the project successfully.");

                    break;
                }

                break;
            }
        }

        /// Checks if an employee with the given ID exists in the system.
        /// <param name="employeeId">The employee ID to check.</param>
        /// <returns>True if the employee ID is valid, false otherwise.</returns>
        public static bool IsEmployeeIdValid(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM Employee WHERE Id = {employeeId}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        /// Checks if a project with the given ID exists in the system.
        /// <param name="projectId">The project ID to check.</param>
        /// <returns>True if the project ID is valid, false otherwise.</returns>
        public static bool IsProjectIdValid(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM Project WHERE Id = {projectId}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
