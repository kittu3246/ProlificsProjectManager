using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConsoleTables;
using Employee_Details;
using AddEmployeeToProjectProps;
using IEntityOperation;

namespace Employee
{
    public class EmployeeMethods : IEntity<EmployeeDetailsProps>
    {
        private static string connectionString = "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";

        /// Adds an employee to the database.
        /// <param name="employeeDetailsObj">The employee details object to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the employee details object is null.</exception>
        public void Add(EmployeeDetailsProps employeeDetailsObj)
        {
            if (employeeDetailsObj == null)
            {
                throw new ArgumentNullException(nameof(employeeDetailsObj), "Employee details object cannot be null.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Employees (Id,FirstName, LastName, Email, PhoneNumber, EmployeeAddress, RollId) " +
                                     "VALUES (@Id,@FirstName, @LastName, @Email, @PhoneNumber, @EmployeeAddress, @RollId)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", employeeDetailsObj.Id);
                    cmd.Parameters.AddWithValue("@FirstName", employeeDetailsObj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employeeDetailsObj.LastName);
                    cmd.Parameters.AddWithValue("@Email", employeeDetailsObj.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employeeDetailsObj.PhoneNumber);
                    cmd.Parameters.AddWithValue("@EmployeeAddress", employeeDetailsObj.EmployeeAddress);
                    cmd.Parameters.AddWithValue("@RollId", employeeDetailsObj.RollId);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Employee Details Added Successfully....");
        }

        /// Displays a table with the details of all employees from the database.
        /// <returns>The console table with employee details.</returns>
        public ConsoleTable ListAll()
        {
            var table = new ConsoleTable("Id", "Firstname", "LastName", "Email", "PhoneNumber", "EmployeeAddress", "RollId");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, FirstName, LastName, Email, PhoneNumber, EmployeeAddress, RollId FROM Employees";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        table.AddRow(reader["Id"], reader["FirstName"], reader["LastName"], reader["Email"],
                            reader["PhoneNumber"], reader["EmployeeAddress"], reader["RollId"]);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            table.Write();
            Console.ResetColor();

            return table;
        }

        /// Displays a table with the details of the employee with the specified ID from the database.
        /// <param name="employeeId">The ID of the employee to display.</param>
        /// <returns>The console table with the employee details.</returns>
        public ConsoleTable ListById(int employeeId)
        {
            var employeeIdTable = new ConsoleTable("Id", "Firstname", "LastName", "Email", "PhoneNumber", "EmployeeAddress", "RollId");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, FirstName, LastName, Email, PhoneNumber, EmployeeAddress, RollId FROM Employees WHERE Id = @EmployeeId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employeeIdTable.AddRow(reader["Id"], reader["FirstName"], reader["LastName"], reader["Email"],
                            reader["PhoneNumber"], reader["EmployeeAddress"], reader["RollId"]);
                    }
                }
            }

            return employeeIdTable;
        }

        /// Deletes the employee with the specified ID from the database.
        /// <param name="deleteEmployeeId">The ID of the employee to delete.</param>
        /// <returns>True if the employee was deleted successfully, false otherwise.</returns>
        public bool DeleteById(int deleteEmployeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Employees WHERE Id = @EmployeeId";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", deleteEmployeeId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CheckEmployeIdExists(int EmployeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Employees WHERE Id = @EmployeeId";
                using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", EmployeId);

                    int employeeCount = (int)cmd.ExecuteScalar();
                    return employeeCount > 0;
                }
            }
        }

        public int CheckRoleIdMatchedToEmployeeId(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkRoleIdQuery = "SELECT Id FROM Employees WHERE RollId = @RollId";
                using (SqlCommand cmd = new SqlCommand(checkRoleIdQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@RollId", roleId);

                    object result = cmd.ExecuteScalar();
                    return result != null ? (int)result : 0;
                }
            }
        }




    public EmployeeDetailsProps GetEmployeeById(int employeeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT * FROM Employees WHERE Id = {employeeId}";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new EmployeeDetailsProps
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        EmployeeAddress = reader["EmployeeAddress"].ToString(),
                        RollId = (int)reader["RollId"]
                        
                    };
                }
                return null;
            }
        }
    }
    }
}
