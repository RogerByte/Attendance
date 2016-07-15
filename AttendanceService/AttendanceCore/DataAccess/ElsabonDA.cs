using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttendanceCore.BusinessLogic;
using System.Data.Odbc;
using AttendanceCore.Entities;
using System.Configuration;
namespace AttendanceCore.DataAccess
{
    public class EslabonDA
    {
        public EslabonDA()
        {
            ConnectionString = ConfigurationManager.AppSettings["EslabonConfigurationString"].ToString();
        }
        private string ConnectionString { get; set; }
        /// <summary>
        /// Método que permite obtener el número de empleado apartir del ID de nómina del mismo.
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public string ObtenerNumeroEmpleado(int EmpleadoId)
        {
            string Response = string.Empty;
            try
            {
                string QueryString = @"SELECT NumeroEmpleado
                                         FROM vwEmpleados
                                        WHERE idEmpleado = '" + EmpleadoId.ToString() + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Response = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.ObtenerNumeroEmpleado - " + ex.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite obtener el nivel de estructura de un empleado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public string ObtenerNivelEstructura(int EmpleadoId)
        {
            string Response = string.Empty;
            try
            {
                string QueryString = @"SELECT NivelDepartamento
                                         FROM vwEmpleados
                                        WHERE idEmpleado = '" + EmpleadoId.ToString() + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Response = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.ObtenerNivelEstructura - " + ex.Message);
            }
            return Response;
        }
        /// <summary>
        /// Obtiene la información complementaria de un empleado desde la lectura del head count
        /// </summary>
        /// <returns></returns>
        public List <ComplementoEmpleado> ObtenerInformacionComplementaria()
        {
            List<ComplementoEmpleado> InformacionComplementaria = new List<ComplementoEmpleado>();
            try
            {
                string QueryString = @"SELECT id, RazonSocial, Nomina, CentroCostos FROM headcount";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        ComplementoEmpleado Employer;
                        string [] CentroCostos;
                        while (reader.Read())
                        {
                            Employer = new ComplementoEmpleado();
                            Employer.EmpleadoId = reader.GetInt32(0);
                            Employer.RazonSocial = reader.GetString(1);
                            Employer.Nomina = reader.GetString(2);
                            CentroCostos = reader.GetString(3).Split(' ');
                            Employer.ClaveCentroCostos = CentroCostos.Length > 0 ? CentroCostos[0] : "";

                            if (Employer.ClaveCentroCostos != "")
                                for(int i = 1; i < CentroCostos.Length; i++)
                                    Employer.DescripcionCentroCostos += CentroCostos[i] + " ";
                            
                            InformacionComplementaria.Add(Employer);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error: EslabonDA.ObtenerInformacionComplementaria - " + exc.Message);
            }
            return InformacionComplementaria;
        }
        /// <summary>
        /// Método que permite obtener la descripción de nómina según el empleado ingresado como parámetro
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public string ObtenerNominaEmpleado(int EmpleadoId)
        {
            string Response = string.Empty;
            try
            {
                string QueryString = @"SELECT Nomina
                                         FROM vwEmpleados
                                        WHERE idEmpleado = '" + EmpleadoId.ToString() + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Response = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.ObtenerNominaEmpleado - " + ex.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite obtener la descripción de compañía según el empleado ingresado como parámetro
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public string ObtenerCompaniaEmpleado(int EmpleadoId)
        {
            string Response = string.Empty;
            try
            {
                string QueryString = @"SELECT Compania
                                         FROM vwEmpleados
                                        WHERE idEmpleado = '" + EmpleadoId.ToString() + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Response = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.ObtenerCompaniaEmpleado - " + ex.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite obtener el nombre del empleado con referencia a su ID de nómina.
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public string ObtenerNombreEmpleado(int EmpleadoId)
        {
            string Response = string.Empty;
            try
            {
                string QueryString = @"SELECT NombreEmpleado
                                         FROM vwEmpleados
                                        WHERE idEmpleado = '" + EmpleadoId.ToString() + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Response = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.ObtenerNumeroEmpleado - " + ex.Message);
            }
            return Response;
        }
        /// <summary>
        /// Obtiene la lista de empleados de la nómina
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoEmpleados()
        {
            List<Catalogo> Empleados = new List<Catalogo>();
            try
            {
                string QueryString = @"SELECT idEmpleado, NombreEmpleado FROM vwEmpleados";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        Catalogo Empleado;
                        while (reader.Read())
                        {
                            Empleado = new Catalogo();
                            Empleado.id = reader.GetInt32(0);
                            Empleado.Descripcion = reader.GetString(1);
                            Empleados.Add(Empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.CatalogoEmpleados - " + ex.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// SELECT DISTINCT descripcion AS Nomina FROM nominas
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoNomina()
        {
            List<Catalogo> Nomina = new List<Catalogo>();
            try
            {
                string QueryString = @"SELECT DISTINCT descripcion AS Nomina FROM nominas";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    int i = 1;
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        Catalogo modelo;
                        while (reader.Read())
                        {
                            modelo = new Catalogo();
                            modelo.id = i;
                            modelo.Descripcion = reader.GetString(0);
                            Nomina.Add(modelo);
                            i++;
                        }
                        modelo = new Catalogo();
                        modelo.id = 1000;
                        modelo.Descripcion = "TODOS";
                        Nomina.Add(modelo);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.CatalogoNomina - " + ex.Message);
            }
            return Nomina;
        }
        /// <summary>
        /// SELECT compania AS id, razon_social AS compania FROM companias
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoCompanias()
        {
            List<Catalogo> Companias = new List<Catalogo>();
            try
            {
                string QueryString = @"SELECT compania AS id, razon_social AS compania FROM companias";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        Catalogo compania;
                        while (reader.Read())
                        {
                            compania = new Catalogo();
                            compania.id = reader.GetInt32(0);
                            compania.Descripcion = reader.GetString(1);
                            Companias.Add(compania);
                        }
                        compania = new Catalogo();
                        compania.id = 1000;
                        compania.Descripcion = "TODAS";
                        Companias.Add(compania);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.CatalogoCompanias - " + ex.Message);
            }
            return Companias;
        }
        /// <summary>
        /// Permite valorar si el empleado tiene justificado el día ingresado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public bool GetJustificacion(int EmpleadoId, DateTime Fecha)
        {
            bool DiaJustificado = false;
            try
            {
                string QueryString = @"EXEC stp_GetJustificacionFalta " + EmpleadoId + ", '" + Fecha.ToString("yyyyMMdd") + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DiaJustificado = reader.GetBoolean(0);
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.GetJustificacion - " + ex.Message);
            }
            return DiaJustificado;
        }
        /// <summary>
        /// Permite obtener la lista de permisos, incapacidades y vacaciones solicitadas durante el periodo que se ingresa como parámetro
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<DiasJustificados> ObtenerDiasJustificados(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DiasJustificados> Lista = new List<DiasJustificados>();
            FechaInicio = FechaInicio.AddMonths(-2); //Va y verifica si existen días de meses pasados
            FechaFin = FechaFin.AddDays(1);
            try
            {
                string QueryString = @" DECLARE @FECHA_INICIO DATETIME 
	                                    DECLARE @FECHA_FIN DATETIME

	                                    SET @FECHA_INICIO = '" + FechaInicio.ToString("yyyyMMdd") + @"' 
	                                    SET @FECHA_FIN = '" + FechaFin.ToString("yyyyMMdd") + @"'

	                                    SELECT A.ID, A.FECHA_DESDE, DATEDIFF(day, A.FECHA_DESDE, A.FECHA_HASTA) + 1 AS C_DIAS, A.CONCEPTO FROM (
                                        SELECT r.id AS ID,
	                                            CONVERT(DATETIME, dbo.ScalarSplit(r.referencia, ' ', 1)) AS FECHA_DESDE,
	                                            CONVERT(DATETIME, dbo.ScalarSplit(r.referencia, ' ', 2)) AS FECHA_HASTA,
	                                            d.descripcion AS CONCEPTO
                                        FROM recibos r INNER JOIN dips d on d.dip = r.dip
                                        WHERE r.dip IN ('4090', '7470', '4070')
	                                        AND r.fecha_registro >= @FECHA_INICIO
                                        UNION 
                                        SELECT c.id AS ID,
	                                            CONVERT(DATETIME, dbo.ScalarSplit(c.referencia, ' ', 1)) AS FECHA_DESDE,
	                                            CONVERT(DATETIME, dbo.ScalarSplit(c.referencia, ' ', 2)) AS FECHA_HASTA,
	                                            d.descripcion AS CONCEPTO
                                        FROM captura c INNER JOIN dips d on d.dip = c.dip
                                        WHERE c.dip IN ('4090', '7470', '4070')
	                                        AND c.fecha_registro >= @FECHA_INICIO ) AS A 
                                        WHERE (A.FECHA_DESDE BETWEEN @FECHA_INICIO AND @FECHA_FIN)
                                        UNION
                                        SELECT id AS ID,
	                                            fecha_incapacidad_desde AS FECHA_DESDE,
	                                            dias_incapacidad AS C_DIAS,
	                                            'INCAPACIDAD' AS CONCEPTO
                                        FROM incapacidades
                                        WHERE (fecha_incapacidad_desde BETWEEN @FECHA_INICIO AND @FECHA_FIN)
                                        UNION
                                        SELECT id AS ID,
	                                            fecha_inicio AS FECHA_DESDE,
	                                            dias_descanso AS C_DIAS,
	                                            'VACACIONES' AS CONCEPTO
                                        FROM vacaciones_empleado
                                        WHERE (fecha_inicio BETWEEN @FECHA_INICIO AND @FECHA_FIN)";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(ConnectionString))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        DiasJustificados Modelo;
                        while (reader.Read())
                        {
                            Modelo = new DiasJustificados();
                            Modelo.EmpleadoId = reader.GetInt32(0);
                            Modelo.FechaDesde = reader.GetDateTime(1);
                            Modelo.NumeroDias = reader.GetInt32(2);
                            Modelo.Concepto = reader.GetString(3);
                            Lista.Add(Modelo);
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.EscribeLog("Error: ElsabonDA.ObtenerDiasJustificados - " + ex.Message);
            }
            return Lista;
        }
    }
}