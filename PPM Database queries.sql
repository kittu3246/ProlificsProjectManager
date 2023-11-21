


CREATE TABLE Projects
(
    ProjectId INT PRIMARY KEY ,
    ProjectName NVARCHAR(255) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL
);
drop table Projects;
CREATE TABLE ProjectEmployeeMapping
(
    ProjectId INT,
    EmployeeId INT,
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    PRIMARY KEY (ProjectId, EmployeeId)
);
drop table ProjectEmployeeMapping;
CREATE TABLE Employees
(
    Id INT PRIMARY KEY ,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    EmployeeAddress NVARCHAR(255) NOT NULL,
    RollId INT NOT NULL
);

drop table Employees;

CREATE TABLE Roles
(
    RollId INT PRIMARY KEY,
    RollName NVARCHAR(255) NOT NULL
);


drop table Roles;
select * from Employees;
select * from Projects;
select * from Roles;
select * from ProjectEmployeeMapping;





ALTER TABLE ProjectEmployeeMapping
ADD CONSTRAINT FK_ProjectEmployeeMapping_Project
FOREIGN KEY (ProjectId) 
REFERENCES Projects(ProjectId)
ON DELETE CASCADE;

//retreiving all the employes to  projects assigned details query

 SELECT P.ProjectId AS ProjectId, P.ProjectName AS ProjectName, P.StartDate AS ProjectStartDate, "
                                            + "P.EndDate AS ProjectEndDate, COUNT(PE.EmployeeId) AS NumberOfEmployees, "
                                            + "STRING_AGG(PE.EmployeeId, ',') AS EmployeeIds "
                                            + "FROM Projects P "
                                            + "LEFT JOIN ProjectEmployeeMapping PE ON P.ProjectId = PE.ProjectId "
                                            + "GROUP BY P.ProjectId, P.ProjectName, P.StartDate, P.EndDate
