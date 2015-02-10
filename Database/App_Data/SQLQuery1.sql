
CREATE TABLE dbo.People 
( 
  [id]     INT         NOT NULL IDENTITY, 
  [FirstName] VARCHAR(30) NOT NULL, 
  [MiddleName] VARCHAR(30) NULL, 
  [LastName]  VARCHAR(30) NOT NULL 
);

use Exam70483Database
Insert into dbo.People values(1,'Stev','Smith','JD');