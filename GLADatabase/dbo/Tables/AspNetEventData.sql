CREATE TABLE [dbo].[AspNetEventData] (
    [eventId]          NVARCHAR (128) NOT NULL,
    [eventName]        NVARCHAR (128) NOT NULL,
    [eventStartDate]   NVARCHAR (10)  NOT NULL,
    [eventEndDate]     NVARCHAR (10)  NULL,
    [volunteersNeeded] INT            NULL,
    [location]         NVARCHAR (256) NULL,
    [startTime]        NVARCHAR (10)  NULL,
    [endTime]          NVARCHAR (10)  NULL,
    [description]      NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([eventId] ASC)
);

