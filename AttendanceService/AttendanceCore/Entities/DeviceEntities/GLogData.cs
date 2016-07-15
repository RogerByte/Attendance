using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities.DeviceEntities
{
    public class GLogData
    {
        public GLogData()
        {
            EmpleadoId = string.Empty;
            VerifyMode = 0;
            InOutMode = 0;
            Year = 0;
            Month = 0;
            Day = 0;
            Hour = 0;
            Minute = 0;
            Second = 0;
            WorkCode = 0;
        }
        public string EmpleadoId;
        public int VerifyMode;
        public int InOutMode;
        public int Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
        public int WorkCode;
    }
}
