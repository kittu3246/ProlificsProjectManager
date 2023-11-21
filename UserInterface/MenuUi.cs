using ConsoleTables;
using Employee;
using AddProject;
using Project;
using AddEmployee;
using ProjectModel;
using AddRoles;
using Role;
using DeleteEmployee;
using AppData;
using SavePoint;
using System.Data.SqlClient;


namespace Menu
{
    /// The MenuUI class is responsible for displaying a menu to the user and handling the user's input based on the chosen option.
    /// It provides options for the user to interact with the Project and Employee modules.


    /// Main functionalities:
    /// - Display a menu with options for the user to choose from.
    /// - Handle the user's input and perform the corresponding actions based on the chosen option.
    /// - Provide options to interact with the Project and Employee modules.

    public static class MenuUI
    {
        public static void MenuMethod()
        {
        string connectionString = "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";
            
            var projectTable = new ConsoleTable("---------------Choose Option---------------");
            projectTable.AddRow("1. Add Project");
            projectTable.AddRow("2. List all projects");
            projectTable.AddRow("3. List Project By ID");
            projectTable.AddRow("4. Delete Project By Id");
            projectTable.AddRow("5. Add Employee to project");
            projectTable.AddRow("6. Delete employee from Project ");
            projectTable.AddRow("7. show all the project details");
            projectTable.AddRow("8. Return to main menu");

            var employeeTable = new ConsoleTable("--------------choose option---------------");
            employeeTable.AddRow("1. Add Employee");
            employeeTable.AddRow("2. List All Employees");
            employeeTable.AddRow("3. List By Employee ID");
            employeeTable.AddRow("4. Delete Employee By ID");
            employeeTable.AddRow("5. Main Menu");
            var roleTable = new ConsoleTable("-------------choose option----------------");
            roleTable.AddRow("1. Add Role");
            roleTable.AddRow("2. List All Roles");
            roleTable.AddRow("3. List By Role ID");
            roleTable.AddRow("4. Delete Role By ID");
            roleTable.AddRow("5. Main Menu");
            var table = new ConsoleTable("-----Menu Options-----");
            table.AddRow("1. Project Module");
            table.AddRow("2. Employee Module");
            table.AddRow("3. Role Module");
            table.AddRow("4. Save");
            table.AddRow("5.Deserialze Data");
            table.AddRow("6. Quit");

            bool returnMainMenu = false;
            while (!returnMainMenu)
            {
                System.Console.WriteLine(table);

                if (int.TryParse(Console.ReadLine(), out int chooseOption))
                {
                    switch (chooseOption)
                    {
                        case 1:
                            HandleProjectModule(projectTable);
                            break;
                        case 2:
                            HandleEmployeeModule(employeeTable);
                            break;
                        case 3:
                            HandleRoleModule(roleTable);
                            break;
                        case 4:
                            // AppDataSerializer serializer = new AppDataSerializer();
                            AppDataSerializer.SaveAppData();

                            break;
                        case 5:
                            // ProjectMethods projectMethods = new ProjectMethods();



                            // string projectDataFilePath = "C:\\Users\\SPotharaju\\Desktop\\ProlificsProjectManager\\solutions\\.vscode\\SerializeData\\SerializeProjectMethodsData.xml";
                            // DeserializeAppData deserializeAppData = new DeserializeAppData();
                            // ProjectMethods.list = DeserializeAppData.DeserializeAppDataBack(projectDataFilePath);
                            // System.Console.WriteLine(ProjectMethods.list);
                            break;
                        case 6:
                            returnMainMenu = true;
                            System.Console.WriteLine("Exiting...");
                            break;
                        default:
                            System.Console.WriteLine(
                                "Invalid option. Please choose a valid option."
                            );
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid integer option.");
                }
            }
        }

        private static void HandleProjectModule(ConsoleTable projectTable)
        {
            EmployeeMethods employeeMethods = new EmployeeMethods();
            ProjectMethods projectMethods = new ProjectMethods();
            string connectionString = "Server = RHJ-9F-D201\\SQLEXPRESS;DataBase = ProlificsProjectManager ; Integrated Security = SSPI;";

            bool returnToMenu = false;
            while (!returnToMenu)
            {
                System.Console.WriteLine(projectTable);

                if (int.TryParse(Console.ReadLine(), out int projectModuleOption))
                {
                    switch (projectModuleOption)
                    {
                        case 1:
                            ProjectProperties addProjectProperties = AddProjectUI.AddProjectUi();
                            if (!projectMethods.CheckProjectIdExists(addProjectProperties.Id))
                            {
                                System.Console.WriteLine("Project added successfully ...");
                                projectMethods.Add(addProjectProperties);
                            }
                            else
                            {
                                System.Console.WriteLine("ProjectId exists already");
                            }
                            System.Console.WriteLine(
                                "Do you want to add an employee with a role? (Y/N)"
                            );
                            string addEmployeeOption = Console.ReadLine();
                            if (addEmployeeOption.Equals("Y", StringComparison.OrdinalIgnoreCase))
                            {
                                int employeeIdToAdd;
                                while (true)
                                {
                                    System.Console.WriteLine("Enter employee Id to add to project");

                                    if (!int.TryParse(Console.ReadLine(), out employeeIdToAdd))
                                    {
                                        Console.WriteLine("enter integer employeeid");
                                        continue;
                                    }
                                    break;
                                }

                                bool existsEmployeeId = employeeMethods.CheckEmployeIdExists(
                                    employeeIdToAdd
                                );
                                if (existsEmployeeId)
                                {
                                    if (
                                        projectMethods.MapEmployeeToProject(
                                            addProjectProperties.Id,
                                            employeeIdToAdd
                                        )
                                    )
                                    {
                                        Console.WriteLine("mapped successfully ...");
                                    }
                                    else
                                    {
                                        Console.WriteLine("project Id does not exists cannot map");
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("Employee Id Does not exist");
                                }
                            }
                            break;
                        case 2:
                            ConsoleTable displayProjects = projectMethods.ListAll();
                            System.Console.WriteLine(displayProjects);
                            break;
                        case 3:
                            System.Console.WriteLine("Enter Project ID:");
                            if (int.TryParse(Console.ReadLine(), out int projectId))
                            {
                                ConsoleTable display = projectMethods.ListById(projectId);
                                if (display.Rows.Any())
                                {
                                    Console.WriteLine(display);
                                }
                                else
                                {
                                    System.Console.WriteLine(
                                        "No data present related to the given Project ID."
                                    );
                                }
                            }
                            else
                            {
                                System.Console.WriteLine(
                                    "Invalid input. Please enter a valid integer Project ID."
                                );
                            }
                            break;
                        case 4:
                            System.Console.WriteLine("Enter Project ID to delete:");
                            if (int.TryParse(Console.ReadLine(), out int deletionId))
                            {
                                if (projectMethods.DeleteById(deletionId))
                                {
                                    System.Console.WriteLine("Project ID deleted successfully.");
                                }
                                else
                                {
                                    System.Console.WriteLine("No project ID present.");
                                }
                            }
                            else
                            {
                                System.Console.WriteLine(
                                    "Invalid input. Please enter a valid integer Project ID."
                                );
                            }
                            break;
                        case 5:
                            var tableContents = projectMethods.ListAll();
                            if (tableContents.Rows.Any())
                            {
                                System.Console.WriteLine(tableContents);

                                int projectIdAdd;
                                while (true)
                                {
                                    System.Console.WriteLine("Enter ProjectId To add Employee");
                                    if (!int.TryParse(Console.ReadLine(), out projectIdAdd))
                                    {
                                        System.Console.WriteLine("Enter Integer Project Id");
                                        continue;
                                    }
                                    break;
                                }
                                var empTableContent = employeeMethods.ListAll();
                                if (empTableContent.Rows.Any())
                                {
                                    int employeIdToAdd;
                                    while (true)
                                    {
                                        System.Console.WriteLine("Enter EmployeeId To add");
                                        if (!int.TryParse(Console.ReadLine(), out employeIdToAdd))
                                        {
                                            System.Console.WriteLine("Enter Integer employee Id");
                                            continue;
                                        }
                                        break;
                                    }
                                    if (projectMethods.CheckProjectIdExists(projectIdAdd))
                                    {
                                        if (employeeMethods.CheckEmployeIdExists(employeIdToAdd))
                                        {
                                            if (
                                                projectMethods.CheckEmployeIdExistsInProject(
                                                    projectIdAdd,
                                                    employeIdToAdd
                                                )
                                            )
                                            {
                                                System.Console.WriteLine(
                                                    $"ALREADY {projectIdAdd} CONTAINS {employeIdToAdd} NOT POSSIBLE TO ADD. / EMPLOYEE ID DOES NOT EXIST"
                                                );
                                            }
                                            else
                                            {
                                                if (
                                                    projectMethods.MapEmployeeToProject(
                                                        projectIdAdd,
                                                        employeIdToAdd
                                                    )
                                                )
                                                {
                                                    System.Console.WriteLine(
                                                        "Mapped Successfully ..."
                                                    );
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine(
                                                        "ProjectId does not exists"
                                                    );
                                                }
                                            }
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("Employee Id does not exist");
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("ProjectId does not exist");
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine(
                                        "No employees present to add to project"
                                    );
                                }
                            }
                            else
                            {
                                System.Console.WriteLine(
                                    "No Projects to add employee put them on bench"
                                );
                            }

                            break;
                        case 6:
                            int employeeIdToDelete;
                            int projectIdToDelete;
                            while (true)
                            {
                                System.Console.WriteLine("Enter Project To delete");
                                if (!int.TryParse(Console.ReadLine(), out projectIdToDelete))
                                {
                                    System.Console.WriteLine("Enter Integer Project Id");
                                    continue;
                                }
                                break;
                            }
                            while (true)
                            {
                                System.Console.WriteLine("Enter EmployeeId To delete");
                                if (!int.TryParse(Console.ReadLine(), out employeeIdToDelete))
                                {
                                    System.Console.WriteLine("Enter Integer Employe Id");
                                    continue;
                                }
                                break;
                            }
                            if (
                                !projectMethods.DeleteEmployeeFromProject(
                                    projectIdToDelete,
                                    employeeIdToDelete
                                )
                            )
                            {
                                System.Console.WriteLine(
                                    $"In particular project {projectIdToDelete} particular id {employeeIdToDelete} is not present /Project Id does not exist"
                                );
                            }
                            else
                            {
                                System.Console.WriteLine("Deleted Successfully ...");
                            }
                            break;
                        // case 7:
                        // var projectdata = new ConsoleTable(
                        //     "Project Id ",
                        //     "Project Name",
                        //     "Project start date",
                        //     "project enddate",
                        //     "Number of Employees",
                        //     "Employe IDs assigned to project"
                        // );

                        // foreach (var projectMethodsList in ProjectMethods.list)
                        // {
                        //     string ids = "";
                        //     if (projectMethodsList.ProjectEmployesList.Count() > 0)
                        //     {
                        //         foreach (int id in projectMethodsList.ProjectEmployesList)
                        //         {
                        //             ids += id.ToString() + ",";
                        //         }
                        //     }
                        //     projectdata.AddRow(
                        //         projectMethodsList.Id,
                        //         projectMethodsList.Name,
                        //         projectMethodsList.StartDate,
                        //         projectMethodsList.EndDate,
                        //         projectMethodsList.ProjectEmployesList.Count(),
                        //         ids
                        //     );
                        // }
                        // System.Console.WriteLine(projectdata);


                        case 7:
                            var projectData = new ConsoleTable(
                                "Project Id",
                                "Project Name",
                                "Project Start Date",
                                "Project End Date",
                                "Number of Employees",
                                "Employee IDs Assigned to Project"
                            );

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                using (
                                    SqlCommand command = new SqlCommand(
                                        "SELECT P.ProjectId AS ProjectId, P.ProjectName AS ProjectName, P.StartDate AS ProjectStartDate, "
                                            + "P.EndDate AS ProjectEndDate, COUNT(PE.EmployeeId) AS NumberOfEmployees, "
                                            + "STRING_AGG(PE.EmployeeId, ',') AS EmployeeIds "
                                            + "FROM Projects P "
                                            + "LEFT JOIN ProjectEmployeeMapping PE ON P.ProjectId = PE.ProjectId "
                                            + "GROUP BY P.ProjectId, P.ProjectName, P.StartDate, P.EndDate",
                                        connection
                                    )
                                )
                                {
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            projectData.AddRow(
                                                reader["ProjectId"],
                                                reader["ProjectName"],
                                                reader["ProjectStartDate"],
                                                reader["ProjectEndDate"],
                                                reader["NumberOfEmployees"],
                                                reader["EmployeeIds"]
                                            );
                                        }
                                    }
                                }
                            }

                            System.Console.WriteLine(projectData);
                            break;

                          

                        case 8:
                            returnToMenu = true;
                            break;

                        default:
                            System.Console.WriteLine(
                                "Invalid option. Please choose a valid option."
                            );
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid integer option.");
                }
            }
        }

        private static void HandleEmployeeModule(ConsoleTable employeeTable)
        {
            EmployeeMethods employeeMethods = new EmployeeMethods();
            ProjectMethods projectMethods = new ProjectMethods();
            bool returnToMenu = false;
            while (!returnToMenu)
            {
                System.Console.WriteLine(employeeTable);

                if (int.TryParse(Console.ReadLine(), out int employeeModuleOption))
                {
                    switch (employeeModuleOption)
                    {
                        case 1:
                            var addEmployeeObj = AddEmployeeUI.AddEmployeeUi();
                            if (addEmployeeObj != null)
                            {
                                employeeMethods.Add(addEmployeeObj);
                            }
                            break;
                        case 2:
                            employeeMethods.ListAll();
                            break;
                        case 3:
                            System.Console.WriteLine("Enter Employee ID:");
                            if (int.TryParse(Console.ReadLine(), out int employeeId))
                            {
                                var employeeIdObj = employeeMethods.ListById(employeeId);
                                if (employeeIdObj.Rows.Any())
                                {
                                    System.Console.WriteLine(employeeIdObj);
                                }
                                else
                                {
                                    System.Console.WriteLine(
                                        "Details do not exist with the given ID."
                                    );
                                }
                            }
                            else
                            {
                                System.Console.WriteLine(
                                    "Invalid input. Please enter a valid integer Employee ID."
                                );
                            }
                            break;
                        case 4:
                            System.Console.WriteLine("Enter Employee ID to delete:");
                            if (int.TryParse(Console.ReadLine(), out int empId))
                            {
                                if (!projectMethods.CheckEmployeeMappedToProject(empId))
                                {
                                    if (employeeMethods.DeleteById(empId))
                                    {
                                        System.Console.WriteLine("Employee deleted successfully.");
                                    }
                                    else
                                    {
                                        System.Console.WriteLine(
                                            "Employee does not exist with the given Employee ID."
                                        );
                                    }
                                }
                                else{
                                    System.Console.WriteLine("Employee mapped to project cannot delete...");
                                }
                            }
                            else
                            {
                                System.Console.WriteLine(
                                    "Invalid input. Please enter a valid integer Employee ID."
                                );
                            }
                            break;
                        case 5:
                            returnToMenu = true;
                            break;
                        default:
                            System.Console.WriteLine(
                                "Invalid option. Please choose a valid option."
                            );
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid integer option.");
                }
            }
        }

        private static void HandleRoleModule(ConsoleTable roleTable)
        {
            EmployeeMethods employeeMethods = new EmployeeMethods();
            RolesMethods rolesMethods = new RolesMethods();

            bool returnToMenu = false;
            while (!returnToMenu)
            {
                System.Console.WriteLine(roleTable);

                if (int.TryParse(Console.ReadLine(), out int roleModuleOption))
                {
                    switch (roleModuleOption)
                    {
                        case 1:
                            if (AddRolesUI.AddRolesUi())
                            {
                                System.Console.WriteLine("role added successfully...");
                            }
                            break;
                        case 2:
                            var display = rolesMethods.ListAll();
                            System.Console.WriteLine("calling ViewRoles ...");
                            if (display != null)
                            {
                                System.Console.WriteLine(display);
                            }
                            else
                            {
                                System.Console.WriteLine("No Roles present ...");
                            }
                            break;
                        case 3:
                            int roleId;
                            Console.WriteLine("Enter Role Id");
                            if (!int.TryParse(Console.ReadLine(), out roleId))
                            {
                                Console.WriteLine(
                                    "Invalid input for Employee Id. Please enter a number."
                                );
                                continue;
                            }
                            var roleIdObj = rolesMethods.ListById(roleId);
                            if (roleIdObj != null)
                            {
                                System.Console.WriteLine(roleIdObj);
                            }
                            else
                            {
                                System.Console.WriteLine("RoleId does not exist..");
                            }

                            break;
                        case 4:
                            Console.WriteLine("enter role id to delete");

                            if (!int.TryParse(Console.ReadLine(), out int deleteRoleId))
                            {
                                Console.WriteLine("enter valid roleid as an integer");
                                continue;
                            }
                            if (employeeMethods.CheckRoleIdMatchedToEmployeeId(deleteRoleId) != 0)
                            {
                                Console.WriteLine(
                                    "Role id -->"
                                        + $"{deleteRoleId}"
                                        + "mapped to project you cannot delete.."
                                );
                            }
                            else
                            {
                                if (rolesMethods.DeleteById(deleteRoleId))
                                {
                                    Console.WriteLine($"{deleteRoleId}" + "deleted successfully..");
                                }
                                else
                                {
                                    Console.WriteLine(
                                        $"{deleteRoleId}" + "entered role id does not exsist"
                                    );
                                }
                            }
                            break;
                        case 5:
                            returnToMenu = true;
                            break;
                        default:
                            System.Console.WriteLine(
                                "Invalid option. Please choose a valid option."
                            );
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid integer option.");
                }
            }
        }
    }
}
