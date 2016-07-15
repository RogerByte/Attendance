using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using AttendanceCore.Entities.DeviceEntities;
using System.Data;
using AttendanceCore.Entities;
using AttendanceCore.ServiceContracts;
using System.Globalization;
using System.Configuration;

namespace AttendanceCore.DataAccess
{
    public class AttendanceDA
    {
        public AttendanceDA()
        {
            connection = new MySqlConnection();
        }
        MySqlConnection connection;
        private void Conectar()
        {
            try
            {
                string value = ConfigurationManager.AppSettings["AttendanceConfigurationString"].ToString();
                connection.ConnectionString = value;
                connection.Open();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public List<Catalogo> CatalogoManagers()
        {
            List<Catalogo> Managers = new List<Catalogo>();
            MySqlCommand command = connection.CreateCommand();
            try
            {
                string query = @"SELECT iEmpleadoId,
	                             vchNombreEmpleado
	                             FROM Empleados 
                                 WHERE bitManager = 1 AND Activo = 1";
                command.CommandText = query;
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                Catalogo Entidad = new Catalogo();
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    Entidad = new Catalogo();
                    Entidad.id = Convert.ToInt32(row["iEmpleadoId"].ToString());
                    Entidad.Descripcion = row["vchNombreEmpleado"].ToString();
                    Managers.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.CatalogoManagers - " + exc.Message);
            }
            return Managers;
        }
        /// <summary>
        /// Permite actualizar el usuario ingresado como parámetro
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool ActualizaUsuario(Usuario User)
        {
            bool Actualizado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"UPDATE USUARIOS SET vchNombreUsuario = '"+ User.Nombre +"', vchUsuario = '"+ User.NombreUsuario +"', vchPassword = '"+ User.Password +"' WHERE iUsuarioId = " + User.idUsuario;
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Actualizado = true;
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizaUsuario - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Método que permite dar de alta un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool InsertaUsuario(Usuario user)
        {
            bool Insertado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"INSERT INTO USUARIOS (vchNombreUsuario, vchUsuario, vchPassword, bActivo) VALUES ('"+ user.Nombre +"', '"+ user.NombreUsuario +"', '"+ user.Password +"', 1)";
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Insertado = true;
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaUsuario - " + exc.Message);
            }
            return Insertado;
        }
        /// <summary>
        /// Método que permite eliminar un usuario administrador
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        public bool EliminaUsuario(int UsuarioId)
        {
            bool Eliminado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"UPDATE Usuarios SET bActivo = 0 WHERE iUsuarioId = " + UsuarioId;
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Eliminado = true;
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.EliminaUsuario - " + exc.Message);
            }
            return Eliminado;
        }
        /// <summary>
        /// Método privado que permite actualizar la fecha de inicio de sesión del usuario ingresado como parámetro
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        private bool ActualizaFechaSesionUsuario(int UsuarioId)
        {
            bool Actualizado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaSesion = DateTime.Now;
                command.CommandText = @"UPDATE USUARIOS SET datFechaSesion = '" + FechaSesion.ToString("yyyy-MM-dd HH:mm:ss") + @"' WHERE iUsuarioId = " + UsuarioId;
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Actualizado = true;
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizaFechaSesionUsuario - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Método que permite obtener toda la lista de los usuarios administradores del sistema
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public List<Usuario> ObtenerListaUsuariosActivos(string Nombre)
        {
            List<Usuario> Usuarios = new List<Usuario>();
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"SELECT iUsuarioId, vchNombreUsuario, vchUsuario, datFechaSesion, vchPassword 
                                        FROM Usuarios WHERE bActivo = 1 AND vchNombreUsuario LIKE '%" + Nombre + "%';";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                Usuario modelo;
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    modelo = new Usuario();
                    modelo.idUsuario = Convert.ToInt32(row["iUsuarioId"].ToString());
                    modelo.Nombre = row["vchNombreUsuario"].ToString();
                    modelo.NombreUsuario = row["vchUsuario"].ToString();
                    modelo.FechaSesion = row["datFechaSesion"].ToString();
                    modelo.Password = row["vchPassword"].ToString();
                    Usuarios.Add(modelo);
                }
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerListaUsuariosActivos - " + exc.Message);
            }
            return Usuarios;
        }
        /// <summary>
        /// Extrae el Usuario si los datos de nombre de usuario y contraseña corresponden a uno existente en la base de datos.
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Usuario ValidaUsuario(string Usuario, string Password)
        {
            Usuario Response = new Usuario();
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"SELECT iUsuarioId, 
                                               vchNombreUsuario 
                                          FROM Usuarios 
                                         WHERE vchUsuario  = '" + Usuario + @"' 
                                           AND vchPassword = '" + Password + "' AND bActivo = 1";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    Response.idUsuario = Convert.ToInt32(row["iUsuarioId"].ToString());
                    Response.Nombre = row["vchNombreUsuario"].ToString();
                }
                reader.Close();
                command.Dispose();
                connection.Close();
                if(Response.idUsuario > 0)
                    ActualizaFechaSesionUsuario(Response.idUsuario);
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ValidaUsuario - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Inserta Registro en la tabla del comedor de ejercito
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public bool InsertaRegistroComedorEjercito(List<Registro> registros)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                foreach (Registro register in registros)
                {
                    command.CommandText = @"INSERT INTO COMEDOR_COORPORATIVO
                                        (iEmpleadoId, VerifyMode, InOutMode, FechaRegistro, FechaCarga) 
                                        VALUES (" + register.EmpleadoId + @", "
                                                      + register.VerifyMode + @", "
                                                      + register.InOutMode + @", '"
                                                      + register.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + FechaCarga.ToString("yyyy-MM-dd HH:mm:ss") + @"')";

                    command.ExecuteNonQuery();
                }
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaRegistroComedorEjercito - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Inserta Registro en la tabla del comedor de Tlalne
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public bool InsertaRegistroComedorTlalne(List<Registro> registros)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                foreach (Registro register in registros)
                {
                    command.CommandText = @"INSERT INTO COMEDOR_CEDIS
                                        (iEmpleadoId, VerifyMode, InOutMode, FechaRegistro, FechaCarga) 
                                        VALUES (" + register.EmpleadoId + @", "
                                                      + register.VerifyMode + @", "
                                                      + register.InOutMode + @", '"
                                                      + register.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + FechaCarga.ToString("yyyy-MM-dd HH:mm:ss") + @"')";
                    command.ExecuteNonQuery();
                }
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaRegistroComedorTlalne - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Inserta Registro en la tabla de la entrada de coorporativo
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public bool InsertaRegistroEntradaEjercito(List<Registro> registros)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                foreach (Registro register in registros)
                {
                    command.CommandText = @"INSERT INTO ENTRADA_COORPORATIVO
                                        (iEmpleadoId, VerifyMode, InOutMode, FechaRegistro, FechaCarga) 
                                        VALUES (" + register.EmpleadoId + @", "
                                                      + register.VerifyMode + @", "
                                                      + register.InOutMode + @", '"
                                                      + register.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + FechaCarga.ToString("yyyy-MM-dd HH:mm:ss") + @"')";
                    command.ExecuteNonQuery();
                }
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaRegistroEntradaEjercito - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Inserta Registro en la tabla de la entrada de coorporativo
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public bool InsertaRegistroEntradaTlalne(List<Registro> registros)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                foreach (Registro register in registros)
                {
                    command.CommandText = @"INSERT INTO ENTRADA_CEDIS
                                        (iEmpleadoId, VerifyMode, InOutMode, FechaRegistro, FechaCarga) 
                                        VALUES (" + register.EmpleadoId + @", "
                                                      + register.VerifyMode + @", "
                                                      + register.InOutMode + @", '"
                                                      + register.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + FechaCarga.ToString("yyyy-MM-dd HH:mm:ss") + @"')";
                    command.ExecuteNonQuery();
                }
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaRegistroEntradaTlalne - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite insertar un Empleado en la base de datos
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        public ServiceMessage InsertaEmpleado(Empleado Empleado)
        {
            ServiceMessage Response = new ServiceMessage();
            Response.MensajeRespuesta = "Existió un problema al insertar el empleado en la base de datos";
            Response.Error = true;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"INSERT INTO Empleados
                                        (iEmpleadoId,
                                        iNumeroEmpleado,
                                        vchNombreEmpleado,
                                        vchPassword,
                                        iPrivilegio,
                                        bEnabled,
                                        vchNumeroTarjeta,
                                        vchFingerPrint,
                                        iFingerPrintLength,
                                        iFingerFlag,
                                        iHorarioId,
                                        vchNomina,
                                        vchCompania,
                                        bExterno,
                                        bitManager,
                                        intManagerID,
                                        vchCorreo,
                                        vchPasswordAttendance,
                                        vchNombreUsuario
                                        )
                                        values
                                        (" + Empleado.iEmpleadoId + @","
                                          + Empleado.NumeroEmpleado + @","
                                          + "'" + Empleado.NombreEmpleado + @"',"
                                          + "'" + Empleado.Password + @"',"
                                          + Empleado.Privilegio + @","
                                          + (Empleado.Enabled ? "1" : "0") + @","
                                          + "'" + Empleado.NumeroTarjeta + @"',"
                                          + "'" + Empleado.FingerPrint + @"',"
                                          + Empleado.FingerPrintLength + @","
                                          + Empleado.FingerFlag + @","
                                          + Empleado.HorarioId + @","
                                          + "'" + Empleado.Nomina + @"',"
                                          + "'" + Empleado.Compania + @"',"
                                          + (Empleado.Externo ? "1" : "0") + @","
                                          + Empleado.EsManager + @","
                                          + Empleado.ManagerId + @","
                                          + "'" + Empleado.CorreoElectronico + @"',"
                                          + "'" + Empleado.PasswordAttendance + @"',"
                                          + "'" + Empleado.NombreUsuario + @"'"
                                          + ")";
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Response.MensajeRespuesta = "Empleado insertado correctamente";
                Response.Error = false;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaEmpleado - " + exc.Message);
                Response.MensajeRespuesta = "Excepción : " + exc.Message;
                Response.Error = true;
            }
            return Response;
        }
        /// <summary>
        /// Permite actualizar un empleado en la base de datos
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        public bool ActualizaEmpleado(Empleado Empleado)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"UPDATE Empleados SET" + @" "
                                        + "iNumeroEmpleado = " + "'" + Empleado.NumeroEmpleado + @"',"
                                        + "vchNombreEmpleado = " + "'" + Empleado.NombreEmpleado + @"',"
                                        + "vchPassword = " + "'" + Empleado.Password + @"',"
                                        + "iPrivilegio = " + Empleado.Privilegio + @","
                                        + "bEnabled = " + (Empleado.Enabled ? "1" : "0") + @","
                                        + "vchNumeroTarjeta = " + "'" + Empleado.NumeroTarjeta + @"',"
                                        + "vchFingerPrint = " + "'" + Empleado.FingerPrint + @"',"
                                        + "iFingerPrintLength = " + Empleado.FingerPrintLength + @","
                                        + "iFingerFlag = " + Empleado.FingerFlag + @","
                                        + "iHorarioId = " + Empleado.HorarioId + @","
                                        + "bExterno = " + (Empleado.Externo ? "1" : "0") + @","
                                        + "vchNombreUsuario = " + "'" + Empleado.NombreUsuario + @"',"
                                        + "vchPasswordAttendance = " + "'" + Empleado.PasswordAttendance + @"',"
                                        + "vchCorreo = " + "'" + Empleado.CorreoElectronico+ @"',"
                                        + "intManagerID = " + Empleado.ManagerId+ @","
                                        + "bitManager = " + (Empleado.EsManager ? "1" : "0") 
                                        + " WHERE iEmpleadoId = " + Empleado.iEmpleadoId;
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizaEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite eliminar de forma lógica un empleado de la base de datos Attendance
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public bool BorraEmpleado(int EmpleadoId)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                command.CommandText = @"UPDATE Empleados SET Activo = 0 WHERE iEmpleadoId = " + EmpleadoId;
                command.ExecuteNonQuery();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.BorraEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite extraer la información de uno o varios empleados según se indique en el parámetro.
        /// </summary>
        /// <returns></returns>
        public List<Empleado> EmpleadosEnDispositivos(string NombreEmpleado)
        {
            List<Empleado> Empleados = new List<Empleado>();
            Empleado Entidad;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"SELECT iEmpleadoId,
                                               iNumeroEmpleado,
                                               vchNombreEmpleado,
                                               vchPassword,
                                               iPrivilegio,
                                               bEnabled,
                                               vchNumeroTarjeta,
                                               vchFingerPrint,
                                               iFingerPrintLength,
                                               iFingerFlag,
                                               iHorarioId,
                                               bExterno,
                                               vchNomina,
                                               vchCompania,
                                               vchNombreUsuario,
                                               vchPasswordAttendance,
                                               vchCorreo,
                                               intManagerID,
                                               bitManager
                                          FROM Empleados
                                         WHERE ('" + NombreEmpleado + @"' = '' 
                                            OR vchNombreEmpleado LIKE CONCAT('%','" + NombreEmpleado + "','%')) AND Activo = 1";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    Entidad = new Empleado();
                    Entidad.iEmpleadoId = Convert.ToInt32(row["iEmpleadoId"].ToString());
                    Entidad.NumeroEmpleado = row["iNumeroEmpleado"].ToString();
                    Entidad.NombreEmpleado = row["vchNombreEmpleado"].ToString();
                    Entidad.Password = row["vchPassword"].ToString();
                    Entidad.Privilegio = Convert.ToInt32(row["iPrivilegio"].ToString());
                    Entidad.Enabled = row["bEnabled"].ToString() == "0" ? false : true;
                    Entidad.NumeroTarjeta = row["vchNumeroTarjeta"].ToString();
                    Entidad.FingerPrint = row["vchFingerPrint"].ToString();
                    Entidad.FingerPrintLength = Convert.ToInt32(row["iFingerPrintLength"].ToString());
                    Entidad.FingerFlag = Convert.ToInt32(row["iFingerFlag"].ToString());
                    Entidad.HorarioId = Convert.ToInt32(row["iHorarioId"].ToString());
                    Entidad.Externo = row["bExterno"].ToString() == "0" ? false : true;
                    Entidad.Nomina = row["vchNomina"].ToString();
                    Entidad.Compania = row["vchCompania"].ToString();
                    Entidad.NombreUsuario = row["vchNombreUsuario"].ToString();
                    Entidad.PasswordAttendance = row["vchPasswordAttendance"].ToString();
                    Entidad.CorreoElectronico = row["vchCorreo"].ToString();
                    Entidad.ManagerId = Convert.ToInt32(row["intManagerID"].ToString());
                    Entidad.EsManager = row["bitManager"].ToString() == "0" ? false : true;
                    Empleados.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.EmpleadosEnDispositivos - " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Método que permite obtener el empleado según su ID en Attendance
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public Empleado ObtenerEmpleado(int EmpleadoId)
        {
            Empleado Response = new Empleado();
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"SELECT iEmpleadoId,
                                               iNumeroEmpleado,
                                               vchNombreEmpleado,
                                               vchPassword,
                                               iPrivilegio,
                                               bEnabled,
                                               vchNumeroTarjeta,
                                               vchFingerPrint,
                                               iFingerPrintLength,
                                               iFingerFlag,
                                               iHorarioId,
                                               bExterno,
                                               vchNomina,
                                               vchCompania,
                                               vchNombreUsuario,
                                               vchPasswordAttendance,
                                               vchCorreo,
                                               intManagerID,
                                               bitManager
                                          FROM Empleados
                                         WHERE iEmpleadoId = " + EmpleadoId;
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    Response = new Empleado();
                    Response.iEmpleadoId = Convert.ToInt32(row["iEmpleadoId"].ToString());
                    Response.NumeroEmpleado = row["iNumeroEmpleado"].ToString();
                    Response.NombreEmpleado = row["vchNombreEmpleado"].ToString();
                    Response.Password = row["vchPassword"].ToString();
                    Response.Privilegio = Convert.ToInt32(row["iPrivilegio"].ToString());
                    Response.Enabled = row["bEnabled"].ToString() == "0" ? false : true;
                    Response.NumeroTarjeta = row["vchNumeroTarjeta"].ToString();
                    Response.FingerPrint = row["vchFingerPrint"].ToString();
                    Response.FingerPrintLength = Convert.ToInt32(row["iFingerPrintLength"].ToString());
                    Response.FingerFlag = Convert.ToInt32(row["iFingerFlag"].ToString());
                    Response.HorarioId = Convert.ToInt32(row["iHorarioId"].ToString());
                    Response.Externo = row["bExterno"].ToString() == "0" ? false : true;
                    Response.Nomina = row["vchNomina"].ToString();
                    Response.Compania = row["vchCompania"].ToString();
                    Response.NombreUsuario = row["vchNombreUsuario"].ToString();
                    Response.PasswordAttendance = row["vchPasswordAttendance"].ToString();
                    Response.CorreoElectronico = row["vchCorreo"].ToString();
                    Response.ManagerId = Convert.ToInt32(row["intManagerID"].ToString());
                    Response.EsManager = row["bitManager"].ToString() == "0" ? false : true;
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Obtiene la lista con el conteo de los retardos de cada empleado
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<ContadorRetardos> ObtenerConteoDeRetardos(int NumeroEmpleado,
                                                              string NombreEmpleado,
                                                              string Compania,
                                                              string Nomina,
                                                              DateTime FechaInicio,
                                                              DateTime FechaFin)
        {
            List<ContadorRetardos> ListaRetardos = new List<ContadorRetardos>();
            ContadorRetardos Entidad;
            MySqlCommand command = new MySqlCommand("stp_GetRetardos", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("EmployerNumber", NumeroEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerName", NombreEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerCompany", Compania));
                command.Parameters.Add(new MySqlParameter("EmployerPaysheet", Nomina));
                command.Parameters.Add(new MySqlParameter("FI", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FF", FechaFin.ToString("yyyy-MM-dd")));
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtRetardos = new DataTable();
                dtRetardos.Load(reader);
                foreach (DataRow row in dtRetardos.Rows)
                {
                    Entidad = new ContadorRetardos();
                    Entidad.EmpleadoId = Convert.ToInt32(row["EmpleadoId"].ToString());
                    Entidad.NumeroEmpleado = Convert.ToInt32(row["NumeroEmpleado"].ToString());
                    Entidad.NombreEmpleado = row["NombreEmpleado"].ToString();
                    Entidad.NumeroRetardos = Convert.ToInt32(row["NumeroRetardos"].ToString());
                    Entidad.Compania = row["Compania"].ToString();
                    Entidad.Nomina = row["Nomina"].ToString();
                    ListaRetardos.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerConteoDeRetardos - " + exc.Message);
            }
            return ListaRetardos;
        }
        /// <summary>
        /// Obtiene la lista de retardos del empleado especificado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<Retardo> ObtenerDetalleRetardos(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            List<Retardo> Retardos = new List<Retardo>();
            Retardo Entidad;
            MySqlCommand command = new MySqlCommand("stp_GetDetalleRetardos", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("EmployerId", EmpleadoId));
                command.Parameters.Add(new MySqlParameter("FI", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FF", FechaFin.ToString("yyyy-MM-dd")));
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Entidad = new Retardo();
                    Entidad.EmpleadoId = EmpleadoId;
                    Entidad.FechaRetardo = reader["FechaRetardo"].ToString();
                    Retardos.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerDetalleRetardos - " + exc.Message);
            }
            return Retardos;
        }
        /// <summary>
        /// Permite obtener la tolerancia de retardos desde la base de datos
        /// </summary>
        /// <returns></returns>
        public int ObtenerToleranciaRetardos()
        {
            int ToleranciaRetardos = 3;
            try
            {
                Conectar();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Dato FROM configuracionincidencias WHERE iConfiguracionId = 2";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    ToleranciaRetardos = Convert.ToInt32(reader["Dato"].ToString());
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerToleranciaRetardos - " + exc.Message);
            }
            return ToleranciaRetardos;
        }
        /// <summary>
        /// Permite actualizar el valor de la tolerancia de retardos
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool ActualizaToleranciaRetardos(int Value)
        {
            bool Actualizado = false;
            try
            {
                Conectar();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE configuracionincidencias SET Dato = '" + Value + "' WHERE iConfiguracionId = 2";
                command.ExecuteNonQuery();
                Actualizado = true;
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizaToleranciaRetardos - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Permite obtener el tiempo de tolerancia configurado
        /// </summary>
        /// <returns></returns>
        public string ObtenerTiempoTolerancia()
        {
            string TeimpoTolerancia = "";
            try
            {
                Conectar();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Dato FROM configuracionincidencias WHERE iConfiguracionId = 1";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    TeimpoTolerancia = reader["Dato"].ToString();
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerTiempoTolerancia - " + exc.Message);
            }
            return TeimpoTolerancia;
        }
        /// <summary>
        /// Permite actualizar el tiempo de tolerancia
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool ActualizarTiempoTolerancia(string Value)
        {
            bool Actualizado = false;
            try
            {
                Conectar();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE configuracionincidencias SET Dato = '" + Value + "' WHERE iConfiguracionId = 1";
                command.ExecuteNonQuery();
                Actualizado = true;
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizarTiempoTolerancia - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Método que obtiene el maestro de faltas
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <param name="NombreEmpleado"></param>
        /// <param name="Compania"></param>
        /// <param name="Nomina"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<DetalleFaltas> ObtenerListaFaltas(int NumeroEmpleado,
                                                                 string NombreEmpleado,
                                                                 string Compania,
                                                                 string Nomina,
                                                                 DateTime FechaInicio,
                                                                 DateTime FechaFin)
        {
            List<DetalleFaltas> Empleados = new List<DetalleFaltas>();
            MySqlCommand command = new MySqlCommand("stp_GetFaltas", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.EnableCaching = true;
                command.Parameters.Add(new MySqlParameter("EmployerNumber", NumeroEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerName", NombreEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerCompany", Compania));
                command.Parameters.Add(new MySqlParameter("EmployerPaysheet", Nomina));
                command.Parameters.Add(new MySqlParameter("FI", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FF", FechaFin.ToString("yyyy-MM-dd")));
                MySqlDataReader reader = command.ExecuteReader();

                DetalleFaltas Entidad;
                while (reader.Read())
                {
                    Entidad = new DetalleFaltas();
                    Entidad.EmpleadoId = Convert.ToInt32(reader["EmpleadoId"].ToString());
                    Entidad.NumeroEmpleado = Convert.ToInt32(reader["NumeroEmpleado"].ToString());
                    Entidad.NombreEmpleado = reader["NombreEmpleado"].ToString();
                    Entidad.Compania = reader["Compania"].ToString();
                    Entidad.Nomina = reader["Nomina"].ToString();
                    Entidad.Fecha = reader["FechaFalta"].ToString();
                    Entidad.NumeroFalta = Convert.ToInt32(reader["NumeroFaltas"].ToString());
                    Empleados.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerListaFaltas - " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Permite obtener los registros para el reporte de incidencias
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <param name="NombreEmpleado"></param>
        /// <param name="Compania"></param>
        /// <param name="Nomina"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<ReporteIncidencia> ObtenerReporteIncidencias(int NumeroEmpleado,
                                                                 string NombreEmpleado,
                                                                 string Compania,
                                                                 string Nomina,
                                                                 DateTime FechaInicio,
                                                                 DateTime FechaFin)
        {
            List<ReporteIncidencia> ListaRegistros = new List<ReporteIncidencia>();
            List<DetalleFaltas> Empleados = new List<DetalleFaltas>();
            MySqlCommand command = new MySqlCommand("stp_ReporteIncidencias", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.EnableCaching = true;
                command.Parameters.Add(new MySqlParameter("EmployerNumber", NumeroEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerName", NombreEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerCompany", Compania));
                command.Parameters.Add(new MySqlParameter("EmployerPaysheet", Nomina));
                command.Parameters.Add(new MySqlParameter("FI", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FF", FechaFin.ToString("yyyy-MM-dd")));
                MySqlDataReader reader = command.ExecuteReader();
                ReporteIncidencia Entidad;
                while (reader.Read())
                {
                    Entidad = new ReporteIncidencia();
                    Entidad.EmpleadoId = Convert.ToInt32(reader["EmpleadoId"].ToString());
                    Entidad.NumeroEmpleado = Convert.ToInt32(reader["NumeroEmpleado"].ToString());
                    Entidad.NombreEmpleado = reader["NombreEmpleado"].ToString();
                    Entidad.Compania = reader["Compania"].ToString();
                    Entidad.Nomina = reader["Nomina"].ToString();
                    Entidad.Fecha = reader["Fecha"].ToString();
                    Entidad.Concepto = reader["Concepto"].ToString();
                    ListaRegistros.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerReporteIncidencias - " + exc.Message);
            }
            return ListaRegistros;
        }
        /// <summary>
        /// Método que obtiene el listado de faltas de un empleado en un periodo dado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<DetalleFaltas> ObtenerDetalleFaltas(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleFaltas> ListaFaltas = new List<DetalleFaltas>();
            MySqlCommand command = new MySqlCommand("stp_GetDetalleFaltas", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("Empleado", EmpleadoId));
                command.Parameters.Add(new MySqlParameter("FI", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FF", FechaFin.ToString("yyyy-MM-dd")));
                MySqlDataReader reader = command.ExecuteReader();
                DetalleFaltas Entidad;
                while (reader.Read())
                {
                    Entidad = new DetalleFaltas();
                    Entidad.EmpleadoId = Convert.ToInt32(reader["iEmpleadoId"].ToString());
                    Entidad.NumeroEmpleado = Convert.ToInt32(reader["iNumeroEmpleado"].ToString());
                    Entidad.NombreEmpleado = reader["vchNombreEmpleado"].ToString();
                    Entidad.Compania = reader["vchCompania"].ToString();
                    Entidad.Nomina = reader["vchNomina"].ToString();
                    Entidad.Fecha = reader["FECHA"].ToString();
                    Entidad.TipoFalta = "Falta Injustificada";
                    ListaFaltas.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerDetalleFaltas - " + exc.Message);
            }
            return ListaFaltas;
        }
        /// <summary>
        /// Permite Realizar una consulta por Stored Procedure de la lista
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <param name="NombreEmpleado"></param>
        /// <param name="Compania"></param>
        /// <param name="Nomina"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<EmpleadoComidas> ObtenerListaEmpleadoComidas(int NumeroEmpleado,
                                                                 string NombreEmpleado,
                                                                 string Compania,
                                                                 string Nomina,
                                                                 DateTime FechaInicio,
                                                                 DateTime FechaFin)
        {
            List<EmpleadoComidas> Empleados = new List<EmpleadoComidas>();
            MySqlCommand command = new MySqlCommand("stp_GetReporteGeneralComedores", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("EmployerNumber", NumeroEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerName", NombreEmpleado));
                command.Parameters.Add(new MySqlParameter("EmployerCompany", Compania));
                command.Parameters.Add(new MySqlParameter("EmployerPaysheet", Nomina));
                command.Parameters.Add(new MySqlParameter("FechaInicio", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FechaFin", FechaFin.ToString("yyyyy-MM-dd HH:mm:ss")));
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtEmpleados = new DataTable();
                dtEmpleados.Load(reader);
                EmpleadoComidas Entidad;
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    Entidad = new EmpleadoComidas();
                    Entidad.EmpleadoId = Convert.ToInt32(row["EmpleadoId"].ToString());
                    Entidad.NumeroEmpleado = Convert.ToInt32(row["NumeroEmpleado"].ToString());
                    Entidad.NombreEmpleado = row["NombreEmpleado"].ToString();
                    Entidad.Compania = row["Compania"].ToString();
                    Entidad.Nomina = row["Nomina"].ToString();
                    Entidad.NumeroComidas = Convert.ToInt32(row["NumeroComidas"].ToString());
                    Empleados.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerListaEmpleadoComidas - " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Detalle de Comidas
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        public List<DetalleComida> ObtenerDetalleComidas(int Empleado, DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleComida> Detalle = new List<DetalleComida>();
            MySqlCommand command = new MySqlCommand("stp_GetDetailComedor", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("EmpleadoId", Empleado));
                command.Parameters.Add(new MySqlParameter("FechaInicio", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FechaFin", FechaFin.ToString("yyyyy-MM-dd HH:mm:ss")));
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtEmpleados = new DataTable();
                dtEmpleados.Load(reader);
                DetalleComida Entidad;
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    Entidad = new DetalleComida();
                    Entidad.FechaRegistro = row["FechaRegistro"].ToString();
                    Entidad.LugarRegistro = row["Lugar"].ToString();
                    Detalle.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerDetalleComidas - " + exc.Message);
            }
            return Detalle;
        }
        /// <summary>
        /// Método que permite realizar la consulta de las configuraciones de comedores
        /// </summary>
        /// <returns></returns>
        public List<Configuracion> ObtenerConfiguracionComedor()
        {
            List<Configuracion> Configuraciones = new List<Configuracion>();
            MySqlCommand command = new MySqlCommand("stp_GetConfiguracionComedor", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtConfiguraciones = new DataTable();
                dtConfiguraciones.Load(reader);
                Configuracion Entidad;
                foreach (DataRow row in dtConfiguraciones.Rows)
                {
                    Entidad = new Configuracion();
                    Entidad.ConfiguracionId = Convert.ToInt32(row["iConfiguracionId"].ToString());
                    Entidad.Descripcion = row["Configuracion"].ToString();
                    Entidad.Value = row["Dato"].ToString();
                    Configuraciones.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerConfiguracionComedor - " + exc.Message);
            }
            return Configuraciones;
        }

        /// <summary>
        /// Método que permite obtener la información para el reporte en excel del comedor
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<ComedorExcel> ObtenerReporteComedorExcel(DateTime FechaInicio, DateTime FechaFin)
        {
            List<ComedorExcel> RegistrosExcel = new List<ComedorExcel>();
            MySqlCommand command = new MySqlCommand("stp_getComedorExcel", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("FechaInicio", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FechaFin", FechaFin.ToString("yyyyy-MM-dd HH:mm:ss")));
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtEmpleados = new DataTable();
                dtEmpleados.Load(reader);
                ComedorExcel Entidad;
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    Entidad = new ComedorExcel();
                    Entidad.EmpleadoId = Convert.ToInt32(row["iEmpleadoID"].ToString());
                    Entidad.NumeroEmpleado = row["NumeroEmpleado"].ToString();
                    Entidad.NombreEmpleado = row["NombreEmpleado"].ToString();
                    Entidad.FechaRegistro = row["FechaRegistro"].ToString();
                    Entidad.Lugar = row["Lugar"].ToString();
                    Entidad.ImporteEmpresa = Convert.ToDecimal(row["ImporteEmpresa"].ToString());
                    Entidad.ImporteRetencion = Convert.ToDecimal(row["ImporteRetencion"].ToString());
                    RegistrosExcel.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerReporteComedorExcel - " + exc.Message);
            }
            return RegistrosExcel;
        }
        /// <summary>
        /// Método que permite actualizar los valores de una configuración
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ActualizarConfiguracion(Configuracion request)
        {
            bool response = false;
            MySqlCommand command = new MySqlCommand("stp_ActualizaConfiguracion", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("ConfiguracionId", request.ConfiguracionId));
                command.Parameters.Add(new MySqlParameter("Valor", request.Value));
                command.ExecuteNonQuery();
                connection.Close();
                response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerConfiguracionComedor - " + exc.Message);
            }
            return response;
        }
        /// <summary>
        /// Permite obtener el layout del comedor a excepción del campo de estructura
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<RegistroReporteComedor> ObtenerLayoutComedor(DateTime FechaInicio, DateTime FechaFin, string Nomina, string Compania)
        {
            List<RegistroReporteComedor> Registros = new List<RegistroReporteComedor>();
            MySqlCommand command = new MySqlCommand("stp_getLayoutComedor", connection);
            try
            {
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("FechaInicio", FechaInicio.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new MySqlParameter("FechaFin", FechaFin.ToString("yyyyy-MM-dd HH:mm:ss")));
                command.Parameters.Add(new MySqlParameter("Nomina", Nomina));
                command.Parameters.Add(new MySqlParameter("Compania", Compania));
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtEmpleados = new DataTable();
                dtEmpleados.Load(reader);
                RegistroReporteComedor Entidad;
                //Inserción de los encabezados:
                Entidad = new RegistroReporteComedor();
                Entidad.EmpleadoId = 0;
                Entidad.Empleado = "Empleado";
                Entidad.RazonSocial = "Razon Social";
                Entidad.Nomina = "Nomina";
                Entidad.DIP = "DIP";
                Entidad.Nombre = "Nombre";
                Entidad.Descripcion = "Descripcion";
                Entidad.Importe1 = "Importe 1";
                Entidad.FechaMovimiento = "FechaMovimiento";
                Entidad.Referencia = "Referencia";
                Entidad.NivelEstructura = "Nivel de Estructura";
                Entidad.Importe2 = "Importe 2";
                Entidad.Importe3 = "Importe 3";
                Entidad.SaldoActual = "Saldo Actual";
                Entidad.SaldoAnterior = "Saldo Anterior";
                Entidad.ImporteCapturado = "Importe Capturado";
                Registros.Add(Entidad);
                //
                foreach (DataRow row in dtEmpleados.Rows)
                {
                    Entidad = new RegistroReporteComedor();
                    Entidad.EmpleadoId = Convert.ToInt32(row["EmpleadoId"].ToString());
                    Entidad.Empleado = row["NumeroEmpleado"].ToString();
                    Entidad.RazonSocial = row["Compania"].ToString();
                    Entidad.Nomina = row["Nomina"].ToString();
                    Entidad.DIP = row["DIP"].ToString();
                    Entidad.Nombre = row["NombreEmpleado"].ToString();
                    Entidad.Descripcion = row["Descripcion"].ToString();
                    Entidad.Importe1 = row["ImporteRetencion"].ToString();
                    Entidad.FechaMovimiento = row["FechaMovimiento"].ToString();
                    Entidad.Referencia = row["Referencia"].ToString();
                    Entidad.Importe2 = row["Importe2"].ToString();
                    Entidad.Importe3 = row["Importe3"].ToString();
                    Entidad.SaldoActual = row["SaldoActual"].ToString();
                    Entidad.SaldoAnterior = row["SaldoAnterior"].ToString();
                    Entidad.ImporteCapturado = row["ImporteCapturado"].ToString();
                    Registros.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ObtenerLayoutComedor - " + exc.Message);
            }
            return Registros;
        }
        /// <summary>
        /// Obitene la lista de Horarios en estructura de catálogo
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> GetCatalogoHorarios()
        {
            List<Catalogo> Horarios = new List<Catalogo>();
            try
            {
                MySqlCommand command = connection.CreateCommand();
                Catalogo Entidad;
                command.CommandText = @"SELECT iHorarioId, vchDescripcion FROM Horarios";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    Entidad = new Catalogo();
                    Entidad.id = Convert.ToInt32(row["iHorarioId"].ToString());
                    Entidad.Descripcion = row["vchDescripcion"].ToString();
                    Horarios.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.GetCatalogoHorarios - " + exc.Message);
            }
            return Horarios;
        }
        /// <summary>
        /// Obitene la lista de horarios dados de alta
        /// </summary>
        /// <returns></returns>
        public List<Horario> GetHorarios()
        {
            List<Horario> Horarios = new List<Horario>();
            try
            {
                MySqlCommand command = connection.CreateCommand();
                Horario Entidad;
                command.CommandText = @"SELECT  H.iHorarioId,
			                                    H.vchDescripcion,
			                                    H.bLunes,
			                                    H.bMartes,
			                                    H.bMiercoles,
			                                    H.bJueves,
			                                    H.bViernes,
			                                    H.bSabado,
			                                    H.bDomingo,
			                                    H.vchEntradaLunes,
			                                    H.vchEntradaMartes,
			                                    H.vchEntradaMiercoles,
			                                    H.vchEntradaJueves,
			                                    H.vchEntradaViernes,
			                                    H.vchEntradaSabado,
			                                    H.vchEntradaDomingo,
			                                    H.vchSalidaLunes,
			                                    H.vchSalidaMartes,
			                                    H.vchSalidaMiercoles,
			                                    H.vchSalidaJueves,
			                                    H.vchSalidaViernes,
			                                    H.vchSalidaSabado,
			                                    H.vchSalidaDomingo
	                                       FROM horarios H";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dtUsuarios = new DataTable();
                dtUsuarios.Load(reader);
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    Entidad = new Horario();
                    Entidad.HorarioId = Convert.ToInt32(row["iHorarioId"].ToString());
                    Entidad.DescripcionHorario = row["vchDescripcion"].ToString();
                    Entidad.Lunes = row["bLunes"].ToString() == "1" ? true : false;
                    Entidad.Martes = row["bMartes"].ToString() == "1" ? true : false;
                    Entidad.Miercoles = row["bMiercoles"].ToString() == "1" ? true : false;
                    Entidad.Jueves = row["bJueves"].ToString() == "1" ? true : false;
                    Entidad.Viernes = row["bViernes"].ToString() == "1" ? true : false;
                    Entidad.Sabado = row["bSabado"].ToString() == "1" ? true : false;
                    Entidad.Domingo = row["bDomingo"].ToString() == "1" ? true : false;
                    Entidad.HorarioLunes = row["vchEntradaLunes"].ToString() + " - " + row["vchSalidaLunes"].ToString();
                    Entidad.HorarioMartes = row["vchEntradaMartes"].ToString() + " - " + row["vchSalidaMartes"].ToString();
                    Entidad.HorarioMiercoles = row["vchEntradaMiercoles"].ToString() + " - " + row["vchSalidaMiercoles"].ToString();
                    Entidad.HorarioJueves = row["vchEntradaJueves"].ToString() + " - " + row["vchSalidaJueves"].ToString();
                    Entidad.HorarioViernes = row["vchEntradaViernes"].ToString() + " - " + row["vchSalidaViernes"].ToString();
                    Entidad.HorarioSabado = row["vchEntradaSabado"].ToString() + " - " + row["vchSalidaSabado"].ToString();
                    Entidad.HorarioDomingo = row["vchEntradaDomingo"].ToString() + " - " + row["vchSalidaDomingo"].ToString();
                    Horarios.Add(Entidad);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.GetHorarios - " + exc.Message);
            }
            return Horarios;
        }
        /// <summary>
        /// Inserta un Horario en la base de datos.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public bool InsertaHorario(Horario request, int Usuario)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"INSERT INTO Horarios(
	                                                vchDescripcion,
	                                                bLunes,
	                                                bMartes,
	                                                bMiercoles,
	                                                bJueves,
	                                                bViernes,
	                                                bSabado,
	                                                bDomingo,
	                                                vchEntradaLunes,
	                                                vchEntradaMartes,
	                                                vchEntradaMiercoles,
	                                                vchEntradaJueves,
	                                                vchEntradaViernes,
	                                                vchEntradaSabado,
	                                                vchEntradaDomingo,
	                                                vchSalidaLunes,
	                                                vchSalidaMartes,
	                                                vchSalidaMiercoles,
	                                                vchSalidaJueves,
	                                                vchSalidaViernes,
	                                                vchSalidaSabado,
	                                                vchSalidaDomingo,
	                                                AUDUSUARIO
                                                ) VALUES ("
                                                + "'" + request.DescripcionHorario + @"',"
                                                + (request.Lunes ? "1" : "0") + @","
                                                + (request.Martes ? "1" : "0") + @","
                                                + (request.Miercoles ? "1" : "0") + @","
                                                + (request.Jueves ? "1" : "0") + @","
                                                + (request.Viernes ? "1" : "0") + @","
                                                + (request.Sabado ? "1" : "0") + @","
                                                + (request.Domingo ? "1" : "0") + @","
                                                + "'" + (request.Lunes ? request.HorarioLunes.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Martes ? request.HorarioMartes.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Miercoles ? request.HorarioMiercoles.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Jueves ? request.HorarioJueves.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Viernes ? request.HorarioViernes.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Sabado ? request.HorarioSabado.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Domingo ? request.HorarioDomingo.Split('-')[0].Trim() : "") + @"',"
                                                + "'" + (request.Lunes ? request.HorarioLunes.Split('-')[1].Trim() : "") + @"',"
                                                + "'" + (request.Martes ? request.HorarioMartes.Split('-')[1].Trim() : "") + @"',"
                                                + "'" + (request.Miercoles ? request.HorarioMiercoles.Split('-')[1].Trim() : "") + @"',"
                                                + "'" + (request.Jueves ? request.HorarioJueves.Split('-')[1].Trim() : "") + @"',"
                                                + "'" + (request.Viernes ? request.HorarioViernes.Split('-')[1].Trim() : "") + @"',"
                                                + "'" + (request.Sabado ? request.HorarioSabado.Split('-')[1].Trim() : "") + @"',"
                                                + "'" + (request.Domingo ? request.HorarioDomingo.Split('-')[1].Trim() : "") + @"',"
                                                + Usuario + ");";
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaHorario - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite Actualizar un horario de la base de datos en la tabla "horarios"
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public bool UpdateHorario(Horario request, int Usuario)
        {
            bool response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"UPDATE Horarios SET" + @" "
                                        + "vchDescripcion = " + "'" + request.DescripcionHorario + @"',"
                                        + "bLunes = " + (request.Lunes ? "1" : "0") + @","
                                        + "bMartes = " + (request.Martes ? "1" : "0") + @","
                                        + "bMiercoles = " + (request.Miercoles ? "1" : "0") + @","
                                        + "bJueves = " + (request.Jueves ? "1" : "0") + @","
                                        + "bViernes = " + (request.Viernes ? "1" : "0") + @","
                                        + "bSabado = " + (request.Sabado ? "1" : "0") + @","
                                        + "bDomingo = " + (request.Domingo ? "1" : "0") + @","
                                        + "vchEntradaLunes = " + "'" + (request.Lunes ? request.HorarioLunes.Split('-')[0].Trim() : "") + @"',"
                                        + "vchEntradaMartes = " + "'" + (request.Martes ? request.HorarioMartes.Split('-')[0].Trim() : "") + @"',"
                                        + "vchEntradaMiercoles = " + "'" + (request.Miercoles ? request.HorarioMiercoles.Split('-')[0].Trim() : "") + @"',"
                                        + "vchEntradaJueves = " + "'" + (request.Jueves ? request.HorarioJueves.Split('-')[0].Trim() : "") + @"',"
                                        + "vchEntradaViernes = " + "'" + (request.Viernes ? request.HorarioViernes.Split('-')[0].Trim() : "") + @"',"
                                        + "vchEntradaSabado = " + "'" + (request.Sabado ? request.HorarioSabado.Split('-')[0].Trim() : "") + @"',"
                                        + "vchEntradaDomingo = " + "'" + (request.Domingo ? request.HorarioDomingo.Split('-')[0].Trim() : "") + @"',"
                                        + "vchSalidaLunes = " + "'" + (request.Lunes ? request.HorarioLunes.Split('-')[1].Trim() : "") + @"',"
                                        + "vchSalidaMartes = " + "'" + (request.Martes ? request.HorarioMartes.Split('-')[1].Trim() : "") + @"',"
                                        + "vchSalidaMiercoles = " + "'" + (request.Miercoles ? request.HorarioMiercoles.Split('-')[1].Trim() : "") + @"',"
                                        + "vchSalidaJueves = " + "'" + (request.Jueves ? request.HorarioJueves.Split('-')[1].Trim() : "") + @"',"
                                        + "vchSalidaViernes = " + "'" + (request.Viernes ? request.HorarioViernes.Split('-')[1].Trim() : "") + @"',"
                                        + "vchSalidaSabado = " + "'" + (request.Sabado ? request.HorarioSabado.Split('-')[1].Trim() : "") + @"',"
                                        + "vchSalidaDomingo = " + "'" + (request.Domingo ? request.HorarioDomingo.Split('-')[1].Trim() : "") + @"',"
                                        + "AUDUSUARIO = " + Usuario + " WHERE iHorarioId = " + request.HorarioId;
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.UpdateHorario - " + exc.Message);
            }
            return response;
        }
        /// <summary>
        /// Elimina el horario seleccionado
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public bool DeleteHorario(int idHorario)
        {
            bool Response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"DELETE FROM Horarios WHERE iHorarioId = " + idHorario;
                Conectar();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Response = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.DeleteHorario - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Verifica si el horario está asignado almenos a un empleado
        /// </summary>
        /// <param name="HorarioId"></param>
        /// <returns></returns>
        public bool isHorarioConEmpleados(int HorarioId)
        {
            bool response = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = @"SELECT iEmpleadoId FROM EMPLEADOS WHERE iHorarioId = " + HorarioId + " LIMIT 1;";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    response = true;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.isHorarioConUsuarios - " + exc.Message);
            }
            return response;
        }
        /// <summary>
        /// Obtiene el último Id asignado a un empleado externo
        /// </summary>
        /// <returns></returns>
        public int getCurrentEmpleadoExternoId()
        {
            int CurrentEmpleadoExternoId = 0;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = "SELECT MAX(iEmpleadoId) iEmpleadoId FROM Empleados WHERE bExterno = 1;";
                Conectar();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    foreach (DataRow row in table.Rows)
                    {
                        CurrentEmpleadoExternoId = Convert.ToInt32(row["iEmpleadoId"].ToString());
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.getCurrentEmpleadoExternoId - " + exc.Message);
            }
            return CurrentEmpleadoExternoId;
        }
        /// <summary>
        /// Permite obtener la lista de los días feriados a través del stored procedure stp_GetDiasFeriados
        /// </summary>
        /// <returns></returns>
        public List<DiaFeriado> GetListaDiasFeriados()
        {
            List<DiaFeriado> DiasFeriados = new List<DiaFeriado>();
            try
            {
                MySqlCommand command = new MySqlCommand("stp_GetDiasFeriados", connection);
                Conectar();
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader reader = command.ExecuteReader();
                DiaFeriado Dia;
                while (reader.Read())
                {
                    Dia = new DiaFeriado();
                    Dia.DiaFeriadoId = Convert.ToInt32(reader["iDiaFestivoId"].ToString());
                    Dia.Descripcion = reader["vchDescripcion"].ToString();
                    Dia.Fecha = Convert.ToDateTime(reader["FechaFestiva"].ToString()).ToString("d MMM yyyy");
                    DiasFeriados.Add(Dia);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("ERROR - AttendanceDA.GetListaDiasFeriados - " + exc.Message);
            }
            return DiasFeriados;
        }
        /// <summary>
        /// Permite dar de alta una fecha feriada
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public bool InsertaDiaFeriado(string Descripcion, DateTime Fecha)
        {
            bool Insertado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                command.CommandText = @"INSERT INTO DiasFestivos
                                        (vchDescripcion, FechaFestiva, Activo) VALUES ('" + Descripcion + "', '" + Fecha.ToString("yyy-MM-dd") + "', 1)";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Insertado = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.InsertaDiaFeriado - " + exc.Message);
            }
            return Insertado;
        }
        /// <summary>
        /// Permite actualizar un día festivo en específico.
        /// </summary>
        /// <param name="DiaFeriadoId"></param>
        /// <param name="Descripcion"></param>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public bool ActualizaDiaFeriado(int DiaFeriadoId, string Descripcion, DateTime Fecha)
        {
            bool Actualizado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                command.CommandText = @"UPDATE DiasFestivos SET vchDescripcion = '" + Descripcion + "', FechaFestiva = '" + Fecha.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE iDiaFestivoId = " + DiaFeriadoId;
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Actualizado = true;
            }
            catch(Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizaDiaFeriado - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Permite borrar el día feriado seleccionado
        /// </summary>
        /// <param name="DiaFeriadoId"></param>
        /// <returns></returns>
        public bool BorraDiaFeriado(int DiaFeriadoId)
        {
            bool Actualizado = false;
            MySqlCommand command = connection.CreateCommand();
            try
            {
                Conectar();
                DateTime FechaCarga = DateTime.Now;
                command.CommandText = @"UPDATE DiasFestivos SET Activo = 0 WHERE iDiaFestivoId = " + DiaFeriadoId;
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                Actualizado = true;
            }
            catch (Exception exc)
            {
                connection.Close();
                BusinessLogic.Log.EscribeLog("Error: AttendanceDA.ActualizaDiaFeriado - " + exc.Message);
            }
            return Actualizado;
        }
    }
}
