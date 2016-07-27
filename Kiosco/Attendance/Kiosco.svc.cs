using Attendance.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace Attendance
{
    public class Kiosco : IKiosco
    {
        public Session Authenticate(Credentials Credentials)
        {
            BusinessLogic.Kiosco Controller = new BusinessLogic.Kiosco();
            return Controller.IngresoSistema(Credentials);
        }
        public List<Incidencia> ListaSolicitudes(int Empleado)
        {
            BusinessLogic.Kiosco Controller = new BusinessLogic.Kiosco();
            return Controller.ListaSolicitudes(Empleado);
        }

        public List<TipoSolicitud> CatalogoSolicitudes()
        {
            BusinessLogic.Kiosco Controller = new BusinessLogic.Kiosco();
            return Controller.CatalogoSolicitudes();
        }
    }
}
