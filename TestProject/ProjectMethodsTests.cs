using NUnit.Framework;
using Project;
using ProjectModel;
using ConsoleTables;

namespace ProjectTests
{
    public class projectMethodsTests
    {

        ProjectMethods projectMethods = new ProjectMethods();
        [Test]
        // Adding a project to the list using AddProject method should add the project to the list.
        public void test_add_project_to_list()
        {
            // Arrange
            ProjectProperties project = new ProjectProperties()
            {
                Id = 1,
                Name = "Project 1",
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 31)
            };

            // Act
            projectMethods.Add(project);

            // Assert
            Assert.AreEqual(1, projectMethods.list.Count());
            Assert.AreEqual(project, projectMethods.list[0]);
        }
    

    [Test]
    // Calling ViewProject method should return a console table with all projects in the list.
    public void test_view_project()
    {
        // Arrange
        ProjectProperties project1 = new ProjectProperties()
        {
            Id = 1,
            Name = "Project 1",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 31)
        };
        ProjectProperties project2 = new ProjectProperties()
        {
            Id = 2,
            Name = "Project 2",
            StartDate = new DateTime(2022, 2, 1),
            EndDate = new DateTime(2022, 2, 28)
        };
        projectMethods.Add(project1);
        projectMethods.Add(project2);

        // Act
        ConsoleTable result = projectMethods.ViewProject();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Rows.Count);
        // Assert.AreEqual("ProjectId", result.Columns[0].Name);
        // Assert.AreEqual("ProjectName", result.Columns[1].Name);
        // Assert.AreEqual("Start Date", result.Columns[2].Name);
        // Assert.AreEqual("End Date", result.Columns[3].Name);
        // Assert.AreEqual(1, result.Rows[0][0]);
        // Assert.AreEqual("Project 1", result.Rows[0][1]);
        // Assert.AreEqual("01-01-2022", result.Rows[0][2]);
        // Assert.AreEqual("01-31-2022", result.Rows[0][3]);
        // Assert.AreEqual(2, result.Rows[1][0]);
        // Assert.AreEqual("Project 2", result.Rows[1][1]);
        // Assert.AreEqual("02-01-2022", result.Rows[1][2]);
        // Assert.AreEqual("02-28-2022", result.Rows[1][3]);
    }

    [Test]
    // Calling ListProjectById method with an existing project ID should return a console table with the project that matches the given ID.
    public void test_list_project_by_id_existing()
    {
        // Arrange
        ProjectProperties project1 = new ProjectProperties()
        {
            Id = 1,
            Name = "Project 1",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 31)
        };
        ProjectProperties project2 = new ProjectProperties()
        {
            Id = 2,
            Name = "Project 2",
            StartDate = new DateTime(2022, 2, 1),
            EndDate = new DateTime(2022, 2, 28)
        };
        projectMethods.AddProject(project1);
        projectMethods.AddProject(project2);

        // Act
        ConsoleTable result = projectMethods.ListProjectById(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Rows.Count);
        // Assert.AreEqual("ProjectId", result.Columns[0].Name);
        // Assert.AreEqual("ProjectName", result.Columns[1].Name);
        // Assert.AreEqual("Start Date", result.Columns[2].Name);
        // Assert.AreEqual("End Date", result.Columns[3].Name);
        // Assert.AreEqual(1, result.Rows[0][0]);
        // Assert.AreEqual("Project 1", result.Rows[0][1]);
        // Assert.AreEqual("01-01-2022", result.Rows[0][2]);
        // Assert.AreEqual("01-31-2022", result.Rows[0][3]);
    }

    [Test]
    // Calling AddProject method with a null project should not add the project to the list.
    public void test_add_project_null()
    {
        // Arrange
        ProjectProperties project = null;

        // Act
        projectMethods.AddProject(project);

        // Assert
        Assert.AreEqual(0, projectMethods.list.Count);
    }

    [Test]
    // Calling ListProjectById method with a non-existing project ID should return an empty console table.
    public void test_list_project_by_id_non_existing()
    {
        // Arrange
        ProjectProperties project1 = new ProjectProperties()
        {
            Id = 1,
            Name = "Project 1",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 31)
        };
        ProjectProperties project2 = new ProjectProperties()
        {
            Id = 2,
            Name = "Project 2",
            StartDate = new DateTime(2022, 2, 1),
            EndDate = new DateTime(2022, 2, 28)
        };
        projectMethods.AddProject(project1);
        projectMethods.AddProject(project2);

        // Act
        ConsoleTable result = projectMethods.ListProjectById(3);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Rows.Count);
    }

    [Test]
    // Calling DeleteProjectById method with a non-existing project ID should not delete any project from the list and return false.
    public void test_delete_project_by_id_non_existing()
    {
        // Arrange
        ProjectProperties project1 = new ProjectProperties()
        {
            Id = 1,
            Name = "Project 1",
            StartDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2022, 1, 31)
        };
        ProjectProperties project2 = new ProjectProperties()
        {
            Id = 2,
            Name = "Project 2",
            StartDate = new DateTime(2022, 2, 1),
            EndDate = new DateTime(2022, 2, 28)
        };
        projectMethods.AddProject(project1);
        projectMethods.AddProject(project2);

        // Act
        bool result = projectMethods.DeleteProjectById(3);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(2, projectMethods.list.Count);
    }
}
}