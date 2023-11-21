using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConsoleTables;
using IEntityOperation;
using ProjectModel;



namespace Project
{
    public class ProjectMethods : IEntity<ProjectProperties>
    {
        private static string connectionString =
            "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";

        /// Adds a project to the database.
        /// <param name="project">The project to add.</param>
        public void Add(ProjectProperties project)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery =
                    "INSERT INTO Projects (ProjectId ,ProjectName, StartDate, EndDate) VALUES (@ProjectId,@ProjectName, @StartDate, @EndDate)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", project.Id);
                    cmd.Parameters.AddWithValue("@ProjectName", project.Name);
                    cmd.Parameters.AddWithValue("@StartDate", project.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", project.EndDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// Returns a console table with all projects from the database.
        /// <returns>A console table with all projects.</returns>
        public ConsoleTable ListAll()
        {
            var table = new ConsoleTable("ProjectId", "ProjectName", "Start Date", "End Date");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery =
                    "SELECT ProjectId, ProjectName, StartDate, EndDate FROM Projects";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        table.AddRow(
                            reader["ProjectId"],
                            reader["ProjectName"],
                            reader["StartDate"],
                            reader["EndDate"]
                        );
                    }
                }
            }

            return table;
        }

        /// Returns a console table with the project that matches the given ID from the database.
        /// <param name="projectId">The ID of the project to list.</param>
        /// <returns>A console table with the project that matches the given ID.</returns>
        public ConsoleTable ListById(int projectId)
        {
            var table = new ConsoleTable("ProjectId", "ProjectName", "Start Date", "End Date");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery =
                    "SELECT ProjectId, ProjectName, StartDate, EndDate FROM Projects WHERE ProjectId = @ProjectId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        table.AddRow(
                            reader["ProjectId"],
                            reader["ProjectName"],
                            reader["StartDate"],
                            reader["EndDate"]
                        );
                    }
                }
            }

            return table;
        }

        /// Deletes the project that matches the given ID from the database.
        /// <param name="projectId">The ID of the project to delete.</param>
        /// <returns>True if the project was deleted successfully, false otherwise.</returns>
        public bool DeleteById(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Projects WHERE ProjectId = @ProjectId";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        /// Maps an employee to a project in the database.
        /// <param name="projectId">The ID of the project.</param>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>True if the mapping was successful, false otherwise.</returns>
        public bool MapEmployeeToProject(int projectId, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertMappingQuery =
                    "INSERT INTO ProjectEmployeeMapping (ProjectId, EmployeeId) VALUES (@ProjectId, @EmployeeId)";
                using (SqlCommand cmd = new SqlCommand(insertMappingQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
        }

        /// Checks if an employee is mapped to a project in the database.
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>True if the employee is mapped to any project, false otherwise.</returns>
        public bool CheckEmployeeMappedToProject(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkMappingQuery =
                    "SELECT COUNT(*) FROM ProjectEmployeeMapping WHERE EmployeeId = @EmployeeId";
                using (SqlCommand cmd = new SqlCommand(checkMappingQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    int mappingCount = (int)cmd.ExecuteScalar();
                    return mappingCount > 0;
                }
            }
        }

        /// Deletes an employee from a project in the database.
        /// <param name="projectId">The ID of the project.</param>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public bool DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteMappingQuery =
                    "DELETE FROM ProjectEmployeeMapping WHERE ProjectId = @ProjectId AND EmployeeId = @EmployeeId";
                using (SqlCommand cmd = new SqlCommand(deleteMappingQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CheckProjectIdExists(int projectIdExists)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (
                    SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM Projects WHERE ProjectId = @ProjectId",
                        connection
                    )
                )
                {
                    command.Parameters.AddWithValue("@ProjectId", projectIdExists);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool CheckEmployeIdExists(int empIdExists)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (
                    SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM Employees WHERE Id = @EmployeeId",
                        connection
                    )
                )
                {
                    command.Parameters.AddWithValue("@EmployeeId", empIdExists);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool CheckEmployeIdExistsInProject(int projectIdExists, int empIdExists)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (
                    SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM ProjectEmployeeMapping WHERE ProjectId = @ProjectId AND EmployeeId = @EmployeeId",
                        connection
                    )
                )
                {
                    command.Parameters.AddWithValue("@ProjectId", projectIdExists);
                    command.Parameters.AddWithValue("@EmployeeId", empIdExists);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
