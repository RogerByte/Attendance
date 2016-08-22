using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Entities
{
    public class PaqueteVacacional
    {
        /// <summary>
        /// Inicialización de un paquete vacacional
        /// </summary>
        /// <param name="dias">Días a otorgar en el paquete</param>
        /// <param name="FechaOtorgamiento">Fecha en la que se otorga el paquete</param>
        public PaqueteVacacional(int dias, DateTime FechaOtorgamiento)
        {
            this.FechaOtorgamiento = FechaOtorgamiento;
            FechaVencimiento = FechaOtorgamiento.AddDays(548);
            Dias = new List<DiaVacacional>();
            DiaVacacional Entidad;
            for (int i = 0; i < dias; i++)
            {
                Entidad = new DiaVacacional();
                Dias.Add(Entidad);
            }
        }
        DateTime FechaOtorgamiento { get; set; }
        DateTime FechaVencimiento { get; set; }
        public List<DiaVacacional> Dias { get; set; }
        /// <summary>
        /// Define si el paquete ha caducado de acuerdo con la fecha ingresada.
        /// </summary>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public bool Caducado(DateTime Fecha)
        {
            bool Caducado = false;
            if (Fecha >= FechaVencimiento)
                Caducado = true;
            return Caducado;
        }
        /// <summary>
        /// Obtiene la fecha en la que fue otorgado el paquete
        /// </summary>
        /// <returns></returns>
        public DateTime FechaOtorgado()
        {
            return FechaOtorgamiento;
        }
    }
}