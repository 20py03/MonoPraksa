CREATE TABLE "Film"(
	"Id" int PRIMARY KEY,
	"Title" varchar (100) NOT NULL,
	"Genre" varchar (30) NOT NULL,
	"Duration" time NOT NULL,
	"YearOfRelease" date,
	"Director" varchar,
	"Summary" text
);


CREATE TABLE "Hall"(
	"Id" int PRIMARY KEY,
	"Name" varchar NOT NULL,
	"Capacity" int,
	"Location" varchar (70)
);

CREATE TABLE "FilmHall"(
	"Id" int PRIMARY KEY,
	"FilmId" int NOT NULL,
	"HallId" int NOT NULL,
	"FilmTime" time,
	CONSTRAINT "FK_Film_Hall_FilmId" FOREIGN KEY ("FilmId") REFERENCES "Film" ("Id"),
	CONSTRAINT "FK_Film_Hall_HallId" FOREIGN KEY ("HallId") REFERENCES "Hall" ("Id")
);

CREATE TABLE "Reservation"(
	"Id" int PRIMARY KEY,
	"UserId" int NOT NULL,
	"FilmHallId" int NOT NULL,
	"NumberOfTickets" int NOT NULL,
	"StatusId" int NOT NULL,
	CONSTRAINT "FK_Reservation_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User"("Id"),
	CONSTRAINT "FK_Reservation_FilmHall_FilmHallId" FOREIGN KEY ("FilmHallId") REFERENCES "FilmHall"("Id"),
	CONSTRAINT "FK_Reservation_Status_StatusId" FOREIGN KEY ("StatusId") REFERENCES "Status"("Id")
);

CREATE TABLE "Status"(
	"Id" int PRIMARY KEY,
	"Name" varchar(20) NOT NULL
);

CREATE TABLE "Ticket"(
	"Id" int PRIMARY KEY,
	"ReservationId" int NOT NULL,
	"Price" decimal(8,2),
	"Seat" int,
	CONSTRAINT "FK_Ticket_Reservation_ReservationId" FOREIGN KEY ("ReservationId") REFERENCES "Reservation"("Id")
);

CREATE TABLE "User"(
	"Id" int PRIMARY KEY,
	"Name" varchar (50),
	"Surname" varchar (50),
	"CardNumber" char(13)
);

INSERT INTO "Film" ("Id", "Title", "Genre", "Duration", "YearOfRelease", "Director", "Summary")
VALUES 
    (1,'The Shawshank Redemption', 'Drama', '02:22:00', '1994-10-14', 'Frank Darabont', 'Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.'),
    (2,'The Godfather', 'Crime', '02:55:00', '1972-03-24', 'Francis Ford Coppola', 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.'),
    (3,'The Dark Knight', 'Action', '02:32:00', '2008-07-18', 'Christopher Nolan', 'When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.'),
    (4,'Pulp Fiction', 'Crime', '02:34:00', '1994-10-14', 'Quentin Tarantino', 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.'),
	(5,'Inception', 'Sci-Fi', '02:28:00', '2010-07-16', 'Christopher Nolan', 'A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.');

INSERT INTO "Hall" ("Id","Name","Capacity","Location")
VALUES
	(1,'1A', 100,'Osijek'),
	(2,'2B', 200,'Vukovar'),
	(3,'3C', 300,'Zagreb'),
	(4,'4C', 400, 'Split'),
	(5,'5D', 500,'Rijeka');


INSERT INTO "FilmHall" ("Id","FilmId", "HallId", "FilmTime")
VALUES 
    (1, 5, 2, '18:00:00'), 
    (2, 4, 5, '20:00:00'),
    (3, 3, 4, '19:30:00'), 
    (4, 2, 3, '21:00:00'), 
    (5, 1, 1, '17:45:00'); 

INSERT INTO "User" ("Id", "Name", "Surname", "CardNumber")
VALUES 
    (1,'John', 'Doe', '1234567890123'),
    (2,'Emma', 'Smith', '9876543210987'),
    (3,'Bob', 'Johnson', '4567890123456'),
    (4,'Emily', 'Williams', '7890123456789'),
    (5,'Michael', 'Brown', '5678901234567');
	
	
INSERT INTO "Status" ("Id", "Name")
VALUES
	(1,'Accepted'),
	(2,'Cancelled'),
	(3,'Denied');

INSERT INTO "Reservation" ("Id", "UserId", "FilmHallId", "NumberOfTickets", "StatusId")
VALUES
    (1,1, 1, 2, 1),
	(2,2, 3 ,5, 2),
	(3,3, 2, 8, 3);
	
INSERT INTO "Ticket" ("Id","ReservationId","Price","Seat")
VALUES
	(1, 2, 15.99, 5),
	(2, 1,19.99,10),
	(3, 3,5.99,7);
	
SELECT * FROM "Film";

SELECT * FROM "Hall";

SELECT * FROM "FilmHall";

SELECT * FROM "Reservation";

SELECT * FROM "Ticket";

SELECT * FROM "User";

SELECT * FROM "Status";

DROP TABLE "Film";

DROP TABLE "Hall";

DROP TABLE "FilmHall";

DROP TABLE "Reservation";

DROP TABLE "Ticket";

DROP TABLE "User";

DROP TABLE "Status";

UPDATE "Film" SET "Duration" = '02:30:00' WHERE "Id" = 1;
UPDATE "Film" SET "Director" = 'Quentin Tarantino' WHERE "Id" = 4;
UPDATE "Film" SET "YearOfRelease" = '1972-03-21' WHERE "Id" = 2;


UPDATE "Hall" SET "Capacity" = 150 WHERE "Id" = 1;
UPDATE "Hall" SET "Location" = 'Zadar' WHERE "Id" = 2;

UPDATE "User" SET "Name" = 'Robert' WHERE "Id" = 3
UPDATE "User" SET "Name" LIKE 'Jonathan', "Surname" LIKE 'Smith' WHERE "Id" = 1;


SELECT "Film"."Title", "Hall"."Name", "FilmHall"."FilmTime"
FROM "FilmHall"
INNER JOIN "Film" ON "Film"."Id" = "FilmHall"."FilmId"
INNER JOIN "Hall" ON "Hall"."Id" = "FilmHall"."HallId";


SELECT "User"."Name", "User"."Surname", "Reservation"."NumberOfTickets"
FROM "User"
LEFT JOIN "Reservation" ON "User"."Id" = "Reservation"."UserId";


SELECT "User"."Name", "User"."Surname", "Film"."Title", "FilmHall"."FilmTime", "Reservation"."NumberOfTickets", "Status"."Name" AS "ReservationStatus"
FROM "Reservation"
INNER JOIN "User" ON "Reservation"."UserId" = "User"."Id"
INNER JOIN "FilmHall" ON "Reservation"."FilmHallId" = "FilmHall"."Id"
INNER JOIN "Film" ON "FilmHall"."FilmId" = "Film"."Id"
INNER JOIN "Status" ON "Reservation"."StatusId" = "Status"."Id";













