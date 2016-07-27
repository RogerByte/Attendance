CREATE VIEW HeadCount AS
SELECT DISTINCT EM.id,
				EM.empleado AS NumeroEmpleado,
				REC.paterno + ' ' + REC.materno + ' ' + REC.nombre AS NombreEmpleado,
				COM.razon_social AS RazonSocial,
				NOM.descripcion AS Nomina,
				CASE WHEN REC.sexo = 1 THEN 'M' ELSE 'F' END AS Genero,
				P.descripcion AS Puesto,
				EM.fecha_antiguedad AS FechaAntiguedad,
				GETDATE() AS FechaHeadcount,
				dbo.fn_getYearsDifference(EM.fecha_antiguedad, GETDATE()) AS AntiguedadAnios,
				ESU.sueldo_mensual AS SueldoMensual,
				ESU.sueldo_diario AS SueldoDiario,
				ING.sueldo_integrado AS SueldoIntegrado,
				dbo.ObtenerDescripcionArea(NE.descripcion, ' ') AS Area,
				(SELECT ne.descripcion 
				   FROM niveles_estructura ne
			 INNER JOIN estructuras_empleado ee
					 ON ee.id_nivel_estructura = ne.id_nivel_estructura 
					AND ee.fecha_vigencia_hasta IS NULL 
					AND ne.fecha_vigencia_hasta IS NULL
				  WHERE ee.id = EM.id
					AND estructura = 2 
					AND ne.compania = EM.compania) AS CentroCostos,
				EM.cedula_imss AS NSS,
				REC.cuenta_individual AS RFC,
				REC.curp AS CURP,
				CONT.descripcion AS CONTRATO,
				CASE WHEN EM.fecha_reingreso IS NULL THEN EM.fecha_alta ELSE EM.fecha_reingreso END AS fecha_ingreso,
				dbo.fn_getYearsDifference(REC.fecha_nacimiento, GETDATE()) AS edad,
				CASE WHEN REC.estado_civil = 0 THEN 'casado' 
				WHEN REC.estado_civil = 1 THEN 'divorciado'
				WHEN REC.estado_civil = 2 THEN 'soltero'
				WHEN REC.estado_civil = 3 THEN 'union libre'
				WHEN REC.estado_civil = 4 THEN 'viudo'
				ELSE 'NINGUNO' END AS estado_civil, /*2-soltero  1- divorciado   3- union libre  4- viudo   0-casado*/
				COM.RFC AS RFC_Compania,
				REC.codigo_postal
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
	INNER JOIN integrados ING 
			ON ING.id = EM.id
	INNER JOIN empleados_contratos EC
			ON EC.id = EM.id AND EC.fecha_vigencia_hasta IS NULL
	INNER JOIN contratos CONT 
			ON CONT.contrato = EC.contrato AND CONT.compania = EM.compania
		 WHERE EM.estatus != 2
		   AND EPZ.fecha_vigencia_hasta IS NULL
		   AND NE.fecha_vigencia_hasta IS NULL
		   AND EE.fecha_vigencia_hasta IS NULL
		   AND ESU.fecha_vigencia_hasta IS NULL
		   AND ING.fecha_hasta IS NULL
		   AND ES.estructura = 1
		   AND EM.id != 4999
		   AND C.compania IN (1,2,4,7,8)
		   AND ESU.compania IN (1,2,4,7,8)
		   AND ING.compania IN (1,2,4,7,8)
		   AND NE.compania IN (1,2,4,7,8)


		 