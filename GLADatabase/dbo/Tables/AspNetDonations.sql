CREATE TABLE [dbo].[AspNetDonations] (
    [Id]             NVARCHAR (50) NOT NULL,
    [cardNumber]     NVARCHAR (30) NOT NULL,
    [expirationDate] NVARCHAR (10) NOT NULL,
    [ccv]            NVARCHAR (3)  NOT NULL,
    [amount]         INT           NOT NULL,
    [orgEvent]       NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

