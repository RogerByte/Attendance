CREATE VIEW DatosEmpleados
AS
SELECT ESEM.NumeroEmpleado AS 'No. Empleado',
	   ESEM.NombreEmpleado AS 'Nombre del empleado',
	   ESEM.RazonSocial AS 'Razón Social',
	   ESEM.Nomina AS 'Nómina',
	   ESEM.Genero AS 'Género',
	   ESEM.Puesto AS 'Puesto',
	   CONVERT(varchar(10), ESEM.FechaAntiguedad, 103) AS 'Fecha de Antiguedad',
	   CONCAT(DATENAME(DAY, ESEM.FechaHeadcount), ' de ', DATENAME(MONTH,ESEM.FechaHeadcount), ' de ', DATENAME(YEAR,ESEM.FechaHeadcount)) AS 'Fecha Headcount',
	   ESEM.AntiguedadAnios AS 'Antigüedad en años',
	   '$ ' + CONVERT(varchar, CAST(ESEM.SueldoMensual AS money), 1) AS 'Salario Mensual',
	   '$ ' + CONVERT(varchar, CAST(ESEM.SueldoDiario AS money), 1) AS 'Sueldo Diario',
	   '$ ' + CONVERT(varchar, CAST(ESEM.SueldoIntegrado AS money), 1) AS 'Sueldo Integrado',
	   ESEM.Area AS 'Área',
	   ESEM.CentroCostos AS 'Centro de costos',

	   CASE WHEN JEFES.NombreEmpleado IS NULL THEN 'NINGUNO'
	   ELSE JEFES.NombreEmpleado END AS 'Nombre (Jefe Inmediato)',
	   CASE WHEN JEFES.Puesto IS NULL THEN 'NA'
	   ELSE JEFES.Puesto END AS 'Puesto (Jefe Inmediato)',
	   ESEM.NSS,
	   ESEM.RFC,
	   ESEM.CURP,
	   ESEM.CONTRATO,
	   BQ.calle, 
	   BQ.numero_calle, 
	   BQ.colonia, 
	   BQ.delegacion, 
	   CASE WHEN BQ.Estado = 'MEXICO' THEN 'ESTADO DE MEXICO' else BQ.Estado END as Estado,
	   CONCAT(BQ.calle, ', ' , BQ.numero_calle, ', ' , BQ.colonia, ', ' , BQ.delegacion, ', ' ,
	   CASE WHEN BQ.Estado = 'MEXICO' THEN 'ESTADO DE MEXICO' else BQ.Estado END, 
	   ', C.P. ', ESEM.codigo_postal) AS DOMICILIO,
	   REC.telefono_propio AS TELEFONO,
	   --CONVERT(varchar(10), ESEM.fecha_ingreso, 103) AS 'Fecha Ingreso',
	   CONCAT(DATENAME(DAY, ESEM.fecha_ingreso), ' de ', DATENAME(MONTH,ESEM.fecha_ingreso), ' de ', DATENAME(YEAR,ESEM.fecha_ingreso)) AS 'Fecha Ingreso',
	   dbo.CantidadConLetra(ESEM.SueldoMensual) AS 'Sueldo Letra',
	   FLOOR(ESEM.Edad) AS Edad,
	   ESEM.estado_civil AS 'Estado Civil',
	   ESEM.RFC_Compania AS 'RFC Compañia',
	   ESEM.codigo_postal AS ' Código postal'
  FROM OPENQUERY(ESLABON, 'SELECT * FROM eslabonstd.dbo.HeadCount') ESEM
LEFT JOIN 
(SELECT iEmpleadoId, intManagerID FROM OPENQUERY(ATTENDANCE, 'SELECT iEmpleadoId, intManagerID FROM fujiattendance.Empleados WHERE iEmpleadoId > 0 AND Activo = 1')) AS ATEM
ON ESEM.id = ATEM.iEmpleadoID
LEFT JOIN (SELECT id, Puesto, NombreEmpleado FROM OPENQUERY(ESLABON, 'SELECT id, Puesto, NombreEmpleado FROM eslabonstd.dbo.HeadCount')) AS JEFES
ON JEFES.id = ATEM.intManagerID
INNER JOIN (select idEmpleado, calle, numero_calle, colonia, delegacion, Estado from openquery(ESLABON, 'select * from ConsultaEmpleados')) AS BQ 
ON BQ.idEmpleado = ESEM.id
INNER JOIN (select id, telefono_propio from openquery(ESLABON, 'select id, telefono_propio from recursos')) AS REC 
ON REC.id = ESEM.id
WHERE ESEM.RazonSocial != 'FUJIFILM DE MEXICO SA DE CV'
