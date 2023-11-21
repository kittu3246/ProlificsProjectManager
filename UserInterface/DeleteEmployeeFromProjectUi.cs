
using AddEmployeToProject;


namespace DeleteEmployee
{
    public class DeleteEmployeeFromProject
    {
        
        /// Deletes an employee from a project by prompting the user to enter an employee ID and a project ID.
        /// Validates the input and checks if the entered IDs exist in the list of employee-project mappings.
        /// Continues to prompt the user until valid IDs are entered.
        
        /// <returns>A boolean indicating whether the employee was successfully removed from the project.</returns>
        public static bool DeleteEmployeFromProject()
        {
            int employeeIdToRemove;
            int projectIdToRemove;
            while (true)
            {
                System.Console.Write("Enter EmployeeId: ");
                if (!int.TryParse(Console.ReadLine(), out employeeIdToRemove))
                {
                    System.Console.Write("Enter a valid employee ID: ");
                    continue;
                }
                System.Console.Write("Enter ProjectId: ");
                if (!int.TryParse(Console.ReadLine(), out projectIdToRemove))
                {
                    System.Console.Write("Enter a valid project ID: ");
                    continue;
                }
                bool employeeProjectMappingIsPresent = AddEmployeeToProjectMethods.addEmployeeToProjectslist.Any(a => a.EmployeeId == employeeIdToRemove && a.ProjectId == projectIdToRemove);
                if (!employeeProjectMappingIsPresent)
                {
                    System.Console.WriteLine("Entered employee-project mapping is not present");
                    continue;
                }
                

                break;
            }
            System.Console.WriteLine("Employee removed from project successfully...");

            return true;
        }
    }
}