CREATE TABLE [dbo].[FassOrders]
(
    [OrderId] NVARCHAR(50) NOT NULL, 
    [Status] VARCHAR(50) NULL, 
    [DateTimeLastUpdated] DATETIME NULL, 
    [StatusLastUpdated] VARCHAR(50) NULL, 
    [DateTimeCreated] DATETIME NULL, 
    [DateTimeCompleted] DATETIME NULL, 
    [CustomerId] NVARCHAR(50) NULL, 
    [FileNo] NVARCHAR(50) NULL, 
    PRIMARY KEY ([OrderId]) 
)
