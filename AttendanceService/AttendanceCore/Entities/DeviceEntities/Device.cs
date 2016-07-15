using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities.DeviceEntities
{
    public class Device
    {
        public Device()
        {
            ip = string.Empty;
            MachineNumber = 1;
            DeviceController = new zkemkeeper.CZKEM();
        }
        public string ip { get; set; }
        public int MachineNumber { get; set; }
        public zkemkeeper.CZKEM DeviceController { get; set; }
    }
}
