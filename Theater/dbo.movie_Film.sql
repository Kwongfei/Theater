CREATE TABLE [dbo].[Table]
(
	[Film.id] NUMERIC(12) NOT NULL PRIMARY KEY, 
    [Film.name] NVARCHAR(50) NULL, 
    [Film.time] NVARCHAR(50) NULL, 
    [Film.actor] NVARCHAR(50) NULL, 
    [Film.director] NVARCHAR(MAX) NULL
)
