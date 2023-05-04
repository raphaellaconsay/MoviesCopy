CREATE TABLE [dbo].[Movie]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Director] NVARCHAR(50) NOT NULL, 
    [Duration] INT NOT NULL, 
    [ReleaseDate] DATETIME NOT NULL, 
    [Rate] INT NOT NULL
)
