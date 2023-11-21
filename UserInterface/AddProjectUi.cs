using ProjectModel;
using System;

namespace AddProject
{
    public class AddProjectUI
    {
       
        /// Prompts the user to enter the project ID, name, start date, and end date. Validates the input and returns a ProjectProperties object with the entered values.
       
        /// <returns>A ProjectProperties object with the entered values.</returns>
        public static ProjectProperties AddProjectUi()
        {
            int projectId = GetValidIntegerInput("Enter Project Id: ");
            string projectName = GetStringInput("Enter Project Name: ");
            DateTime startDate = GetValidDateInput("Enter Project StartDate in the format MM/DD/YYYY: ");
            DateTime endDate = GetValidEndDateInput(startDate);

            ProjectProperties projectProperties = new ProjectProperties
            {
                Id = projectId,
                Name = projectName,
                StartDate = startDate,
                EndDate = endDate
            };

           
            return projectProperties;
        }

       
        /// Prompts the user to enter an integer value and validates the input.
        
        /// <param name="prompt">The prompt message to display to the user.</param>
        /// <returns>The valid integer input entered by the user.</returns>
        private static int GetValidIntegerInput(string prompt)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }
                break;
            }
            return input;
        }

      
        /// Prompts the user to enter a string value.
        
        /// <param name="prompt">The prompt message to display to the user.</param>
        /// <returns>The string input entered by the user.</returns>
        private static string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        
        /// Prompts the user to enter a date in the format MM/DD/YYYY and validates the input.
      
        /// <param name="prompt">The prompt message to display to the user.</param>
        /// <returns>The valid date input entered by the user.</returns>
        private static DateTime GetValidDateInput(string prompt)
        {
            DateTime input;
            while (true)
            {
                Console.Write(prompt);
                if (!DateTime.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid date format. Please use MM/DD/YYYY.");
                    continue;
                }
                break;
            }
            return input;
        }

        
        /// Prompts the user to enter an end date and validates the input, ensuring that it is greater than the start date.
       
        /// <param name="startDate">The start date of the project.</param>
        /// <returns>The valid end date input entered by the user.</returns>
        private static DateTime GetValidEndDateInput(DateTime startDate)
        {
            DateTime endDate;
            while (true)
            {
                endDate = GetValidDateInput("Enter Project EndDate in the format MM/DD/YYYY: ");
                if (endDate <= startDate)
                {
                    Console.WriteLine("End date must be greater than the start date.");
                    continue;
                }
                break;
            }
            return endDate;
        }
    
       
    }

}