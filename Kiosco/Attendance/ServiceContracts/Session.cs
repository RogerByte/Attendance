using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.ServiceContracts
{
    public class Session
    {
        public Session()
        {
            Message = string.Empty;
            NombreUsuario = string.Empty;
            Password = string.Empty;
            EmpleadoId = 0;
            NumeroEmpleado = string.Empty;
            NombreEmpleado = string.Empty;
            SaldoVacacional = 0;
            Compania = string.Empty;
            isManager = false;
            Puesto = string.Empty;
            Incidencias = new List<Incidencia>();
        }
        public string Message { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int EmpleadoId { get; set; }
        public string NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public int SaldoVacacional { get; set; }
        public string Compania { get; set; }
        public bool isManager { get; set; }
        public string Puesto { get; set; }
        public List<Incidencia> Incidencias { get; set; }
    }
}