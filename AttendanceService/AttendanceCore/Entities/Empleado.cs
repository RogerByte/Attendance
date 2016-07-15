using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Empleado
    {
        public Empleado()
        {
            iEmpleadoId = 0;
            HorarioId = 0;
            NumeroEmpleado = string.Empty;
            NombreEmpleado = string.Empty;
            Password = string.Empty;
            Privilegio = 0;
            Enabled = true;
            NumeroTarjeta = string.Empty;
            FingerPrint = string.Empty;
            Externo = false;
            Nomina = string.Empty;
            Compania = string.Empty;
            NombreUsuario = string.Empty;
            PasswordAttendance = string.Empty;
            CorreoElectronico = string.Empty;
            ManagerId = 0;
            EsManager = false;
        }
        public int iEmpleadoId { get; set; }
        public int HorarioId { get; set; }
        public string NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Password { get; set; }
        public int Privilegio { get; set; }
        public bool Enabled { get; set; }
        public string NumeroTarjeta { get; set; }
        public string FingerPrint { get; set; }
        public int FingerPrintLength { get; set; }
        public int FingerFlag { get; set; }
        public bool Externo { get; set; }
        public string Compania { get; set; }
        public string Nomina { get; set; }
        public string NombreUsuario { get; set; }
        public string PasswordAttendance { get; set; }
        public string CorreoElectronico { get; set; }
        public int ManagerId { get; set; }
        public bool EsManager { get; set; }
    }
}
