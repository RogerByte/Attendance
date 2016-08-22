using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AttendanceCore.ServiceContracts;
using AttendanceCore.Entities;
using AttendanceCore.BusinessLogic;
using AttendanceCore.Entities.DeviceEntities;

namespace AttendanceCore
{
    public class AttendanceService : IAttendanceService
    {
        public List<Catalogo> CatalogoManagers()
        {
            AttendanceController controller = new AttendanceController();
            return controller.CatalogoManagers();
        }
        public bool AltaUsuario(Usuario Request)
        {
            AttendanceController controller = new AttendanceController();
            return controller.AltaUsuario(Request);
        }
        public bool BajaUsuario(int UsuarioId)
        {
            AttendanceController controller = new AttendanceController();
            return controller.BajaUsuario(UsuarioId);
        }
        public bool CambioUsuario(Usuario Request)
        {
            AttendanceController controller = new AttendanceController();
            return controller.CambiosUsuario(Request);
        }
        public List<Usuario> ConsultaUsuario(string Request)
        {
            AttendanceController controller = new AttendanceController();
            return controller.ConsultaUsuarios(Request);
        }
        public UsuarioResponse ValidaUsuario(UsuarioRequest Request)
        {
            UsuarioResponse Response = new UsuarioResponse();
            Usuario Entidad = new Usuario();
            AttendanceController Controller = new AttendanceController();
            Entidad = Controller.ValidaUsuario(Request.NombreUsuario, Request.Password);
            Response.usuario = Entidad;
            if (Response.usuario.idUsuario > 0)
                Response.UsuarioValido = true;
            return Response;
        }
        public EmpleadosAttendance ObtenerListaEmpleados(String NombreEmpleado)
        {
            EmpleadosAttendance Response = new EmpleadosAttendance();
            AttendanceController Controller = new AttendanceController();
            Response.Empleados = Controller.ListaEmpleados(NombreEmpleado);
            return Response;
        }
        public List<Catalogo> ObtenerCatalogoHorarios()
        {
            List<Catalogo> Horarios = new List<Catalogo>();
            AttendanceController Controller = new AttendanceController();
            Horarios = Controller.CatalogoHorarios();
            return Horarios;
        }
        public List<Catalogo> ObtenerCatalogoEmpleados()
        {
            List<Catalogo> Empleados = new List<Catalogo>();
            AttendanceController Controller = new AttendanceController();
            Empleados = Controller.CatalogoEmpleados();
            return Empleados;
        }
        public List<Catalogo> ObtenerCatalogoNomina()
        {
            List<Catalogo> Nomina = new List<Catalogo>();
            AttendanceController Controller = new AttendanceController();
            Nomina = Controller.CatalogoNomina();
            return Nomina;
        }
        public List<Catalogo> ObtenerCatalogoCompanias()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.CatalogoCompanias();
        }
        public List<Horario> ObtenerListaHorarios()
        {
            List<Horario> Response = new List<Horario>();
            AttendanceController Controller = new AttendanceController();
            Response = Controller.ListaHorarios();
            return Response;
        }
        public bool InsertaHorario(AltaHorarioRequest Request)
        {
            bool Response = false;
            AttendanceController Controller = new AttendanceController();
            Response = Controller.InsertaHorario(Request.horario, Request.Usuario);
            return Response;
        }
        public bool ActualizaHorario(AltaHorarioRequest Request)
        {
            bool Response = false;
            AttendanceController Controller = new AttendanceController();
            Response = Controller.ActualizaHorario(Request.horario, Request.Usuario);
            return Response;
        }
        public EliminaHorarioResponse EliminaHorario(int HorarioId)
        {
            EliminaHorarioResponse Response = new EliminaHorarioResponse();
            AttendanceController Controller = new AttendanceController();
            Response = Controller.EliminaHorario(HorarioId);
            return Response;
        }
        public DeviceEmployeer ConsultaEmpleadoEnReloj(int idEmpleado, int Dispositivo)
        {
            DeviceEmployeer Empleado = new DeviceEmployeer();
            AttendanceController Controller = new AttendanceController();
            Empleado = Controller.ConsultaEmpleadoEnDispositivo(idEmpleado, Dispositivo);
            return Empleado;
        }
        public bool AltaEmpleadoReloj(DeviceEmployeer Empleado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.AltaEmpleadoDispositivo(Empleado);
        }
        public bool AltaEmpleadosReloj(List<DeviceEmployeer> Empleados, int Dispositivo)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.AltaEmpleadosDispositivo(Empleados, Dispositivo);
        }
        public bool BorraEmpleadoReloj(int Empleado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.BorraEmpleadoDispositivo(Empleado);
        }
        public bool BorraEmpleados(List<int> IdEmpleados, int Dispositivo)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.BorraEmpleadosDispositivo(IdEmpleados, Dispositivo);
        }
        public List<DeviceEmployeer> ConsultaEmpleadosReloj(int Dispositivo)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ExtraeEmpleados(Dispositivo);
        }
        public bool ActualizaEmpleadoBaseDatos(Empleado empleado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizaEmpleadoDB(empleado);
        }
        public ServiceMessage AltaEmpleado(Empleado empleado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.AltaEmpleado(empleado);
        }
        public bool ActualizaEmpleado(Empleado empleado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.EditaEmpleado(empleado);
        }
        public bool SincronizaEmpleado(Empleado empleado, int RelojChecadorOrigen)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.SyncEmpleado(empleado, RelojChecadorOrigen);
        }
        public bool BorraEmpleado(int EmpleadoId)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.BorraEmpleado(EmpleadoId);
        }
        public ServiceMessage AltaEmpleadoExterno(Empleado empleado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.AltaEmpleadoExterno(empleado);
        }
        public ServiceMessage ActualizarRegistrosComedor()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizarDatosComedores();
        }
        public ServiceMessage ActualizarRegistrosEntrada()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizarDatosEntradas();
        }
        public bool SetTime()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.setTime();
        }
        public List<EmpleadoComidas> ObtenerEmpleadosComidas(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerEmpleadosComidas(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
        }
        public List<DetalleComida> ObtenerDetalleComidas(int Empleado, DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerDetalleComidas(Empleado, FechaInicio, FechaFin);
        }
        public List<RegistroReporteComedor> ObtenerLayoutComedor(DateTime FechaInicio, DateTime FechaFin, string Nomina, string Compania)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerLayoutComedor(FechaInicio, FechaFin, Nomina, Compania);
        }
        public List<Configuracion> ObtenerConfiguraciones()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerConfiguraciones();
        }
        public bool ActualizarConfiguracion(Configuracion Request)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizaConfiguracion(Request);
        }
        public List<ComedorExcel> ObtenerReporteComedorExcel(DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerReporteComedorExcel(FechaInicio, FechaFin);
        }
        public List<EmpleadoFaltas> ObtenerFaltas(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerMaestroFaltas(NumeroEmpleado,NombreEmpleado,Compania, Nomina, FechaInicio, FechaFin);
        }
        public List<DetalleFaltas> ObtenerDetalleFaltas(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerDetalleFaltas(EmpleadoId, FechaInicio, FechaFin);
        }
        public List<Retardo> ObtenerDetalleRetardos(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerDetalleRetardos(EmpleadoId, FechaInicio, FechaFin);
        }
        public List<DiaFeriado> ObtenerDiasFeriados()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerDiasFeriados();
        }
        public bool InsertarDiaFeriado(string Descripcion, DateTime Fecha)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.InsertarDiaFeriado(Descripcion, Fecha);
        }
        public bool ActualizaDiaFeriado(int DiaFeriadoId, string Descripcion, DateTime Fecha)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizaDiaFeriado(DiaFeriadoId, Descripcion, Fecha);
        }
        public string ObtenerDiasRetardo()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerDiasRetardo();
        }
        public bool ActualizaDiasRetardo(int Dias)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizaDiasRetardo(Dias);
        }
        public string ObtenerTiempoTolerancia()
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerTiempoTolerancia();
        }
        public bool ActualizaTiempoTolerancia(string TiempoTolerancia)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ActualizaTiempoTolerancia(TiempoTolerancia);
        }
        public bool BorrarDiaFeriado(int DiaFeriado)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.BorrarDiaFeriado(DiaFeriado);
        }
        public List<ReporteIncidencia> ObtenerReporteIncidencias(int NumeroEmpleado,
                                                                 string NombreEmpleado,
                                                                 string Compania,
                                                                 string Nomina,
                                                                 DateTime FechaInicio,
                                                                 DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerReporteIncidencias(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
        }
        public List<RegistroLayoutIncidencias> ObtenerLayoutIncidencias(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin)
        {
            AttendanceController Controller = new AttendanceController();
            return Controller.ObtenerLayoutIncidencias(NumeroEmpleado, NombreEmpleado, Compania, Nomina, FechaInicio, FechaFin);
        }
    }
}
