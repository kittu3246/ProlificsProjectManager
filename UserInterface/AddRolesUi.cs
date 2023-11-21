// using ProjectModel;
// using System;
// using Roles;
// using Role;

// namespace AddRoles
// {
//     public class AddRolesUI
//     {
//         static RolesMethods rolesMethods = new RolesMethods();

//         /// Prompts the user to enter a role ID and role name, performs the operation to add the roles, and returns true if the operation was successful.

//         /// <returns>True if the operation was successful, otherwise false.</returns>
//         public static bool AddRolesUi()
//         {
//             Console.WriteLine("Enter the Role Id");
//             int roleId;
//             while (!int.TryParse(Console.ReadLine(), out roleId))
//             {
//                 Console.WriteLine("Invalid Role Id. Please enter a valid integer value.");
//             }

//             Console.WriteLine("Enter the Role Name");
//             string roleName = Console.ReadLine();

//             // Perform the operation to add roles

//             if (!rolesMethods.CheckRollIdExists(roleId))
//             {
//                 RolesProperties roleObj = new RolesProperties
//                 {
//                     RollId = roleId,
//                     RollName = roleName
//                 };

//                 rolesMethods.Add(roleObj);
//                 return true;
//             }
//             else
//             {
//                 Console.WriteLine("Failed to add role.");
//             }

//             return false;

//             // Return true if the operation was successful
//         }
//     }
// }




using System;
using Roles;
using Role;
using System.Data.SqlClient; // Import the necessary namespace for SqlConnection

namespace AddRoles
{
    public class AddRolesUI
    {
        private static RolesMethods rolesMethods = new RolesMethods();

        // Modify this connection string according to your database configuration
        private static string connectionString = "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";

        /// Prompts the user to enter a role ID and role name, performs the operation to add the roles, and returns true if the operation was successful.

        /// <returns>True if the operation was successful, otherwise false.</returns>
        public static bool AddRolesUi()
        {
            Console.WriteLine("Enter the Role Id");
            int roleId;
            while (!int.TryParse(Console.ReadLine(), out roleId))
            {
                Console.WriteLine("Invalid Role Id. Please enter a valid integer value.");
            }

            Console.WriteLine("Enter the Role Name");
            string roleName = Console.ReadLine();

            if (!IsRoleExists(roleId))
            {
                RolesProperties roleObj = new RolesProperties
                {
                    RollId = roleId,
                    RollName = roleName
                };

                rolesMethods.Add(roleObj);
                Console.WriteLine("Role added successfully.");
                return true;
            }
            else
            {
                Console.WriteLine("Role with the same ID already exists. Failed to add role.");
            }

            return false;
        }

        /// Checks if a role with the given ID exists in the system.

        /// <param name="roleId">The role ID to check.</param>
        /// <returns>True if the role ID is valid, false otherwise.</returns>
        public static bool IsRoleExists(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM Roles WHERE RollId = {roleId}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
