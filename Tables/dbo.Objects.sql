CREATE TABLE [dbo].[Objects] (
    [Id]        TINYINT      NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [Latitude]  FLOAT (53)   NULL,
    [Longitude] FLOAT (53)   NULL,
    [Voltage]   TINYINT      NULL,
    [Type]      TINYINT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Objects_Type] FOREIGN KEY ([Type]) REFERENCES [dbo].[Type] ([Id]),
    CONSTRAINT [FK_Objects_Voltage] FOREIGN KEY ([Voltage]) REFERENCES [dbo].[Voltage] ([Id])
);

