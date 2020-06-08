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

insert into BakeryType values ('Cakes',1)
insert into BakeryType values ('Breads',1)
insert into BakeryType values ('Desserts',1)

insert into Bakery values (1,'Butter Cake',30000,4,'Different types of cake batter within the butter cake family include chocolate, white, yellow and marble; for white and yellow cakes coloring typically depends on whether they have whole eggs, or extra egg yolks in them (yellow cake) or egg whites only (white cake).',1)
insert into Bakery values (1,'Pound Cake',45000,5,'These cakes are usually very lightly flavored and served plain or topped with a simple glaze or water icing. A pound cake is usually baked in a loaf or Bundt pan. Many coffee cakes, sour cream cakes, and fruit crumb cakes are variations of pound cake.',1)
insert into Bakery values (1,'Angel Food Cake',40000,4,'Most angel food cakes have a spongy, chewy quality derived from their relatively high sugar content and the absence of egg yolks. Baked in ungreased two-piece tube pans, angel food cakes are cooled by being inverted, since this type of cake would collapse if cooled right-side-up in the pan or if removed from the pan while still warm. There is also no butter here, so the cake is fat free.',1)
insert into Bakery values (1,'Red Velvet Cake',50000,5,'You might have heard the cake referred to as the $200 cake — legend has it that the red velvet cake was first baked in the 1920s by a chef at the Waldorf-Astoria. A guest was so taken with the cake that she wrote the chef, asking for the recipe — along with a bill, hence it has other name. Whatever you call it, it is delicious.',1)
insert into Bakery values (1,'Carrot Cake',27000,4,'uses the leavening practices of butter cake, but instead of butter uses a neutral oil like vegetable or canola oil. For this reason, it will keep a little longer than butter cakes but can sometimes come out on the greasy side.',1)

insert into Bakery values (2,'Baguette',20000,4,'Nothing else in the bread family, not even the wonderfully flaky croissant, conjures images of the Eiffel Tower and all things French the way the baguette does. The long, stick-like loaf, also called French bread (thanks to its origins), is made with flour, yeast, water, and salt.',1)
insert into Bakery values (2,'Breadstick',18000,4,'Would it really be an Italian meal without a serving of this pencil-thin dry bread sitting atop the table as an appetizer? Much smaller than a baguette, breadsticks are said to have originated in the boot-shaped country in the 17th century. Nowadays, American restaurants sometimes serve them soft and warm, topped with cheese and garlic, or as a dessert, with icing and cinnamon.',1)
insert into Bakery values (2,'Pumpernickel',27000,5,'A type of rye bread, flavorful pumpernickel hails from Germany, where it’s made with coarsely ground whole rye berries. The traditional version requires a lot of patience to create, since the recipe calls for baking pumpernickel at a low temperature for as long as 24 hours.',1)

insert into Bakery values (3,'Macaron',35000,4,'This easy French macaron recipe makes a batch of the most dainty, delicate, and delicious cookies that will float right into your mouth and disappear.',1)
insert into Bakery values (3,'Benne Wafer',27000,5,'Benne Wafers are thin, crispy cookies made of toasted sesame and the taste is reminiscent of almond or caramel. The dessert has been a specialty of South Carolina Low Country since Colonial times after being brought to the colonies from East Africa. "Benne" is the Bantu word for "sesame," and the seeds were planted throughout the south.',1)
insert into Bakery values (3,'Fortune Cookie',27000,5,'Fortune cookies are crisp, sugar cookies made from flour, sugar, vanilla and sesame seed oil, and are most often found thrown into the bottom of your Chinese takeout bag. But it was not actually created in China.',1)

insert into Bakery values (4,'Normal Chicken Sandwich',27000,5,'Sandwiches are one of the preferable breakfasts. The speciality of the chicken sandwich is that it has deep fried chicken along with lots of cheese which give it an amazing touch and taste as well. The sandwich has lots of veggies in it which makes them healthy also.',1)
insert into Bakery values (4,'Egg Sandwich',27000,5,'Eggs are very rich in protein, but having just boiled egg in breakfast is quite boring. So, have egg sandwiches instead of that boring egg. The speciality of eggs sandwich is that it has deep fried egg along with lots of cheese which give it an amazing touch and taste as well. The sandwich has lots of veggies in it which makes them healthy also. The sandwich is yummy in taste.',1)
insert into Bakery values (4,'Grilled Cheese Sandwich',27000,5,'We all love cheese by the depth of our heart. The speciality of grilled cheese sandwich is that it has grilled cheese instead of normal cheese which gives it an amazing touch and taste as well. The sandwich has lots of veggies in it which makes them healthy also.',1)

INSERT INTO Bakery 
SELECT 1,'Butter Cake',30000,4,'Different types of cake batter within the butter cake family include chocolate, white, yellow and marble; for white and yellow cakes coloring typically depends on whether they have whole eggs, or extra egg yolks in them (yellow cake) or egg whites only (white cake).',1, BulkColumn, 100, '20200505', 20 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\buttercake.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 1,'Pound Cake',45000,5,'These cakes are usually very lightly flavored and served plain or topped with a simple glaze or water icing. A pound cake is usually baked in a loaf or Bundt pan. Many coffee cakes, sour cream cakes, and fruit crumb cakes are variations of pound cake.',1, BulkColumn, 100, '20200305', 25  
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\poundcake.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 1,'Angel Food Cake',40000,4,'Most angel food cakes have a spongy, chewy quality derived from their relatively high sugar content and the absence of egg yolks. Baked in ungreased two-piece tube pans, angel food cakes are cooled by being inverted, since this type of cake would collapse if cooled right-side-up in the pan or if removed from the pan while still warm. There is also no butter here, so the cake is fat free.',1, BulkColumn, 100, '20200205', 0 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\angelfoodcake.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 1,'Red Velvet Cake',50000,5,'You might have heard the cake referred to as the $200 cake — legend has it that the red velvet cake was first baked in the 1920s by a chef at the Waldorf-Astoria. A guest was so taken with the cake that she wrote the chef, asking for the recipe — along with a bill, hence it has other name. Whatever you call it, it is delicious.',1, BulkColumn, 100, '20200505', 0 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\redvelvetcake.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 1,'Carrot Cake',27000,4,'uses the leavening practices of butter cake, but instead of butter uses a neutral oil like vegetable or canola oil. For this reason, it will keep a little longer than butter cakes but can sometimes come out on the greasy side.',1, BulkColumn, 100, '20200505', 0
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\carrotcake.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 2,'Baguette',20000,4,'Nothing else in the bread family, not even the wonderfully flaky croissant, conjures images of the Eiffel Tower and all things French the way the baguette does. The long, stick-like loaf, also called French bread (thanks to its origins), is made with flour, yeast, water, and salt.',1, BulkColumn, 100, '20200505', 0 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\img1.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 2,'Breadstick',18000,4,'Would it really be an Italian meal without a serving of this pencil-thin dry bread sitting atop the table as an appetizer? Much smaller than a baguette, breadsticks are said to have originated in the boot-shaped country in the 17th century. Nowadays, American restaurants sometimes serve them soft and warm, topped with cheese and garlic, or as a dessert, with icing and cinnamon.',1, BulkColumn, 100, '20200505', 0 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\img2.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 2,'Pumpernickel',27000,5,'A type of rye bread, flavorful pumpernickel hails from Germany, where it’s made with coarsely ground whole rye berries. The traditional version requires a lot of patience to create, since the recipe calls for baking pumpernickel at a low temperature for as long as 24 hours.',1, BulkColumn, 100, '20200505', 0
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\img3.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 3,'Macaron',35000,4,'This easy French macaron recipe makes a batch of the most dainty, delicate, and delicious cookies that will float right into your mouth and disappear.',1, BulkColumn, 100, '20200505', 0 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\img4.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 3,'Benne Wafer',27000,5,'Benne Wafers are thin, crispy cookies made of toasted sesame and the taste is reminiscent of almond or caramel. The dessert has been a specialty of South Carolina Low Country since Colonial times after being brought to the colonies from East Africa. "Benne" is the Bantu word for "sesame," and the seeds were planted throughout the south.',1, BulkColumn, 100, '20200505', 30 
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\img5.jpg', Single_Blob) as image

INSERT INTO Bakery 
SELECT 3,'Fortune Cookie',27000,5,'Fortune cookies are crisp, sugar cookies made from flour, sugar, vanilla and sesame seed oil, and are most often found thrown into the bottom of your Chinese takeout bag. But it was not actually created in China.',1, BulkColumn, 100, '20200505', 20
FROM Openrowset( Bulk 'C:\Users\USER\Desktop\image\img6.jpg', Single_Blob) as image
