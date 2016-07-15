using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCore.Entities
{
    public class Horario
    {
        public int HorarioId { get; set; }
        public string DescripcionHorario { get; set; }
        public bool Lunes { get; set; }
        public string HorarioLunes { get; set; }
        public bool Martes { get; set; }
        public string HorarioMartes { get; set; }
        
        public bool Miercoles { get; set; }
        public string HorarioMiercoles { get; set; }
        
        public bool Jueves { get; set; }
        public string HorarioJueves { get; set; }
        
        public bool Viernes { get; set; }
        public string HorarioViernes { get; set; }
        
        public bool Sabado { get; set; }
        public string HorarioSabado { get; set; }
        
        public bool Domingo { get; set; }
        public string HorarioDomingo { get; set; }
        

        public Horario()
        {
            HorarioId = 0;
            DescripcionHorario = string.Empty;
            Lunes = false;
            Martes = false;
            Miercoles = false;
            Jueves = false;
            Viernes = false;
            Sabado = false;
            Domingo = false;
            HorarioLunes = string.Empty;
            HorarioMartes = string.Empty;
            HorarioMiercoles = string.Empty;
            HorarioJueves = string.Empty;
            HorarioViernes = string.Empty;
            HorarioSabado = string.Empty;
            HorarioDomingo = string.Empty;
        }
    }
}
