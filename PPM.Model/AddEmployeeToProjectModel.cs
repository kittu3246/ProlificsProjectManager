namespace AddEmployeeToProjectProps
{

    /// Represents the properties of an employee being added to a project.

    public class AddEmployeeToProjectProperties
    {

        /// Gets or sets the ID of the project the employee is being added to.

        public int ProjectId { get; set; }


        /// Gets or sets the ID of the employee being added to the project.

        public int EmployeeId { get; set; }


        /// Gets or sets the first name of the employee.

        public string FirstName { get; set; }


        /// Gets or sets the last name of the employee.

        public string LastName { get; set; }
    }
}
