CREATE DATABASE  IF NOT EXISTS `fujiattendance` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `fujiattendance`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: 192.168.1.22    Database: fujiattendance
-- ------------------------------------------------------
-- Server version	5.6.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `comedor_cedis`
--

DROP TABLE IF EXISTS `comedor_cedis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `comedor_cedis` (
  `iEmpleadoId` int(11) NOT NULL,
  `VerifyMode` int(11) DEFAULT NULL,
  `InOutMode` int(11) DEFAULT NULL,
  `FechaRegistro` datetime NOT NULL,
  `FechaCarga` datetime NOT NULL,
  KEY `iEmpleadoId` (`iEmpleadoId`),
  KEY `FechaIndex` (`FechaRegistro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `comedor_coorporativo`
--

DROP TABLE IF EXISTS `comedor_coorporativo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `comedor_coorporativo` (
  `iEmpleadoId` int(11) NOT NULL,
  `VerifyMode` int(11) DEFAULT NULL,
  `InOutMode` int(11) DEFAULT NULL,
  `FechaRegistro` datetime NOT NULL,
  `FechaCarga` datetime NOT NULL,
  KEY `iEmpleadoId` (`iEmpleadoId`),
  KEY `FechaIndex` (`FechaRegistro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `configuracionincidencias`
--

DROP TABLE IF EXISTS `configuracionincidencias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `configuracionincidencias` (
  `iConfiguracionId` int(11) NOT NULL AUTO_INCREMENT,
  `Configuracion` varchar(100) NOT NULL,
  `Dato` varchar(100) NOT NULL,
  PRIMARY KEY (`iConfiguracionId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `configuracionreportecomedor`
--

DROP TABLE IF EXISTS `configuracionreportecomedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `configuracionreportecomedor` (
  `iConfiguracionId` int(11) NOT NULL AUTO_INCREMENT,
  `Configuracion` varchar(100) DEFAULT NULL,
  `Dato` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`iConfiguracionId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `diasfestivos`
--

DROP TABLE IF EXISTS `diasfestivos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `diasfestivos` (
  `iDiaFestivoId` int(11) NOT NULL AUTO_INCREMENT,
  `vchDescripcion` varchar(100) DEFAULT NULL,
  `FechaFestiva` date NOT NULL,
  `Activo` bit(1) DEFAULT NULL,
  PRIMARY KEY (`iDiaFestivoId`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `dispositivos`
--

DROP TABLE IF EXISTS `dispositivos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dispositivos` (
  `MachineNumber` int(11) NOT NULL,
  `IP` varchar(15) NOT NULL,
  `Descripcion` varchar(30) DEFAULT NULL,
  `bloqueado` bit(1) DEFAULT NULL,
  PRIMARY KEY (`MachineNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `empleados`
--

DROP TABLE IF EXISTS `empleados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `empleados` (
  `iEmpleadoId` int(11) NOT NULL,
  `iNumeroEmpleado` int(11) DEFAULT NULL,
  `vchNombreEmpleado` varchar(100) DEFAULT NULL,
  `vchPassword` varchar(100) DEFAULT NULL,
  `iPrivilegio` int(11) DEFAULT NULL,
  `bEnabled` bit(1) DEFAULT NULL,
  `vchNumeroTarjeta` varchar(100) DEFAULT NULL,
  `vchFingerPrint` varchar(21000) DEFAULT NULL,
  `iFingerPrintLength` int(11) DEFAULT NULL,
  `iFingerFlag` int(11) DEFAULT NULL,
  `iHorarioId` int(11) NOT NULL,
  `bExterno` bit(1) DEFAULT NULL,
  `vchNomina` varchar(60) DEFAULT NULL,
  `vchCompania` varchar(30) DEFAULT NULL,
  `Activo` bit(1) DEFAULT b'1',
  `vchNombreUsuario` varchar(50) DEFAULT NULL,
  `vchPasswordAttendance` varchar(50) DEFAULT NULL,
  `vchCorreo` varchar(70) DEFAULT NULL,
  `intManagerID` int(11) DEFAULT NULL,
  `bitManager` bit(1) DEFAULT NULL,
  PRIMARY KEY (`iEmpleadoId`),
  KEY `iHorarioId` (`iHorarioId`),
  KEY `NombreIndex` (`vchNombreEmpleado`),
  CONSTRAINT `empleados_ibfk_1` FOREIGN KEY (`iHorarioId`) REFERENCES `horarios` (`iHorarioId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `entrada_cedis`
--

DROP TABLE IF EXISTS `entrada_cedis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `entrada_cedis` (
  `iEmpleadoId` int(11) NOT NULL,
  `VerifyMode` int(11) DEFAULT NULL,
  `InOutMode` int(11) DEFAULT NULL,
  `FechaRegistro` datetime NOT NULL,
  `FechaCarga` datetime NOT NULL,
  KEY `FechaIndex` (`FechaRegistro`),
  KEY `iEmpleadoId` (`iEmpleadoId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `entrada_coorporativo`
--

DROP TABLE IF EXISTS `entrada_coorporativo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `entrada_coorporativo` (
  `iEmpleadoId` int(11) NOT NULL,
  `VerifyMode` int(11) DEFAULT NULL,
  `InOutMode` int(11) DEFAULT NULL,
  `FechaRegistro` datetime NOT NULL,
  `FechaCarga` datetime NOT NULL,
  KEY `FechaIndex` (`FechaRegistro`),
  KEY `iEmpleadoId` (`iEmpleadoId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `horarios`
--

DROP TABLE IF EXISTS `horarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `horarios` (
  `iHorarioId` int(11) NOT NULL AUTO_INCREMENT,
  `vchDescripcion` varchar(100) DEFAULT NULL,
  `bLunes` bit(1) NOT NULL,
  `bMartes` bit(1) NOT NULL,
  `bMiercoles` bit(1) NOT NULL,
  `bJueves` bit(1) NOT NULL,
  `bViernes` bit(1) NOT NULL,
  `bSabado` bit(1) NOT NULL,
  `bDomingo` bit(1) NOT NULL,
  `vchEntradaLunes` varchar(5) DEFAULT NULL,
  `vchEntradaMartes` varchar(5) DEFAULT NULL,
  `vchEntradaMiercoles` varchar(5) DEFAULT NULL,
  `vchEntradaJueves` varchar(5) DEFAULT NULL,
  `vchEntradaViernes` varchar(5) DEFAULT NULL,
  `vchEntradaSabado` varchar(5) DEFAULT NULL,
  `vchEntradaDomingo` varchar(5) DEFAULT NULL,
  `vchSalidaLunes` varchar(5) DEFAULT NULL,
  `vchSalidaMartes` varchar(5) DEFAULT NULL,
  `vchSalidaMiercoles` varchar(5) DEFAULT NULL,
  `vchSalidaJueves` varchar(5) DEFAULT NULL,
  `vchSalidaViernes` varchar(5) DEFAULT NULL,
  `vchSalidaSabado` varchar(5) DEFAULT NULL,
  `vchSalidaDomingo` varchar(5) DEFAULT NULL,
  `AUDUSUARIO` int(11) DEFAULT NULL,
  PRIMARY KEY (`iHorarioId`),
  KEY `AUDUSUARIO` (`AUDUSUARIO`),
  CONSTRAINT `horarios_ibfk_1` FOREIGN KEY (`AUDUSUARIO`) REFERENCES `usuarios` (`iUsuarioId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuarios` (
  `iUsuarioId` int(11) NOT NULL AUTO_INCREMENT,
  `vchNombreUsuario` varchar(100) DEFAULT NULL,
  `vchUsuario` varchar(100) DEFAULT NULL,
  `vchPassword` varchar(100) DEFAULT NULL,
  `bActivo` bit(1) DEFAULT NULL,
  `datFechaSesion` datetime DEFAULT NULL,
  PRIMARY KEY (`iUsuarioId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'fujiattendance'
--

--
-- Dumping routines for database 'fujiattendance'
--
/*!50003 DROP PROCEDURE IF EXISTS `stp_ActualizaConfiguracion` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_ActualizaConfiguracion`(ConfiguracionId INT, Valor VARCHAR(100))
BEGIN
	UPDATE ConfiguracionReporteComedor SET Dato = Valor WHERE iConfiguracionId = ConfiguracionId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_getComedorExcel` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_getComedorExcel`(FechaInicio DATETIME, FechaFin DATETIME)
BEGIN
	SELECT iEmpleadoID,
	   iNumeroEmpleado AS NumeroEmpleado,
	   vchNombreEmpleado AS NombreEmpleado,
	   FechaRegistro,
	   Lugar,
	   ImporteEmpresa,
	   ImporteRetencion
  FROM (
	SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado,
		   EM.vchNombreEmpleado,
		   COM.FechaRegistro,
		   'COORPORATIVO' AS Lugar,
		   (SELECT Dato 
			  FROM ConfiguracionReporteComedor 
			 WHERE iConfiguracionId = 1) AS ImporteEmpresa,
		   (SELECT Dato 
			  FROM ConfiguracionReporteComedor 
			 WHERE iConfiguracionId = 2) AS ImporteRetencion
	  FROM comedor_coorporativo COM 
INNER JOIN Empleados EM ON COM.iEmpleadoId = EM.iEmpleadoId
UNION
	SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado,
		   EM.vchNombreEmpleado,
		   COM.FechaRegistro,
		   'TLALNEPANTLA' AS Lugar,
		   (SELECT Dato 
			  FROM ConfiguracionReporteComedor 
			 WHERE iConfiguracionId = 1) AS ImporteEmpresa,
		   (SELECT Dato 
			  FROM ConfiguracionReporteComedor 
			 WHERE iConfiguracionId = 2) AS ImporteRetencion
	  FROM comedor_cedis COM 
INNER JOIN Empleados EM ON COM.iEmpleadoId = EM.iEmpleadoId ) AS A
WHERE FechaRegistro BETWEEN FechaInicio AND FechaFin;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetConfiguracionComedor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetConfiguracionComedor`()
BEGIN
	SELECT iConfiguracionId, Configuracion, Dato FROM ConfiguracionReporteComedor;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetDetailComedor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetDetailComedor`(EmpleadoId INT, FechaInicio DATETIME, FechaFin DATETIME)
BEGIN
	SELECT FechaRegistro, Lugar FROM (
	SELECT FechaRegistro,
		   'Coorporativo' AS Lugar
	  FROM comedor_coorporativo 
	 WHERE iEmpleadoId = EmpleadoId
	 UNION
	SELECT FechaRegistro,
		   'Tlalnepantla' AS Lugar
	  FROM comedor_cedis 
	 WHERE iEmpleadoId = EmpleadoId) AS A
	 WHERE FechaRegistro BETWEEN FechaInicio AND FechaFin
	ORDER BY FechaRegistro;
	
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetDetalleFaltas` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetDetalleFaltas`(Empleado INT, FI DATETIME, FF DATETIME )
BEGIN
  DECLARE FechaInicio DATE default '20150401';
  DECLARE FechaFin DATE default '20150430';
  DECLARE DIAS_RESTANTES INT default 0;
  DECLARE CONTADOR_DIA DATE default FechaInicio;
  DECLARE int_val INT DEFAULT 0;
  IF FF <= CURDATE() THEN
	SET FechaFin = FF;
  else
	SET FechaFin = CURDATE();
  END IF;
  SET FechaInicio = FI;
  SET DIAS_RESTANTES = datediff(FechaFin, FechaInicio);
  SET CONTADOR_DIA = FechaInicio;
  DROP TABLE IF EXISTS TEMP;
  CREATE TEMPORARY TABLE TEMP (FECHA DATE, NOMBRE_DIA VARCHAR(10));
  
  test_loop : LOOP
    IF (int_val = DIAS_RESTANTES + 1) THEN
      LEAVE test_loop;
    END IF;
    IF (SELECT COUNT(*) FROM diasfestivos WHERE FechaFestiva = CONTADOR_DIA AND Activo = 1) = 0 THEN
      INSERT INTO TEMP (FECHA, NOMBRE_DIA) VALUES (CONTADOR_DIA, DATE_FORMAT(CONTADOR_DIA, '%W'));
    END IF;
    SET CONTADOR_DIA = DATE_ADD(CONTADOR_DIA ,INTERVAL 1 DAY);
    SET int_val = int_val + 1;
  END LOOP;  
    SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado,
           EM.vchNombreEmpleado,
		   EM.vchCompania,
		   EM.vchNomina,
           T.FECHA
      FROM Empleados EM
INNER JOIN TEMP T ON T.FECHA = T.FECHA
INNER JOIN Horarios H ON H.iHorarioId = EM.iHorarioId
 LEFT JOIN (  SELECT E.iEmpleadoId, FechaRegistro, DATE(FechaRegistro) AS DIA FROM Entrada_Coorporativo COR INNER JOIN Empleados E ON E.iEmpleadoId = COR.iEmpleadoId WHERE E.Activo = 1 AND FechaRegistro BETWEEN FechaInicio AND DATE_ADD(FechaFin ,INTERVAL 1 DAY) GROUP BY iEmpleadoId, DIA
              UNION
              SELECT E.iEmpleadoId, FechaRegistro, DATE(FechaRegistro) AS DIA FROM Entrada_Cedis CED INNER JOIN Empleados E ON E.iEmpleadoId = CED.iEmpleadoId WHERE E.Activo = 1 AND FechaRegistro BETWEEN FechaInicio AND DATE_ADD(FechaFin ,INTERVAL 1 DAY) GROUP BY iEmpleadoId, DIA
           )  AS Asistencias ON DATE(Asistencias.FechaRegistro) = DATE(T.FECHA) AND EM.iEmpleadoId = Asistencias.iEmpleadoId
     WHERE EM.bExterno = 0
       AND EM.Activo = 1
	   AND EM.iEmpleadoId = Empleado
       AND (CASE WHEN H.bLunes = 0 AND T.NOMBRE_DIA = 'Monday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bMartes = 0 AND T.NOMBRE_DIA = 'Tuesday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bMiercoles = 0 AND T.NOMBRE_DIA = 'Wednesday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bJueves = 0 AND T.NOMBRE_DIA = 'Thursday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bViernes = 0 AND T.NOMBRE_DIA = 'Friday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bSabado = 0 AND T.NOMBRE_DIA = 'Saturday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bDomingo = 0 AND T.NOMBRE_DIA = 'Sunday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND Asistencias.DIA IS NULL;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetDetalleRetardos` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetDetalleRetardos`(
EmployerId INT,
FI DATETIME,
FF DATETIME
)
BEGIN
  IF FF >= CURDATE() THEN
	SET FF = CURDATE();
  END IF;
SELECT IEmpleadoId AS EmpleadoId,
	   iNumeroEmpleado AS NumeroEmpleado, 
	   vchNombreEmpleado AS NombreEmpleado, 
	   FechaRegistro AS FechaRetardo,
     Date_format(FechaRegistro, '%d/%m/%Y') AS Dia
       FROM (
SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado, 
		   EM.vchNombreEmpleado,
		   COR.FechaRegistro AS FechaRegistro,
		   DAY(COR.FechaRegistro) AS Dia,
		   DATE_FORMAT(COR.FechaRegistro, '%W') AS NomDia,
		   DATE_FORMAT(COR.FechaRegistro, '%H:%i') AS HoraEntrada,
		   CASE 
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Monday' AND HO.bLunes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaLunes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Tuesday' AND HO.bMartes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMartes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Wednesday' AND HO.bMiercoles = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMiercoles) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Thursday' AND HO.bJueves = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaJueves) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Friday' AND HO.bViernes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaViernes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Saturday' AND HO.bSabado = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i')  <= TIME_FORMAT((TIME(HO.vchEntradaSabado) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Sunday' AND HO.bDomingo = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaDomingo) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   ELSE	'IGNORADO'
		   END AS Retardo
	  FROM Entrada_coorporativo COR
INNER JOIN Empleados EM ON EM.iEmpleadoId = COR.iEmpleadoId
INNER JOIN Horarios HO ON EM.iHorarioId = HO.iHorarioId
	 WHERE EM.Activo = 1 
	   AND EM.bExterno = 0
	   AND FechaRegistro Between FI AND DATE_ADD(FF ,INTERVAL 1 DAY)
  GROUP BY Dia, NomDia, iEmpleadoId
UNION
SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado, 
		   EM.vchNombreEmpleado,
		   COR.FechaRegistro AS FechaRegistro,
		   DAY(COR.FechaRegistro) AS Dia,
		   DATE_FORMAT(COR.FechaRegistro, '%W') AS NomDia,
		   DATE_FORMAT(COR.FechaRegistro, '%H:%i') AS HoraEntrada,
		   CASE 
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Monday' AND HO.bLunes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaLunes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Tuesday' AND HO.bMartes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMartes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Wednesday' AND HO.bMiercoles = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMiercoles) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Thursday' AND HO.bJueves = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaJueves) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Friday' AND HO.bViernes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaViernes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Saturday' AND HO.bSabado = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i')  <= TIME_FORMAT((TIME(HO.vchEntradaSabado) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Sunday' AND HO.bDomingo = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaDomingo) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   ELSE	'IGNORADO'
		   END AS Retardo
	  FROM Entrada_cedis COR
INNER JOIN Empleados EM ON EM.iEmpleadoId = COR.iEmpleadoId
INNER JOIN Horarios HO ON EM.iHorarioId = HO.iHorarioId
	 WHERE EM.Activo = 1 
	   AND EM.bExterno = 0
	   AND FechaRegistro Between FI AND DATE_ADD(FF ,INTERVAL 1 DAY)
  GROUP BY Dia, NomDia, iEmpleadoId ) AS A WHERE Retardo = 'RETARDO' AND iEmpleadoId = EmployerId GROUP BY Dia;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetDiasFeriados` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetDiasFeriados`()
BEGIN
  DECLARE ANIO VARCHAR(5) DEFAULT '2015';
  SET ANIO = YEAR(CURRENT_DATE());
	SELECT iDiaFestivoId, vchDescripcion, FechaFestiva FROM diasfestivos WHERE FechaFestiva > CONCAT(ANIO, '0101') AND Activo = 1;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetFaltas` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetFaltas`(
EmployerNumber INT,
EmployerName VARCHAR(100),
EmployerCompany VARCHAR(100),
EmployerPaysheet VARCHAR(100),
FI DATETIME,
FF DATETIME)
BEGIN
  DECLARE FechaInicio DATE default '20150401';
  DECLARE FechaFin DATE default '20150430';
  DECLARE DIAS_RESTANTES INT default 0;
  DECLARE CONTADOR_DIA DATE default FechaInicio;
  DECLARE int_val INT DEFAULT 0;
  IF FF <= CURDATE() THEN
	SET FechaFin = FF;
  else
	SET FechaFin = CURDATE();
  END IF;
  SET FechaInicio = FI;
  SET DIAS_RESTANTES = datediff(FechaFin, FechaInicio);
  SET CONTADOR_DIA = FechaInicio;
  DROP TABLE IF EXISTS TEMP;
  CREATE TEMPORARY TABLE TEMP (FECHA DATE, NOMBRE_DIA VARCHAR(10));
  
  test_loop : LOOP
    IF (int_val = DIAS_RESTANTES + 1) THEN
      LEAVE test_loop;
    END IF;
    IF (SELECT COUNT(*) FROM diasfestivos WHERE FechaFestiva = CONTADOR_DIA And Activo = 1) = 0 THEN
      INSERT INTO TEMP (FECHA, NOMBRE_DIA) VALUES (CONTADOR_DIA, DATE_FORMAT(CONTADOR_DIA, '%W'));
    END IF;
    SET CONTADOR_DIA = DATE_ADD(CONTADOR_DIA ,INTERVAL 1 DAY);
    SET int_val = int_val + 1;
  END LOOP;
    SELECT EM.iEmpleadoId AS EmpleadoId,
		   EM.iNumeroEmpleado AS NumeroEmpleado,
           EM.vchNombreEmpleado AS NombreEmpleado,
		   EM.vchCompania AS Compania,
		   EM.vchNomina AS Nomina,
           T.FECHA AS FechaFalta,
		   1 AS NumeroFaltas
      FROM Empleados EM
INNER JOIN TEMP T ON T.FECHA = T.FECHA
INNER JOIN Horarios H ON H.iHorarioId = EM.iHorarioId
 LEFT JOIN (  SELECT E.iEmpleadoId, FechaRegistro, DATE(FechaRegistro) AS DIA FROM Entrada_Coorporativo COR INNER JOIN Empleados E ON E.iEmpleadoId = COR.iEmpleadoId WHERE E.Activo = 1 AND FechaRegistro BETWEEN FechaInicio AND DATE_ADD(FechaFin ,INTERVAL 1 DAY) GROUP BY iEmpleadoId, DIA
              UNION
              SELECT E.iEmpleadoId, FechaRegistro, DATE(FechaRegistro) AS DIA FROM Entrada_Cedis CED INNER JOIN Empleados E ON E.iEmpleadoId = CED.iEmpleadoId WHERE E.Activo = 1 AND FechaRegistro BETWEEN FechaInicio AND DATE_ADD(FechaFin ,INTERVAL 1 DAY) GROUP BY iEmpleadoId, DIA
           )  AS Asistencias ON DATE(Asistencias.FechaRegistro) = DATE(T.FECHA) AND EM.iEmpleadoId = Asistencias.iEmpleadoId
     WHERE EM.bExterno = 0
       AND EM.Activo = 1
       AND (CASE WHEN H.bLunes = 0 AND T.NOMBRE_DIA = 'Monday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bMartes = 0 AND T.NOMBRE_DIA = 'Tuesday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bMiercoles = 0 AND T.NOMBRE_DIA = 'Wednesday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bJueves = 0 AND T.NOMBRE_DIA = 'Thursday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bViernes = 0 AND T.NOMBRE_DIA = 'Friday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bSabado = 0 AND T.NOMBRE_DIA = 'Saturday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bDomingo = 0 AND T.NOMBRE_DIA = 'Sunday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND Asistencias.DIA IS NULL
	   AND (EmployerNumber = 0 OR EM.iNumeroEmpleado = EmployerNumber) 
	   AND (EmployerName = '' OR EM.vchNombreEmpleado LIKE CONCAT('%',EmployerName, '%')) 
	   AND (EmployerCompany = '' OR EM.vchCompania = EmployerCompany) 
       AND (EmployerPaysheet = '' OR EM.vchNomina = EmployerPaysheet);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_getLayoutComedor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_getLayoutComedor`(
FECHAINICIO DATETIME,
FECHAFIN DATETIME,
Nomina VARCHAR(100),
Compania VARCHAR(100)
)
BEGIN
	SELECT iEmpleadoId AS EmpleadoId,
		   iNumeroEmpleado AS NumeroEmpleado,
		   vchCompania AS Compania,
	       vchNomina AS Nomina,
		   '7944' AS DIP,
		   vchNombreEmpleado AS NombreEmpleado,
		   'DESCUENTO ALIMENTACION' AS Descripcion,
		   (SELECT CAST(Dato as DECIMAL(15,2)) FROM ConfiguracionReporteComedor WHERE iConfiguracionId = 2) * COUNT(FechaRegistro) AS ImporteRetencion, 
		   DATE_FORMAT(LAST_DAY(FECHAFIN),'%d/%m/%Y') AS FechaMovimiento,
		   COUNT(FechaRegistro) AS NumeroComidas,
		   'COMEDOR' AS Referencia,
		   '0' AS Importe2,
		   '0' AS Importe3,
		   '0' AS SaldoActual,
		   '0' AS SaldoAnterior,
		   '0' AS ImporteCapturado
	  FROM
	(SELECT EM.iEmpleadoId,
			EM.iNumeroEmpleado, 
			EM.vchNombreEmpleado, 
			EM.vchCompania, 
			EM.vchNomina, 
			COM.FechaRegistro
	   FROM COMEDOR_COORPORATIVO COM
 INNER JOIN EMPLEADOS EM ON EM.iEmpleadoId = COM.iEmpleadoId
	  WHERE COM.FechaRegistro BETWEEN FechaInicio AND FechaFin AND EM.bExterno = 0
	  UNION
	 SELECT EM.iEmpleadoId,
			EM.iNumeroEmpleado, 
			EM.vchNombreEmpleado, 
			EM.vchCompania, 
			EM.vchNomina, 
			COM.FechaRegistro
	   FROM COMEDOR_CEDIS COM
 INNER JOIN EMPLEADOS EM ON EM.iEmpleadoId = COM.iEmpleadoId
	  WHERE COM.FechaRegistro BETWEEN FechaInicio AND FechaFin AND EM.bExterno = 0) AS A
	 WHERE (Compania = '' OR vchCompania = Compania) 
	   AND (Nomina = '' OR vchNomina = Nomina)
  GROUP BY iEmpleadoId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetReporteGeneralComedores` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetReporteGeneralComedores`(
EmployerNumber INT,
EmployerName VARCHAR(100),
EmployerCompany VARCHAR(100),
EmployerPaysheet VARCHAR(100),
FechaInicio DATETIME,
FechaFin DATETIME
)
BEGIN
	SELECT iEmpleadoId AS EmpleadoId,
		   iNumeroEmpleado AS NumeroEmpleado,
		   vchNombreEmpleado AS NombreEmpleado,
		   vchCompania AS Compania,
		   vchNomina AS Nomina,
		   COUNT(FechaRegistro) AS NumeroComidas
	  FROM
	(SELECT EM.iEmpleadoId,
			EM.iNumeroEmpleado, 
			EM.vchNombreEmpleado, 
			EM.vchCompania, 
			EM.vchNomina, 
			COM.FechaRegistro
	   FROM COMEDOR_COORPORATIVO COM
 INNER JOIN EMPLEADOS EM ON EM.iEmpleadoId = COM.iEmpleadoId
	  WHERE COM.FechaRegistro BETWEEN FechaInicio AND FechaFin
	  UNION
	 SELECT EM.iEmpleadoId,
			EM.iNumeroEmpleado, 
			EM.vchNombreEmpleado, 
			EM.vchCompania, 
			EM.vchNomina, 
			COM.FechaRegistro
	   FROM COMEDOR_CEDIS COM
 INNER JOIN EMPLEADOS EM ON EM.iEmpleadoId = COM.iEmpleadoId
	  WHERE COM.FechaRegistro BETWEEN FechaInicio AND FechaFin) AS A
 
 WHERE (EmployerNumber = 0 OR iNumeroEmpleado = EmployerNumber) AND
	   (EmployerName = '' OR vchNombreEmpleado LIKE CONCAT('%',EmployerName, '%')) AND
	   (EmployerCompany = '' OR vchCompania = EmployerCompany) AND
	   (EmployerPaysheet = '' OR vchNomina = EmployerPaysheet)
  GROUP BY iEmpleadoId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_GetRetardos` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_GetRetardos`(
EmployerNumber INT,
EmployerName VARCHAR(100),
EmployerCompany VARCHAR(100),
EmployerPaysheet VARCHAR(100),
FI DATETIME,
FF DATETIME)
BEGIN
  IF FF >= CURDATE() THEN
	SET FF = CURDATE();
  END IF;
SELECT IEmpleadoId        AS EmpleadoId,
  	   iNumeroEmpleado    AS NumeroEmpleado, 
  	   vchNombreEmpleado  AS NombreEmpleado,
  	   vchCompania        AS Compania,
  	   vchNomina          AS Nomina,
  	   COUNT(Retardo)     AS NumeroRetardos
FROM (
SELECT iEmpleadoId,
       iNumeroEmpleado,
       vchNombreEmpleado,
       vchNomina,
       vchCompania,
       FechaRegistro,
       Dia,
       Retardo
FROM (
SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado, 
		   EM.vchNombreEmpleado,
		   EM.vchNomina,
		   EM.vchCompania,
		   DATE(COR.FechaRegistro) AS FechaRegistro,
		   Date_format(COR.FechaRegistro, '%d/%m/%Y') AS Dia,
		   CASE 
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Monday' AND HO.bLunes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaLunes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Tuesday' AND HO.bMartes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMartes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Wednesday' AND HO.bMiercoles = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMiercoles) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Thursday' AND HO.bJueves = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaJueves) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Friday' AND HO.bViernes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaViernes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Saturday' AND HO.bSabado = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i')  <= TIME_FORMAT((TIME(HO.vchEntradaSabado) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Sunday' AND HO.bDomingo = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaDomingo) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   ELSE	'IGNORADO'
		   END AS Retardo
	  FROM Entrada_coorporativo COR
INNER JOIN Empleados EM ON EM.iEmpleadoId = COR.iEmpleadoId
INNER JOIN Horarios HO ON EM.iHorarioId = HO.iHorarioId
	 WHERE EM.Activo = 1 
	   AND EM.bExterno = 0
	   AND FechaRegistro Between FI AND DATE_ADD(FF ,INTERVAL 1 DAY)
	   AND (EmployerNumber = 0 OR EM.iNumeroEmpleado = EmployerNumber) 
	   AND (EmployerName = '' OR EM.vchNombreEmpleado LIKE CONCAT('%',EmployerName, '%')) 
	   AND (EmployerCompany = '' OR EM.vchCompania = EmployerCompany) 
     AND (EmployerPaysheet = '' OR EM.vchNomina = EmployerPaysheet)
GROUP BY Dia, iEmpleadoId
UNION
SELECT EM.iEmpleadoId,
		   EM.iNumeroEmpleado, 
		   EM.vchNombreEmpleado,
		   EM.vchNomina,
		   EM.vchCompania,
		   DATE(COR.FechaRegistro) AS FechaRegistro,
		   Date_format(COR.FechaRegistro, '%d/%m/%Y') AS Dia,
		   CASE 
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Monday' AND HO.bLunes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaLunes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Tuesday' AND HO.bMartes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMartes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Wednesday' AND HO.bMiercoles = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaMiercoles) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Thursday' AND HO.bJueves = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaJueves) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Friday' AND HO.bViernes = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaViernes) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Saturday' AND HO.bSabado = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i')  <= TIME_FORMAT((TIME(HO.vchEntradaSabado) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   WHEN Date_format(COR.FechaRegistro, '%W') = 'Sunday' AND HO.bDomingo = 1 THEN
				CASE WHEN Date_format(COR.FechaRegistro, '%H:%i') <= TIME_FORMAT((TIME(HO.vchEntradaDomingo) + TIME((SELECT Dato FROM ConfiguracionIncidencias WHERE iConfiguracionId = 1))), '%H:%i') THEN 'PUNTUAL' ELSE 'RETARDO' END
		   ELSE	'IGNORADO'
		   END AS Retardo
	  FROM Entrada_cedis COR
INNER JOIN Empleados EM ON EM.iEmpleadoId = COR.iEmpleadoId
INNER JOIN Horarios HO ON EM.iHorarioId = HO.iHorarioId
	 WHERE EM.Activo = 1 
	   AND EM.bExterno = 0
	   AND FechaRegistro Between FI AND DATE_ADD(FF ,INTERVAL 1 DAY)
	   AND (EmployerNumber = 0 OR EM.iNumeroEmpleado = EmployerNumber) 
	   AND (EmployerName = '' OR EM.vchNombreEmpleado LIKE CONCAT('%',EmployerName, '%')) 
	   AND (EmployerCompany = '' OR EM.vchCompania = EmployerCompany) 
       AND (EmployerPaysheet = '' OR EM.vchNomina = EmployerPaysheet)
  GROUP BY Dia, iEmpleadoId 
  ) AS A GROUP BY Dia, iEmpleadoId
  ) AS B WHERE Retardo = 'RETARDO' GROUP BY iEmpleadoId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `stp_ReporteIncidencias` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`lgonzalez`@`%` PROCEDURE `stp_ReporteIncidencias`(
EmployerNumber INT,
EmployerName VARCHAR(100),
EmployerCompany VARCHAR(100),
EmployerPaysheet VARCHAR(100),
FI DATETIME,
FF DATETIME)
BEGIN
  DECLARE FechaInicio DATE default '20150401';
  DECLARE FechaFin DATE default '20150430';
  DECLARE DIAS_RESTANTES INT default 0;
  DECLARE CONTADOR_DIA DATE default FechaInicio;
  DECLARE int_val INT DEFAULT 0;
  IF FF <= CURDATE() THEN
	SET FechaFin = FF;
  else
	SET FechaFin = CURDATE();
  END IF;
  SET FechaInicio = FI;
  SET DIAS_RESTANTES = datediff(FechaFin, FechaInicio);
  SET CONTADOR_DIA = FechaInicio;
  DROP TABLE IF EXISTS TEMP;
  CREATE TEMPORARY TABLE TEMP (FECHA DATE, NOMBRE_DIA VARCHAR(10));
  
  test_loop : LOOP
    IF (int_val = DIAS_RESTANTES + 1) THEN
      LEAVE test_loop;
    END IF;
    IF (SELECT COUNT(*) FROM diasfestivos WHERE FechaFestiva = CONTADOR_DIA And Activo = 1) = 0 THEN
      INSERT INTO TEMP (FECHA, NOMBRE_DIA) VALUES (CONTADOR_DIA, DATE_FORMAT(CONTADOR_DIA, '%W'));
    END IF;
    SET CONTADOR_DIA = DATE_ADD(CONTADOR_DIA ,INTERVAL 1 DAY);
    SET int_val = int_val + 1;
  END LOOP;
  SELECT EmpleadoId, NumeroEmpleado, NombreEmpleado, Compania, Nomina, FECHA, Concepto FROM(
    SELECT EM.iEmpleadoId AS EmpleadoId,
    		   EM.iNumeroEmpleado AS NumeroEmpleado,
           EM.vchNombreEmpleado AS NombreEmpleado,
    		   EM.vchCompania AS Compania,
    		   EM.vchNomina AS Nomina,
           CASE WHEN Asistencias.DIA IS NULL THEN T.FECHA ELSE Asistencias.FechaRegistro END AS FECHA,
           T.FECHA AS FECHA_REFERENCIA,
    		   CASE WHEN Asistencias.DIA IS NULL THEN 'FALTA' ELSE 'ASISTENCIA' END AS Concepto
      FROM Empleados EM
INNER JOIN TEMP T ON T.FECHA = T.FECHA
INNER JOIN Horarios H ON H.iHorarioId = EM.iHorarioId
 LEFT JOIN (  SELECT E.iEmpleadoId, FechaRegistro, DATE(FechaRegistro) AS DIA FROM Entrada_Coorporativo COR INNER JOIN Empleados E ON E.iEmpleadoId = COR.iEmpleadoId WHERE E.Activo = 1 AND FechaRegistro BETWEEN FechaInicio AND DATE_ADD(FechaFin ,INTERVAL 1 DAY) GROUP BY iEmpleadoId, DIA
              UNION
              SELECT E.iEmpleadoId, FechaRegistro, DATE(FechaRegistro) AS DIA FROM Entrada_Cedis CED INNER JOIN Empleados E ON E.iEmpleadoId = CED.iEmpleadoId WHERE E.Activo = 1 AND FechaRegistro BETWEEN FechaInicio AND DATE_ADD(FechaFin ,INTERVAL 1 DAY) GROUP BY iEmpleadoId, DIA
           )  AS Asistencias ON DATE(Asistencias.FechaRegistro) = DATE(T.FECHA) AND EM.iEmpleadoId = Asistencias.iEmpleadoId
     WHERE EM.bExterno = 0
       AND EM.Activo = 1
       AND (CASE WHEN H.bLunes = 0 AND T.NOMBRE_DIA = 'Monday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bMartes = 0 AND T.NOMBRE_DIA = 'Tuesday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bMiercoles = 0 AND T.NOMBRE_DIA = 'Wednesday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bJueves = 0 AND T.NOMBRE_DIA = 'Thursday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bViernes = 0 AND T.NOMBRE_DIA = 'Friday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bSabado = 0 AND T.NOMBRE_DIA = 'Saturday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
       AND (CASE WHEN H.bDomingo = 0 AND T.NOMBRE_DIA = 'Sunday' THEN '' ELSE T.NOMBRE_DIA END) = T.NOMBRE_DIA
  	   AND (EmployerNumber = 0 OR EM.iNumeroEmpleado = EmployerNumber) 
  	   AND (EmployerName = '' OR EM.vchNombreEmpleado LIKE CONCAT('%',EmployerName, '%')) 
  	   AND (EmployerCompany = '' OR EM.vchCompania = EmployerCompany) 
       AND (EmployerPaysheet = '' OR EM.vchNomina = EmployerPaysheet)
  ORDER BY NombreEmpleado, Fecha ASC
  ) AS TABLA GROUP BY FECHA_REFERENCIA, EmpleadoId ;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-07-18 15:53:28
