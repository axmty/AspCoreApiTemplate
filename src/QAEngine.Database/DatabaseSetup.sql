--------------------------------------------------------
--------------------------------------------------------
------------------------ SCHEMA ------------------------
--------------------------------------------------------
--------------------------------------------------------

USE [master]
GO

IF EXISTS(SELECT * FROM sys.databases WHERE NAME = 'QAEngine')
BEGIN
	ALTER DATABASE [QAEngine] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [QAEngine]
END
GO

CREATE DATABASE [QAEngine]
GO

USE [QAEngine]
GO

CREATE TABLE [dbo].[Question]
(
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](255) NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[IsClosed] [bit] NOT NULL
	CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
	(
		[QuestionID] ASC
	)
) ON [PRIMARY]
GO

--------------------------------------------------------
--------------------------------------------------------
------------------------- DATA -------------------------
--------------------------------------------------------
--------------------------------------------------------

INSERT INTO [dbo].[Question] ([Content], [CreateDate], [IsClosed]) VALUES
('This is question 1', '2019-01-01', 0),
('This is question 2', '2019-01-02', 0),
('This is question 3 that is closed', '2019-01-03', 1)
