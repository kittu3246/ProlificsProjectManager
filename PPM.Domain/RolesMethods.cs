using ConsoleTables;
using Roles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using IEntityOperation;

namespace Role
{
    public class RolesMethods : IEntity<RolesProperties>
    {
        private static string connectionString = "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";
        /// Adds a role to the database if the role ID doesn't already exist.
        /// <param name="roleObj">The role object to add.</param>
        public void Add(RolesProperties roleObj)
        {
            if (!CheckRollIdExists(roleObj.RollId))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Roles (RollId, RollName) VALUES (@RollId, @RollName)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@RollId", roleObj.RollId);
                        cmd.Parameters.AddWithValue("@RollName", roleObj.RollName);

                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Role Added Successfully....");
            }
            else
            {
                Console.WriteLine("Role with the same ID already exists.");
            }
        }

        /// Checks if a role ID already exists in the database.
        /// <param name="roleObj">The role object to check.</param>
        /// <returns>True if the role ID exists, false otherwise.</returns>
        public bool CheckRollId(RolesProperties roleObj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Roles WHERE RollId = @RollId";
                using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@RollId", roleObj.RollId);

                    int roleCount = (int)cmd.ExecuteScalar();
                    return roleCount > 0;
                }
            }
        }

        /// Displays a table of all roles from the database.
        public ConsoleTable ListAll()
        {
            var table = new ConsoleTable("RollId", "RollName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT RollId, RollName FROM Roles";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        table.AddRow(reader["RollId"], reader["RollName"]);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            table.Write();
            Console.ResetColor();

            return table;
        }

        /// Returns a role object with the specified ID from the database.
        /// <param name="rollId">The ID of the role to retrieve.</param>
        /// <returns>The console table with the role details.</returns>
        public ConsoleTable ListById(int rollId)
        {
            var table = new ConsoleTable("RollId", "RollName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT RollId, RollName FROM Roles WHERE RollId = @RollId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@RollId", rollId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        table.AddRow(reader["RollId"], reader["RollName"]);
                    }
                }
            }

            return table;
        }

        /// Deletes a role with the specified ID from the database.
        /// <param name="deleteByRollId">The ID of the role to delete.</param>
        /// <returns>True if the role was deleted, false otherwise.</returns>
        public bool DeleteById(int deleteByRollId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Roles WHERE RollId = @RollId";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@RollId", deleteByRollId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        /// Checks if any roles exist in the database.
        public bool CheckRolesExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Roles";
                using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                {
                    int rolesCount = (int)cmd.ExecuteScalar();
                    return rolesCount > 0;
                }
            }
        }

        /// Checks if a role with the specified ID exists in the database.
        /// <param name="rollId">The ID of the role to check.</param>
        /// <returns>True if the role ID exists, false otherwise.</returns>
        public bool CheckRollIdExists(int rollId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM Roles WHERE RollId = @RollId";
                using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@RollId", rollId);

                    int roleCount = (int)cmd.ExecuteScalar();
                    return roleCount > 0;
                }
            }
        }
    }
}
