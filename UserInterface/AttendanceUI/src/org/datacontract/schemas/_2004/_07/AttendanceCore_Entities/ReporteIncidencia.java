/**
 * ReporteIncidencia.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.4 Apr 22, 2006 (06:55:48 PDT) WSDL2Java emitter.
 */

package org.datacontract.schemas._2004._07.AttendanceCore_Entities;

public class ReporteIncidencia  implements java.io.Serializable {
    private java.lang.String claveCentroCostos;

    private java.lang.String compania;

    private java.lang.String concepto;

    private java.lang.String descripcionCentroCostos;

    private java.lang.Integer empleadoId;

    private java.lang.String fecha;

    private java.lang.String nombreEmpleado;

    private java.lang.String nomina;

    private java.lang.Integer numeroEmpleado;

    private java.lang.String razonSocial;

    public ReporteIncidencia() {
    }

    public ReporteIncidencia(
           java.lang.String claveCentroCostos,
           java.lang.String compania,
           java.lang.String concepto,
           java.lang.String descripcionCentroCostos,
           java.lang.Integer empleadoId,
           java.lang.String fecha,
           java.lang.String nombreEmpleado,
           java.lang.String nomina,
           java.lang.Integer numeroEmpleado,
           java.lang.String razonSocial) {
           this.claveCentroCostos = claveCentroCostos;
           this.compania = compania;
           this.concepto = concepto;
           this.descripcionCentroCostos = descripcionCentroCostos;
           this.empleadoId = empleadoId;
           this.fecha = fecha;
           this.nombreEmpleado = nombreEmpleado;
           this.nomina = nomina;
           this.numeroEmpleado = numeroEmpleado;
           this.razonSocial = razonSocial;
    }


    /**
     * Gets the claveCentroCostos value for this ReporteIncidencia.
     * 
     * @return claveCentroCostos
     */
    public java.lang.String getClaveCentroCostos() {
        return claveCentroCostos;
    }


    /**
     * Sets the claveCentroCostos value for this ReporteIncidencia.
     * 
     * @param claveCentroCostos
     */
    public void setClaveCentroCostos(java.lang.String claveCentroCostos) {
        this.claveCentroCostos = claveCentroCostos;
    }


    /**
     * Gets the compania value for this ReporteIncidencia.
     * 
     * @return compania
     */
    public java.lang.String getCompania() {
        return compania;
    }


    /**
     * Sets the compania value for this ReporteIncidencia.
     * 
     * @param compania
     */
    public void setCompania(java.lang.String compania) {
        this.compania = compania;
    }


    /**
     * Gets the concepto value for this ReporteIncidencia.
     * 
     * @return concepto
     */
    public java.lang.String getConcepto() {
        return concepto;
    }


    /**
     * Sets the concepto value for this ReporteIncidencia.
     * 
     * @param concepto
     */
    public void setConcepto(java.lang.String concepto) {
        this.concepto = concepto;
    }


    /**
     * Gets the descripcionCentroCostos value for this ReporteIncidencia.
     * 
     * @return descripcionCentroCostos
     */
    public java.lang.String getDescripcionCentroCostos() {
        return descripcionCentroCostos;
    }


    /**
     * Sets the descripcionCentroCostos value for this ReporteIncidencia.
     * 
     * @param descripcionCentroCostos
     */
    public void setDescripcionCentroCostos(java.lang.String descripcionCentroCostos) {
        this.descripcionCentroCostos = descripcionCentroCostos;
    }


    /**
     * Gets the empleadoId value for this ReporteIncidencia.
     * 
     * @return empleadoId
     */
    public java.lang.Integer getEmpleadoId() {
        return empleadoId;
    }


    /**
     * Sets the empleadoId value for this ReporteIncidencia.
     * 
     * @param empleadoId
     */
    public void setEmpleadoId(java.lang.Integer empleadoId) {
        this.empleadoId = empleadoId;
    }


    /**
     * Gets the fecha value for this ReporteIncidencia.
     * 
     * @return fecha
     */
    public java.lang.String getFecha() {
        return fecha;
    }


    /**
     * Sets the fecha value for this ReporteIncidencia.
     * 
     * @param fecha
     */
    public void setFecha(java.lang.String fecha) {
        this.fecha = fecha;
    }


    /**
     * Gets the nombreEmpleado value for this ReporteIncidencia.
     * 
     * @return nombreEmpleado
     */
    public java.lang.String getNombreEmpleado() {
        return nombreEmpleado;
    }


    /**
     * Sets the nombreEmpleado value for this ReporteIncidencia.
     * 
     * @param nombreEmpleado
     */
    public void setNombreEmpleado(java.lang.String nombreEmpleado) {
        this.nombreEmpleado = nombreEmpleado;
    }


    /**
     * Gets the nomina value for this ReporteIncidencia.
     * 
     * @return nomina
     */
    public java.lang.String getNomina() {
        return nomina;
    }


    /**
     * Sets the nomina value for this ReporteIncidencia.
     * 
     * @param nomina
     */
    public void setNomina(java.lang.String nomina) {
        this.nomina = nomina;
    }


    /**
     * Gets the numeroEmpleado value for this ReporteIncidencia.
     * 
     * @return numeroEmpleado
     */
    public java.lang.Integer getNumeroEmpleado() {
        return numeroEmpleado;
    }


    /**
     * Sets the numeroEmpleado value for this ReporteIncidencia.
     * 
     * @param numeroEmpleado
     */
    public void setNumeroEmpleado(java.lang.Integer numeroEmpleado) {
        this.numeroEmpleado = numeroEmpleado;
    }


    /**
     * Gets the razonSocial value for this ReporteIncidencia.
     * 
     * @return razonSocial
     */
    public java.lang.String getRazonSocial() {
        return razonSocial;
    }


    /**
     * Sets the razonSocial value for this ReporteIncidencia.
     * 
     * @param razonSocial
     */
    public void setRazonSocial(java.lang.String razonSocial) {
        this.razonSocial = razonSocial;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof ReporteIncidencia)) return false;
        ReporteIncidencia other = (ReporteIncidencia) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true && 
            ((this.claveCentroCostos==null && other.getClaveCentroCostos()==null) || 
             (this.claveCentroCostos!=null &&
              this.claveCentroCostos.equals(other.getClaveCentroCostos()))) &&
            ((this.compania==null && other.getCompania()==null) || 
             (this.compania!=null &&
              this.compania.equals(other.getCompania()))) &&
            ((this.concepto==null && other.getConcepto()==null) || 
             (this.concepto!=null &&
              this.concepto.equals(other.getConcepto()))) &&
            ((this.descripcionCentroCostos==null && other.getDescripcionCentroCostos()==null) || 
             (this.descripcionCentroCostos!=null &&
              this.descripcionCentroCostos.equals(other.getDescripcionCentroCostos()))) &&
            ((this.empleadoId==null && other.getEmpleadoId()==null) || 
             (this.empleadoId!=null &&
              this.empleadoId.equals(other.getEmpleadoId()))) &&
            ((this.fecha==null && other.getFecha()==null) || 
             (this.fecha!=null &&
              this.fecha.equals(other.getFecha()))) &&
            ((this.nombreEmpleado==null && other.getNombreEmpleado()==null) || 
             (this.nombreEmpleado!=null &&
              this.nombreEmpleado.equals(other.getNombreEmpleado()))) &&
            ((this.nomina==null && other.getNomina()==null) || 
             (this.nomina!=null &&
              this.nomina.equals(other.getNomina()))) &&
            ((this.numeroEmpleado==null && other.getNumeroEmpleado()==null) || 
             (this.numeroEmpleado!=null &&
              this.numeroEmpleado.equals(other.getNumeroEmpleado()))) &&
            ((this.razonSocial==null && other.getRazonSocial()==null) || 
             (this.razonSocial!=null &&
              this.razonSocial.equals(other.getRazonSocial())));
        __equalsCalc = null;
        return _equals;
    }

    private boolean __hashCodeCalc = false;
    public synchronized int hashCode() {
        if (__hashCodeCalc) {
            return 0;
        }
        __hashCodeCalc = true;
        int _hashCode = 1;
        if (getClaveCentroCostos() != null) {
            _hashCode += getClaveCentroCostos().hashCode();
        }
        if (getCompania() != null) {
            _hashCode += getCompania().hashCode();
        }
        if (getConcepto() != null) {
            _hashCode += getConcepto().hashCode();
        }
        if (getDescripcionCentroCostos() != null) {
            _hashCode += getDescripcionCentroCostos().hashCode();
        }
        if (getEmpleadoId() != null) {
            _hashCode += getEmpleadoId().hashCode();
        }
        if (getFecha() != null) {
            _hashCode += getFecha().hashCode();
        }
        if (getNombreEmpleado() != null) {
            _hashCode += getNombreEmpleado().hashCode();
        }
        if (getNomina() != null) {
            _hashCode += getNomina().hashCode();
        }
        if (getNumeroEmpleado() != null) {
            _hashCode += getNumeroEmpleado().hashCode();
        }
        if (getRazonSocial() != null) {
            _hashCode += getRazonSocial().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(ReporteIncidencia.class, true);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "ReporteIncidencia"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("claveCentroCostos");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "ClaveCentroCostos"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("compania");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "Compania"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("concepto");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "Concepto"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("descripcionCentroCostos");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "DescripcionCentroCostos"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("empleadoId");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "EmpleadoId"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "int"));
        elemField.setMinOccurs(0);
        elemField.setNillable(false);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("fecha");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "Fecha"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("nombreEmpleado");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "NombreEmpleado"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("nomina");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "Nomina"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("numeroEmpleado");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "NumeroEmpleado"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "int"));
        elemField.setMinOccurs(0);
        elemField.setNillable(false);
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("razonSocial");
        elemField.setXmlName(new javax.xml.namespace.QName("http://schemas.datacontract.org/2004/07/AttendanceCore.Entities", "RazonSocial"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        elemField.setMinOccurs(0);
        elemField.setNillable(true);
        typeDesc.addFieldDesc(elemField);
    }

    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

    /**
     * Get Custom Serializer
     */
    public static org.apache.axis.encoding.Serializer getSerializer(
           java.lang.String mechType, 
           java.lang.Class _javaType,  
           javax.xml.namespace.QName _xmlType) {
        return 
          new  org.apache.axis.encoding.ser.BeanSerializer(
            _javaType, _xmlType, typeDesc);
    }

    /**
     * Get Custom Deserializer
     */
    public static org.apache.axis.encoding.Deserializer getDeserializer(
           java.lang.String mechType, 
           java.lang.Class _javaType,  
           javax.xml.namespace.QName _xmlType) {
        return 
          new  org.apache.axis.encoding.ser.BeanDeserializer(
            _javaType, _xmlType, typeDesc);
    }

}
