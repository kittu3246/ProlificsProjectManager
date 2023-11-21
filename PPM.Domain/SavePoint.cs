using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using ProjectModel;
using Roles;
using AddEmployeeToProjectProps;
using Employee_Details;
using Project;
using Role;
using AddEmployeToProject;
using Employee;


namespace SavePoint
{
    public class AppDataSerializer
    {
        private static string connectionString =
            "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";

        public static void SerializeData(
            string projectData,
            string employeeData,
            string roleData,
            string employeeProjectData
        )
        {
            List<ProjectProperties> projects = GetProjectsFromDatabase();
            List<EmployeeDetailsProps> employees = GetEmployeesFromDatabase();
            List<RolesProperties> roles = GetRolesFromDatabase();
            
            List<AddEmployeeToProjectProperties> employeeProjects = GetEmployeeProjectsFromDatabase();

            try
            {
                using (var writer = new StreamWriter(projectData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<ProjectProperties>));
                    serializer.Serialize(writer, projects);
                }

                using (var writer = new StreamWriter(employeeData))
                {
                    XmlSerializer serializer = new XmlSerializer(
                        typeof(List<EmployeeDetailsProps>)
                    );
                    serializer.Serialize(writer, employees);
                }

                using (var writer = new StreamWriter(roleData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<RolesProperties>));
                    serializer.Serialize(writer, roles);
                }

                using (var writer = new StreamWriter(employeeProjectData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<AddEmployeeToProjectProperties>));
                    serializer.Serialize(writer, employeeProjects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while serializing data: " + ex.Message);
            }
        }

        private static List<ProjectProperties> GetProjectsFromDatabase()
        {
            List<ProjectProperties> projects = new List<ProjectProperties>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Projects";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProjectProperties project = new ProjectProperties
                        {
                            Id = (int)reader["ProjectId"],
                            Name = reader["ProjectName"].ToString(),
                            StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                            EndDate = DateTime.Parse(reader["EndDate"].ToString())
                        };

                        projects.Add(project);
                    }
                }
            }

            return projects;
        }

        private static List<EmployeeDetailsProps> GetEmployeesFromDatabase()
        {
            List<EmployeeDetailsProps> employees = new List<EmployeeDetailsProps>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Employees";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EmployeeDetailsProps employee = new EmployeeDetailsProps
                        {
                            Id = (int)reader["Id"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            EmployeeAddress = reader["EmployeeAddress"].ToString(),
                            RollId = (int)reader["RollId"]
                        };
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        private static List<RolesProperties> GetRolesFromDatabase()
        {
            List<RolesProperties> roles = new List<RolesProperties>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Roles";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RolesProperties role = new RolesProperties
                        {
                            RollId = (int)reader["RollId"],
                            RollName = reader["RollName"].ToString()
                        };
                        roles.Add(role);
                    }
                }
            }

            return roles;
        }


        private static List<AddEmployeeToProjectProperties> GetEmployeeProjectsFromDatabase()
        {
            List<AddEmployeeToProjectProperties> employeeProjects = new List<AddEmployeeToProjectProperties>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM ProjectEmployeeMapping";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AddEmployeeToProjectProperties employeeProject = new AddEmployeeToProjectProperties
                        {
                            // Assuming the structure of your AddEmployeeToProjectProperties class
                            ProjectId = (int)reader["ProjectId"],
                            EmployeeId = (int)reader["EmployeeId"]
                        };
                        employeeProjects.Add(employeeProject);
                    }
                }
            }

            return employeeProjects;
        }


        public static void SaveAppData()
        {
            string projectPath =
                "C:\\Users\\SPotharaju\\Desktop\\ProlificsProjectManager\\solutions\\.vscode\\SerializeData\\SerializeProjectMethodsData.xml";
            string employeePath =
                "C:\\Users\\SPotharaju\\Desktop\\ProlificsProjectManager\\solutions\\.vscode\\SerializeData\\SerializeEmployeMethodsData.xml";
            string rolePath =
                "C:\\Users\\SPotharaju\\Desktop\\ProlificsProjectManager\\solutions\\.vscode\\SerializeData\\SerializeRoleMethodsData.xml";
            string employeeProjectPath =
                "C:\\Users\\SPotharaju\\Desktop\\ProlificsProjectManager\\solutions\\.vscode\\SerializeData\\SerializeAddEmployeToProject.XML";

            AppDataSerializer.SerializeData(
                projectPath,
                employeePath,
                rolePath,
                employeeProjectPath
            );

            Console.WriteLine("Application data saved successfully.");
        }
    }
}
