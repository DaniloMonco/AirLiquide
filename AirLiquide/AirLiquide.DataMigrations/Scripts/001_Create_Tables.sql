use AirLiquide
GO
Create table Customer
(
	id uniqueidentifier not null primary key,
	name varchar(100) not null,
	age int not null
)