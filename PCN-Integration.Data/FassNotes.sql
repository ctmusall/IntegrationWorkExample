CREATE TABLE [dbo].[FassNotes]
(
  [Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [PCNShortDescription] NVARCHAR(50) NULL, 
    [PCNLongDescription] NVARCHAR(50) NULL, 
    [MapToFassCancellationReason] NVARCHAR(50) NULL, 
    [Status] NVARCHAR(50) NULL
)