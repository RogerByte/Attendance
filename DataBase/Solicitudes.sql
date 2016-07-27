DROP TABLE CAT_Solicitudes;
DROP TABLE CAT_Estatus_Solicitudes;
DROP TABLE Solicitudes;
/*--------------------CATALOGO DE SOLICITUDES--------------------*/
CREATE TABLE CAT_Solicitudes(
	iTipoSolicitud INT PRIMARY KEY AUTO_INCREMENT,
	vchDescripcion VARCHAR(100) NOT NULL,
	vchDip VARCHAR(10),
	bitActivo BIT NOT NULL
);
INSERT INTO CAT_Solicitudes (vchDescripcion, vchDip, bitActivo) VALUES ('VACACIONES', '4060', 1);
INSERT INTO CAT_Solicitudes (vchDescripcion, vchDip, bitActivo) VALUES ('PERMISO', '', 1);
INSERT INTO CAT_Solicitudes (vchDescripcion, vchDip, bitActivo) VALUES ('PERMISO CON GOCE', '', 1);
INSERT INTO CAT_Solicitudes (vchDescripcion, vchDip, bitActivo) VALUES ('INCAPACIDAD', '', 1);
/*--------------------CATALOGO DE ESTATUS DE SOLICITUDES--------------------*/
CREATE TABLE CAT_Estatus_Solicitudes(
	iEstatusSolicitud INT PRIMARY KEY AUTO_INCREMENT,
	vchDescripcion VARCHAR(100) NOT NULL,
	bitActivo BIT NOT NULL
);
INSERT INTO CAT_Estatus_Solicitudes(vchDescripcion, bitActivo) VALUES('EN ESPERA', 1);
INSERT INTO CAT_Estatus_Solicitudes(vchDescripcion, bitActivo) VALUES('ACEPTADA', 1);
INSERT INTO CAT_Estatus_Solicitudes(vchDescripcion, bitActivo) VALUES('RECHAZADA', 1);
/*--------------------TABLA DE SOLICITUDES--------------------*/
CREATE TABLE Solicitudes(
	iSolicitudId INT PRIMARY KEY AUTO_INCREMENT,
	iTipoSolicitud INT NOT NULL,
	iEmpleadoSolicitante INT NOT NULL,
	datFechaInicio DATETIME NOT NULL,
	datFechaFin DATETIME NOT NULL,
	vchObservaciones VARCHAR(200),
	iEstatusSolicitud INT NOT NULL,
	bitCerrada BIT NOT NULL,
	FOREIGN KEY (iEmpleadoSolicitante) REFERENCES Empleados(iEmpleadoId),
	FOREIGN KEY (iTipoSolicitud) REFERENCES CAT_Solicitudes(iTipoSolicitud),
	FOREIGN KEY (iEstatusSolicitud) REFERENCES CAT_Estatus_Solicitudes(iEstatusSolicitud)
);

SELECT * FROM Empleados WHERE vchNombreEmpleado LIKE '%LAZARO%';
/*Método para insertar una nueva solicitud*/
INSERT INTO Solicitudes
(iTipoSolicitud, iEmpleadoSolicitante, datFechaInicio, datFechaFin, vchObservaciones, iEstatusSolicitud, bitCerrada) 
VALUES 
(1,5833, '2016-04-12', '2016-04-14', 'Vacaciones porque me siento cansado', 1, 0);

	SELECT S.iSolicitudId, 
		   S.iTipoSolicitud,
		   S.iEmpleadoSolicitante,
		   S.datFechaInicio,
		   S.datFechaFin,
		   S.iEstatusSolicitud,
		   S.bitCerrada,
		   CS.vchDescripcion, 
		   CES.vchDescripcion
	  FROM Solicitudes S
INNER JOIN CAT_Solicitudes CS
		ON CS.iTipoSolicitud = S.iTipoSolicitud
INNER JOIN CAT_Estatus_Solicitudes CES
		ON CES.iEstatusSolicitud = S.iEstatusSolicitud
	 WHERE S.bitCerrada = 0
	   AND iEmpleadoSolicitante = 5833;

		UPDATE Solicitudes 
		   SET iTipoSolicitud = 1,
			   datFechaInicio = '2016-04-12 00:00:00',
			   datFechaFin = '2016-04-12 00:00:00',
			   iEstatussolicitud = 1,
			   bitCerrada = 0,
			   vchObservaciones = ''
		 WHERE iSolicitudId = 1;

SELECT last_insert_id();
							   
