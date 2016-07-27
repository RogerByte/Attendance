CREATE VIEW ConsultaEmpleados AS
SELECT DISTINCT REC.paterno + ' ' + REC.materno + ' ' + REC.nombre AS NombreEmpleado,
				EM.id AS idEmpleado,
				EM.empleado AS NumeroEmpleado,
				P.descripcion AS Puesto,
				NE.nivel AS NivelDepartamento,
				NE.descripcion AS Departamento,
				EM.compania AS idCompania,
				COM.razon_social AS Compania,
				NOM.descripcion AS Nomina,
				REC.fecha_nacimiento AS FechaNacimiento,
				EM.cedula_imss,
				REC.curp,
				REC.calle,
				REC.numero_calle,
				REC.colonia,
				cat_del.descripcion as delegacion,
				CASE WHEN e.descripcion = 'DISTRITO FEDERAL' THEN 'CIUDAD DE MEXICO' ELSE e.descripcion END AS Estado,
				REC.codigo_postal,
				ESU.sueldo_mensual,
				INTEGRADOS.sueldo_integrado
		  FROM empleados EM 
	INNER JOIN recursos REC 
			ON REC.id = EM.id
	INNER JOIN empleados_plazas EPZ 
			ON EPZ.id = EM.id AND EPZ.compania = EM.compania
	INNER JOIN plazas PZ
			ON EPZ.plaza = PZ.plaza AND PZ.compania = EM.compania
	INNER JOIN puestos P
			ON PZ.id_nivel_puesto = P.id_nivel_puesto AND P.compania = PZ.compania
	INNER JOIN contratos C
			ON C.contrato = PZ.contrato AND C.compania = PZ.compania
	INNER JOIN estructuras_empleado EE
			ON EE.id = EM.id
	INNER JOIN niveles_estructura NE
			ON NE.id_nivel_estructura = EE.id_nivel_estructura
	INNER JOIN estructuras ES
			ON ES.compania = EM.compania AND NE.estructura = ES.estructura
	INNER JOIN nominas NOM
			ON EM.nomina = NOM.nomina AND EM.compania = NOM.compania
	INNER JOIN companias COM
			ON COM.compania = EM.compania
	INNER JOIN delegaciones_municipios cat_del 
			ON cat_del.delegacion_municipio = REC.delegacion_municipio
	INNER JOIN estados e 
			ON e.estado = REC.estado
	INNER JOIN empleados_sueldos ESU 
			ON ESU.id = EM.id
	INNER JOIN integrados INTEGRADOS 
			ON INTEGRADOS.id = EM.id
		 WHERE EM.estatus != 2
		   AND EPZ.fecha_vigencia_hasta IS NULL
		   AND NE.fecha_vigencia_hasta IS NULL
		   AND EE.fecha_vigencia_hasta IS NULL
		   AND ESU.fecha_vigencia_hasta IS NULL
		   AND INTEGRADOS.fecha_hasta IS NULL
		   AND ES.estructura = 2
		   AND EM.id != 4999
