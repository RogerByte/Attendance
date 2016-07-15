using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AttendanceCore.BusinessLogic
{
    public class Log
    {
        public static void EscribeLog(String Mensaje)
        {
            try
            {
                string LogDirectory = "C:\\RelojChecadorServicio\\Log\\";
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);
                DateTime Fecha = DateTime.Now;
                string content = "[" + Fecha.ToString("yyyy/MM/dd HH:mm:ss") + "]" + " " + Mensaje;
                string ArchivoLog = LogDirectory + Fecha.ToShortDateString().Replace("/", "-") + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(ArchivoLog, true))
                {
                    file.WriteLine(content);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error en la escritura de la bitácora: " + exc.Message);
            }
        }
    }
}
