CREATE TABLE [dbo].[AspNetVolunteeredEvents] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [UserId]  NVARCHAR (MAX) NOT NULL,
    [EventId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

