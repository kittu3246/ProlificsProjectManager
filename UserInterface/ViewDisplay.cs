using ConsoleTables;
using Project;

namespace View
{
    public  class ViewDisplayUI
    {
        static ProjectMethods projectMethods = new ProjectMethods();
        public static void ViewDisplay()
        {
            ConsoleTable consoleTable = projectMethods.ListAll();
            System.Console.WriteLine(consoleTable);
        }
    }
}
