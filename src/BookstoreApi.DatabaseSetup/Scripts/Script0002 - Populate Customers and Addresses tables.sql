INSERT INTO [Customers]
(
	[FirstName], [LastName], [Phone], [Email], [CreatedAt], [UpdatedAt]
)
VALUES
(
	'David', 'Goodenough', '0615372819', 'david.goodenough@gmail.com', '2020-01-01', '2020-02-02'
)

INSERT INTO [Addresses]
(
	[CustomerId], [Name], [Line1], [Line2], [IsDefault], [CreatedAt], [UpdatedAt]
)
VALUES
(
	1, 'Maison', '14, RUE AZERTY', NULL, 1, '2020-01-01', '2020-02-02'
)

GO