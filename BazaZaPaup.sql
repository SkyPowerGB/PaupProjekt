/*
SQLyog Community v8.61 
MySQL - 5.5.5-10.4.28-MariaDB : Database - servisvoziladb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`servisvoziladb` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci */;

USE `servisvoziladb`;

/*Table structure for table `listausluga` */

DROP TABLE IF EXISTS `listausluga`;

CREATE TABLE `listausluga` (
  `kol` int(11) DEFAULT NULL,
  `koef` decimal(5,2) DEFAULT NULL,
  `UslugaID` int(11) DEFAULT NULL,
  `idListe` int(11) NOT NULL AUTO_INCREMENT,
  `RačunID` int(11) DEFAULT NULL,
  PRIMARY KEY (`idListe`),
  KEY `FK_RačunID` (`RačunID`),
  KEY `fk_UslugaID` (`UslugaID`),
  CONSTRAINT `FK_RačunID` FOREIGN KEY (`RačunID`) REFERENCES `račun` (`RačunID`),
  CONSTRAINT `fk_UslugaID` FOREIGN KEY (`UslugaID`) REFERENCES `usluge` (`UslugaID`)
) ENGINE=InnoDB AUTO_INCREMENT=156 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `listausluga` */

insert  into `listausluga`(`kol`,`koef`,`UslugaID`,`idListe`,`RačunID`) values (1,'1.00',10,154,30),(2,'1.00',12,155,30);

/*Table structure for table `ovlasti` */

DROP TABLE IF EXISTS `ovlasti`;

CREATE TABLE `ovlasti` (
  `sifra` varchar(5) NOT NULL,
  `naziv` varchar(255) NOT NULL,
  PRIMARY KEY (`sifra`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `ovlasti` */

insert  into `ovlasti`(`sifra`,`naziv`) values ('AD','Administrator'),('KO','korisnik'),('MO','Moderator');

/*Table structure for table `račun` */

DROP TABLE IF EXISTS `račun`;

CREATE TABLE `račun` (
  `RačunID` int(11) NOT NULL AUTO_INCREMENT,
  `ServisID` int(11) DEFAULT NULL,
  `DatumIzdavanja` date DEFAULT NULL,
  `UkupanIznos` decimal(10,2) DEFAULT NULL,
  `Izdan` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`RačunID`),
  KEY `ServisID` (`ServisID`),
  CONSTRAINT `račun_ibfk_1` FOREIGN KEY (`ServisID`) REFERENCES `servis` (`ServisID`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `račun` */

insert  into `račun`(`RačunID`,`ServisID`,`DatumIzdavanja`,`UkupanIznos`,`Izdan`) values (30,51,'2023-06-12','146.00',1);

/*Table structure for table `servis` */

DROP TABLE IF EXISTS `servis`;

CREATE TABLE `servis` (
  `ServisID` int(11) NOT NULL AUTO_INCREMENT,
  `VoziloID` int(11) DEFAULT NULL,
  `VlasnikID` int(11) DEFAULT NULL,
  `Datum` datetime DEFAULT NULL,
  `OpisProblema` varchar(255) DEFAULT NULL,
  `StatusServisa` varchar(255) DEFAULT NULL,
  `DatumServisa` datetime DEFAULT NULL,
  `slikaVozila` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ServisID`),
  KEY `VoziloID` (`VoziloID`),
  KEY `VlasnikID` (`VlasnikID`),
  CONSTRAINT `servis_ibfk_1` FOREIGN KEY (`VoziloID`) REFERENCES `vozilo` (`VoziloID`),
  CONSTRAINT `servis_ibfk_2` FOREIGN KEY (`VlasnikID`) REFERENCES `vlasnik` (`VlasnikID`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `servis` */

insert  into `servis`(`ServisID`,`VoziloID`,`VlasnikID`,`Datum`,`OpisProblema`,`StatusServisa`,`DatumServisa`,`slikaVozila`) values (51,3,9,'2023-12-06 00:35:47','Servis motora','Zaprimljen',NULL,'~/SlikeServisi/Bez naslova233547163.png');

/*Table structure for table `usluge` */

DROP TABLE IF EXISTS `usluge`;

CREATE TABLE `usluge` (
  `UslugaID` int(11) NOT NULL AUTO_INCREMENT,
  `nazivUsluga` varchar(255) DEFAULT NULL,
  `cijenaUsluga` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`UslugaID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `usluge` */

insert  into `usluge`(`UslugaID`,`nazivUsluga`,`cijenaUsluga`) values (10,'nova','100.00'),(12,'nova2','23.00');

/*Table structure for table `vlasnik` */

DROP TABLE IF EXISTS `vlasnik`;

CREATE TABLE `vlasnik` (
  `VlasnikID` int(11) NOT NULL AUTO_INCREMENT,
  `Ime` varchar(255) DEFAULT NULL,
  `Prezime` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Lozinka` varchar(255) DEFAULT NULL,
  `ovlast` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`VlasnikID`),
  KEY `FK_korisnici_ovlast` (`ovlast`),
  CONSTRAINT `FK_korisnici_ovlast` FOREIGN KEY (`ovlast`) REFERENCES `ovlasti` (`sifra`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `vlasnik` */

insert  into `vlasnik`(`VlasnikID`,`Ime`,`Prezime`,`Email`,`Lozinka`,`ovlast`) values (9,'Gabriel','Zizek','h@gmail.com','pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=','AD'),(31,'Radnik','R','r@gmail.com','pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=','MO'),(32,'Admin','a','ad@gmail.com','pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=','AD'),(33,'Radnik','rd','rd@gmail.com','pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=','MO'),(34,'Korisnik','k','kr@gmail.com','pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=','KO');

/*Table structure for table `vozilo` */

DROP TABLE IF EXISTS `vozilo`;

CREATE TABLE `vozilo` (
  `VoziloID` int(11) NOT NULL AUTO_INCREMENT,
  `Marka` varchar(255) DEFAULT NULL,
  `Model` varchar(255) DEFAULT NULL,
  `GodinaProizvodnje` int(11) DEFAULT NULL,
  `Registracija` varchar(255) DEFAULT NULL,
  `VlasnikID` int(11) DEFAULT NULL,
  PRIMARY KEY (`VoziloID`),
  KEY `FK_vozilo_Vlasnik` (`VlasnikID`),
  CONSTRAINT `FK_vozilo_Vlasnik` FOREIGN KEY (`VlasnikID`) REFERENCES `vlasnik` (`VlasnikID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

/*Data for the table `vozilo` */

insert  into `vozilo`(`VoziloID`,`Marka`,`Model`,`GodinaProizvodnje`,`Registracija`,`VlasnikID`) values (3,'nekaj','nekaj',2002,'123ftw',9);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
