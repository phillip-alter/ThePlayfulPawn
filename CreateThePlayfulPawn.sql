/********************************************************
* This script creates the database named the_playful_pawn 
*********************************************************/
USE master;
GO

IF  DB_ID('ThePlayfulPawn') IS NOT NULL
DROP DATABASE ThePlayfulPawn;
GO

CREATE DATABASE ThePlayfulPawn;
GO
USE ThePlayfulPawn;

-- 1. Create tables *without* the circular reference
CREATE TABLE [Vendors] (
[VendorID] int IDENTITY(1,1),
[VendorName] varchar(50),
[VendorAddressID] int,
[ContactFirst] varchar(50),
[ContactLast] varchar(50),
PRIMARY KEY ([VendorID])
);
CREATE TABLE [Food] (
[FoodID] int IDENTITY(1,1),
[VendorID] int,
[Name] varchar(255),
[Price] decimal(5,2),
[Inventory] int,
PRIMARY KEY ([FoodID]),
CONSTRAINT [FK_Food.VendorID]
FOREIGN KEY ([VendorID])
REFERENCES [Vendors]([VendorID])
);
CREATE TABLE [Addresses] (
[AddressID] int IDENTITY(1,1),
[Line1] varchar(60),
[Line2] varchar(60) NULL,
[City] varchar(40),
[State] varchar(2),
[ZipCode] int,
[Phone] varchar(12),
PRIMARY KEY ([AddressID]),
);
CREATE TABLE [Customers] (
[CustomerID] int IDENTITY(1,1),
[FirstName] varchar(60),
[LastName] varchar(60),
[AddressID] int,
PRIMARY KEY ([CustomerID]),
);
CREATE TABLE [Reservations] (
[ReservationID] int IDENTITY(1,1),
[CustomerID] int,
[GroupTotal] int,
[DateTime] date,
PRIMARY KEY ([ReservationID]),
CONSTRAINT [FK_Reservations.CustomerID]
FOREIGN KEY ([CustomerID])
REFERENCES [Customers]([CustomerID])
);
CREATE TABLE [Games] (
[GameID] int IDENTITY(1,1),
[VendorID] int,
[GameName] varchar(255),
[Price] decimal(5,2),
[Description] varchar(255),
[MaxPlayerCount] int,
PRIMARY KEY ([GameID]),
CONSTRAINT [FK_Games.VendorID]
FOREIGN KEY ([VendorID])
REFERENCES [Vendors]([VendorID])
);

ALTER TABLE Customers
ADD CONSTRAINT FK_Customers_AddressID
FOREIGN KEY (AddressID)
REFERENCES Addresses(AddressID);

-- add data

INSERT INTO [Addresses] (Line1, Line2, City, State, ZipCode, Phone)
VALUES 
('123 Board Game St', 'Suite 100', 'Game City', 'NY', '12345', '555-1234'),
('456 Game Ave', '', 'Play Town', 'CA', '67890', '555-5678'),
('789 Fun Rd', 'Apt 2B', 'Toyland', 'TX', '54321', '555-9012'),
('101 Game Blvd', '', 'Boardville', 'FL', '98765', '555-3456'),
('202 Play St', '', 'Fun City', 'IL', '13579', '555-7890'),
('303 Toy Ln', '', 'Game Land', 'WA', '24680', '555-2345'),
('404 Boardwalk Dr', '', 'Playground City', 'OR', '86420', '555-6789'),
('505 Fun Ave', '', 'Toy Town', 'CO', '97531', '555-0123'),
('606 Game St.', '', 'Board Game City','MI','11223','555-4567'),
('707 Play Rd.', '', 'Funville','OH','33445','555-8901'),
('808 Toy Blvd.', '', 'Game World','PA','55667','555-2345'),
('909 Board Game Way', '', 'Play City', 'NJ', '77889', '555-6780'),
('1010 Fun St.', '', 'Toyland City','MA','99000','555-1235'),
('1111 Game Ave.', '', 'Board Game Town','VA','22334','555-5679'),
('1212 Play Rd.', '', 'Funland City','MD','44556','555-9013'),
('1313 Toy Blvd.', '', 'Game City','GA','66778','555-3457'),
('1414 Board Game St.', '', 'Playground Town','NC','88990','555-7891'),
('1515 Fun Ave.', '', 'Toyland Town','AZ','11234','555-2346'),
('1616 Game Rd.', '', 'Board Game World','WA','33456','555-6781'),
('1717 Play Blvd.', '', 'Fun City Town','TX','55678','555-0124'),
('1818 Toy St.', '', 'Game World City','IL','77890','555-4568'),
('1919 Board Game Ave.', '', 'Playground World','CA','99001','555-8902'),
('2020 Fun Rd.', '', 'Toyland World','FL','22345','555-2347'),
('2121 Game Blvd.', '', 'Board Game City Town','OR','44567','555-6782'),
('2222 Play St.', '', 'Funland World','CO','66789','555-0125');

INSERT INTO [Vendors] (VendorName, VendorAddressID, ContactFirst, ContactLast)
VALUES
('Cephalofair Games', 1, 'Isaac', 'Childres'),
('Z-Man Games', 2, 'Rob','Daviau'),
('Czech Games Edition', 3, 'Vlaada', 'Chvátil'),
('FryxGames', 4, 'Jacob', 'Fryxelius'),
('GMT Games', 5, 'Jason', 'Matthews'),
('Fantasy Flight Games', 6, 'Corey', 'Konieczka'),
('Stonemaier Games', 7, 'Jamey', 'Stegmaier'),
('Feuerland Spiele',8,'Jens','Drogemuller'),
('Stronghold Games', 9, 'Alexander', 'Pfister'),
('Serious Poulp', 10, 'Nicolas', 'Lange'),
('Ravensburger', 11, 'Stefan', 'Feld'),
('Repos Production', 12, 'Antoine','Bauza'),
('Ares Games', 13, 'Francesco', 'Nepitello'),
('Lookout Games', 14, 'Uwe', 'Rosenberg'),
('WizKids',15,'Kevin','Barrett');

INSERT INTO [Games] (VendorID,GameName,Price,Description,MaxPlayerCount)
VALUES
(1,'Gloomhaven',69.9,'Action / Movement Programming, Co-operative Play, Grid Movement, Hand Management, Modular Board, Role Playing',4),
(2,'Pandemic Legacy: Season 1',159.9,'Action Point Allowance System, Co-operative Play, Hand Management, Point to Point Movement, Set Collection, Trading, Variable Player Powers',4),
(3,'Through the Ages: A New Story of Civilization',98.9,'Action Point Allowance System, Auction/Bidding, Card Drafting',4),
(4,'Terraforming Mars',79.9,'Card Drafting, Hand Management, Set Collection, Tile Placement, Variable Player Powers',5),
(5,'Twilight Struggle',64.9,'Area Control / Area Influence, Campaign / Battle Card Driven, Dice Rolling, Hand Management, Simultaneous Action Selection',2),
(6,'Star Wars: Rebellion',109.9,'Area Control / Area Influence, Area Movement, Dice Rolling, Hand Management, Partnerships, Variable Player Powers',4),
(7,'Scythe',104.9,'Area Control / Area Influence, Grid Movement, Simultaneous Action Selection, Variable Player Powers',5),
(8,'Terra Mystica',79.9,'Area Control / Area Influence, Route/Network Building, Variable Phase Order, Variable Player Powers',5),
(9,'Great Western Trail',42.9,'Deck / Pool Building, Hand Management, Point to Point Movement',4),
(10,'The 7th Continent',79.9,'Co-operative Play, Grid Movement, Hand Management, Modular Board, Storytelling, Variable Player Powers',4),
(8,'Gaia Project',79.9,'Modular Board, Route/Network Building, Variable Player Powers',4),
(11,'The Castles of Burgundy',49.9,'Dice Rolling, Set Collection, Tile Placement, Variable Phase Order',4),
(13,'7 Wonders Duel',21.9,'Card Drafting, Set Collection',2),
(12,'War of the Ring (Second Edition)',79.9,'Area Control / Area Influence, Area Movement, Campaign / Battle Card Driven, Dice Rolling',4),
(14,'Caverna: The Cave Farmers',152.9,'Tile Placement, Worker Placement',7),
(11,'Puerto Rico',49.9,'Variable Phase Order',5),
(14,'Agricola',59.9,'Area Enclosure, Card Drafting, Hand Management, Variable Player Powers, Worker Placement',5),
(15,'Mage Knight Board Game',84.9,'Card Drafting, Co-operative Play, Deck / Pool Building, Dice Rolling, Grid Movement',4),
(7,'Viticulture Essential Edition',64.9,'Hand Management, Variable Phase Order, Worker Placement',6),
(6,'Arkham Horror: The Card Game',39.9,'Action Point Allowance System, Co-operative Play, Deck / Pool Building',2);

INSERT INTO [Food] (VendorID, Name, Price, Inventory)
VALUES 
(1, 'Pizza', 12.99, 50),
(2, 'Soda', 1.99, 100),
(3, 'Chips', 2.49, 75),
(4, 'Sandwich', 8.99, 30),
(5, 'Salad', 7.49, 40),
(6, 'Cookies', 3.99, 60),
(7, 'Brownies', 4.49, 20),
(8, 'Ice Cream', 5.99, 80);

INSERT INTO Customers (FirstName, LastName, AddressID)
VALUES 
('John', 'Doe', 15),
('Jane', 'Smith', 16),
('Alice', 'Johnson', 17),
('Bob', 'Brown', 18),
('Charlie', 'Davis', 19),
('Eve', 'Wilson', 20),
('Frank', 'Garcia', 21),
('Grace', 'Martinez', 22),
('Hank', 'Lopez', 23),
('Ivy', 'Gonzalez', 24);

INSERT INTO Reservations (CustomerID, GroupTotal, DateTime)
VALUES 
(1, 4, '2023-10-01 18:00:00'),
(2, 6, '2023-10-02 19:30:00'),
(3, 2, '2023-10-03 17:15:00'),
(4, 8, '2023-10-04 20:00:00'),
(5, 5, '2023-10-05 18:30:00'),
(6, 7, '2023-10-06 19:45:00'),
(7, 3, '2023-10-07 17:00:00'),
(8, 9, '2023-10-08 20:15:00');