-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  mer. 07 nov. 2018 à 23:14
-- Version du serveur :  5.7.21
-- Version de PHP :  7.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `bd_cinema_cynov`
--

-- --------------------------------------------------------

--
-- Structure de la table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `prenom` varchar(100) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `admin`
--

INSERT INTO `admin` (`id`, `prenom`, `nom`, `email`, `password`) VALUES
(1, 'Jhon', 'Jhony', 'jhon@cynov.com', '21232f297a57a5a743894a0e4a801fc3'),
(2, 'Martin', 'MARTIN', 'martin@cynov.com', '47bce5c74f589f4867dbd57e9ca9f808');

-- --------------------------------------------------------

--
-- Structure de la table `salle`
--

DROP TABLE IF EXISTS `salle`;
CREATE TABLE IF NOT EXISTS `salle` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nb_places` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `salle`
--

INSERT INTO `salle` (`id`, `nb_places`) VALUES
(1, 30),
(2, 30),
(3, 50),
(4, 50),
(5, 50);

-- --------------------------------------------------------

--
-- Structure de la table `visionnage`
--

DROP TABLE IF EXISTS `visionnage`;
CREATE TABLE IF NOT EXISTS `visionnage` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(100) NOT NULL,
  `realisateur` varchar(100) DEFAULT NULL,
  `producteur` varchar(100) DEFAULT NULL,
  `type` varchar(33) NOT NULL DEFAULT 'Long métrage',
  `is_3d` tinyint(1) NOT NULL DEFAULT '0',
  `is_original` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `visionnage`
--

INSERT INTO `visionnage` (`id`, `nom`, `realisateur`, `producteur`, `type`, `is_3d`, `is_original`) VALUES
(1, 'An Impossible Love', 'Catherine Corsini', 'Elisabeth Perez', 'Long métrage', 0, 1),
(2, 'Le jeu', 'Fred Cavayé', '', 'Long métrage', 0, 1),
(3, 'visioni1', 'real1', 'prod1', 'Court métrage', 1, 0),
(4, 'visioni2', 'real2', 'prod2', 'Série', 0, 1),
(5, 'visioni3', 'real3', 'prod3', 'Long métrage', 1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `visionnage_salle`
--

DROP TABLE IF EXISTS `visionnage_salle`;
CREATE TABLE IF NOT EXISTS `visionnage_salle` (
  `id_visionnage` int(11) NOT NULL,
  `id_salle` int(11) NOT NULL,
  `date` date NOT NULL,
  `horaire` time NOT NULL,
  `nbPlacesDispo` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_visionnage`,`id_salle`,`date`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `visionnage_salle`
--

INSERT INTO `visionnage_salle` (`id_visionnage`, `id_salle`, `date`, `horaire`, `nbPlacesDispo`) VALUES
(1, 1, '2018-11-01', '04:12:11', 30),
(2, 2, '2018-11-11', '15:14:00', 0),
(1, 3, '2018-11-05', '06:17:00', 50),
(1, 4, '2018-12-06', '13:34:00', 49),
(2, 3, '2018-11-07', '20:30:00', 49),
(1, 4, '2018-11-08', '17:15:00', 49),
(3, 2, '2018-11-08', '15:55:00', 30);

-- --------------------------------------------------------

--
-- Structure de la table `visionnage_visiteur`
--

DROP TABLE IF EXISTS `visionnage_visiteur`;
CREATE TABLE IF NOT EXISTS `visionnage_visiteur` (
  `id_visionnage` int(11) NOT NULL,
  `id_visiteur` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_visionnage`,`id_visiteur`,`date`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `visionnage_visiteur`
--

INSERT INTO `visionnage_visiteur` (`id_visionnage`, `id_visiteur`, `date`) VALUES
(1, 2, '2018-11-08 00:00:00'),
(1, 2, '2018-12-06 00:00:00'),
(2, 2, '2018-11-07 00:00:00'),
(2, 2, '2018-11-11 00:00:00');

-- --------------------------------------------------------

--
-- Structure de la table `visiteur`
--

DROP TABLE IF EXISTS `visiteur`;
CREATE TABLE IF NOT EXISTS `visiteur` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `prenom` varchar(100) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `visiteur`
--

INSERT INTO `visiteur` (`id`, `prenom`, `nom`, `email`, `password`) VALUES
(1, 'Bernard', 'BERNARDY', 'b.Bernardy@cynov.com', 'a3a74c05bfc1925c5766e7fb5e50fbf6'),
(2, 'Michel', 'MICHELY', 'm.Michely@cynov.com', 'f3abb86bd34cf4d52698f14c0da1dc60'),
(3, 'samy', 'sami', 's.samy@cynov.com', 'da9414575226afc5410f794f728b50d9');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
