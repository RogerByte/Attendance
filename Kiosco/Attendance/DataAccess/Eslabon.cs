using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using Attendance.Entities;
using System.Configuration;

namespace Attendance.DataAccess
{
    public class Eslabon
    {
        private static string CadenaConexion = ConfigurationManager.AppSettings["EslabonConfigurationString"].ToString();
        /// <summary>
        /// Permite obtener la cantidad de días que el empleado ha gozado de vacaciones en el periodo definido entre FechaInicio y FechaFin
        /// </summary>
        /// <param name="empleado"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public static int GetTakenVacations(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            int TakenVacations = 0;
            try
            {
                string QueryString = "SELECT SUM(VACACIONES.dias_descanso) AS VacacionesTomadas " +
                                 "	    FROM vacaciones_empleado VACACIONES " +
                                 "INNER JOIN vwEmpleados EMPLEADOS " +
                                 "        ON VACACIONES.id = EMPLEADOS.idEmpleado " +
                                 "	 	 AND VACACIONES.compania = EMPLEADOS.idCompania " +
                                 "     WHERE EMPLEADOS.idEmpleado = " + EmpleadoId +
                                 "	 	 AND VACACIONES.fecha_inicio BETWEEN '" + FechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                 "		 AND '" + FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(CadenaConexion))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            TakenVacations = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                //Agregar bitácora para marcar las excepciones surgidas en la clase de acceso a datos.
            }
            return TakenVacations;
        }
        /// <summary>
        /// Permite obtener las vacaciones tomadas según el tiempo de antiguedad
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public static int GetTakenVacations(int EmpleadoId, int Antiguedad)
        {
            int TakenVacations = 0;
            try
            {
                string QueryString = "SELECT SUM(dias_descanso) FROM vacaciones_empleado WHERE id = " + EmpleadoId + " AND antiguedad = " + Antiguedad;
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(CadenaConexion))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TakenVacations = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                //Agregar bitácora para marcar las excepciones surgidas en la clase de acceso a datos.
            }
            return TakenVacations;
        }
        /// <summary>
        /// Permite obtener la fecha de antigüedad del empleado ingresado como parámetro
        /// </summary>
        /// <param name="EmpladoId"></param>
        /// <returns>DateTime</returns>
        public static DateTime GetAntiquity(int EmpleadoId)
        {
            DateTime AntiquityDate = new DateTime();
            try
            {
                string QueryString = "SELECT fecha_antiguedad " +
                                 "	    FROM Empleados EMP " +
                                 "INNER JOIN vwEmpleados VISTA " +
                                 "		  ON VISTA.idEmpleado = EMP.id " +
                                 "		 AND VISTA.idCompania = EMP.compania " +
                                 "	   WHERE VISTA.idEmpleado = " + EmpleadoId;
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(CadenaConexion))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AntiquityDate = reader.GetDateTime(0);
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                //Agregar bitácora para marcar las excepciones surgidas en la clase de acceso a datos.
            }
            return AntiquityDate;
        }
        /// <summary>
        /// Permite obtener el puesto de un empleado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public string ObtenerPuestoEmpleado(int EmpleadoId)
        {
            string Puesto = string.Empty;
            try
            {
                string QueryString = @"SELECT Puesto
                                         FROM vwEmpleados
                                        WHERE idEmpleado = " + EmpleadoId.ToString();
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(CadenaConexion))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Puesto = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return Puesto;
        }
        /// <summary>
        /// Obtiene el parámetro máximo de antiguedad de la tabla de vacaciones
        /// </summary>
        /// <returns></returns>
        public static int MaximaAntiguedadVacaciones(int EmpleadoId)
        {
            int AntVacaciones = 0;
            try
            {
                string QueryString = @"SELECT MAX (antiguedad) FROM vacaciones_empleado WHERE id =" + EmpleadoId.ToString();
                System.Data.Odbc.OdbcCommand command = new System.Data.Odbc.OdbcCommand(QueryString);
                using (System.Data.Odbc.OdbcConnection connection = new System.Data.Odbc.OdbcConnection(CadenaConexion))
                {
                    command.Connection = connection;
                    connection.Open();
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AntVacaciones = Convert.ToInt32(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return AntVacaciones;
        }
    }
}