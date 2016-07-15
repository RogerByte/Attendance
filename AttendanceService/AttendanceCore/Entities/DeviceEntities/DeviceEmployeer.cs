using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities.DeviceEntities
{
    public class DeviceEmployeer
    {
        public string NumeroEmpleado;
        public string NombreEmpleado;
        public string Password;
        public int Privilegio;
        public bool Enabled;
        public string NumeroTarjeta;
        public string FingerPrint;
        public int FingerPrintLength;
        public int FingerFlag;

        public DeviceEmployeer()
        {
            NumeroEmpleado = string.Empty;
            NombreEmpleado = string.Empty;
            Password = string.Empty;
            Privilegio = 0;
            Enabled = false;
            NumeroTarjeta = string.Empty;
            FingerPrint = string.Empty;
        }
    }
}
