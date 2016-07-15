using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using AttendanceCore.Entities;

namespace AttendanceCore.ServiceContracts
{
    [DataContract(Name="UsuarioRequest", Namespace = "AttendanceService.RequestContracts")]
    public class UsuarioRequest
    {
        public UsuarioRequest()
        {
            NombreUsuario = string.Empty;
            Password = string.Empty;
        }
        [DataMember]
        public string NombreUsuario { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
    [DataContract(Name = "AltaHorarioRequest", Namespace = "AttendanceService.RequestContracts")]
    public class AltaHorarioRequest
    {
        public AltaHorarioRequest()
        {
            Usuario = 0;
            horario = new Horario();
        }
        [DataMember]
        public Horario horario { get; set; }
        [DataMember]
        public int Usuario { get; set; }
    }
}
