using Attendance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attendance.DataAccess;
using Attendance.ServiceContracts;
using System.Security.Principal;
using System.DirectoryServices;

namespace Attendance.BusinessLogic
{
    public class Kiosco
    {
        public Kiosco()
        { 

        }
        private bool ValidarUsuarioDominio(Credentials Request)
        {
            bool result = false;
            using (DirectoryEntry _entry = new DirectoryEntry())
            {
                _entry.Username = Request.username;
                _entry.Password = Request.password;
                DirectorySearcher _searcher = new DirectorySearcher(_entry);
                _searcher.Filter = "(objectclass=user)";
                try
                {
                    SearchResult _sr = _searcher.FindOne();
                    //string _name = _sr.Properties["displayname"][0].ToString();
                    string _name = _sr.Properties["displayname"].ToString();
                    result = true;
                }
                catch
                { /* Error handling omitted to keep code short: remember to handle exceptions !*/ }
            }

            return result; //true = user authenticated!
        }
        /// <summary>
        /// obtiene el catalogo de solicitudes
        /// </summary>
        /// <returns></returns>
        public List<TipoSolicitud> CatalogoSolicitudes()
        {
            List<TipoSolicitud> catalogo = new List<TipoSolicitud>();
            try
            {
                Attendance.DataAccess.Attendance DataBase = new DataAccess.Attendance();
                List<cat_solicitud> listaCatalogo = DataBase.ObtenerCatalogoTipoSolicitudes();
                TipoSolicitud modelo;
                foreach(cat_solicitud registro in listaCatalogo)
                {
                    modelo = new TipoSolicitud();
                    modelo.value = registro.iTipoSolicitudId.ToString();
                    modelo.display = registro.Descripcion;
                    catalogo.Add(modelo);
                }
            }
            catch(Exception ex)
            {
                Log.EscribeLog("[ERROR] : BusinessLogic.CatalogoSolicitudes - " + ex.Message);
            }
            return catalogo;
        }
        /// <summary>
        /// Genera la lista de incidencias a partir de la lista de solicitudes
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        public List<Incidencia> ListaSolicitudes(int Empleado)
        {
            List<Incidencia> Response = new List<Incidencia>();
            try
            {
                DataAccess.Attendance AttendanceDB = new DataAccess.Attendance();
                List<Solicitud> Solicitudes = AttendanceDB.ObtenerSolicitudesVigentes(Empleado);
                Incidencia incidencia;
                foreach (Solicitud solicitud in Solicitudes)
                {
                    incidencia = new Incidencia();
                    incidencia.id = solicitud.iSolicitudId;
                    switch(solicitud.iTipoSolicitud)
                    {
                        case 1:
                            incidencia.icono = "Presentation/assets/Vacations.png";
                            break;
                        case 2:
                            incidencia.icono = "Presentation/assets/Permiso.png";
                            break;
                        case 3:
                            incidencia.icono = "Presentation/assets/PermisoGoce.png";
                            break;
                        case 4:
                            incidencia.icono = "Presentation/assets/Incapacidad.png";
                            break;
                        default:
                            break;
                    }
                    incidencia.descripcion = solicitud.strTipoSolicitud;
                    incidencia.periodo = solicitud.FechaInicio.ToString("dd-MM-yyyy") + " - " + solicitud.FechaFin.ToString("dd-MM-yyyy");
                    incidencia.estatus = solicitud.strEstatusSolicitud;
                    Response.Add(incidencia);
                }
            }
            catch(Exception exc)
            {
                Log.EscribeLog("[ERROR] : BusinessLogic.ListaSolicitudes - " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite validar un usuario al Kiosco regresando su identificador de empleado en la nómina.
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public Session IngresoSistema(Credentials Request)
        {
            Session Response = new Session();
            try
            {
                DataAccess.Attendance AttendanceDB = new DataAccess.Attendance();
                DataAccess.Eslabon EslabonDB = new DataAccess.Eslabon();
                
                if(ValidarUsuarioDominio(Request))
                {
                    Response.EmpleadoId = AttendanceDB.ObtenerIdEmpleado(Request.username);
                    if (Response.EmpleadoId > 0)
                    {
                        Response.SaldoVacacional = SaldoVacacional(Response.EmpleadoId, 0);
                        Empleado Modelo = AttendanceDB.ObtenerInformacionGeneralEmpleado(Response.EmpleadoId);
                        Modelo.Puesto = EslabonDB.ObtenerPuestoEmpleado(Response.EmpleadoId);
                        Response.Compania = Modelo.Compania;
                        Response.isManager = Modelo.isManager;
                        Response.NombreEmpleado = Modelo.NombreEmpleado;
                        Response.NumeroEmpleado = Modelo.NumeroEmpleado;
                        Response.NombreUsuario = Modelo.NombreUsuario;
                        Response.Puesto = Modelo.Puesto;
                        Response.Message = "Inicio de Sesión exitoso";
                        Response.Incidencias = ListaSolicitudes(Response.EmpleadoId);
                    }
                    else
                    {
                        Response.Message = "Usuario o contraseña incorrectos";
                    }
                }
                else
                {
                    Response.EmpleadoId = AttendanceDB.ValidaUsuario(Request.username, Request.password);
                    if (Response.EmpleadoId > 0)
                    {
                        Response.SaldoVacacional = SaldoVacacional(Response.EmpleadoId, 0);
                        Empleado Modelo = AttendanceDB.ObtenerInformacionGeneralEmpleado(Response.EmpleadoId);
                        Modelo.Puesto = EslabonDB.ObtenerPuestoEmpleado(Response.EmpleadoId);
                        Response.Compania = Modelo.Compania;
                        Response.isManager = Modelo.isManager;
                        Response.NombreEmpleado = Modelo.NombreEmpleado;
                        Response.NumeroEmpleado = Modelo.NumeroEmpleado;
                        Response.NombreUsuario = Modelo.NombreUsuario;
                        Response.Puesto = Modelo.Puesto;
                        Response.Message = "Inicio de Sesión exitoso";
                        Response.Incidencias = ListaSolicitudes(Response.EmpleadoId);
                    }
                    else
                    {
                        Response.Message = "Usuario o contraseña incorrectos";
                    }
                }
                
            }
            catch(Exception exc)
            {
                Log.EscribeLog("[ERROR] : BusinessLogic.IngresoSistema - " + exc.Message);
                Response.Message = exc.Message;
            }
            return Response;
        }
        /// <summary>
        /// Obtiene el Saldo vacacional según el algoritmo expuesto en Recursos Humanos.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int SaldoVacacional(int EmpleadoId)
        {
            List<int> S = new List<int>();
            DateTime FechaIngreso = new DateTime();
            DateTime FechaActual = DateTime.Now;
            int Saldo = 0;
            int SaldoNegativo = 0;
            int DiasRestantesPeriodoAnterior = 0;
            try 
            {
                FechaIngreso = Eslabon.GetAntiquity(EmpleadoId);
                int AntiquityYears = 0;
                if (FechaActual.Month == FechaIngreso.Month)
                {
                    if (FechaActual.Day >= FechaIngreso.Day)
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year;
                    else
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year - 1;
                }
                else
                {
                    if (FechaActual.Month >= FechaIngreso.Month)
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year;
                    else
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year - 1;
                }
                int AnioInicial = (AntiquityYears - 2 >= 0 ? AntiquityYears - 2 : 0);
                for (int i = AnioInicial; i <= AntiquityYears; i++)
                {
                    int SaldoAnio = DaysAllowed(i);
                    DateTime FechaA = FechaIngreso;
                    FechaA = FechaA.AddYears(i);
                    DateTime FechaB = FechaIngreso;
                    FechaB = FechaB.AddYears(i + 1);
                    FechaB = FechaB.AddHours(-1);

                    int VacacionesTomadas = Eslabon.GetTakenVacations(EmpleadoId, FechaA, FechaB);
                    if(DiasRestantesPeriodoAnterior > 0)
                    {
                        int DiasDeuda = VacacionesTomadas + SaldoNegativo;
                        if(DiasDeuda == S[i-1])
                        {
                            S[i - 1] = 0;
                            DiasRestantesPeriodoAnterior = 0;
                            VacacionesTomadas = 0;
                            SaldoNegativo = 0;
                        }
                        else if (DiasDeuda > S[i-1])
                        {
                            VacacionesTomadas -= S[i - 1];
                            S[i - 1] = 0;
                        }
                        else if (DiasDeuda < S[i-1])
                        {
                            DiasRestantesPeriodoAnterior -= DiasDeuda;
                            S[i - 1] = DiasRestantesPeriodoAnterior;
                            VacacionesTomadas = 0;
                            SaldoNegativo = 0;
                        }
                    }
                    SaldoAnio = SaldoAnio - VacacionesTomadas + SaldoNegativo;
                    if(SaldoAnio < 0 && i != AntiquityYears)
                    {
                        SaldoNegativo = SaldoAnio;
                        S.Add(0);
                    }
                    else
                    {
                        DiasRestantesPeriodoAnterior = SaldoAnio;
                        S.Add(SaldoAnio);
                        SaldoNegativo = 0;
                    }
                }
                for (int i = 0; i < S.Count; i++)
                {
                    DateTime FechaA = FechaIngreso;
                    FechaA = FechaA.AddYears(i);
                    FechaA = FechaA.AddDays(548);
                    if (FechaA >= FechaActual)
                        Saldo += S[i];
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error: " + exc.Message);
            }
            return Saldo;
        }
        /// <summary>
        /// Calcula Saldo Vacacional por periodos de antiguedad
        /// </summary>
        /// <param name="EmpleadoId"></param>
        /// <returns></returns>
        public int SaldoVacacional(int id, int a)
        {
            List<int> S = new List<int>();
            DateTime FechaIngreso = new DateTime();
            DateTime FechaActual = DateTime.Now;
            int Saldo = 0;
            int SaldoNegativo = 0;
            int DiasRestantesPeriodoAnterior = 0;
            try
            {
                FechaIngreso = Eslabon.GetAntiquity(id);
                int AntiquityYears = 0;
                if (FechaActual.Month == FechaIngreso.Month)
                {
                    if (FechaActual.Day >= FechaIngreso.Day)
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year;
                    else
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year - 1;
                }
                else
                {
                    if (FechaActual.Month >= FechaIngreso.Month)
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year;
                    else
                        AntiquityYears = FechaActual.Year - FechaIngreso.Year - 1;
                }
                int MaxAntiguedadVacaciones = Eslabon.MaximaAntiguedadVacaciones(id);
                int AnioInicial = AntiquityYears == 0 ? 0 : AntiquityYears - 1;
                for (int i = AnioInicial; i <= AntiquityYears; i++)
                {
                    int SaldoAnio = DaysAllowed(i);
                    int VacacionesTomadas = Eslabon.GetTakenVacations(id, i);
                    if (DiasRestantesPeriodoAnterior > 0)
                    {
                        int DiasDeuda = VacacionesTomadas + SaldoNegativo;
                        if (DiasDeuda == S[(S.Count) - 1])
                        {
                            S[(S.Count) - 1] = 0;
                            DiasRestantesPeriodoAnterior = 0;
                            VacacionesTomadas = 0;
                            SaldoNegativo = 0;
                        }
                        else if (DiasDeuda > S[(S.Count) - 1])
                        {
                            VacacionesTomadas -= S[(S.Count) - 1];
                            S[(S.Count) - 1] = 0;
                        }
                        else if (DiasDeuda < S[(S.Count) - 1])
                        {
                            DiasRestantesPeriodoAnterior -= DiasDeuda;
                            S[(S.Count) - 1] = DiasRestantesPeriodoAnterior;
                            VacacionesTomadas = 0;
                            SaldoNegativo = 0;
                        }
                    }
                    SaldoAnio = SaldoAnio - VacacionesTomadas + SaldoNegativo;
                    if (SaldoAnio < 0 && i != AntiquityYears)
                    {
                        SaldoNegativo = SaldoAnio;
                        S.Add(0);
                    }
                    else
                    {
                        DiasRestantesPeriodoAnterior = SaldoAnio;
                        S.Add(SaldoAnio);
                        SaldoNegativo = 0;
                    }
                }
                for (int i = 0; i < S.Count; i++)
                {
                    DateTime FechaA = FechaIngreso.AddYears(DateTime.Today.AddYears(-2).Year - FechaIngreso.Year);
                    FechaA = FechaA.AddYears(i);
                    FechaA = FechaA.AddDays(548);
                    if (FechaA >= FechaActual)
                        Saldo += S[i];
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: " + exc.Message);
            }
            return Saldo;
        }
        /// <summary>
        /// Obtiene el número de días de vacaciones permitidos según el número de años que se ingresa como parámetro
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        private int DiasPermitidos(int years)
        {
            int DiasPermitidos = 0;
            for (int i = 1; i <= years; i++)
            {
                if (DiasPermitidos == 0)
                    DiasPermitidos = 6;
                if (i >= 2 && i <= 5)
                    DiasPermitidos += 2;
                if (i == 10 || i == 15 || i == 20 || i == 25)
                    DiasPermitidos += 2;
            }
            return DiasPermitidos;
        }
        /// <summary>
        /// Equivalente al método de DiasPermitidos pero en su forma recursiva
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int DaysAllowed(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 6;
            if (n <= 5 || n == 10 || n == 15 || n == 20 || n == 25)
                return DaysAllowed(n - 1) + 2;
            else
                return DaysAllowed(n - 1);
        }
    }
}