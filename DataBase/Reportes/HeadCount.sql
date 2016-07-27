CREATE VIEW HeadCount
AS
SELECT ESEM.NumeroEmpleado AS 'No. Empleado',
	   ESEM.NombreEmpleado AS 'Nombre del empleado',
	   ESEM.RazonSocial AS 'Razón Social',
	   ESEM.Nomina AS 'Nómina',
	   ESEM.Genero AS 'Género',
	   ESEM.Puesto AS 'Puesto',
	   CONVERT(varchar(10), ESEM.FechaAntiguedad, 103) AS 'Fecha de Antiguedad',
	   CONVERT(varchar(10), ESEM.FechaHeadcount, 103) AS 'Fecha Headcount',
	   ESEM.AntiguedadAnios AS 'Antigüedad en años',
	   '$ ' + CONVERT(varchar, CAST(ESEM.SueldoMensual AS money), 1) AS 'Salario Mensual',
	   '$ ' + CONVERT(varchar, CAST(ESEM.SueldoDiario AS money), 1) AS 'Sueldo Diario',
	   ESEM.SueldoIntegrado AS 'Sueldo Integrado',
	   ESEM.Area AS 'Área',
	   ESEM.CentroCostos AS 'Centro de costos',

	   CASE WHEN JEFES.NombreEmpleado IS NULL THEN 'NINGUNO'
	   ELSE JEFES.NombreEmpleado END AS 'Nombre (Jefe Inmediato)',
	   CASE WHEN JEFES.Puesto IS NULL THEN 'NA'
	   ELSE JEFES.Puesto END AS 'Puesto (Jefe Inmediato)',
	   ESEM.NSS,
	   ESEM.RFC,
	   ESEM.CURP,
	   ESEM.CONTRATO
  FROM OPENQUERY(ESLABON, 'SELECT * FROM eslabonstd.dbo.HeadCount') ESEM
LEFT JOIN 
(SELECT iEmpleadoId, intManagerID FROM OPENQUERY(ATTENDANCE, 'SELECT iEmpleadoId, intManagerID FROM fujiattendance.Empleados WHERE iEmpleadoId > 0 AND Activo = 1')) AS ATEM
ON ESEM.id = ATEM.iEmpleadoID
LEFT JOIN (SELECT id, Puesto, NombreEmpleado FROM OPENQUERY(ESLABON, 'SELECT id, Puesto, NombreEmpleado FROM eslabonstd.dbo.HeadCount')) AS JEFES
ON JEFES.id = ATEM.intManagerID
WHERE ESEM.RazonSocial != 'FUJIFILM DE MEXICO SA DE CV'