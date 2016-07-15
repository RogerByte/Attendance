using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AttendanceCore.ServiceContracts
{
    [DataContract(Name = "UsuarioResponse", Namespace = "AttendanceService.ResponseContracts")]
    public class UsuarioResponse
    {
        public UsuarioResponse()
        {
            UsuarioValido = false;
            usuario = new Entities.Usuario();
        }
        [DataMember]
        public bool UsuarioValido { get; set; }
        [DataMember]
        public Entities.Usuario usuario { get; set; }
    }
    [DataContract(Name = "EmpleadosAttendance", Namespace = "AttendanceService.ResponseContracts")]
    public class EmpleadosAttendance
    {
        public EmpleadosAttendance()
        {
            Empleados = new List<Entities.Empleado>();
        }
        [DataMember]
        public List<Entities.Empleado> Empleados { get; set; }
    }
    [DataContract(Name = "EliminaHorarioResponse", Namespace = "AttendanceService.ResponseContracts")]
    public class EliminaHorarioResponse
    {
        public EliminaHorarioResponse()
        {
            Respuesta = string.Empty;
            Eliminado = false;
        }
        [DataMember]
        public string Respuesta { get; set; }
        [DataMember]
        public bool Eliminado { get; set; }
    }
    [DataContract(Name = "ServiceMessage", Namespace = "AttendanceService.ResponseContracts")]
    public class ServiceMessage
    {
        public ServiceMessage()
        {
            MensajeRespuesta = string.Empty;
            Error = false;
        }
        [DataMember]
        public string MensajeRespuesta { get; set; }
        [DataMember]
        public bool Error { get; set; }
    }
}
