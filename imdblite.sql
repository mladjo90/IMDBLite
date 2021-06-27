﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 8.0.40.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 6/27/2021 4:35:18 PM
-- Server version: 5.6.44-log
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE imdblite;

--
-- Drop table `cast_in_movie`
--
DROP TABLE IF EXISTS cast_in_movie;

--
-- Drop table `casts`
--
DROP TABLE IF EXISTS casts;

--
-- Drop procedure `FindFreeUser`
--
DROP PROCEDURE IF EXISTS FindFreeUser;

--
-- Drop table `ratings`
--
DROP TABLE IF EXISTS ratings;

--
-- Drop table `movies`
--
DROP TABLE IF EXISTS movies;

--
-- Set default database
--
USE imdblite;

--
-- Create table `movies`
--
CREATE TABLE movies (
  ID int(11) NOT NULL AUTO_INCREMENT,
  Title varchar(255) NOT NULL,
  CoverImg varchar(255) DEFAULT NULL,
  ReleaseDate date DEFAULT NULL,
  IsTVShow tinyint(1) DEFAULT 0,
  PRIMARY KEY (ID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 14,
AVG_ROW_LENGTH = 1260,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create table `ratings`
--
CREATE TABLE ratings (
  ID int(11) NOT NULL AUTO_INCREMENT,
  UserID varchar(255) NOT NULL,
  RatingStarts decimal(2, 1) NOT NULL,
  MovieID int(11) NOT NULL,
  PRIMARY KEY (ID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 38,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create foreign key
--
ALTER TABLE ratings
ADD CONSTRAINT FK_ratings_MovieID FOREIGN KEY (MovieID)
REFERENCES movies (ID) ON DELETE NO ACTION;

DELIMITER $$

--
-- Create procedure `FindFreeUser`
--
CREATE PROCEDURE FindFreeUser (OUT success_param int, OUT pUserName varchar(255))
SQL SECURITY INVOKER
BEGIN
  START TRANSACTION;
    SET success_param = 0;
    SELECT
      ratings.UserID INTO pUserName
    FROM ratings
    WHERE ratings.UserID LIKE 'FreeUser%'
    ORDER BY ratings.UserID DESC LIMIT 1;
    IF pUserName IS NULL THEN
      SET pUserName = 'FreeUser00001';
    ELSE
      SELECT
        CONCAT('FreeUser', LPAD(CONVERT(REPLACE(pUserName, 'FreeUser', ''), UNSIGNED integer) + 1, 5, '0')) INTO pUserName;
    END IF;
    SET success_param = 1;
  COMMIT;
END
$$

DELIMITER ;

--
-- Create table `casts`
--
CREATE TABLE casts (
  ID int(11) NOT NULL AUTO_INCREMENT,
  FirstName varchar(255) NOT NULL,
  LastName varchar(255) NOT NULL,
  Gender bit(1) NOT NULL COMMENT '0 - male; 1 -female',
  PRIMARY KEY (ID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 29,
AVG_ROW_LENGTH = 585,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create table `cast_in_movie`
--
CREATE TABLE cast_in_movie (
  MovieID int(11) NOT NULL,
  CastID int(11) NOT NULL
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 16384,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create foreign key
--
ALTER TABLE cast_in_movie
ADD CONSTRAINT FK_cast_in_movie_CastID FOREIGN KEY (CastID)
REFERENCES casts (ID) ON DELETE NO ACTION;

--
-- Create foreign key
--
ALTER TABLE cast_in_movie
ADD CONSTRAINT FK_cast_in_movie_MovieID FOREIGN KEY (MovieID)
REFERENCES movies (ID) ON DELETE NO ACTION;

-- 
-- Dumping data for table movies
--
INSERT INTO movies VALUES
(1, 'Balkanska Medja', 'https://demo.aleacontrol.net/mladjan/balkanska.jpg', '2020-02-04', 0),
(2, 'Senke nad Balkanom', 'https://demo.aleacontrol.net/mladjan/senke.jpg', '2017-03-01', 1),
(3, 'Montevideo, Bog te video', 'https://demo.aleacontrol.net/mladjan/montevideo.jpg', '2010-03-11', 1),
(4, 'Juzni Vetar', 'https://demo.aleacontrol.net/mladjan/juzni.jpg', '2019-09-09', 0),
(5, 'Loki', 'https://demo.aleacontrol.net/mladjan/loki.jpg', '2021-01-04', 0),
(6, 'Cruela', 'https://demo.aleacontrol.net/mladjan/cruela.jpg', '2021-04-07', 0),
(7, 'Wrath of Man', 'https://demo.aleacontrol.net/mladjan/man.jpg', '2021-03-09', 0),
(8, 'Inside', 'https://demo.aleacontrol.net/mladjan/inside.jpg', '2021-02-23', 0),
(9, 'The Godfather', 'https://demo.aleacontrol.net/mladjan/godfather.jpg', '1972-03-08', 0),
(10, 'The Dark Knight', 'https://demo.aleacontrol.net/mladjan/batman.jpg', '2008-10-07', 0),
(11, 'Schindler''s List', 'https://demo.aleacontrol.net/mladjan/schindler.jpg', '1993-11-10', 0),
(12, 'Fight Club', 'https://demo.aleacontrol.net/mladjan/fight.jpg', '1999-06-08', 0),
(13, 'Forrest Gump', 'https://demo.aleacontrol.net/mladjan/forrest.jpg', '1994-06-10', 0);

-- 
-- Dumping data for table casts
--
INSERT INTO casts VALUES
(1, 'Anton', 'Pampushnyy', False),
(2, 'Yuriy', 'Kutsenko', False),
(3, 'Milena', 'Radulovic', True),
(4, 'Milos', 'Bikovic', False),
(5, 'Andrija', 'Kuzmanovic', False),
(6, 'Marija', 'Bergam', True),
(7, 'Dragan', 'Bjelogrlic', False),
(8, 'Petar', 'Strugar', False),
(9, 'Nina', 'Jankovic', True),
(10, 'Miodrag', 'Radonic', False),
(11, 'Jovana', 'Stojiljkovic', True),
(12, 'Tom', 'Hiddleston', False),
(13, 'Sophia', 'Di Martino', True),
(14, 'Emma', 'Stone', True),
(15, 'Emma', 'Thompson', True),
(16, 'Jason', 'Statham', False),
(17, 'Holt', 'McCallany', False),
(18, 'Bo', 'Burnham', False),
(19, 'Marlon', 'Brando', False),
(20, 'Al', 'Pacino', False),
(21, 'Christian', 'Bale', False),
(22, 'Heath', 'Ledger', False),
(23, 'Liam', 'Neesoon', False),
(24, 'Ben', 'Kingsley', False),
(25, 'Edward', 'Norton', False),
(26, 'Brad', 'Pitt', False),
(27, 'Tom', 'Hanks', False),
(28, 'Rebecca', 'Williams', True);

-- 
-- Dumping data for table ratings
--
INSERT INTO ratings VALUES
(18, 'FreeUser00001', 3.0, 1),
(19, 'FreeUser00002', 3.0, 4),
(20, 'FreeUser00002', 1.0, 5),
(21, 'FreeUser00002', 5.0, 6),
(22, 'FreeUser00003', 5.0, 9),
(23, 'FreeUser00004', 5.0, 3),
(24, 'FreeUser00005', 4.0, 1),
(25, 'FreeUser00006', 2.0, 1),
(26, 'FreeUser00007', 4.0, 1),
(27, 'FreeUser00008', 3.0, 1),
(28, 'FreeUser00009', 3.0, 1),
(29, 'FreeUser00010', 3.0, 1),
(30, 'FreeUser00011', 3.0, 1),
(31, 'FreeUser00012', 3.0, 1),
(32, 'FreeUser00013', 2.0, 1),
(33, 'FreeUser00014', 2.0, 4),
(34, 'FreeUser00015', 5.0, 2),
(35, 'FreeUser00016', 3.0, 1),
(36, 'FreeUser00017', 4.0, 1),
(37, 'FreeUser00018', 3.0, 1);

-- 
-- Dumping data for table cast_in_movie
--
INSERT INTO cast_in_movie VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(2, 5),
(2, 6),
(2, 7),
(3, 4),
(3, 8),
(3, 9),
(4, 4),
(4, 10),
(4, 11),
(5, 12),
(5, 13),
(6, 14),
(6, 15),
(7, 16),
(7, 17),
(8, 17),
(8, 18),
(9, 19),
(9, 20),
(10, 21),
(10, 22),
(11, 23),
(11, 24),
(12, 25),
(12, 26),
(13, 27),
(13, 28);

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;