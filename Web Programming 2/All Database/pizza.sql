-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jun 14, 2021 at 06:36 PM
-- Server version: 8.0.21
-- PHP Version: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pizza`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `id` int NOT NULL AUTO_INCREMENT,
  `admin` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id`, `admin`) VALUES
(1, 'admin1'),
(2, 'admin2');

-- --------------------------------------------------------

--
-- Table structure for table `beverage`
--

DROP TABLE IF EXISTS `beverage`;
CREATE TABLE IF NOT EXISTS `beverage` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(100) NOT NULL,
  `price` int NOT NULL,
  `availability` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `beverage`
--

INSERT INTO `beverage` (`id`, `type`, `price`, `availability`) VALUES
(1, 'Water', 2, 10),
(2, 'Cola', 4, 10),
(3, 'Beer', 6, 10),
(4, 'Ayran', 3, 10),
(5, 'Tea', 4, 10),
(6, 'Coffee', 4, 10);

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `customer_number` int NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `address` varchar(100) NOT NULL,
  PRIMARY KEY (`customer_number`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`customer_number`, `username`, `address`) VALUES
(3, 'Ammar', 'vila 210'),
(6, 'Ammar 3', '1234');

-- --------------------------------------------------------

--
-- Table structure for table `dessert`
--

DROP TABLE IF EXISTS `dessert`;
CREATE TABLE IF NOT EXISTS `dessert` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(100) NOT NULL,
  `price` int NOT NULL,
  `availability` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `dessert`
--

INSERT INTO `dessert` (`id`, `type`, `price`, `availability`) VALUES
(1, 'Apple Pie', 2, 10),
(2, 'Chocolate Cake', 4, 10),
(3, 'Banana Pudding', 6, 10),
(4, 'Ice Cream', 3, 10),
(5, 'Qurabiya', 4, 10),
(6, 'Stroopwafel', 4, 10);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_number` int NOT NULL,
  `total_price` int NOT NULL,
  `discount` int NOT NULL,
  `order_date` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `customer_number`, `total_price`, `discount`, `order_date`) VALUES
(4, 3, 250, 50, '2021-06-13'),
(3, 3, 33, 0, '2021-06-13'),
(6, 3, 536, 107, '2021-06-14'),
(7, 3, 3, 0, '2021-06-14'),
(8, 3, 15, 0, '2021-06-14'),
(9, 3, 52, 8, '2021-06-14'),
(10, 3, 50, 5, '2021-06-14'),
(11, 3, 100, 20, '2021-06-14'),
(12, 3, 19, 0, '2021-06-14');

-- --------------------------------------------------------

--
-- Table structure for table `pizza`
--

DROP TABLE IF EXISTS `pizza`;
CREATE TABLE IF NOT EXISTS `pizza` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(100) NOT NULL,
  `small_Price` int NOT NULL,
  `smallAvailability` int NOT NULL,
  `medium_Price` int NOT NULL,
  `mediumAvailability` int NOT NULL,
  `large_Price` int NOT NULL,
  `largeAvailablitiy` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `pizza`
--

INSERT INTO `pizza` (`id`, `type`, `small_Price`, `smallAvailability`, `medium_Price`, `mediumAvailability`, `large_Price`, `largeAvailablitiy`) VALUES
(1, 'Vegetarian Pizza', 15, 10, 17, 10, 20, 10),
(2, 'Chicken Pizza', 18, 10, 20, 10, 22, 10),
(3, 'Meat Pizza', 18, 10, 20, 10, 22, 10),
(4, 'Pepperoni Pizza', 19, 10, 21, 10, 23, 10),
(5, 'Mix Pizza', 20, 10, 22, 10, 24, 10),
(6, 'COME308 Special Pizza', 22, 10, 23, 10, 26, 10);

-- --------------------------------------------------------

--
-- Table structure for table `profit_table`
--

DROP TABLE IF EXISTS `profit_table`;
CREATE TABLE IF NOT EXISTS `profit_table` (
  `id` int NOT NULL AUTO_INCREMENT,
  `total_sale` int NOT NULL,
  `gross_sale` int NOT NULL,
  `net_income` int NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `profit_table`
--

INSERT INTO `profit_table` (`id`, `total_sale`, `gross_sale`, `net_income`, `start_date`, `end_date`) VALUES
(1, 141, 49, 28, '2021-06-11', '2021-06-13'),
(2, 73, 26, 15, '2021-06-11', '2021-06-13'),
(3, 33, 12, 7, '2021-06-12', '2021-06-14'),
(4, 415, 145, 83, '2021-06-11', '2021-06-18'),
(5, 233, 82, 47, '2021-06-11', '2021-06-18'),
(6, 665, 233, 133, '2021-06-07', '2021-06-21'),
(7, 616, 216, 123, '2021-06-14', '2021-06-21');

-- --------------------------------------------------------

--
-- Table structure for table `region-table`
--

DROP TABLE IF EXISTS `region-table`;
CREATE TABLE IF NOT EXISTS `region-table` (
  `id` int NOT NULL AUTO_INCREMENT,
  `region` varchar(100) NOT NULL,
  `delivery-time` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `region-table`
--

INSERT INTO `region-table` (`id`, `region`, `delivery-time`) VALUES
(1, 'Toros University', 10),
(2, 'Pozcu', 20),
(3, 'Mezitli', 30),
(4, 'Other', 45);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
