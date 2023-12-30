CREATE TABLE [dbo].[Students]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Firstname] NVARCHAR(50) NOT NULL, 
    [Lastname] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATE NOT NULL, 
    [DateCreatedUtc] DATETIMEOFFSET NOT NULL, 
    [DateLastUpdatedUtc] DATETIMEOFFSET NULL, 
    [RowVersion] ROWVERSION NOT NULL
)
