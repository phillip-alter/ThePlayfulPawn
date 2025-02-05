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
CREATE TABLE Addresses (
  AddressID          INT            PRIMARY KEY   IDENTITY,
  Line1              VARCHAR(60)    NOT NULL,
  Line2              VARCHAR(60)    DEFAULT NULL,
  City               VARCHAR(40)    NOT NULL,
  State              VARCHAR(2)     NOT NULL,
  ZipCode            VARCHAR(10)    DEFAULT NULL,
  Phone              VARCHAR(12)    NOT NULL,
);

CREATE TABLE Vendors (
  VendorID           INT            PRIMARY KEY  IDENTITY,
  VendorName			VARCHAR(255)	NOT NULL,
  VendorAddressID 		INT			REFERENCES Addresses (AddressID),
  ContactFirst		VARCHAR(255)	NOT NULL,
  ContactLast		VARCHAR(255)	NOT NULL,
  VendorTerms		VARCHAR(255)	NOT NULL
);

CREATE TABLE Food (
	FoodID			INT				PRIMARY KEY IDENTITY,
	VendorID		INT				REFERENCES Vendors (VendorID),
	Name			VARCHAR(255)	NOT NULL,
	Price			MONEY			NOT NULL,
	Inventory		INT				DEFAULT 0,
);

CREATE TABLE Games (
  GameID        INT            PRIMARY KEY   IDENTITY,
  VendorID		INT				REFERENCES Vendors (VendorID),
  GameName		VARCHAR(255)   NOT NULL      UNIQUE,
  Price			MONEY			NOT  NULL,
  Description	VARCHAR(255)	NOT NULL,
  Age			INT				NOT NULL,
  PlayerCount	INT				NOT NULL
);

CREATE TABLE Reservations (
	ReservationID	INT				PRIMARY KEY IDENTITY,
	CustomerID		INT,			--REFERENCES Customers (CustomerID),  -- Removed for now!
	GroupTotal		INT				DEFAULT NULL,
	DateTime		DATE			NOT NULL,
	GameID			INT				REFERENCES Games (GameID)
);

CREATE TABLE Customers (
  CustomerID           INT            PRIMARY KEY   IDENTITY,
  FirstName            VARCHAR(60)    NOT NULL,
  LastName             VARCHAR(60)    NOT NULL,
  AddressID    			INT           REFERENCES Addresses (AddressID),
  PlayCount				INT				DEFAULT NULL,
  ReservationID			INT				-- No reference yet!
);

-- 2. Add the foreign key constraints *after* table creation
ALTER TABLE Reservations
ADD CONSTRAINT FK_Reservations_Customers
FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID);

ALTER TABLE Customers
ADD CONSTRAINT FK_Customers_Reservations
FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID);