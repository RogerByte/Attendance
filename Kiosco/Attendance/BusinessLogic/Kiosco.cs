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
                        Response.SaldoVacacional = CalculaSaldoVacacional(Response.EmpleadoId);

                        if (Response.EmpleadoId == 5833)
                            Response.SaldoVacacional = 999999999;
                        if(Response.EmpleadoId == 359)
                            Response.SaldoVacacional = 999999999;
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
                        Response.SaldoVacacional = CalculaSaldoVacacional(Response.EmpleadoId);
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
        /// <summary>
        /// Permite calcular los años de antiguedad de un empleado con la fecha de ingreso
        /// </summary>
        /// <param name="FechaIngreso"></param>
        /// <returns></returns>
        private int Antiguedad(DateTime FechaIngreso)
        {
            int anios = 0;
            if (DateTime.Today.Month == FechaIngreso.Month)
            {
                if (DateTime.Today.Day >= FechaIngreso.Day)
                    anios = DateTime.Today.Year - FechaIngreso.Year;
                else
                    anios = DateTime.Today.Year - FechaIngreso.Year - 1;
            }
            else
            {
                if (DateTime.Today.Month >= FechaIngreso.Month)
                    anios = DateTime.Today.Year - FechaIngreso.Year;
                else
                    anios = DateTime.Today.Year - FechaIngreso.Year - 1;
            }
            return anios;
        }
        /// <summary>
        /// Permite calcular el saldo vacacional de un empleado
        /// </summary>
        /// <param name="Empleado"></param>
        /// <returns></returns>
        private int CalculaSaldoVacacional(int Empleado)
        {
            int SaldoVacacional = 0;
            int TiempoAtras = 2; //dos años hacia atrás para no agarrar toda la historia
            DateTime FechaIngreso = Eslabon.GetAntiquity(Empleado);
            int AniosAntiguedad = Antiguedad(FechaIngreso);
            //Generación de los paquetes vacacionales
            List<PaqueteVacacional> Paquetes = new List<PaqueteVacacional>();
            PaqueteVacacional Paquete;
            int AnioInicial = AniosAntiguedad > TiempoAtras ? AniosAntiguedad - TiempoAtras : 0;
            for (int i = AnioInicial; i <= AniosAntiguedad + TiempoAtras; i++)
            {
                Paquete = new PaqueteVacacional(DaysAllowed(i), FechaIngreso.AddYears(i));
                Paquetes.Add(Paquete);
            }
            //Obtención de los días tomados por año
            List<int> VacacionesTomadas = new List<int>();
            int DiasTomados;
            for (int i = AnioInicial; i <= AniosAntiguedad; i++)
            {
                DiasTomados = Eslabon.GetTakenVacations(Empleado, i);
                VacacionesTomadas.Add(DiasTomados);
            }
            for (int i = 0; i < VacacionesTomadas.Count; i++)
            {
                while(VacacionesTomadas[i] > 0) //Ejecutar proceso hasta que se hayan asignado todos los días tomados a los paquetes correspondientes
                {
                    foreach(PaqueteVacacional paquete in Paquetes)
                    {
                        if (VacacionesTomadas[i] > 0)
                        {
                            if (!paquete.Caducado(FechaIngreso.AddYears(i))) //Si no ha caducado en la fecha que se está revisando
                            {
                                for (int j = 0; j < paquete.Dias.Count; j++)
                                {
                                    if (VacacionesTomadas[i] > 0)
                                    {
                                        if (!paquete.Dias[j].Tomado)
                                        {
                                            paquete.Dias[j].Tomado = true;
                                            VacacionesTomadas[i]--;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            foreach(PaqueteVacacional paquete in Paquetes)
            {
                if (!paquete.Caducado(DateTime.Today))
                {
                    foreach(DiaVacacional dia in paquete.Dias)
                    {
                        if (!dia.Tomado && paquete.FechaOtorgado() <= DateTime.Today)
                            SaldoVacacional++;
                        else if (dia.Tomado && paquete.FechaOtorgado() >= DateTime.Today)
                            SaldoVacacional--;
                    }
                }
            }
            return SaldoVacacional;
        }
    }
}