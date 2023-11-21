namespace ProjectModel
{
    public class ProjectProperties
    {
        // Properties
        public int Id { get; set; }            // Unique project ID
        public string Name { get; set; }       // Name of the project
        public DateTime StartDate { get; set; }  // Start date of the project
        public DateTime EndDate { get; set; }    // End date of the project

        public  List<int> ProjectEmployesList = new List<int>();
    }
}
