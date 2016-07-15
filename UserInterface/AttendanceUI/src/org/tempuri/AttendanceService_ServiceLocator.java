/**
 * AttendanceService_ServiceLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package org.tempuri;
import presentation.Settings;
public class AttendanceService_ServiceLocator extends org.apache.axis.client.Service implements org.tempuri.AttendanceService_Service {

    public AttendanceService_ServiceLocator() {
    }


    public AttendanceService_ServiceLocator(org.apache.axis.EngineConfiguration config) {
        super(config);
    }

    public AttendanceService_ServiceLocator(java.lang.String wsdlLoc, javax.xml.namespace.QName sName) throws javax.xml.rpc.ServiceException {
        super(wsdlLoc, sName);
    }

    // Use to get a proxy class for BasicHttpBinding_AttendanceService
    private java.lang.String BasicHttpBinding_AttendanceService_address = Settings.getUrlService();

    public java.lang.String getBasicHttpBinding_AttendanceServiceAddress() {
        return BasicHttpBinding_AttendanceService_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String BasicHttpBinding_AttendanceServiceWSDDServiceName = "BasicHttpBinding_AttendanceService";

    public java.lang.String getBasicHttpBinding_AttendanceServiceWSDDServiceName() {
        return BasicHttpBinding_AttendanceServiceWSDDServiceName;
    }

    public void setBasicHttpBinding_AttendanceServiceWSDDServiceName(java.lang.String name) {
        BasicHttpBinding_AttendanceServiceWSDDServiceName = name;
    }

    public org.tempuri.AttendanceService_PortType getBasicHttpBinding_AttendanceService() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(BasicHttpBinding_AttendanceService_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getBasicHttpBinding_AttendanceService(endpoint);
    }

    public org.tempuri.AttendanceService_PortType getBasicHttpBinding_AttendanceService(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            org.tempuri.BasicHttpBinding_AttendanceServiceStub _stub = new org.tempuri.BasicHttpBinding_AttendanceServiceStub(portAddress, this);
            _stub.setPortName(getBasicHttpBinding_AttendanceServiceWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

    public void setBasicHttpBinding_AttendanceServiceEndpointAddress(java.lang.String address) {
        BasicHttpBinding_AttendanceService_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (org.tempuri.AttendanceService_PortType.class.isAssignableFrom(serviceEndpointInterface)) {
                org.tempuri.BasicHttpBinding_AttendanceServiceStub _stub = new org.tempuri.BasicHttpBinding_AttendanceServiceStub(new java.net.URL(BasicHttpBinding_AttendanceService_address), this);
                _stub.setPortName(getBasicHttpBinding_AttendanceServiceWSDDServiceName());
                return _stub;
            }
        }
        catch (java.lang.Throwable t) {
            throw new javax.xml.rpc.ServiceException(t);
        }
        throw new javax.xml.rpc.ServiceException("There is no stub implementation for the interface:  " + (serviceEndpointInterface == null ? "null" : serviceEndpointInterface.getName()));
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(javax.xml.namespace.QName portName, Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        if (portName == null) {
            return getPort(serviceEndpointInterface);
        }
        java.lang.String inputPortName = portName.getLocalPart();
        if ("BasicHttpBinding_AttendanceService".equals(inputPortName)) {
            return getBasicHttpBinding_AttendanceService();
        }
        else  {
            java.rmi.Remote _stub = getPort(serviceEndpointInterface);
            ((org.apache.axis.client.Stub) _stub).setPortName(portName);
            return _stub;
        }
    }

    public javax.xml.namespace.QName getServiceName() {
        return new javax.xml.namespace.QName("http://tempuri.org/", "AttendanceService");
    }

    private java.util.HashSet ports = null;

    public java.util.Iterator getPorts() {
        if (ports == null) {
            ports = new java.util.HashSet();
            ports.add(new javax.xml.namespace.QName("http://tempuri.org/", "BasicHttpBinding_AttendanceService"));
        }
        return ports.iterator();
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        
if ("BasicHttpBinding_AttendanceService".equals(portName)) {
            setBasicHttpBinding_AttendanceServiceEndpointAddress(address);
        }
        else 
{ // Unknown Port Name
            throw new javax.xml.rpc.ServiceException(" Cannot set Endpoint Address for Unknown Port" + portName);
        }
    }

    /**
    * Set the endpoint address for the specified port name.
    */
    public void setEndpointAddress(javax.xml.namespace.QName portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        setEndpointAddress(portName.getLocalPart(), address);
    }

}
