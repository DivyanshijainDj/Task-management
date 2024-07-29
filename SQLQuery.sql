
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(50) NOT NULL, -- 'Employee', 'Manager', 'Admin'
    TeamID INT NULL
    
);

-- Create Teams Table
CREATE TABLE Teams (
    TeamID INT IDENTITY(1,1) PRIMARY KEY,
    TeamName NVARCHAR(100) NOT NULL,
    ManagerID INT NULL,
    CONSTRAINT FK_Manager_Team FOREIGN KEY (ManagerID) REFERENCES Users(UserID)
);

-- Create Tasks Table
CREATE TABLE Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL, -- 'Pending', 'InProgress', 'Completed'
    DueDate DATETIME,
    AssignedUserID INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Task_User FOREIGN KEY (AssignedUserID) REFERENCES Users(UserID)
);

-- Create Reports Table
CREATE TABLE Reports (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    ReportDate DATETIME NOT NULL DEFAULT GETDATE(),
    TeamID INT,
    CompletedTasks INT,
    PendingTasks INT,
    OverdueTasks INT,
    CONSTRAINT FK_Report_Team FOREIGN KEY (TeamID) REFERENCES Teams(TeamID)
);


ALTER TABLE Users
ADD CONSTRAINT FK_Team_User
FOREIGN KEY (TeamID) REFERENCES Teams(TeamID);