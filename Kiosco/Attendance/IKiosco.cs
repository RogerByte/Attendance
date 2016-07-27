using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Attendance.ServiceContracts;
namespace Attendance
{
    [ServiceContract]
    public interface IKiosco
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/authenticate")]
        Session Authenticate(Credentials Credentials);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/ListaSolicitudes")]
        List<Incidencia> ListaSolicitudes(int Empleado);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/CatalogoSolicitudes")]
        List<TipoSolicitud> CatalogoSolicitudes();
    }
}
