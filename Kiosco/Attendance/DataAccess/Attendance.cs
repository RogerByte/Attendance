using Attendance.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Attendance.DataAccess
{
    public class Attendance
    {
        MySqlConnection connection = new MySqlConnection();
        string connectionString;
        private void Conectar()
        {
            try
            {
                connectionString = ConfigurationManager.AppSettings["AttendanceConfigurationString"].ToString();
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        /// <summary>
        /// Obtiene el número de identificador del empleado según sus credenciales para el Kiosco
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public int ValidaUsuario(string username, string password)
        {
            int idEmpleado = 0;
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT iEmpleadoId FROM Empleados WHERE vchNombreUsuario = '" + username +
                "' AND vchPasswordAttendance = '" + password +"'";
            Conectar();
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dtEmpleados = new DataTable();
            dtEmpleados.Load(reader);
            foreach(DataRow row in dtEmpleados.Rows)
            {
                idEmpleado = Convert.ToInt32(row["iEmpleadoId"].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return idEmpleado;
        }
        /// <summary>
        /// método para obtener el catalogo de tipos de solicitudes
        /// </summary>
        /// <returns></returns>
        public List<cat_solicitud> ObtenerCatalogoTipoSolicitudes()
        {
            List<cat_solicitud> catalogo = new List<cat_solicitud>();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"select iTipoSolicitud, vchDescripcion, vchDip from cat_solicitudes where bitActivo = 1";
            Conectar();
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dtcatalogo = new DataTable();
            dtcatalogo.Load(reader);
            cat_solicitud registro;
            foreach (DataRow row in dtcatalogo.Rows)
            {
                registro = new cat_solicitud();
                registro.Activo = true;
                registro.iTipoSolicitudId = Convert.ToInt32(row["iTipoSolicitud"].ToString());
                registro.Descripcion = row["vchDescripcion"].ToString();
                registro.Dip = row["vchDip"].ToString();                
                catalogo.Add(registro);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return catalogo;
        }
        /// <summary>
        /// Obitenel a lista de solicitudes vigentes
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public List<Solicitud> ObtenerSolicitudesVigentes(int EmpleadoId)
        {
            List<Solicitud> Solicitudes = new List<Solicitud>();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT S.iSolicitudId, 
		                                   S.iTipoSolicitud,
		                                   S.iEmpleadoSolicitante,
		                                   S.datFechaInicio,
		                                   S.datFechaFin,
		                                   S.iEstatusSolicitud,
		                                   S.bitCerrada,
		                                   CS.vchDescripcion AS TipoSolicitud, 
		                                   CES.vchDescripcion AS EstatusSolicitud
	                                  FROM Solicitudes S
                                INNER JOIN CAT_Solicitudes CS
		                                ON CS.iTipoSolicitud = S.iTipoSolicitud
                                INNER JOIN CAT_Estatus_Solicitudes CES
		                                ON CES.iEstatusSolicitud = S.iEstatusSolicitud
	                                 WHERE S.bitCerrada = 0
	                                   AND iEmpleadoSolicitante = " + EmpleadoId;
            Conectar();
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dtSolicitudes = new DataTable();
            dtSolicitudes.Load(reader);
            Solicitud Modelo;
            foreach (DataRow row in dtSolicitudes.Rows)
            {
                Modelo = new Solicitud();
                Modelo.EmpleadoId = EmpleadoId;
                Modelo.Cerrada = false;
                Modelo.iSolicitudId = Convert.ToInt32(row["iSolicitudId"].ToString());
                Modelo.iTipoSolicitud = Convert.ToInt32(row["iTipoSolicitud"].ToString());
                Modelo.FechaInicio = Convert.ToDateTime(row["datFechaInicio"].ToString());
                Modelo.FechaFin = Convert.ToDateTime(row["datFechaFin"].ToString());
                Modelo.iEstatusSolicitud = Convert.ToInt32(row["iEstatusSolicitud"].ToString());
                Modelo.strTipoSolicitud = row["TipoSolicitud"].ToString();
                Modelo.strEstatusSolicitud = row["EstatusSolicitud"].ToString();
                Solicitudes.Add(Modelo);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return Solicitudes;
        }
        /// <summary>
        /// Permite actualizar la información de una solicitud
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public bool ActualizaSolicitud(Solicitud Request)
        {
            bool Actualizado = false;
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction tr = null;
            if (Request.FechaInicio > Request.FechaFin)
                throw new Exception("Incongruencia de fechas en la solicitud");
            try
            {
                command.CommandText = @"UPDATE Solicitudes 
		                                   SET iTipoSolicitud = " + Request.iTipoSolicitud + @",
			                                   datFechaInicio = '" + Request.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + @"',
			                                   datFechaFin = '" + Request.FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + @"',
			                                   iEstatussolicitud = " + Request.iEstatusSolicitud + @",
			                                   bitCerrada = " + (Request.Cerrada ? "1" : "0") + @",
			                                   vchObservaciones = '" + Request.strObservaciones + @"'
		                                 WHERE iSolicitudId = " + Request.iSolicitudId;
                Conectar();
                tr = connection.BeginTransaction();
                command.ExecuteNonQuery();
                tr.Commit();
                Actualizado = true;
                command.Dispose();
                connection.Close();
            }
            catch { tr.Rollback(); }
            return Actualizado;
        }
        /// <summary>
        /// Permite insertar un registro de solicitud
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public int InsertaSolicitud(Solicitud Request)
        {
            int SolicitudId = 0;
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction tr = null;
            if (Request.FechaInicio > Request.FechaFin)
                throw new Exception("Incongruencia de fechas en la solicitud");
            try
            {
                command.CommandText = @"INSERT INTO Solicitudes (iTipoSolicitud, iEmpleadoSolicitante, datFechaInicio, datFechaFin, vchObservaciones, iEstatusSolicitud, bitCerrada) VALUES(" + Request.iTipoSolicitud + ", " + Request.EmpleadoId + ", '" + Request.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Request.FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Request.strObservaciones + "', " + Request.iEstatusSolicitud + ", " + (Request.Cerrada ? "0" : "1") + ")";
                Conectar();
                tr = connection.BeginTransaction();
                command.ExecuteNonQuery();
                SolicitudId = Convert.ToInt32(command.LastInsertedId);
                tr.Commit();
                command.Dispose();
                connection.Close();
            }
            catch
            {
                tr.Rollback();
            }
            return SolicitudId;
        }
        /// <summary>
        /// Obtiene el id del empleado a partir de su nombre de usuario
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int ObtenerIdEmpleado(string username)
        {
            int idEmpleado = 0;
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT iEmpleadoId FROM Empleados WHERE vchNombreUsuario = '" + username + "'";
            Conectar();
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dtEmpleados = new DataTable();
            dtEmpleados.Load(reader);
            foreach (DataRow row in dtEmpleados.Rows)
            {
                idEmpleado = Convert.ToInt32(row["iEmpleadoId"].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return idEmpleado;
        }
        /// <summary>
        /// Método que permite obtener la información general del empleado desde la base de datos de Attendance
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public Empleado ObtenerInformacionGeneralEmpleado(int EmpleadoId)
        {
            Empleado Response = new Empleado();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT iEmpleadoId,
                                    iNumeroEmpleado,
                                    vchNombreEmpleado,
                                    vchCompania,
                                    vchNombreUsuario,
                                    vchPasswordAttendance,
                                    vchCorreo,
                                    intManagerID,
                                    bitManager 
                                    FROM Empleados WHERE Activo = 1 AND iEmpleadoId = " + EmpleadoId;
            Conectar();
            MySqlDataReader reader = command.ExecuteReader();
            DataTable dtEmpleado = new DataTable();
            dtEmpleado.Load(reader);
            foreach (DataRow row in dtEmpleado.Rows)
            {
                Response.Id = Convert.ToInt32(row["iEmpleadoId"].ToString());
                Response.NumeroEmpleado = row["iNumeroEmpleado"].ToString();
                Response.NombreEmpleado = row["vchNombreEmpleado"].ToString();
                Response.Compania = row["vchCompania"].ToString();
                Response.NombreUsuario = row["vchNombreUsuario"].ToString();
                Response.Password = row["vchPasswordAttendance"].ToString();
                Response.Correo = row["vchCorreo"].ToString();
                Response.ManagerId = Convert.ToInt32(row["intManagerID"].ToString());
                Response.isManager = (row["bitManager"].ToString() == "0" ? false : true);
            }
            reader.Close();
            command.Dispose();
            connection.Clone();
            return Response;
        }
    }
}