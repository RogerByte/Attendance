using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttendanceCore.DeviceAccess;
using AttendanceCore.DataAccess;
using AttendanceCore.Entities.DeviceEntities;
using AttendanceCore.Entities;
using AttendanceCore.ServiceContracts;
using System.Threading;

namespace AttendanceCore.BusinessLogic
{
    public class AttendanceController
    {
        /// <summary>
        /// Método para el alta de usuarios
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public bool AltaUsuario(Usuario Request)
        {
            AttendanceDA dbAttendance = new AttendanceDA();
            return dbAttendance.InsertaUsuario(Request);
        }
        /// <summary>
        /// Método para la baja de usuarios
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        public bool BajaUsuario(int UsuarioId)
        {
            AttendanceDA dbAttendance = new AttendanceDA();
            return dbAttendance.EliminaUsuario(UsuarioId);
        }
        /// <summary>
        /// Permite efectuar cambios en el registro del empleado ingresado como parámetro
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CambiosUsuario(Usuario user)
        {
            AttendanceDA dbAttendance = new AttendanceDA();
            return dbAttendance.ActualizaUsuario(user);
        }
        /// <summary>
        /// Permite efectuar la consulta de uno o varios usuarios con el parámetro parecido al nombre
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public List<Usuario> ConsultaUsuarios(string Nombre)
        {
            AttendanceDA dbAttendance = new AttendanceDA();
            return dbAttendance.ObtenerListaUsuariosActivos(Nombre);
        }
        /// <summary>
        /// Método que permite validar un usuario para el acceso al sistema de attendance
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Usuario ValidaUsuario(string Usuario, string Password)
        {
            Usuario Response = new Usuario();
            AttendanceDA dbAttendance = new AttendanceDA();
            Response = dbAttendance.ValidaUsuario(Usuario, Password);
            return Response;
        }
        /// <summary>
        /// Método que obitene la lista de empleados dados de alta en el sistema de attendance
        /// </summary>
        /// <returns></returns>
        public List<Empleado> ListaEmpleados(string NombreEmpleado)
        {
            List<Empleado> Empleados = new List<Empleado>();
            try
            {
                AttendanceDA FujiAttendanceDB = new AttendanceDA();
                Empleados = FujiAttendanceDB.EmpleadosEnDispositivos(NombreEmpleado);
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ListaEmpleados - " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Permite obtener el catálogo de Managers
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoManagers()
        {
            List<Catalogo> Managers = new List<Catalogo>();
            try
            {
                AttendanceDA FujiAttendanceDB = new AttendanceDA();
                Managers = FujiAttendanceDB.CatalogoManagers();
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.CatalogoManagers - " + exc.Message);
            }
            return Managers;
        }
        /// <summary>
        /// Método que obtiene el catálogo de horarios
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoHorarios()
        {
            List<Catalogo> Horarios = new List<Catalogo>();
            try
            {
                AttendanceDA FujiAttendanceDB = new AttendanceDA();
                Horarios = FujiAttendanceDB.GetCatalogoHorarios();
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.CatalogoHorarios - " + exc.Message);
            }
            return Horarios;
        }
        /// <summary>
        /// Método que obitene el catálogo de Empleados de Eslabón
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoEmpleados()
        {
            List<Catalogo> Empleados = new List<Catalogo>();
            try
            {
                EslabonDA EslabonDB = new EslabonDA();
                Empleados = EslabonDB.CatalogoEmpleados();
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.CatalogoEmpleados - " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Obtiene el catálogo de nóminas desde eslabón
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoNomina()
        {
            List<Catalogo> Nomina = new List<Catalogo>();
            try
            {
                EslabonDA baseDatos = new EslabonDA();
                Nomina = baseDatos.CatalogoNomina();
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.CatalogoNomina - " + exc.Message);
            }
            return Nomina;
        }
        /// <summary>
        /// Método para obtener el catálogo de las companias de Eslabón
        /// </summary>
        /// <returns></returns>
        public List<Catalogo> CatalogoCompanias()
        {
            List<Catalogo> Companias = new List<Catalogo>();
            try
            {
                EslabonDA baseDatos = new EslabonDA();
                Companias = baseDatos.CatalogoCompanias();
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.CatalogoCompanias - " + exc.Message);
            }
            return Companias;
        }
        /// <summary>
        /// Método que obitene la lista de horarios que han sido de alta en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Horario> ListaHorarios()
        {
            List<Horario> Horarios = new List<Horario>();
            try
            {
                AttendanceDA AttendanceDB = new AttendanceDA();
                Horarios = AttendanceDB.GetHorarios();
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ListaHorarios - " + exc.Message);
            }
            return Horarios;
        }
        /// <summary>
        /// Método que permite insertar un horario al catálogo de horarios.
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public bool InsertaHorario(Horario horario, int Usuario)
        {
            bool Response = false;
            try
            {
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                if (BaseDatosAttendance.InsertaHorario(horario, Usuario))
                    Response = true;
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.InsertaHorario - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite actualziar un horario del catálogo de horarios
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public bool ActualizaHorario(Horario horario, int Usuario)
        {
            bool Response = false;
            try
            {
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                if (BaseDatosAttendance.UpdateHorario(horario, Usuario))
                    Response = true;
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.InsertaHorario - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite eliminar un horario en específico
        /// </summary>
        /// <param name="HorarioId"></param>
        /// <returns></returns>
        public EliminaHorarioResponse EliminaHorario(int HorarioId)
        {
            EliminaHorarioResponse Response = new EliminaHorarioResponse();
            try
            {
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                if (BaseDatosAttendance.isHorarioConEmpleados(HorarioId))
                {
                    Response.Eliminado = false;
                    Response.Respuesta = "El horario está asignado, no puede ser eliminado.";
                }
                else
                {
                    Response.Eliminado = BaseDatosAttendance.DeleteHorario(HorarioId);
                    if (Response.Eliminado)
                        Response.Respuesta = "El horario ha sido eliminado exitosamente";
                    else
                        Response.Respuesta = "Existió un problema al intentar eliminar el horario.";
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.EliminaHorario - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Consulta un Empleado en alguno de los relojes checadores según se ingrese el parámetro
        /// </summary>
        /// <param name="idEmpleado">Identificador del empleado del cual se desea hacer la consulta</param>
        /// <param name="Dispositivo">Identificador del Dispositivo, 1 - Entrada Ejercito, 2 - Comedor Ejercito, 3 - Entrada CEDIS, 4 - Comedor CEDIS</param>
        /// <returns></returns>
        public DeviceEmployeer ConsultaEmpleadoEnDispositivo(int idEmpleado, int Dispositivo)
        {
            DeviceEmployeer Empleado = new DeviceEmployeer();
            DeviceDriver ControladorReloj = new DeviceDriver();
            switch (Dispositivo)
            {
                case 1: //Entrada de Ejercito
                    Empleado = ControladorReloj.ConsultaEmpleado(idEmpleado.ToString(), ControladorReloj.EntradaCoorporativo);
                    break;
                case 2: //Comedor de Ejercito
                    Empleado = ControladorReloj.ConsultaEmpleado(idEmpleado.ToString(), ControladorReloj.ComedorCoorporativo);
                    break;
                case 3: //Entrada de Almacén
                    Empleado = ControladorReloj.ConsultaEmpleado(idEmpleado.ToString(), ControladorReloj.EntradaCEDIS);
                    break;
                case 4: //Comedor de Almacén
                    Empleado = ControladorReloj.ConsultaEmpleado(idEmpleado.ToString(), ControladorReloj.ComedorCEDIS);
                    break;
                default:
                    break;
            }
            return Empleado;
        }
        /// <summary>
        /// Da de alta un empleado en todos los dispositivos de reloj checador
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        public bool AltaEmpleadoDispositivo(DeviceEmployeer Empleado)
        {
            bool AltaExistosa = false;
            DeviceDriver ControladorReloj = new DeviceDriver();
            try
            {
                AltaExistosa = ControladorReloj.AltaEmpleado(Empleado);
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.AltaEmpleadoDispositivo - " + exc.Message);
            }
            return AltaExistosa;
        }
        /// <summary>
        /// Da de alta la lista de empleados en los dispositivos de reloj checador
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        public bool AltaEmpleadosDispositivo(List<DeviceEmployeer> Empleados)
        {
            bool AltaExistosa = false;
            DeviceDriver ControladorReloj = new DeviceDriver();
            try
            {
                AltaExistosa = ControladorReloj.AltaEmpleados(Empleados);
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.AltaEmpleadosDispositivo - " + exc.Message);
            }
            return AltaExistosa;
        }
        /// <summary>
        /// Borra el Usuario seleccionado de los dispositivos
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public bool BorraEmpleadoDispositivo(int EmpleadoId)
        {
            bool BorradoExistoso = false;
            try
            {
                DeviceDriver ControladorReloj = new DeviceDriver();
                BorradoExistoso = ControladorReloj.BorrarEmpleado(EmpleadoId.ToString());
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.BorraEmpleadoDispositivo - " + exc.Message);
            }
            return BorradoExistoso;
        }
        /// <summary>
        /// Borra la lista de empleados que son ingresados como parámetro
        /// </summary>
        /// <param name="IdEmpleados"></param>
        /// <returns></returns>
        public bool BorraEmpleadosDispositivo(List<int> IdEmpleados, int Dispositivo)
        {
            bool Response = false;
            try
            {
                DeviceDriver ControladorReloj = new DeviceDriver();
                switch(Dispositivo)
                {
                    case 1:
                        Response = ControladorReloj.BorrarEmpleados(IdEmpleados, ControladorReloj.EntradaCoorporativo);
                        break;
                    case 2:
                        Response = ControladorReloj.BorrarEmpleados(IdEmpleados, ControladorReloj.ComedorCoorporativo);
                        break;
                    case 3:
                        Response = ControladorReloj.BorrarEmpleados(IdEmpleados, ControladorReloj.EntradaCEDIS);
                        break;
                    case 4:
                        Response = ControladorReloj.BorrarEmpleados(IdEmpleados, ControladorReloj.ComedorCEDIS);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.BorraEmpleadoDispositivo - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Extrae todos los empleados contenidos en el reloj checador seleccionado
        /// </summary>
        /// <param name="Dispositivo">Identificador del Dispositivo, 1 - Entrada Ejercito, 2 - Comedor Ejercito, 3 - Entrada CEDIS, 4 - Comedor CEDIS</param>
        /// <returns></returns>
        public List<DeviceEmployeer> ExtraeEmpleados(int Dispositivo)
        {
            List<DeviceEmployeer> Empleados = new List<DeviceEmployeer>();
            try
            {
                DeviceDriver ControladorReloj = new DeviceDriver();
                switch (Dispositivo)
                {
                    case 1: //Entrada de Ejercito
                        Empleados = ControladorReloj.ExtraeEmpleados(ControladorReloj.EntradaCoorporativo);
                        break;
                    case 2: //Comedor de Ejercito
                        Empleados = ControladorReloj.ExtraeEmpleados(ControladorReloj.ComedorCoorporativo);
                        break;
                    case 3: //Entrada de Almacén
                        Empleados = ControladorReloj.ExtraeEmpleados(ControladorReloj.EntradaCEDIS);
                        break;
                    case 4: //Comedor de Almacén
                        Empleados = ControladorReloj.ExtraeEmpleados(ControladorReloj.ComedorCEDIS);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ExtraeEmpleados - " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Permite actualizar un Empleado en la base de datos.
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool ActualizaEmpleadoDB(Empleado empleado)
        {
            bool Response = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Response = BaseDatos.ActualizaEmpleado(empleado);
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaEmpleadoDB - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que incluye la lógica de negocio para el alta de empleados
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public ServiceMessage AltaEmpleado(Empleado empleado)
        {
            ServiceMessage Response = new ServiceMessage();
            try
            {
                EslabonDA Eslabon = new EslabonDA();
                empleado.NumeroEmpleado = Eslabon.ObtenerNumeroEmpleado(empleado.iEmpleadoId);
                empleado.Nomina = Eslabon.ObtenerNominaEmpleado(empleado.iEmpleadoId);
                empleado.Compania = Eslabon.ObtenerCompaniaEmpleado(empleado.iEmpleadoId);
                empleado.Enabled = true;
                AttendanceDA Attendance = new AttendanceDA();
                Response = Attendance.InsertaEmpleado(empleado);
                if (!Response.Error)
                {
                    DeviceDriver RelojChecador = new DeviceDriver();
                    DeviceEmployeer EmpleadoReloj = new DeviceEmployeer();
                    EmpleadoReloj.NumeroEmpleado = empleado.iEmpleadoId.ToString();
                    EmpleadoReloj.NombreEmpleado = empleado.NombreEmpleado;
                    EmpleadoReloj.NumeroTarjeta = empleado.NumeroTarjeta;
                    EmpleadoReloj.Password = empleado.Password;
                    EmpleadoReloj.Privilegio = empleado.Privilegio;
                    EmpleadoReloj.Enabled = empleado.Enabled;
                    EmpleadoReloj.FingerFlag = empleado.FingerFlag;
                    EmpleadoReloj.FingerPrint = empleado.FingerPrint;
                    EmpleadoReloj.FingerPrintLength = empleado.FingerPrintLength;
                    Response.Error = !RelojChecador.AltaEmpleado(EmpleadoReloj);
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.AltaEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite actualizar la información de un empleado
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool EditaEmpleado(Empleado empleado)
        {
            bool Response = false;
            try
            {
                empleado.Enabled = true;
                AttendanceDA Attendance = new AttendanceDA();
                Empleado RegistroActual = new Empleado(); //Guardar el empleado actual según el ID del registro a renovar.
                RegistroActual = Attendance.ObtenerEmpleado(empleado.iEmpleadoId);
                Response = Attendance.ActualizaEmpleado(empleado);

                if (Response)
                {
                    if (RegistroActual.NumeroTarjeta != empleado.NumeroTarjeta)
                    {
                        DeviceDriver RelojChecador = new DeviceDriver();
                        DeviceEmployeer EmpleadoReloj = new DeviceEmployeer();
                        EmpleadoReloj.NumeroEmpleado = empleado.iEmpleadoId.ToString();
                        EmpleadoReloj.NombreEmpleado = empleado.NombreEmpleado;
                        EmpleadoReloj.NumeroTarjeta = empleado.NumeroTarjeta;
                        EmpleadoReloj.Password = empleado.Password;
                        EmpleadoReloj.Privilegio = empleado.Privilegio;
                        EmpleadoReloj.Enabled = empleado.Enabled;
                        EmpleadoReloj.FingerFlag = empleado.FingerFlag;
                        EmpleadoReloj.FingerPrint = empleado.FingerPrint;
                        EmpleadoReloj.FingerPrintLength = empleado.FingerPrintLength;
                        Response = RelojChecador.AltaEmpleado(EmpleadoReloj);
                    }
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.EditaEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite sincronizar todos los relojes desde el reloj indicado, actualizando también el registro en la base de datos
        /// </summary>
        /// <param name="empleado"></param>
        /// <param name="DispositivoOrigen"></param>
        /// <returns></returns>
        public bool SyncEmpleado(Empleado empleado, int DispositivoOrigen)
        {
            bool Response = false;
            try
            {
                DeviceDriver RelojChecador = new DeviceDriver();
                DeviceEmployeer EmpleadoReloj = new DeviceEmployeer();
                
                switch(DispositivoOrigen)
                {
                    case 1: //Entrada Coorporativo
                        EmpleadoReloj = RelojChecador.ConsultaEmpleado(empleado.iEmpleadoId.ToString(), RelojChecador.EntradaCoorporativo);
                        empleado.Enabled = EmpleadoReloj.Enabled;
                        empleado.FingerFlag = EmpleadoReloj.FingerFlag;
                        empleado.FingerPrint = EmpleadoReloj.FingerPrint;
                        empleado.FingerPrintLength = EmpleadoReloj.FingerPrintLength;
                        empleado.NumeroTarjeta = EmpleadoReloj.NumeroTarjeta;
                        empleado.Password = EmpleadoReloj.Password;
                        empleado.Privilegio = EmpleadoReloj.Privilegio;
                        Response = EditaEmpleado(empleado);
                        break;
                    case 2: //comedor coorporativo
                        EmpleadoReloj = RelojChecador.ConsultaEmpleado(empleado.iEmpleadoId.ToString(), RelojChecador.ComedorCoorporativo);
                        empleado.Enabled = EmpleadoReloj.Enabled;
                        empleado.FingerFlag = EmpleadoReloj.FingerFlag;
                        empleado.FingerPrint = EmpleadoReloj.FingerPrint;
                        empleado.FingerPrintLength = EmpleadoReloj.FingerPrintLength;
                        empleado.NumeroTarjeta = EmpleadoReloj.NumeroTarjeta;
                        empleado.Password = EmpleadoReloj.Password;
                        empleado.Privilegio = EmpleadoReloj.Privilegio;
                        Response = EditaEmpleado(empleado);
                        break;
                    case 3://Entrada CEDIS
                        EmpleadoReloj = RelojChecador.ConsultaEmpleado(empleado.iEmpleadoId.ToString(), RelojChecador.EntradaCEDIS);
                        empleado.Enabled = EmpleadoReloj.Enabled;
                        empleado.FingerFlag = EmpleadoReloj.FingerFlag;
                        empleado.FingerPrint = EmpleadoReloj.FingerPrint;
                        empleado.FingerPrintLength = EmpleadoReloj.FingerPrintLength;
                        empleado.NumeroTarjeta = EmpleadoReloj.NumeroTarjeta;
                        empleado.Password = EmpleadoReloj.Password;
                        empleado.Privilegio = EmpleadoReloj.Privilegio;
                        Response = EditaEmpleado(empleado);
                        break;
                    case 4://comedor CEDIS
                        EmpleadoReloj = RelojChecador.ConsultaEmpleado(empleado.iEmpleadoId.ToString(), RelojChecador.ComedorCEDIS);
                        empleado.Enabled = EmpleadoReloj.Enabled;
                        empleado.FingerFlag = EmpleadoReloj.FingerFlag;
                        empleado.FingerPrint = EmpleadoReloj.FingerPrint;
                        empleado.FingerPrintLength = EmpleadoReloj.FingerPrintLength;
                        empleado.NumeroTarjeta = EmpleadoReloj.NumeroTarjeta;
                        empleado.Password = EmpleadoReloj.Password;
                        empleado.Privilegio = EmpleadoReloj.Privilegio;
                        Response = EditaEmpleado(empleado);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.SyncEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite borrar un empleado de los dispositivos y de la base de datos
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public bool BorraEmpleado(int EmpleadoId)
        {
            bool Response = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                DeviceDriver Driver = new DeviceDriver();
                Response = Driver.BorrarEmpleado(EmpleadoId.ToString());
                if (Response)
                    Response = BaseDatos.BorraEmpleado(EmpleadoId);
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.BorraEmpleado - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Establecer hora en los dispositivos
        /// </summary>
        /// <returns></returns>
        public bool setTime()
        {
            DeviceDriver driver = new DeviceDriver();
            return driver.setTime();
        }
        /// <summary>
        /// Método de uso comodín para ajustar los números de empleados
        /// </summary>
        /// <returns></returns>
        public bool AplicaNumeroEmpleadoCorrecto()
        {
            EslabonDA BaseEslabon = new EslabonDA();
            AttendanceDA BaseAttendance = new AttendanceDA();
            List<Empleado> Empleados = new List<Empleado>();
            Empleados = BaseAttendance.EmpleadosEnDispositivos("");
            foreach (Empleado Empleado in Empleados)
            {
                Empleado.NumeroEmpleado = BaseEslabon.ObtenerNumeroEmpleado(Empleado.iEmpleadoId);
                if (Empleado.NumeroEmpleado == "")
                    Log.EscribeLog("La persona es: " + Empleado.NombreEmpleado);
            }
            return true;
        }
        /// <summary>
        /// Método de uso comodín para ajustar los nombres de los empleados
        /// </summary>
        /// <returns></returns>
        public bool AplicaNombreEmpleadoCorrecto()
        {
            EslabonDA BaseEslabon = new EslabonDA();
            AttendanceDA BaseAttendance = new AttendanceDA();
            List<Empleado> Empleados = new List<Empleado>();
            Empleados = BaseAttendance.EmpleadosEnDispositivos("");
            foreach (Empleado Empleado in Empleados)
            {
                Empleado.NombreEmpleado = BaseEslabon.ObtenerNombreEmpleado(Empleado.iEmpleadoId);
                BaseAttendance.ActualizaEmpleado(Empleado);
            }
            return true;
        }
        /// <summary>
        /// Método que permite dar de alta empleados externos
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public ServiceMessage AltaEmpleadoExterno(Empleado empleado)
        {
            ServiceMessage Response = new ServiceMessage();
            try
            {
                AttendanceDA BaseAttendance = new AttendanceDA();
                int EmpleadoId = BaseAttendance.getCurrentEmpleadoExternoId();
                if (EmpleadoId < 9000)
                    EmpleadoId = 9000;
                else
                    EmpleadoId++;
                empleado.Compania = "NA";
                empleado.Nomina = "NA";
                empleado.iEmpleadoId = EmpleadoId;
                empleado.NumeroEmpleado = EmpleadoId.ToString();
                empleado.Enabled = true;
                Response = BaseAttendance.InsertaEmpleado(empleado);
                if (!Response.Error)
                {
                    DeviceDriver RelojChecador = new DeviceDriver();
                    DeviceEmployeer EmpleadoReloj = new DeviceEmployeer();
                    EmpleadoReloj.NumeroEmpleado = empleado.iEmpleadoId.ToString();
                    EmpleadoReloj.NombreEmpleado = empleado.NombreEmpleado;
                    EmpleadoReloj.NumeroTarjeta = empleado.NumeroTarjeta;
                    EmpleadoReloj.Password = empleado.Password;
                    EmpleadoReloj.Privilegio = empleado.Privilegio;
                    EmpleadoReloj.Enabled = empleado.Enabled;
                    EmpleadoReloj.FingerFlag = empleado.FingerFlag;
                    EmpleadoReloj.FingerPrint = empleado.FingerPrint;
                    EmpleadoReloj.FingerPrintLength = empleado.FingerPrintLength;
                    Response.Error = !RelojChecador.AltaEmpleado(EmpleadoReloj);
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.AltaEmpleadoExterno - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite extraer la información de los relojes checadores de los comedores y depositarla en la base de datos de Attendance.
        /// </summary>
        /// <returns></returns>
        public ServiceMessage ActualizarDatosComedores()
        {
            ServiceMessage Response = new ServiceMessage();
            try
            {
                Thread AThread = new Thread(new ThreadStart(ActualizaRegistrosComedorCoorporativo));
                Thread BThread = new Thread(new ThreadStart(ActualizaResgitrosComedorTlalne));
                AThread.Start();
                BThread.Start();
                AThread.Join();
                BThread.Join();
                Response.Error = false;
                Response.MensajeRespuesta = "Actualización correcta";
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizarDatosComedores - " + exc.Message);
                Response.MensajeRespuesta = "Error al actualizar datos : " + exc.Message;
                Response.Error = true;
            }
            return Response;
        }
        /// <summary>
        /// Método que permite extraer la información de los relojes checadores de las entradas y depositarla en la base de datos de Attendance.
        /// </summary>
        /// <returns></returns>
        public ServiceMessage ActualizarDatosEntradas()
        {
            ServiceMessage Response = new ServiceMessage();
            try
            {
                Thread AThread = new Thread(new ThreadStart(ActualizaRegistrosEntradaCoorporativo));
                Thread BThread = new Thread(new ThreadStart(ActualizaRegistrosEntradaTlalne));
                AThread.Start();
                BThread.Start();
                AThread.Join();
                BThread.Join();
                Response.Error = false;
                Response.MensajeRespuesta = "Actualización correcta";
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizarDatosEntradas - " + exc.Message);
                Response.MensajeRespuesta = "Error al actualizar datos : " + exc.Message;
                Response.Error = true;
            }
            return Response;
        }
        /// <summary>
        /// Método que permite la actualización de los datos del comedor de Tlalnepantla, diseñada para ser implementada en concurrencia (threads)
        /// </summary>
        private void ActualizaResgitrosComedorTlalne()
        {
            try
            {
                DeviceDriver Relojes = new DeviceDriver();
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                List<GLogData> Registros = Relojes.ObtenerRegistros(Relojes.ComedorCEDIS);
                List<Registro> RegDataBase = new List<Registro>();
                foreach (GLogData log in Registros)
                {
                    Registro reg = new Registro();
                    string Month = log.Month.ToString().Length == 2 ? log.Month.ToString() : "0" + log.Month.ToString();
                    string Day = log.Day.ToString().Length == 2 ? log.Day.ToString() : "0" + log.Day.ToString();
                    string Hour = log.Hour.ToString().Length == 2 ? log.Hour.ToString() : "0" + log.Hour.ToString();
                    string Minute = log.Minute.ToString().Length == 2 ? log.Minute.ToString() : "0" + log.Minute.ToString();
                    string Second = log.Second.ToString().Length == 2 ? log.Second.ToString() : "0" + log.Second.ToString();
                    reg.EmpleadoId = Convert.ToInt32(log.EmpleadoId);
                    reg.InOutMode = log.InOutMode;
                    reg.VerifyMode = log.VerifyMode;

                    reg.FechaRegistro = DateTime.ParseExact(log.Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second,
                        "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    RegDataBase.Add(reg);
                }
                if (BaseDatosAttendance.InsertaRegistroComedorTlalne(RegDataBase))
                    Relojes.BorraRegistros(Relojes.ComedorCEDIS);
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaResgitrosComedorTlalne - " + exc.Message);
            }
        }
        /// <summary>
        /// Método que permite la actualización de los datos del comedor del coorporativo, diseñada para ser implementada en concurrencia (threads)
        /// </summary>
        private void ActualizaRegistrosComedorCoorporativo()
        {
            try
            {
                DeviceDriver Relojes = new DeviceDriver();
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                List<GLogData> Registros = Relojes.ObtenerRegistros(Relojes.ComedorCoorporativo);
                List<Registro> RegDataBase = new List<Registro>();
                foreach (GLogData log in Registros)
                {
                    Registro reg = new Registro();
                    string Month = log.Month.ToString().Length == 2 ? log.Month.ToString() : "0" + log.Month.ToString();
                    string Day = log.Day.ToString().Length == 2 ? log.Day.ToString() : "0" + log.Day.ToString();
                    string Hour = log.Hour.ToString().Length == 2 ? log.Hour.ToString() : "0" + log.Hour.ToString();
                    string Minute = log.Minute.ToString().Length == 2 ? log.Minute.ToString() : "0" + log.Minute.ToString();
                    string Second = log.Second.ToString().Length == 2 ? log.Second.ToString() : "0" + log.Second.ToString();
                    reg.EmpleadoId = Convert.ToInt32(log.EmpleadoId);
                    reg.InOutMode = log.InOutMode;
                    reg.VerifyMode = log.VerifyMode;

                    reg.FechaRegistro = DateTime.ParseExact(log.Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second,
                        "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    RegDataBase.Add(reg);
                }
                if (BaseDatosAttendance.InsertaRegistroComedorEjercito(RegDataBase))
                {
                    Relojes.BorraRegistros(Relojes.ComedorCoorporativo);
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaRegistrosComedorCoorporativo - " + exc.Message);
            }
        }
        /// <summary>
        /// Método que permite la actualización de los datos de la entrada del coorporativo, diseñada para ser implementada en concurrencia (threads)
        /// </summary>
        private void ActualizaRegistrosEntradaCoorporativo()
        {
            try
            {
                DeviceDriver Relojes = new DeviceDriver();
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                List<GLogData> Registros = Relojes.ObtenerRegistros(Relojes.EntradaCoorporativo);
                List<Registro> RegDataBase = new List<Registro>();
                foreach (GLogData log in Registros)
                {
                    Registro reg = new Registro();
                    string Month = log.Month.ToString().Length == 2 ? log.Month.ToString() : "0" + log.Month.ToString();
                    string Day = log.Day.ToString().Length == 2 ? log.Day.ToString() : "0" + log.Day.ToString();
                    string Hour = log.Hour.ToString().Length == 2 ? log.Hour.ToString() : "0" + log.Hour.ToString();
                    string Minute = log.Minute.ToString().Length == 2 ? log.Minute.ToString() : "0" + log.Minute.ToString();
                    string Second = log.Second.ToString().Length == 2 ? log.Second.ToString() : "0" + log.Second.ToString();
                    reg.EmpleadoId = Convert.ToInt32(log.EmpleadoId);
                    reg.InOutMode = log.InOutMode;
                    reg.VerifyMode = log.VerifyMode;

                    reg.FechaRegistro = DateTime.ParseExact(log.Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second,
                        "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    RegDataBase.Add(reg);
                }
                if (BaseDatosAttendance.InsertaRegistroEntradaEjercito(RegDataBase))
                {
                    Relojes.BorraRegistros(Relojes.EntradaCoorporativo);
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaRegistrosEntradaCoorporativo - " + exc.Message);
            }
        }
        /// <summary>
        /// Método que permite la actualización de los datos de la entrada del Tlalne, diseñada para ser implementada en concurrencia (threads)
        /// </summary>
        private void ActualizaRegistrosEntradaTlalne()
        {
            try
            {
                DeviceDriver Relojes = new DeviceDriver();
                AttendanceDA BaseDatosAttendance = new AttendanceDA();
                List<GLogData> Registros = Relojes.ObtenerRegistros(Relojes.EntradaCEDIS);
                List<Registro> RegDataBase = new List<Registro>();
                foreach (GLogData log in Registros)
                {
                    Registro reg = new Registro();
                    string Month = log.Month.ToString().Length == 2 ? log.Month.ToString() : "0" + log.Month.ToString();
                    string Day = log.Day.ToString().Length == 2 ? log.Day.ToString() : "0" + log.Day.ToString();
                    string Hour = log.Hour.ToString().Length == 2 ? log.Hour.ToString() : "0" + log.Hour.ToString();
                    string Minute = log.Minute.ToString().Length == 2 ? log.Minute.ToString() : "0" + log.Minute.ToString();
                    string Second = log.Second.ToString().Length == 2 ? log.Second.ToString() : "0" + log.Second.ToString();
                    reg.EmpleadoId = Convert.ToInt32(log.EmpleadoId);
                    reg.InOutMode = log.InOutMode;
                    reg.VerifyMode = log.VerifyMode;

                    reg.FechaRegistro = DateTime.ParseExact(log.Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second,
                        "yyyy-MM-dd HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    RegDataBase.Add(reg);
                }
                if (BaseDatosAttendance.InsertaRegistroEntradaTlalne(RegDataBase))
                {
                    Relojes.BorraRegistros(Relojes.EntradaCEDIS);
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaRegistrosEntradaCoorporativo - " + exc.Message);
            }
        }
        /// <summary>
        /// Método que permite obtener el reporte general de comidas
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <param name="NombreEmpleado"></param>
        /// <param name="Compania"></param>
        /// <param name="Nomina"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<EmpleadoComidas> ObtenerEmpleadosComidas(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EmpleadoComidas> ListaEmpleadosComidas = new List<EmpleadoComidas>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                ListaEmpleadosComidas = BaseDatos.ObtenerListaEmpleadoComidas(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 0, 0));
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerEmpleadosComidas - " + exc.Message);
            }
            return ListaEmpleadosComidas;
        }
        /// <summary>
        /// Método que permite obtener la lista de fechas de registro por empleado
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        public List<DetalleComida> ObtenerDetalleComidas(int Empleado, DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleComida> ListaDetalle = new List<DetalleComida>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                ListaDetalle = BaseDatos.ObtenerDetalleComidas(Empleado, FechaInicio, new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 0, 0));
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerDetalleComidas - " + exc.Message);
            }
            return ListaDetalle;
        }
        /// <summary>
        /// Método que permite obtener el layout para eslabón de los comedores
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="Nomina"></param>
        /// <param name="Compania"></param>
        /// <returns></returns>
        public List<RegistroReporteComedor> ObtenerLayoutComedor(DateTime FechaInicio, DateTime FechaFin, string Nomina, string Compania)
        {
            List<RegistroReporteComedor> Registros = new List<RegistroReporteComedor>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                EslabonDA BaseEslabon = new EslabonDA();
                Registros = BaseDatos.ObtenerLayoutComedor(FechaInicio, new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 0, 0), Nomina, Compania);
                foreach (RegistroReporteComedor registro in Registros)
                {
                    if (registro.EmpleadoId != 0)
                    {
                        registro.NivelEstructura = BaseEslabon.ObtenerNivelEstructura(registro.EmpleadoId);
                    }
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerLayoutComedor - " + exc.Message);
            }
            return Registros;
        }
        /// <summary>
        /// Método que permite obtener la lista de configuraciones
        /// </summary>
        /// <returns></returns>
        public List<Configuracion> ObtenerConfiguraciones()
        {
            AttendanceDA BaseDatos = new AttendanceDA();
            return BaseDatos.ObtenerConfiguracionComedor();
        }
        /// <summary>
        /// Método que permite actualizar una configuración dada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ActualizaConfiguracion(Configuracion request)
        {
            AttendanceDA BaseDatos = new AttendanceDA();
            return BaseDatos.ActualizarConfiguracion(request);
        }
        /// <summary>
        /// Método que permite obtener la información para el reporte de comedor en excel
        /// </summary>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<ComedorExcel> ObtenerReporteComedorExcel(DateTime FechaInicio, DateTime FechaFin)
        {
            List<ComedorExcel> Registros = new List<ComedorExcel>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                EslabonDA Nomina = new EslabonDA();
                Registros = BaseDatos.ObtenerReporteComedorExcel(FechaInicio, new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 0, 0));
                List<ComplementoEmpleado> InformacionComplementaria = Nomina.ObtenerInformacionComplementaria();
                foreach (ComedorExcel registro in Registros)
                {
                    foreach (ComplementoEmpleado complemento in InformacionComplementaria)
                    {
                        if(registro.EmpleadoId == complemento.EmpleadoId)
                        {
                            registro.ClaveCentroCostos = complemento.ClaveCentroCostos;
                            registro.DescripcionCentroCostos = complemento.DescripcionCentroCostos;
                            registro.Nomina = complemento.Nomina;
                            registro.RazonSocial = complemento.RazonSocial;
                            break;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerReporteComedorExcel - " + exc.Message);
            }
            return Registros;
        }
        
        /// <summary>
        /// Método que permite obtener el reporte genérico de incidencias
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
            List<ReporteIncidencia> Response = new List<ReporteIncidencia>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                EslabonDA Eslabon = new EslabonDA();
                Response = BaseDatos.ObtenerReporteIncidencias(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
                List<DiasJustificados> Justificaciones = Eslabon.ObtenerDiasJustificados(FechaInicio, FechaFin);
                List<ComplementoEmpleado> InformacionComplementaria = Eslabon.ObtenerInformacionComplementaria();
                foreach (ReporteIncidencia registro in Response)
                {
                    foreach (ComplementoEmpleado complemento in InformacionComplementaria)
                    {
                        if (registro.EmpleadoId == complemento.EmpleadoId)
                        {
                            registro.ClaveCentroCostos = complemento.ClaveCentroCostos;
                            registro.DescripcionCentroCostos = complemento.DescripcionCentroCostos;
                            registro.Nomina = complemento.Nomina;
                            registro.RazonSocial = complemento.RazonSocial;
                            break;
                        }
                    }
                }
                foreach(DiasJustificados Justificacion in Justificaciones)
                {
                    foreach(ReporteIncidencia Registro in Response)
                    {
                        if(Registro.EmpleadoId == Justificacion.EmpleadoId)
                        {
                            for(int i = 0; i < Justificacion.NumeroDias; i++)
                            {
                                if (Registro.Fecha == Justificacion.FechaDesde.AddDays(i).ToString())
                                {
                                    Registro.Concepto = Justificacion.Concepto;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerMaestroFaltas - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que extrae el listado de faltas de toda la empresa según el filtrado parametrizado
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <param name="NombreEmpleado"></param>
        /// <param name="Compania"></param>
        /// <param name="Nomina"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<EmpleadoFaltas> ObtenerMaestroFaltas(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin)
        {
            List<EmpleadoFaltas> Faltas = new List<EmpleadoFaltas>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                EslabonDA Eslabon = new EslabonDA();
                List<DetalleFaltas> ListaFaltas = new List<DetalleFaltas>();
                ListaFaltas = BaseDatos.ObtenerListaFaltas(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
                DetalleFaltas[] CopiaListaFaltas = new DetalleFaltas[ListaFaltas.Count];
                ListaFaltas.CopyTo(CopiaListaFaltas);
                List<DiasJustificados> Justificaciones = Eslabon.ObtenerDiasJustificados(FechaInicio, FechaFin);
                foreach(DiasJustificados Justificacion in Justificaciones)
                {
                    foreach (DetalleFaltas Falta in CopiaListaFaltas)
                    {
                        if (Falta.EmpleadoId == Justificacion.EmpleadoId)
                        {
                            for (int i = 0; i < Justificacion.NumeroDias; i++)
                            {
                                if (Falta.Fecha == Justificacion.FechaDesde.AddDays(i).ToString())
                                {
                                    ListaFaltas.Remove(Falta);
                                    break;
                                }
                            }
                        }
                    }
                }
                //Eliminación de las faltas que están justificadas
                //foreach (DetalleFaltas Falta in CopiaListaFaltas)
                //{
                //    if (Eslabon.GetJustificacion(Falta.EmpleadoId, Convert.ToDateTime(Falta.Fecha)))
                //        ListaFaltas.Remove(Falta);
                //}
                //Agrupación de faltas por empleado
                EmpleadoFaltas Modelo;
                foreach(DetalleFaltas Falta in ListaFaltas)
                {
                    bool Encontrado = false;
                    foreach(EmpleadoFaltas AgrupadoFaltas in Faltas)
                    {
                        if (AgrupadoFaltas.EmpleadoId == Falta.EmpleadoId)
                        {
                            AgrupadoFaltas.NumeroFaltas++;
                            Encontrado = true;
                            break;
                        }
                    }
                    if(!Encontrado)
                    {
                        Modelo = new EmpleadoFaltas();
                        Modelo.EmpleadoId = Falta.EmpleadoId;
                        Modelo.Compania = Falta.Compania;
                        Modelo.NombreEmpleado = Falta.NombreEmpleado;
                        Modelo.Nomina = Falta.Nomina;
                        Modelo.NumeroEmpleado = Falta.NumeroEmpleado;
                        Modelo.NumeroFaltas = Falta.NumeroFalta;
                        Faltas.Add(Modelo);
                    }
                }
                List<ContadorRetardos> Retardos = new List<ContadorRetardos>();
                Retardos = BaseDatos.ObtenerConteoDeRetardos(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
                //Obtenemos las faltas por retardos acumulados
                List<EmpleadoFaltas> FaltasPorRetardo = new List<EmpleadoFaltas>();
                int ToleranciaRetardos = BaseDatos.ObtenerToleranciaRetardos();
                foreach(ContadorRetardos Retardo in Retardos)
                {
                    if (Retardo.NumeroRetardos >= ToleranciaRetardos)
                    {
                        EmpleadoFaltas Falta = new EmpleadoFaltas();
                        Falta.EmpleadoId = Retardo.EmpleadoId;
                        Falta.Compania = Retardo.Compania;
                        Falta.NombreEmpleado = Retardo.NombreEmpleado;
                        Falta.Nomina = Retardo.Nomina;
                        Falta.NumeroEmpleado = Retardo.NumeroEmpleado;
                        Falta.NumeroFaltas = Retardo.NumeroRetardos / ToleranciaRetardos;
                        Falta.TipoFalta = "FALTA POR RETARDO";
                        FaltasPorRetardo.Add(Falta);
                    }
                }
                //Agregamos las faltas por retardos a la lista de faltas
                foreach (EmpleadoFaltas Falta in FaltasPorRetardo)
                {
                    bool Encontrado = false;
                    foreach (EmpleadoFaltas AgrupadoFaltas in Faltas)
                    {
                        if (AgrupadoFaltas.EmpleadoId == Falta.EmpleadoId)
                        {
                            AgrupadoFaltas.NumeroFaltas += Falta.NumeroFaltas;
                            Encontrado = true;
                            break;
                        }
                    }
                    if (!Encontrado)
                    {
                        Faltas.Add(Falta);
                    }
                }
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerMaestroFaltas - " + exc.Message);
            }
            return Faltas;
        }
        /// <summary>
        /// Método que permite obtener el reporte de layout de incidencias
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <param name="NombreEmpleado"></param>
        /// <param name="Compania"></param>
        /// <param name="Nomina"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<RegistroLayoutIncidencias> ObtenerLayoutIncidencias(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin)
        {
            List<RegistroLayoutIncidencias> Reporte = new List<RegistroLayoutIncidencias>();
            try
            {
                RegistroLayoutIncidencias Entidad = new RegistroLayoutIncidencias();
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
                Reporte.Add(Entidad);
                AttendanceDA BaseDatos = new AttendanceDA();
                EslabonDA Eslabon = new EslabonDA();
                List<DetalleFaltas> Faltas = BaseDatos.ObtenerListaFaltas(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
                DetalleFaltas[] CopiaListaFaltas = new DetalleFaltas[Faltas.Count];
                Faltas.CopyTo(CopiaListaFaltas);
                List<DiasJustificados> Justificaciones = Eslabon.ObtenerDiasJustificados(FechaInicio, FechaFin);
                foreach (DiasJustificados Justificacion in Justificaciones)
                {
                    foreach (DetalleFaltas Falta in CopiaListaFaltas)
                    {
                        if (Falta.EmpleadoId == Justificacion.EmpleadoId)
                        {
                            for (int i = 0; i < Justificacion.NumeroDias; i++)
                            {
                                if (Falta.Fecha == Justificacion.FechaDesde.AddDays(i).ToString())
                                {
                                    Faltas.Remove(Falta);
                                    break;
                                }
                            }
                        }
                    }
                }
                //foreach (DetalleFaltas Falta in CopiaListaFaltas)
                //{
                //    if (Eslabon.GetJustificacion(Falta.EmpleadoId, Convert.ToDateTime(Falta.Fecha)))
                //        Faltas.Remove(Falta);
                //}
                List<ContadorRetardos> Retardos = new List<ContadorRetardos>();
                Retardos = BaseDatos.ObtenerConteoDeRetardos(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
                int ToleranciaRetardos = BaseDatos.ObtenerToleranciaRetardos();
                foreach (ContadorRetardos Retardo in Retardos)
                {
                    DetalleFaltas FaltaPorRetardo;
                    for (int i = 0 ; i < Retardo.NumeroRetardos / ToleranciaRetardos ; i++)
                    {
                        FaltaPorRetardo = new DetalleFaltas();
                        FaltaPorRetardo.EmpleadoId = Retardo.EmpleadoId;
                        FaltaPorRetardo.Compania = Retardo.Compania;
                        FaltaPorRetardo.NombreEmpleado = Retardo.NombreEmpleado;
                        FaltaPorRetardo.Nomina = Retardo.Nomina;
                        FaltaPorRetardo.NumeroEmpleado = Retardo.NumeroEmpleado;
                        FaltaPorRetardo.TipoFalta = "FALTA POR RETARDO";
                        Faltas.Add(FaltaPorRetardo);
                    }
                }
                foreach(DetalleFaltas Falta in Faltas)
                {
                    Entidad = new RegistroLayoutIncidencias();
                    Entidad.EmpleadoId = Falta.EmpleadoId;
                    Entidad.Empleado = Falta.NumeroEmpleado.ToString();
                    Entidad.RazonSocial = Falta.Compania;
                    Entidad.Nomina = Falta.Nomina;
                    Entidad.DIP = "7410";
                    Entidad.Nombre = Falta.NombreEmpleado;
                    Entidad.Descripcion = Falta.TipoFalta == "FALTA POR RETARDO" ? Falta.TipoFalta : "FALTA INJUSTIFICADA";
                    Entidad.Importe1 = "1.0";
                    Entidad.FechaMovimiento = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
                    Entidad.Referencia = "FALTA INJUSTIFICADA";
                    Entidad.NivelEstructura = Eslabon.ObtenerNivelEstructura(Entidad.EmpleadoId);
                    Entidad.Importe2 = "0";
                    Entidad.Importe3 = "0";
                    Entidad.SaldoActual = "0";
                    Entidad.SaldoAnterior = "0";
                    Entidad.ImporteCapturado = "0";
                    Reporte.Add(Entidad);
                }
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerLayoutIncidencias - " + exc.Message);
            }
            return Reporte;
        }
        /// <summary>
        /// Permite obtener el detalle de las faltas, filtrando por empleado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<DetalleFaltas> ObtenerDetalleFaltas(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleFaltas> Faltas = new List<DetalleFaltas>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                EslabonDA Eslabon = new EslabonDA();
                Faltas = BaseDatos.ObtenerDetalleFaltas(EmpleadoId, FechaInicio, FechaFin);
                DetalleFaltas[] CopiaFaltas = new DetalleFaltas[Faltas.Count];
                Faltas.CopyTo(CopiaFaltas);
                List<DiasJustificados> Justificaciones = Eslabon.ObtenerDiasJustificados(FechaInicio, FechaFin);
                foreach (DiasJustificados Justificacion in Justificaciones)
                {
                    foreach (DetalleFaltas Falta in CopiaFaltas)
                    {
                        if (Falta.EmpleadoId == Justificacion.EmpleadoId)
                        {
                            for (int i = 0; i < Justificacion.NumeroDias; i++)
                            {
                                if (Falta.Fecha == Justificacion.FechaDesde.AddDays(i).ToString())
                                {
                                    Faltas.Remove(Falta);
                                    break;
                                }
                            }
                        }
                    }
                }
                //Se eliminan las faltas justificadas
                //foreach (DetalleFaltas Falta in CopiaFaltas)
                //    if (Eslabon.GetJustificacion(Falta.EmpleadoId, Convert.ToDateTime(Falta.Fecha)))
                //        Faltas.Remove(Falta);
                foreach (DetalleFaltas Falta in Faltas)
                    Falta.Fecha = Convert.ToDateTime(Falta.Fecha).ToString("dd/MM/yyyy");
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerDetalleFaltas - " + exc.Message);
            }
            return Faltas;
        }
        /// <summary>
        /// Permite obtener los retardos acumulados del empleado en el periodo seleccionado
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<Retardo> ObtenerDetalleRetardos(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            List<Retardo> Retardos = new List<Retardo>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Retardos = BaseDatos.ObtenerDetalleRetardos(EmpleadoId, FechaInicio, FechaFin);
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerDetalleRetardos - " + exc.Message);
            }
            return Retardos;
        }
        /// <summary>
        /// Método que permite obtener los días feriados
        /// </summary>
        /// <returns></returns>
        public List<DiaFeriado> ObtenerDiasFeriados()
        {
            List<DiaFeriado> DiasFeriados = new List<DiaFeriado>();
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                DiasFeriados = BaseDatos.GetListaDiasFeriados();
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerDiasFeriados - " + exc.Message);
            }
            return DiasFeriados;
        }
        /// <summary>
        /// Permite insertar un día feriado a la base de datos
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public bool InsertarDiaFeriado(string Descripcion, DateTime Fecha)
        {
            bool Insertado = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Insertado = BaseDatos.InsertaDiaFeriado(Descripcion, Fecha);
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.InsertarDiaFeriado - " + exc.Message);
            }
            return Insertado;
        }
        /// <summary>
        /// Método que permite actualizar el día feriado seleccionado
        /// </summary>
        /// <param name="DiaFeriadoId"></param>
        /// <param name="Descripcion"></param>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public bool ActualizaDiaFeriado(int DiaFeriadoId, string Descripcion, DateTime Fecha)
        {
            bool Actualizado = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Actualizado = BaseDatos.ActualizaDiaFeriado(DiaFeriadoId, Descripcion, Fecha);
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaDiaFeriado - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Método que permite obtener el número de retardos tolerados para generar una falta
        /// </summary>
        /// <returns></returns>
        public string ObtenerDiasRetardo()
        {
            string DiasRetardo = string.Empty;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                DiasRetardo = BaseDatos.ObtenerToleranciaRetardos().ToString();
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerDiasRetardo - " + exc.Message);
            }
            return DiasRetardo;
        }
        /// <summary>
        /// Método que permite actualizar la cantidad de retardos que amerita una falta
        /// </summary>
        /// <param name="Dias"></param>
        /// <returns></returns>
        public bool ActualizaDiasRetardo(int Dias)
        {
            bool Actualizado = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Actualizado = BaseDatos.ActualizaToleranciaRetardos(Dias);
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaDiasRetardo - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Método que permite obtener el tiempo de tolerancia para que se efectúe un retardo
        /// </summary>
        /// <returns></returns>
        public string ObtenerTiempoTolerancia()
        {
            string TiempoTolerancia = "00 : 00";
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                TiempoTolerancia = BaseDatos.ObtenerTiempoTolerancia();
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ObtenerTiempoTolerancia - " + exc.Message);
            }
            return TiempoTolerancia;
        }
        /// <summary>
        /// Método que permite actualizar el valor de tiempo de tolerancia para efectuar un retardo en las entradas de los empleados
        /// </summary>
        /// <param name="TiempoTolerancia"></param>
        /// <returns></returns>
        public bool ActualizaTiempoTolerancia(string TiempoTolerancia)
        {
            bool Actualizado = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Actualizado = BaseDatos.ActualizarTiempoTolerancia(TiempoTolerancia);
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.ActualizaTiempoTolerancia - " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Permite hacer el borrado del día feriado
        /// </summary>
        /// <param name="DiaFeriado"></param>
        /// <returns></returns>
        public bool BorrarDiaFeriado(int DiaFeriado)
        {
            bool Borrado = false;
            try
            {
                AttendanceDA BaseDatos = new AttendanceDA();
                Borrado = BaseDatos.BorraDiaFeriado(DiaFeriado);
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error : BusinessLogic.AttendanceController.BorraDiaFeriado - " + exc.Message);
            }
            return Borrado;
        }
    }
}
