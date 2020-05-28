CREATE DATABASE TMDT;
GO
USE TMDT;
GO
Create table Customer
(
	ID int Identity(1,1) primary key,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Phone nvarchar(11),
	Email nvarchar(max),
	Sex int,
	DateOfBirth date,
	Address nvarchar(max),

);
GO
Create table Account
(
	ID int Identity(1,1) primary key,
	Username nvarchar(150),
	Password nvarchar(150),
	IDCustomer int,
	Type int,
	Status int,
	
	Foreign key (IDCustomer) references Customer(ID),
);
GO
Create table BakeryType
(
	ID int Identity(1,1) primary key,
	Name nvarchar(max),
	Status bit,
);
GO
Create table Bakery
(
	ID int Identity(1,1) primary key,
	IDType int,
	Name nvarchar(max),
	Price bigint,
	Rating float,  -- Tao danh gia ?
	Describe nvarchar(max),
	Status int,

	Foreign key (IDType) references BakeryType(ID),
);
GO
Create table Orders
(
	ID int Identity(1,1) primary key,
	IDCustomer int,
	CreateDate datetime,
	Note nvarchar(max),
	Status int,

	Foreign key (IDCustomer) references Customer(ID),
);
GO
Create table OrderDetail
(
	IDOrder int,
	IDBakery int,
	Quantity int,
	Total bigint,
	Status int,

	Constraint orderdetail_pk Primary key (IDOrder,IDBakery),
	Foreign key (IDOrder) references Orders(ID),
	Foreign key (IDBakery) references Bakery(ID),
);

