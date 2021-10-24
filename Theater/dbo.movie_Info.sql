CREATE TABLE [dbo].[movie_Info] (
    [movie_Name]     NVARCHAR (50)  NOT NULL,
    [movie_Director] NVARCHAR (50)  NULL,
    [movie_Actors]   NVARCHAR (50)  NULL,
    [movie_Type]     NVARCHAR (50)  NULL,
    [movie_Image]    NVARCHAR(50)          NULL,
    [movie_RelDate]  DATE           NULL,
    [movie_Intro]    NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([movie_Name] ASC)
);

