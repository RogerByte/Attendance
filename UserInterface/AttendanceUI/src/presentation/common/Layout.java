package presentation.common;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.PrintWriter;
import java.util.Calendar;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.ComedorExcel;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.RegistroLayoutIncidencias;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.RegistroReporteComedor;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.ReporteIncidencia;
import org.tempuri.AttendanceServiceProxy;

import presentation.common.entities.TaskResponse;

import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFCellStyle;
import org.apache.poi.hssf.usermodel.HSSFFont;
import org.apache.poi.hssf.usermodel.HSSFRichTextString;
import org.apache.poi.hssf.usermodel.HSSFRow;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;

public class Layout
{
	String Archivo;
	public void setArchivo(String Archivo)
	{
		this.Archivo = Archivo;
	}
	public String getArchivo()
	{
		return Archivo;
	}
	public Layout(String Archivo)
	{
		setArchivo(Archivo);
	}
	public TaskResponse GeneraLayoutComedor(Calendar FechaInicio, Calendar FechaFin, String Nomina, String Compania)
	{
		TaskResponse Response = new TaskResponse();
		AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
		try 
		{
			RegistroReporteComedor[] Registros = Servicio.obtenerLayoutComedor(FechaInicio, FechaFin, Nomina, Compania);
			File f;
			f = new File(getArchivo().toLowerCase().endsWith(".txt") ? getArchivo() : getArchivo() + ".txt");
			FileWriter w = new FileWriter(f);
			BufferedWriter bw = new BufferedWriter(w);
			PrintWriter wr = new PrintWriter(bw);
			
			wr.write(
				Registros[0].getEmpleado() + "\t" + 
				Registros[0].getRazonSocial() + "\t" + 
				Registros[0].getNomina() + "\t" + 
				Registros[0].getDIP() + "\t" +
				Registros[0].getNombre() + "\t" +
				Registros[0].getDescripcion() + "\t" +
				Registros[0].getImporte1() + "\t" +
				Registros[0].getFechaMovimiento() + "\t" +
				Registros[0].getReferencia() + "\t" +
				Registros[0].getNivelEstructura() + "\t" +
				Registros[0].getImporte2() + "\t" +
				Registros[0].getImporte3() + "\t" +
				Registros[0].getSaldoActual() + "\t" +
				Registros[0].getSaldoAnterior() + "\t" +
				Registros[0].getImporteCapturado() 
				
			);
			for(RegistroReporteComedor Registro : Registros)
			{
				if(Registro.getEmpleadoId() != 0)
					wr.append(
							"\n" +
							Registro.getEmpleado() + "\t" + 
							Registro.getRazonSocial() + "\t" + 
							Registro.getNomina() + "\t" + 
							Registro.getDIP() + "\t" +
							Registro.getNombre() + "\t" +
							Registro.getDescripcion() + "\t" +
							Registro.getImporte1() + "\t" +
							Registro.getFechaMovimiento() + "\t" +
							Registro.getReferencia() + "\t" +
							Registro.getNivelEstructura() + "\t" +
							Registro.getImporte2() + "\t" +
							Registro.getImporte3() + "\t" +
							Registro.getSaldoActual() + "\t" +
							Registro.getSaldoAnterior() + "\t" +
							Registro.getImporteCapturado()
						);
			}
			wr.close();
			bw.close();
			Response.setMensaje("Layout generado correctamente");
			Response.setTipoMensaje(1);
		}
		catch(Exception exc)
		{
			Response.setMensaje(exc.getMessage());
			Response.setTipoMensaje(2);
		}
		return Response;
	}
	/**
	 * Obtener Layout de incidencias
	 * @param FechaInicio
	 * @param FechaFin
	 * @param Nomina
	 * @param Compania
	 * @return
	 */
	public TaskResponse GeneraLayoutIncidencias(Calendar FechaInicio, Calendar FechaFin, String Nomina, String Compania)
	{
		TaskResponse Response = new TaskResponse();
		AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
		try 
		{
			RegistroLayoutIncidencias[] Registros = Servicio.obtenerLayoutIncidencias(0, "", Compania, Nomina, FechaInicio, FechaFin);
			File f;
			f = new File(getArchivo().toLowerCase().endsWith(".txt") ? getArchivo() : getArchivo() + ".txt");
			FileWriter w = new FileWriter(f);
			BufferedWriter bw = new BufferedWriter(w);
			PrintWriter wr = new PrintWriter(bw);
			
			wr.write(
				Registros[0].getEmpleado() + "\t" + 
				Registros[0].getRazonSocial() + "\t" + 
				Registros[0].getNomina() + "\t" + 
				Registros[0].getDIP() + "\t" +
				Registros[0].getNombre() + "\t" +
				Registros[0].getDescripcion() + "\t" +
				Registros[0].getImporte1() + "\t" +
				Registros[0].getFechaMovimiento() + "\t" +
				Registros[0].getReferencia() + "\t" +
				Registros[0].getNivelEstructura() + "\t" +
				Registros[0].getImporte2() + "\t" +
				Registros[0].getImporte3() + "\t" +
				Registros[0].getSaldoActual() + "\t" +
				Registros[0].getSaldoAnterior() + "\t" +
				Registros[0].getImporteCapturado() 
				
			);
			for(RegistroLayoutIncidencias Registro : Registros)
			{
				if(Registro.getEmpleadoId() != 0)
					wr.append(
							"\n" +
							Registro.getEmpleado() + "\t" + 
							Registro.getRazonSocial() + "\t" + 
							Registro.getNomina() + "\t" + 
							Registro.getDIP() + "\t" +
							Registro.getNombre() + "\t" +
							Registro.getDescripcion() + "\t" +
							Registro.getImporte1() + "\t" +
							Registro.getFechaMovimiento() + "\t" +
							Registro.getReferencia() + "\t" +
							Registro.getNivelEstructura() + "\t" +
							Registro.getImporte2() + "\t" +
							Registro.getImporte3() + "\t" +
							Registro.getSaldoActual() + "\t" +
							Registro.getSaldoAnterior() + "\t" +
							Registro.getImporteCapturado()
						);
			}
			wr.close();
			bw.close();
			Response.setMensaje("Layout generado correctamente");
			Response.setTipoMensaje(1);
		}
		catch(Exception exc)
		{
			Response.setMensaje(exc.getMessage());
			Response.setTipoMensaje(2);
		}
		return Response;
	}
	/**
	 * ReporteComedorExcel, Obtener Reporte Excel de comedor
	 * @param FechaInicio
	 * @param FechaFin
	 * @return
	 */
	public TaskResponse ReporteComedorExcel(Calendar FechaInicio, Calendar FechaFin)
	{
		TaskResponse Response = new TaskResponse();
		AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
		try
		{
			ComedorExcel [] Registros = Servicio.obtenerReporteComedorExcel(FechaInicio, FechaFin);
			if(Registros.length > 0)
			{
				HSSFWorkbook wb = new HSSFWorkbook();
				HSSFSheet sheet = wb.createSheet("Reporte Comedor");
				HSSFRow row = sheet.createRow(0);
				HSSFCellStyle cellStyle = wb.createCellStyle();
				HSSFFont font = wb.createFont();
				font.setBold(true);
				cellStyle.setFont(font);
				//Establecer Headers
				HSSFCell cell = row.createCell(0);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Número Empleado"));
				cell = row.createCell(1);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Nombre Empleado"));
				cell = row.createCell(2);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Fecha"));
				cell = row.createCell(3);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Lugar"));
				cell = row.createCell(4);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Importe Empresa"));
				cell = row.createCell(5);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Importe Retención"));
				cell = row.createCell(6);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Razón social"));
				cell = row.createCell(7);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Tipo de Nómina"));
				cell = row.createCell(8);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Centro de costos"));
				cell = row.createCell(9);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Descripcion centro de costos"));
				int Fila = 1;
				for(ComedorExcel Registro : Registros)
				{
					row = sheet.createRow(Fila);
					cell = row.createCell(0);
					cell.setCellValue(Registro.getNumeroEmpleado());
					cell = row.createCell(1);
					cell.setCellValue(Registro.getNombreEmpleado());
					cell = row.createCell(2);
					cell.setCellValue(Registro.getFechaRegistro());
					cell = row.createCell(3);
					cell.setCellValue(Registro.getLugar());
					cell = row.createCell(4);
					cell.setCellValue(Registro.getImporteEmpresa().doubleValue());
					cell = row.createCell(5);
					cell.setCellValue(Registro.getImporteRetencion().doubleValue());
					cell = row.createCell(6);
					cell.setCellValue(Registro.getRazonSocial());
					cell = row.createCell(7);
					cell.setCellValue(Registro.getNomina());
					cell = row.createCell(8);
					cell.setCellValue(Registro.getClaveCentroCostos());
					cell = row.createCell(9);
					cell.setCellValue(Registro.getDescripcionCentroCostos());
					Fila++;
				}
				FileOutputStream fileOut = new FileOutputStream(getArchivo().endsWith(".xls") ? getArchivo() : getArchivo() + ".xls");
				wb.write(fileOut);
	            fileOut.close();
	            wb.close();
	            Response.setMensaje("Reporte generado correctamente");
	            Response.setTipoMensaje(1);
			}
		}
		catch(Exception exc)
		{
			Response.setMensaje("Error: " + exc.getMessage());
			Response.setTipoMensaje(2);
		}
		return Response;
	}
	public TaskResponse ReporteIncidencias(Calendar FechaInicio, Calendar FechaFin)
	{
		TaskResponse Response = new TaskResponse();
		AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
		try
		{
			ReporteIncidencia [] Registros = Servicio.obtenerReporteIncidencias(0, "", "", "", FechaInicio, FechaFin);
			if(Registros.length > 0)
			{
				HSSFWorkbook wb = new HSSFWorkbook();
				HSSFSheet sheet = wb.createSheet("Reporte Incidencias");
				HSSFRow row = sheet.createRow(0);
				HSSFCellStyle cellStyle = wb.createCellStyle();
				HSSFFont font = wb.createFont();
				font.setBold(true);
				cellStyle.setFont(font);
				//Establecer Headers
				HSSFCell cell = row.createCell(0);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Número Empleado"));
				cell = row.createCell(1);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Nombre Empleado"));
				cell = row.createCell(2);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Fecha"));
				cell = row.createCell(3);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Concepto"));
				cell = row.createCell(4);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Razón social"));
				cell = row.createCell(5);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Tipo de Nómina"));
				cell = row.createCell(6);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Centro de costos"));
				cell = row.createCell(7);
				cell.setCellStyle(cellStyle);
				cell.setCellValue(new HSSFRichTextString("Descripcion centro de costos"));
				int Fila = 1;
				for(ReporteIncidencia Registro : Registros)
				{
					row = sheet.createRow(Fila);
					cell = row.createCell(0);
					cell.setCellValue(Registro.getNumeroEmpleado());
					cell = row.createCell(1);
					cell.setCellValue(Registro.getNombreEmpleado());
					cell = row.createCell(2);
					cell.setCellValue(Registro.getFecha());
					cell = row.createCell(3);
					cell.setCellValue(Registro.getConcepto());
					cell = row.createCell(4);
					cell.setCellValue(Registro.getRazonSocial());
					cell = row.createCell(5);
					cell.setCellValue(Registro.getNomina());
					cell = row.createCell(6);
					cell.setCellValue(Registro.getClaveCentroCostos());
					cell = row.createCell(7);
					cell.setCellValue(Registro.getDescripcionCentroCostos());
					Fila++;
				}
				FileOutputStream fileOut = new FileOutputStream(getArchivo().endsWith(".xls") ? getArchivo() : getArchivo() + ".xls");
				wb.write(fileOut);
	            fileOut.close();
	            wb.close();
	            Response.setMensaje("Reporte generado correctamente");
	            Response.setTipoMensaje(1);
			}
		}
		catch(Exception exc)
		{
			Response.setMensaje("Error: " + exc.getMessage());
			Response.setTipoMensaje(2);
		}
		return Response;
	}
}
