using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AttendanceCore.ServiceContracts;
using AttendanceCore.Entities;
using AttendanceCore.Entities.DeviceEntities;

namespace AttendanceCore
{
    [ServiceContract(Name="AttendanceService")]
    public interface IAttendanceService
    {
        [OperationContract(Name = "CatalogoManagers")]
        List<Catalogo> CatalogoManagers();

        [OperationContract(Name = "AltaUsuario")]
        bool AltaUsuario(Usuario Request);

        [OperationContract(Name = "BajaUsuario")]
        bool BajaUsuario(int UsuarioId);

        [OperationContract(Name = "CambioUsuario")]
        bool CambioUsuario(Usuario Request);

        [OperationContract(Name = "ConsultaUsuarios")]
        List<Usuario> ConsultaUsuario(string Request);

        [OperationContract(Name="ValidaUsuario")]
        UsuarioResponse ValidaUsuario(UsuarioRequest Request);
        
        [OperationContract(Name = "ObtenerListaEmpleados")]
        EmpleadosAttendance ObtenerListaEmpleados(string NombreEmpleado);

        [OperationContract(Name = "ObtenerCatalogoHorarios")]
        List<Catalogo> ObtenerCatalogoHorarios();

        [OperationContract(Name = "ObtenerCatalogoEmpleados")]
        List<Catalogo> ObtenerCatalogoEmpleados();

        [OperationContract(Name = "ObtenerCatalogoNomina")]
        List<Catalogo> ObtenerCatalogoNomina();

        [OperationContract(Name = "ObtenerCatalogoCompanias")]
        List<Catalogo> ObtenerCatalogoCompanias();

        [OperationContract(Name = "ObtenerListaHorarios")]
        List<Horario> ObtenerListaHorarios();

        [OperationContract(Name = "InsertaHorario")]
        bool InsertaHorario(AltaHorarioRequest Request);

        [OperationContract(Name = "ActualizaHorario")]
        bool ActualizaHorario(AltaHorarioRequest Request);

        [OperationContract(Name = "EliminaHorario")]
        EliminaHorarioResponse EliminaHorario(int HorarioId);

        [OperationContract(Name = "ConsultaEmpleadoEnReloj")]
        DeviceEmployeer ConsultaEmpleadoEnReloj(int idEmpleado, int Dispositivo);

        [OperationContract(Name = "AltaEmpleadoReloj")]
        bool AltaEmpleadoReloj(DeviceEmployeer Empleado);

        [OperationContract(Name = "AltaEmpleadosReloj")]
        bool AltaEmpleadosReloj(List<DeviceEmployeer> Empleados, int Dispositivo);

        [OperationContract(Name = "BorraEmpleadoReloj")]
        bool BorraEmpleadoReloj(int Empleado);

        [OperationContract(Name = "BorraEmpleados")]
        bool BorraEmpleados(List<int> IdEmpleados, int Dispositivo);

        [OperationContract(Name = "ConsultaEmpleadosReloj")]
        List<DeviceEmployeer> ConsultaEmpleadosReloj(int Dispositivo);

        [OperationContract(Name = "ActualizaEmpleadoBaseDatos")]
        bool ActualizaEmpleadoBaseDatos(Empleado empleado);

        [OperationContract(Name = "AltaEmpleado")]
        ServiceMessage AltaEmpleado(Empleado empleado);

        [OperationContract(Name = "ActualizaEmpleado")]
        bool ActualizaEmpleado(Empleado empleado);

        [OperationContract(Name = "SincronizaEmpleado")]
        bool SincronizaEmpleado(Empleado empleado, int RelojChecadorOrigen);

        [OperationContract(Name = "BorraEmpleado")]
        bool BorraEmpleado(int EmpleadoId);

        [OperationContract(Name = "SetTime")]
        bool SetTime();

        [OperationContract(Name = "AltaEmpleadoExterno")]
        ServiceMessage AltaEmpleadoExterno(Empleado empleado);

        [OperationContract(Name = "ActualizarRegistrosComedor")]
        ServiceMessage ActualizarRegistrosComedor();

        [OperationContract(Name = "ActualizarRegistrosEntrada")]
        ServiceMessage ActualizarRegistrosEntrada();

        [OperationContract(Name = "ObtenerEmpleadosComidas")]
        List<EmpleadoComidas> ObtenerEmpleadosComidas(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerDetalleComidas")]
        List<DetalleComida> ObtenerDetalleComidas(int Empleado, DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerLayoutComedor")]
        List<RegistroReporteComedor> ObtenerLayoutComedor(DateTime FechaInicio, DateTime FechaFin, string Nomina, string Compania);

        [OperationContract(Name = "ObtenerConfiguraciones")]
        List<Configuracion> ObtenerConfiguraciones();

        [OperationContract(Name = "ActualizarConfiguracion")]
        bool ActualizarConfiguracion(Configuracion Request);

        [OperationContract(Name = "ObtenerReporteComedorExcel")]
        List<ComedorExcel> ObtenerReporteComedorExcel(DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerFaltas")]
        List<EmpleadoFaltas> ObtenerFaltas(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerDetalleFaltas")]
        List<DetalleFaltas> ObtenerDetalleFaltas(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerDetalleRetardos")]
        List<Retardo> ObtenerDetalleRetardos(int EmpleadoId, DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerDiasFeriados")]
        List<DiaFeriado> ObtenerDiasFeriados();

        [OperationContract(Name = "InsertarDiaFeriado")]
        bool InsertarDiaFeriado(string Descripcion, DateTime Fecha);

        [OperationContract(Name = "ActualizaDiaFeriado")]
        bool ActualizaDiaFeriado(int DiaFeriadoId, string Descripcion, DateTime Fecha);

        [OperationContract(Name = "ObtenerDiasRetardo")]
        string ObtenerDiasRetardo();

        [OperationContract(Name = "ActualizaDiasRetardo")]
        bool ActualizaDiasRetardo(int Dias);

        [OperationContract(Name = "ObtenerTiempoTolerancia")]
        string ObtenerTiempoTolerancia();

        [OperationContract(Name = "ActualizaTiempoTolerancia")]
        bool ActualizaTiempoTolerancia(string TiempoTolerancia);

        [OperationContract(Name = "BorrarDiaFeriado")]
        bool BorrarDiaFeriado(int DiaFeriado);

        [OperationContract(Name = "ObtenerReporteIncidencias")]
        List<ReporteIncidencia> ObtenerReporteIncidencias(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin);

        [OperationContract(Name = "ObtenerLayoutIncidencias")]
        List<RegistroLayoutIncidencias> ObtenerLayoutIncidencias(int NumeroEmpleado, string NombreEmpleado, string Compania, string Nomina, DateTime FechaInicio, DateTime FechaFin);
    }
}
