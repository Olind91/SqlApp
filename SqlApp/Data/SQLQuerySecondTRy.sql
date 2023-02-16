CREATE TABLE Addresses (
  Id int not null identity(1,1) primary key,
  StreetName varchar(50) not null,
  StreetNumber varchar (150) null,
  PostalCode char(5) not null,
  City varchar(50) not null,
);

CREATE TABLE Products (
  Id int not null identity primary key,
  Name nvarchar(150) not null,
  Description nvarchar(max) null,
  StockPrice money not null,
);

GO

CREATE TABLE Customers (
  Id int not null identity primary key,
  FirstName varchar(50) not null,
  LastName varchar(50) not null,
  Email varchar(150) not null unique,
  AddressId int not null references Addresses(Id),
);

GO

CREATE TABLE Orders (
  Id int not null identity primary key,
  CustomerId int not null references Customers(Id),
  OrderDate datetime2 not null,
  DueDate datetime2 not null,
  TotalPrice money not null,
  VAT money null,
);

GO

CREATE TABLE OrderRows (
  OrderId int not null references Orders(Id),
  ProductId int not null references Products(Id),
  Quantity int not null,
  Price money not null,
  PRIMARY KEY (OrderID, ProductId)
);