using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttendanceCore.Entities.DeviceEntities;
using System.Threading;
using System.Configuration;

namespace AttendanceCore.DeviceAccess
{
    public class DeviceDriver
    {
        private Device _MasterDevice;
        public Device MasterDevice
        {
            get
            {
                return _MasterDevice;
            }
            set
            {
                if (value.ip != "" && value.MachineNumber > 0 && value.MachineNumber < 5)
                {
                    _MasterDevice = value;
                }
                else
                {
                    //El dispositivo primario por default se define como la entrada del coorporativo
                    _MasterDevice = EntradaCoorporativo;
                }
            }
        }
        public Device EntradaCoorporativo { get; set; }
        public Device EntradaCEDIS { get; set; }
        public Device ComedorCoorporativo { get; set; }
        public Device ComedorCEDIS { get; set; }
        private bool EnableDeviceOperations { get; set; }

        public DeviceDriver()
        {
            EntradaCoorporativo = new Device();
            EntradaCEDIS = new Device();
            ComedorCoorporativo = new Device();
            ComedorCEDIS = new Device();
            EntradaCoorporativo.ip = ConfigurationManager.AppSettings["EntradaCoorporativo"].ToString();
            EntradaCoorporativo.MachineNumber = 1;
            EntradaCEDIS.ip = ConfigurationManager.AppSettings["EntradaCEDIS"].ToString();
            EntradaCEDIS.MachineNumber = 2;
            ComedorCoorporativo.ip = ConfigurationManager.AppSettings["ComedorCoorporativo"].ToString();
            ComedorCoorporativo.MachineNumber = 3;
            ComedorCEDIS.ip = ConfigurationManager.AppSettings["ComedorCEDIS"].ToString();
            ComedorCEDIS.MachineNumber = 4;
            EnableDeviceOperations = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableDevices"]);
        }
        /// <summary>
        /// Permite copiar un empleado dado de alta en el dispositivo maestro a todos los demás dispositivos
        /// </summary>
        /// <param name="NumeroEmpleado">Número del empleado que se requiere sincronizar</param>
        /// <returns>True, si la sincronización se efectuó con éxito</returns>
        public bool SyncEmpleado(string NumeroEmpleado)
        {
            bool response = false;
            if (EnableDeviceOperations)
                try
                {
                    List<Device> Dispositivos = new List<Device>();
                    if (MasterDevice != EntradaCoorporativo)
                        Dispositivos.Add(EntradaCoorporativo);
                    if (MasterDevice != EntradaCEDIS)
                        Dispositivos.Add(EntradaCEDIS);
                    if (MasterDevice != ComedorCoorporativo)
                        Dispositivos.Add(ComedorCoorporativo);
                    if (MasterDevice != ComedorCEDIS)
                        Dispositivos.Add(ComedorCEDIS);
                    if (MasterDevice.DeviceController.Connect_Net(MasterDevice.ip, 4370))
                    {
                        DeviceEmployeer EmpleadoOrigen = new DeviceEmployeer();
                        EmpleadoOrigen.NombreEmpleado = NumeroEmpleado;
                        //Extracción de la información del empleado desde el dispositivo origen
                        MasterDevice.DeviceController.SSR_GetUserInfo(MasterDevice.MachineNumber, EmpleadoOrigen.NumeroEmpleado, out EmpleadoOrigen.NombreEmpleado, out EmpleadoOrigen.Password, out EmpleadoOrigen.Privilegio, out EmpleadoOrigen.Enabled);
                        while (MasterDevice.DeviceController.SSR_GetAllUserInfo(MasterDevice.MachineNumber, out EmpleadoOrigen.NumeroEmpleado, out EmpleadoOrigen.NombreEmpleado, out EmpleadoOrigen.Password, out EmpleadoOrigen.Privilegio, out EmpleadoOrigen.Enabled))
                        {
                            if (EmpleadoOrigen.NumeroEmpleado == NumeroEmpleado)
                            {
                                MasterDevice.DeviceController.GetStrCardNumber(out EmpleadoOrigen.NumeroTarjeta);
                                MasterDevice.DeviceController.GetUserTmpExStr(MasterDevice.MachineNumber, NumeroEmpleado, 0, out EmpleadoOrigen.FingerFlag, out EmpleadoOrigen.FingerPrint, out EmpleadoOrigen.FingerPrintLength);
                                break;
                            }
                        }
                        MasterDevice.DeviceController.Disconnect();
                        //---------------------------------------------------------------------
                        foreach (Device Dispositivo in Dispositivos)
                        {
                            if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                            {
                                int idwFingerIndex = 0;
                                int iFlag = 1;
                                Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false);
                                //Actualización de la información del empleado en el dispositivo destino
                                Dispositivo.DeviceController.set_STR_CardNumber(1, EmpleadoOrigen.NumeroTarjeta);
                                if (
                                Dispositivo.DeviceController.SSR_SetUserInfo(Dispositivo.MachineNumber,
                                                                   EmpleadoOrigen.NumeroEmpleado,
                                                                   EmpleadoOrigen.NombreEmpleado,
                                                                   EmpleadoOrigen.Password,
                                                                   EmpleadoOrigen.Privilegio,
                                                                   EmpleadoOrigen.Enabled))//upload user information to the memory
                                {
                                    if (
                                    Dispositivo.DeviceController.SetUserTmpExStr(Dispositivo.MachineNumber,
                                                                       EmpleadoOrigen.NumeroEmpleado,
                                                                       idwFingerIndex,
                                                                       iFlag,
                                                                       EmpleadoOrigen.FingerPrint))//upload templates information to the memory
                                    {
                                        response = true; //Los datos fueron escritos correctamente
                                    }
                                    Dispositivo.DeviceController.RefreshData(Dispositivo.MachineNumber);
                                    Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                                    Dispositivo.DeviceController.Disconnect();
                                }
                            }
                            if (!response)
                            {
                                BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.SyncEmpleado: No fue posible actualizar la información en el dispositivo [" + Dispositivo.MachineNumber + "]");
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.SyncEmpleado: " + exc.Message);
                }
            else
                response = true;
            return response;
        }
        /// <summary>
        /// Permite dar de alta una serie de empleados a todos los relojes checadores, este método es recomendable cuando se desea realizar una alta masiva de empleados
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        public bool AltaEmpleados(List<DeviceEmployeer> Empleados)
        {
            bool response = false;
            if (EnableDeviceOperations)
                try
                {
                    List<Device> Dispositivos = new List<Device>();
                    Dispositivos.Add(EntradaCoorporativo);
                    Dispositivos.Add(EntradaCEDIS);
                    Dispositivos.Add(ComedorCoorporativo);
                    Dispositivos.Add(ComedorCEDIS);
                    foreach (Device Dispositivo in Dispositivos)
                    {
                        bool Conectado = Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370);
                        int idwFingerIndex = 0;
                        int iFlag = 1;
                        if (Conectado)
                        {
                            Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false);
                            //Actualización de la información del empleado en el dispositivo destino
                            foreach (DeviceEmployeer empleado in Empleados)
                            {
                                Dispositivo.DeviceController.set_STR_CardNumber(1, empleado.NumeroTarjeta);
                                if (
                                Dispositivo.DeviceController.SSR_SetUserInfo(Dispositivo.MachineNumber,
                                                                   empleado.NumeroEmpleado,
                                                                   empleado.NombreEmpleado,
                                                                   empleado.Password,
                                                                   empleado.Privilegio,
                                                                   empleado.Enabled))//upload user information to the memory
                                {
                                    if (empleado.FingerPrint != "" && empleado.FingerPrint != null)
                                    {
                                        if (
                                        Dispositivo.DeviceController.SetUserTmpExStr(Dispositivo.MachineNumber,
                                                                           empleado.NumeroEmpleado,
                                                                           idwFingerIndex,
                                                                           iFlag,
                                                                           empleado.FingerPrint))//upload templates information to the memory
                                        {
                                            response = true; //Los datos fueron escritos correctamente
                                        }
                                        else
                                        {
                                            BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.AltaEmpleados: Empleado con error [" + empleado.NombreEmpleado + "]");
                                        }
                                    }
                                    else
                                    {
                                        response = true;
                                    }
                                }
                                else
                                {
                                    BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.AltaEmpleados: Empleado con error [" + empleado.NombreEmpleado + "]");
                                }
                            }
                            Dispositivo.DeviceController.RefreshData(Dispositivo.MachineNumber);
                            Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                            Dispositivo.DeviceController.Disconnect();
                            if (!response)
                            {
                                BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.AltaEmpleados: No fue posible actualizar la información en el dispositivo [" + Dispositivo.MachineNumber + "]");
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.AltaEmpleado: " + exc.Message);
                }
            else
                response = true;
            return response;
        }
        /// <summary>
        /// Permite dar el alta de un empleado a los relojes checadores
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public bool AltaEmpleado(DeviceEmployeer empleado)
        {
            bool response = false;
            if (EnableDeviceOperations)
                try
                {
                    List<Device> Dispositivos = new List<Device>();
                    Dispositivos.Add(EntradaCoorporativo);
                    Dispositivos.Add(EntradaCEDIS);
                    Dispositivos.Add(ComedorCoorporativo);
                    Dispositivos.Add(ComedorCEDIS);
                    Thread A = new Thread(() => ActualizaInformacionEmpleado(EntradaCoorporativo, empleado));
                    A.Start();
                    Thread B = new Thread(() => ActualizaInformacionEmpleado(EntradaCEDIS, empleado));
                    B.Start();
                    Thread C = new Thread(() => ActualizaInformacionEmpleado(ComedorCoorporativo, empleado));
                    C.Start();
                    Thread D = new Thread(() => ActualizaInformacionEmpleado(ComedorCEDIS, empleado));
                    D.Start();
                    A.Join();
                    B.Join();
                    C.Join();
                    D.Join();
                    response = true;
                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.AltaEmpleado: " + exc.Message);
                }
            else
                response = true;
            return response;
        }
        /// <summary>
        /// Método privado para actualizar la información de un empleado en el reloj checador seleccionad, este método es privador para su utilización dentro de los threads de un método público
        /// </summary>
        /// <param name="Dispositivo"></param>
        /// <param name="empleado"></param>
        /// <returns></returns>
        private bool ActualizaInformacionEmpleado(Device Dispositivo, DeviceEmployeer empleado)
        {
            bool response = false;
            if (EnableDeviceOperations)
            {
                if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                {
                    int idwFingerIndex = 0;
                    int iFlag = 1;
                    Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false);
                    //Actualización de la información del empleado en el dispositivo destino
                    Dispositivo.DeviceController.set_STR_CardNumber(1, empleado.NumeroTarjeta);
                    if (
                    Dispositivo.DeviceController.SSR_SetUserInfo(Dispositivo.MachineNumber,
                                                       empleado.NumeroEmpleado,
                                                       empleado.NombreEmpleado,
                                                       empleado.Password,
                                                       empleado.Privilegio,
                                                       empleado.Enabled))//upload user information to the memory
                    {
                        if (empleado.FingerPrint != null && empleado.FingerPrint != string.Empty)
                        {
                            Dispositivo.DeviceController.SetUserTmpExStr(Dispositivo.MachineNumber,
                                                               empleado.NumeroEmpleado,
                                                               idwFingerIndex,
                                                               iFlag,
                                                               empleado.FingerPrint);//upload templates information to the memory
                        }
                        response = true;
                        Dispositivo.DeviceController.RefreshData(Dispositivo.MachineNumber);
                    }
                    Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                    Dispositivo.DeviceController.Disconnect();
                }
                if (!response)
                {
                    BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.AltaEmpleado: No fue posible actualizar la información en el dispositivo [" + Dispositivo.MachineNumber + "]");
                }
            }
            else
                response = true;
            return response;
        }
        /// <summary>
        /// Borra empleado de todos los relojes checadores
        /// </summary>
        /// <param name="NumeroEmpleado"></param>
        /// <returns></returns>
        public bool BorrarEmpleado(string NumeroEmpleado)
        {
            bool Response = false;
            if (EnableDeviceOperations)
                try
                {
                    List<Device> Dispositivos = new List<Device>();
                    Dispositivos.Add(EntradaCoorporativo);
                    Dispositivos.Add(EntradaCEDIS);
                    Dispositivos.Add(ComedorCoorporativo);
                    Dispositivos.Add(ComedorCEDIS);
                    foreach (Device Dispositivo in Dispositivos)
                    {
                        if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                        {
                            Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false);
                            for (int i = 0; i < 10; i++)
                                Dispositivo.DeviceController.SSR_DelUserTmp(Dispositivo.MachineNumber, NumeroEmpleado, i);
                            if (!Dispositivo.DeviceController.SSR_DeleteEnrollData(Dispositivo.MachineNumber, NumeroEmpleado, 12))
                            {
                                BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.BorrarEmpleado: No fue posible borrar la información en el dispositivo [" + Dispositivo.MachineNumber + "]");
                            }
                            else
                            {
                                Dispositivo.DeviceController.SSR_DelUserTmpExt(Dispositivo.MachineNumber, NumeroEmpleado, 13);
                                Response = true;
                            }
                            Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                            Dispositivo.DeviceController.Disconnect();
                        }
                    }
                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.BorrarEmpleado: " + exc.Message);
                }
            else
                Response = true;
            return Response;
        }
        /// <summary>
        /// Permite eliminar varios empleados en una sóla conexión a los dispositivos, este método es mejor implementarlo en lugar de iterrar con el método de borrarEmpleado
        /// </summary>
        /// <param name="Empleados"></param>
        /// <returns></returns>
        public bool BorrarEmpleados(List<int> Empleados, Device Dispositivo)
        {
            bool Response = false;
            if (EnableDeviceOperations)
                try
                {
                    if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                    {
                        Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false);
                        foreach (int EmpleadoId in Empleados)
                        {
                            if (!Dispositivo.DeviceController.SSR_DeleteEnrollData(Dispositivo.MachineNumber, EmpleadoId.ToString(), 12))
                            {
                                BusinessLogic.Log.EscribeLog("[WARNING] : DeviceDriver.BorrarEmpleado: No fue posible borrar la información en el dispositivo [" + Dispositivo.MachineNumber + "]");
                            }
                            else
                            {
                                Response = true;
                            }
                        }
                        Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                        Dispositivo.DeviceController.Disconnect();
                    }

                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.BorrarEmpleado: " + exc.Message);
                }
            else
                Response = true;
            return Response;
        }
        /// <summary>
        /// Permite obtener los registros de un reloj checador
        /// </summary>
        /// <param name="Dispositivo"></param>
        public List<GLogData> ObtenerRegistros(Device Dispositivo)
        {
            List<GLogData> Response = new List<GLogData>();
            try
            {
                if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                {
                    if (Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false))
                    {
                        if (Dispositivo.DeviceController.ReadAllGLogData(Dispositivo.MachineNumber))
                        {
                            GLogData log = new GLogData();
                            while (Dispositivo.DeviceController.SSR_GetGeneralLogData(Dispositivo.MachineNumber, out log.EmpleadoId, out log.VerifyMode, out log.InOutMode, out log.Year, out log.Month, out log.Day, out log.Hour, out log.Minute, out log.Second, ref log.WorkCode))
                            {
                                Response.Add(log);
                                log = new GLogData();
        }
                        }
                        Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                    }
                    Dispositivo.DeviceController.Disconnect();
                }
            }
            catch (Exception exc)
        {
                BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.ObtenerRegistros " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Permite borrar los registros del dispositivo insertado como parámetro
        /// </summary>
        /// <param name="Dispositivo"></param>
        /// <returns></returns>
        public bool BorraRegistros(Device Dispositivo)
        {
            bool Response = false;
            if (EnableDeviceOperations)
                try
                {
                    if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                    {
                        if (Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false))
                        {
                            Response = Dispositivo.DeviceController.ClearGLog(Dispositivo.MachineNumber);
                            Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                        }
                        Dispositivo.DeviceController.Disconnect();
                    }
                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : DeviceDriver.ObtenerRegistros " + exc.Message);
                }
            else
                Response = true;
            return Response;
        }
        /// <summary>
        /// Extrae la lista de empleados dados de alta en el reloj checador seleccionado.
        /// </summary>
        /// <returns></returns>
        public List<DeviceEmployeer> ExtraeEmpleados(Device DispositivoOrigen)
        {
            List<DeviceEmployeer> Empleados = new List<DeviceEmployeer>();
            try
            {
                DeviceEmployeer empleado = new DeviceEmployeer();
                if (DispositivoOrigen.DeviceController.Connect_Net(DispositivoOrigen.ip, 4370))
                {
                    DispositivoOrigen.DeviceController.EnableDevice(DispositivoOrigen.MachineNumber, false);
                    while (DispositivoOrigen.DeviceController.SSR_GetAllUserInfo(DispositivoOrigen.MachineNumber, out empleado.NumeroEmpleado, out empleado.NombreEmpleado, out empleado.Password, out empleado.Privilegio, out empleado.Enabled))
                    {
                        DispositivoOrigen.DeviceController.GetStrCardNumber(out empleado.NumeroTarjeta);
                        DispositivoOrigen.DeviceController.GetUserTmpExStr(DispositivoOrigen.MachineNumber, empleado.NumeroEmpleado, 0, out empleado.FingerFlag, out empleado.FingerPrint, out empleado.FingerPrintLength);
                        Empleados.Add(empleado);
                        empleado = new DeviceEmployeer();
                    }
                    DispositivoOrigen.DeviceController.EnableDevice(DispositivoOrigen.MachineNumber, true);
                    DispositivoOrigen.DeviceController.Disconnect();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("[ERROR] : ExtraeEmpleados.ExtraeEmpleados: " + exc.Message);
            }
            return Empleados;
        }
        /// <summary>
        /// Consulta la información del empleado desde el reloj checador ingresado como parámetro
        /// </summary>
        /// <param name="idEmpleado">Identificador del empleado que se desea consultar</param>
        /// <param name="Dispositivo">Dispositivo en el cual se desea hacer la consulta</param>
        /// <returns></returns>
        public DeviceEmployeer ConsultaEmpleado(string idEmpleado, Device Dispositivo)
        {
            DeviceEmployeer Empleado = new DeviceEmployeer();
            try
            {
                if (Dispositivo.DeviceController.Connect_Net(Dispositivo.ip, 4370))
                {
                Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, false);
                if (Dispositivo.DeviceController.SSR_GetUserInfo(Dispositivo.MachineNumber, idEmpleado, out Empleado.NombreEmpleado, out Empleado.Password, out Empleado.Privilegio, out Empleado.Enabled))
                {
                    Dispositivo.DeviceController.GetStrCardNumber(out Empleado.NumeroTarjeta);
                    Dispositivo.DeviceController.GetUserTmpExStr(Dispositivo.MachineNumber, idEmpleado, 0, out Empleado.FingerFlag, out Empleado.FingerPrint, out Empleado.FingerPrintLength);
                    Empleado.NumeroEmpleado = idEmpleado;
                }
                Dispositivo.DeviceController.EnableDevice(Dispositivo.MachineNumber, true);
                Dispositivo.DeviceController.Disconnect();
            }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("[ERROR] : ExtraeEmpleados.ConsultaEmpleado: " + exc.Message);
            }
            return Empleado;
        }
        /// <summary>
        /// Método para resetear los relojes checadores, desde los empleados hasta los logs de consulta
        /// </summary>
        /// <returns></returns>
        public bool setTime()
        {
            bool aplicado = true;
            if (EnableDeviceOperations)
                try
                {
                    List<Device> Checadores = new List<Device>();
                    Checadores.Add(EntradaCoorporativo);
                    Checadores.Add(ComedorCoorporativo);
                    Checadores.Add(EntradaCEDIS);
                    Checadores.Add(ComedorCEDIS);
                    DateTime fechaHoy = DateTime.Now;
                    foreach (Device dispositivo in Checadores)
                    {
                        if (dispositivo.DeviceController.Connect_Net(dispositivo.ip, 4370))
                        {
                            dispositivo.DeviceController.EnableDevice(dispositivo.MachineNumber, false);
                            aplicado = aplicado & dispositivo.DeviceController.SetDeviceTime2(dispositivo.MachineNumber, fechaHoy.Year, fechaHoy.Month, fechaHoy.Day, fechaHoy.Hour, fechaHoy.Minute, fechaHoy.Second);
                            dispositivo.DeviceController.EnableDevice(dispositivo.MachineNumber, true);
                            dispositivo.DeviceController.Disconnect();
                        }
                    }
                }
                catch (Exception exc)
                {
                    BusinessLogic.Log.EscribeLog("[ERROR] : ExtraeEmpleados.BorrarLog: " + exc.Message);
                }
            else
                aplicado = true;
            return aplicado;
        }
    }
}
