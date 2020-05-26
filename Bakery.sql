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

Create table BakeryType
(
	ID int Identity(1,1) primary key,
	Name nvarchar(max),
	Status bit,
);

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

Create table Order
(
	ID int Identity(1,1) primary key,
	IDCustomer int,
	CreateDate datetime,
	Note nvarchar(max),
	Status int,

	Foreign key (IDCustomer) references Customer(ID),
);

Create table OrderDetail
(
	IDOrder int,
	IDBakery int,
	Quantity int,
	Total bigint,
	Status int,

	Constraint orderdetail_pk Primary key (IDOrder,IDBakery),
	Foreign key (IDOrder) references Order(ID),
	Foreign key (IDBakery) references Bakery(ID),
);

Create table Delivery
(

);
