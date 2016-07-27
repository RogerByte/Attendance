using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Entities
{
    public class Empleado
    {
        public Empleado()
        {
            Id = 0;
            NumeroEmpleado = string.Empty;
            NombreEmpleado = string.Empty;
            Compania = string.Empty;
            NombreUsuario = string.Empty;
            Password = string.Empty;
            Correo = string.Empty;
            ManagerId = 0;
            isManager = false;
            Puesto = string.Empty;
        }
        public int Id { get; set; }
        public string NumeroEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Compania { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Correo { get; set; }
        public int ManagerId { get; set; }
        public bool isManager { get; set; }
        public string Puesto { get; set; }
    }
}