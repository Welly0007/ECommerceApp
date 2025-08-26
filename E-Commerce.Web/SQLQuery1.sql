DROP LOGIN [DESKTOP-5F8921H\acer];

Create Database [ECommerce]
go
Create Table ProductBrands(
Id int primary key identity(1,1),
[Name] varchar(255) not null
)

Create Table ProductTypes(
Id int primary key identity(1,1),
[Name] varchar(255) not null
)

Create Table Products(
Id Int Primary Key identity(1,1),
[Name] varchar(255) not null,
[Description] varchar(max),
[PictureUrl] varchar(max),
[Price] decimal(10,2),
[BrandId] int not null
Constraint FK_Products_ProductsBrands_BrandId
Foreign Key(BrandId) References ProductBrands(Id),
[TypeId] int not null,
Constraint FK_Products_ProductsTypes_TypeId
Foreign Key(TypeId) References ProductTypes(Id)
)