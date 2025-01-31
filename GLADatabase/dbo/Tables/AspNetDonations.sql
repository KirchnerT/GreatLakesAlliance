﻿CREATE TABLE [dbo].[AspNetDonations] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [cardNumber]     NVARCHAR (30)  NOT NULL,
    [expirationDate] NVARCHAR (10)  NOT NULL,
    [ccv]            NVARCHAR (3)   NOT NULL,
    [amount]         INT            NOT NULL,
    [eventId]        INT            NULL,
    [userId]         NVARCHAR (128) NOT NULL,
    [fullName]       NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);





