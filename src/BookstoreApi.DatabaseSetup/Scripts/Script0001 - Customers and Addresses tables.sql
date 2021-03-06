CREATE TABLE [Customers]
(
	[CustomerId]	INT IDENTITY(1, 1),
	[FirstName]		NVARCHAR(50) NOT NULL,
	[LastName]		NVARCHAR(50) NOT NULL,
	[Phone]			VARCHAR(20),
	[Email]			VARCHAR(50),
	[CreatedAt]		DATETIMEOFFSET,
	[UpdatedAt]		DATETIMEOFFSET

	CONSTRAINT PK_Customers PRIMARY KEY CLUSTERED ([CustomerId])
)

CREATE TABLE [Addresses]
(
	[AddressId]		INT IDENTITY(1, 1),
	[CustomerId]	INT NOT NULL,
	[Name]			NVARCHAR(30),
	[Line1]			NVARCHAR(100),
	[Line2]			NVARCHAR(100) NULL,
	[Postcode]		CHAR(5),
	[IsDefault]		BIT,
	[CreatedAt]		DATETIMEOFFSET,
	[UpdatedAt]		DATETIMEOFFSET

	CONSTRAINT PK_Addresses PRIMARY KEY CLUSTERED ([AddressId])
	
	CONSTRAINT FK_Addresses_CustomerId_Customers FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId])
)

GO