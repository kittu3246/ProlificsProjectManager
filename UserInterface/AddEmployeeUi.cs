// using Employee_Details;
// using System;
// using System.Text.RegularExpressions;
// using Role;
// using Employee;

// namespace AddEmployee
// {
//     using System;
//     using System.Linq;
//     using System.Text.RegularExpressions;

//     public class AddEmployeeUI
//     {
//         static  EmployeeMethods employeeMethods = new EmployeeMethods();
//         static RolesMethods rolesMethods = new RolesMethods();

//         /// Adds a new employee to the system.

//         /// <returns>The employee details if added successfully, null otherwise.</returns>
//         public static EmployeeDetailsProps AddEmployeeUi()
//         {
//             int id;
//             while (true)
//             {
//                 Console.WriteLine("Enter Employee Id");
//                 if (!int.TryParse(Console.ReadLine(), out id))
//                 {
//                     Console.WriteLine("Invalid input for Employee Id. Please enter a number.");
//                     continue;
//                 }

//                 bool isValid = EmployeeMethods.list.Any(a => a.Id == id);
//                 if (isValid)
//                 {
//                     Console.WriteLine("Employee Id already exists.");
//                     continue;
//                 }

//                 Console.Write("Enter Employee First Name : ");
//                 string firstName = Console.ReadLine();

//                 Console.Write("Enter Employee Last Name : ");
//                 string lastName = Console.ReadLine();

//                 string email = null;
//                 while (true)
//                 {
//                     Console.Write("Enter Employee Email : ");
//                     email = Console.ReadLine();

//                     if (!IsValidEmail(email))
//                     {
//                         Console.WriteLine("Employee Email is not valid.");
//                     }
//                     else
//                     {
//                         break;
//                     }
//                 }

//                 string inputPhoneNumber = null;
//                 while (true)
//                 {
//                     Console.Write("Enter Employee Phone Number : ");
//                     inputPhoneNumber = Console.ReadLine();

//                     if (inputPhoneNumber.Length != 10)
//                     {
//                         Console.WriteLine("Please enter a valid phone number.");
//                     }
//                     else
//                     {
//                         break;
//                     }
//                 }

//                 Console.Write("Enter Employee Address : ");
//                 string employeeAddress = Console.ReadLine();

//                 int rollId;
//                 while (true)
//                 {
//                     Console.Write("Enter Roll Id : ");
//                     if (!int.TryParse(Console.ReadLine(), out rollId))
//                     {
//                         Console.WriteLine("Invalid input for Roll Id. Please enter a number.");
//                         continue;
//                     }

//                     if (rolesMethods.CheckRolesExists())
//                     {
//                         if (rolesMethods.CheckRollIdExists(rollId))
//                         {
//                             EmployeeDetailsProps employeeDetails = new EmployeeDetailsProps
//                             {
//                                 Id = id,
//                                 FirstName = firstName,
//                                 LastName = lastName,
//                                 PhoneNumber = inputPhoneNumber,
//                                 Email = email,
//                                 EmployeeAddress = employeeAddress,
//                                 RollId = rollId
//                             };

//                             return employeeDetails;
//                         }
//                         else
//                         {
//                             Console.WriteLine("Enter a valid Roll Id");
//                             continue;
//                         }
//                     }
//                     else
//                     {
//                         Console.WriteLine("First add roles and add employee.\nThere are no roles present.");
//                     }
//                     break;
//                 }
//                 break;
//             }
//             return null;
//         }


//         /// Validates the format of an email address.

//         /// <param name="email">The email address to validate.</param>
//         /// <returns>True if the email is valid, false otherwise.</returns>
//         private static bool IsValidEmail(string email)
//         {
//             string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
//             return Regex.IsMatch(email, emailPattern);
//         }
//     }

// }





using System;
using System.Linq;
using System.Text.RegularExpressions;
using Employee_Details;
using Role;
using Employee;

namespace AddEmployee
{
    public class AddEmployeeUI
    {
        private static EmployeeMethods employeeMethods = new EmployeeMethods();
        private static RolesMethods rolesMethods = new RolesMethods();

        /// Adds a new employee to the system.

        /// <returns>The employee details if added successfully, null otherwise.</returns>
        public static EmployeeDetailsProps AddEmployeeUi()
        {
            int id;
            while (true)
            {
                Console.WriteLine("Enter Employee Id");
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid input for Employee Id. Please enter a number.");
                    continue;
                }

                if (employeeMethods.CheckEmployeIdExists(id))
                {
                    Console.WriteLine("Employee Id already exists.");
                    continue;
                }

                Console.Write("Enter Employee First Name : ");
                string firstName = Console.ReadLine();

                Console.Write("Enter Employee Last Name : ");
                string lastName = Console.ReadLine();

                string email = null;
                while (true)
                {
                    Console.Write("Enter Employee Email : ");
                    email = Console.ReadLine();

                    if (!IsValidEmail(email))
                    {
                        Console.WriteLine("Employee Email is not valid.");
                    }
                    else
                    {
                        break;
                    }
                }

                string inputPhoneNumber = null;
                while (true)
                {
                    Console.Write("Enter Employee Phone Number : ");
                    inputPhoneNumber = Console.ReadLine();

                    if (inputPhoneNumber.Length != 10)
                    {
                        Console.WriteLine("Please enter a valid phone number.");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Write("Enter Employee Address : ");
                string employeeAddress = Console.ReadLine();

                int rollId;
                while (true)
                {
                    Console.Write("Enter Roll Id : ");
                    if (!int.TryParse(Console.ReadLine(), out rollId))
                    {
                        Console.WriteLine("Invalid input for Roll Id. Please enter a number.");
                        continue;
                    }

                    if (rolesMethods.CheckRolesExists())
                    {
                        if (rolesMethods.CheckRollIdExists(rollId))
                        {
                            EmployeeDetailsProps employeeDetails = new EmployeeDetailsProps
                            {
                                Id = id,
                                FirstName = firstName,
                                LastName = lastName,
                                PhoneNumber = inputPhoneNumber,
                                Email = email,
                                EmployeeAddress = employeeAddress,
                                RollId = rollId
                            };

                            return employeeDetails;
                        }
                        else
                        {
                            Console.WriteLine("Enter a valid Roll Id");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("First add roles and add employee.\nThere are no roles present.");
                    }
                    break;
                }
                break;
            }
            return null;
        }

        /// Validates the format of an email address.

        /// <param name="email">The email address to validate.</param>
        /// <returns>True if the email is valid, false otherwise.</returns>
        private static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
