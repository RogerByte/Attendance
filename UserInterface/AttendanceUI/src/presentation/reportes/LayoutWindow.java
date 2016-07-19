package presentation.reportes;

import java.io.File;
import java.rmi.RemoteException;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Calendar;
import org.tempuri.AttendanceServiceProxy;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.DatePicker;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.FileChooser;
import javafx.stage.Modality;
import javafx.stage.Stage;
import presentation.common.Layout;
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.Catalogo;
import presentation.common.entities.TaskResponse;

public class LayoutWindow extends AnchorPane{
	/**
	 * 
	 * @param Case <br> 
	 * 1 - Layout Comedores <br>
	 * 2 - Layout Incidencias <br>
	 * 3 - Reporte General Comedor <br>
	 * 4 - Reporte General Incidencias <br>
	 */
	public LayoutWindow(int Case){
		this.stage = new Stage();
		MessageController mensaje = new MessageController(getStage());
		try	{
			setCase(Case);
			FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/reportes/LayoutWindow.fxml"));
			fxmlLoader.setRoot(this);
			fxmlLoader.setController(this);
			fxmlLoader.load();
			switch(getCase()){
				case 1:
					lblTitulo.setText("Layout comedores"); //Reporte layout de comedores
					break;
				case 2:
					lblTitulo.setText("Layout incidencias"); //Reporte layout de incidencias
					break;
				case 3:
					lblTitulo.setText("Informativo Comedor");
					break;
				case 4:
					lblTitulo.setText("Informativo Incidencias");
					break;
				default:
					mensaje.btnSalir.setOnAction((e)->{
						getStage().close();
						mensaje.getStage().close();
					});
					mensaje.showMessage("Error de procedimiento en reportes de layout.", 2);
					break;
			}
			CargaCatalogoRazonSocial();
			CargaCatalogoNomina();
			btnCancelar.setOnAction((e)->{
				getStage().close();
			});
			btnGenerar.setOnAction((e)->{
				if(ValidaForma()){
					GenerarReporte();
				}
			});
			dtpkFechaInicio.setShowWeekNumbers(false);
			dtpkFechaFin.setShowWeekNumbers(false);
		}
		catch (Exception exception) {
			mensaje.showMessage("Error en el sistema: " + exception.getMessage(), 2);
		}
	}
	public void show(){
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.setTitle(lblTitulo.getText());
		stage.show();
	}
	private int Case;
	private Calendar FechaInicio;
	private Calendar FechaFin;
	private String Nomina;
	private String RazonSocial;
	private String NumeroEmpleado;
	private Stage stage;
	public int getCase() {
		return Case;
	}
	public void setCase(int Case) {
		this.Case = Case;
	}
	public Calendar getFechaInicio() {
		return FechaInicio;
	}
	public void setFechaInicio(Calendar fechaInicio) {
		FechaInicio = fechaInicio;
	}
	public Calendar getFechaFin() {
		return FechaFin;
	}
	public void setFechaFin(Calendar fechaFin) {
		FechaFin = fechaFin;
	}
	public String getNomina() {
		return Nomina;
	}
	public void setNomina(String nomina) {
		Nomina = nomina;
	}
	public String getRazonSocial() {
		return RazonSocial;
	}
	public void setRazonSocial(String razonSocial) {
		RazonSocial = razonSocial;
	}
	public String getNumeroEmpleado() {
		return NumeroEmpleado;
	}
	public void setNumeroEmpleado(String numeroEmpleado) {
		NumeroEmpleado = numeroEmpleado;
	}
	public Stage getStage() {
		return stage;
	}
	public void setStage(Stage stage) {
		this.stage = stage;
	}
	@FXML Label lblTitulo;
	@FXML TextField txtNumeroEmpleado;
	@FXML DatePicker dtpkFechaInicio;
	@FXML DatePicker dtpkFechaFin;
	@FXML ComboBox<Catalogo> cmbRazonSocial;
	@FXML ComboBox<Catalogo> cmbNomina;
	@FXML Button btnCancelar;
	@FXML Button btnGenerar;
	
	private void GenerarReporte(){
		switch(getCase()){
		case 1:
			try
			{
				final ProgressController progress = new ProgressController(stage);
				final MessageController Mensaje = new MessageController(stage);
				FileChooser fileChooser = new FileChooser();
				//Set extension filter
		        FileChooser.ExtensionFilter extFilter = new FileChooser.ExtensionFilter("Archivos Texto (*.txt)", "*.txt");
		        fileChooser.getExtensionFilters().add(extFilter);
		        //Show save file dialog
		        File Archivo = fileChooser.showSaveDialog(stage);
		        final String path = Archivo != null ? Archivo.getAbsolutePath() : "";
				final Task<TaskResponse> task = new Task<TaskResponse>() {
					@Override
					protected TaskResponse call() throws Exception {
						TaskResponse Response = new TaskResponse();
						Catalogo entidad_nomina = cmbNomina.getItems().get(cmbNomina.getSelectionModel().getSelectedIndex());
						if(entidad_nomina.id == 1000)
							setNomina("");
						else
							setNomina(entidad_nomina.displayString);
						Catalogo entidad_comapnias = cmbRazonSocial.getItems().get(cmbRazonSocial.getSelectionModel().getSelectedIndex());
						if(entidad_comapnias.id == 1000)
							setRazonSocial("");
						else
							setRazonSocial(entidad_comapnias.displayString);
						Layout generadorLayout = new Layout(path);
						try
						{
							Response = generadorLayout.GeneraLayoutComedor(ObtenerFecha(dtpkFechaInicio.getValue()), 
									ObtenerFecha(dtpkFechaFin.getValue()),
									getNomina(),
									getRazonSocial()
									);
						}
						catch(Exception exc)
						{
							Response.setMensaje("Excepción: " + exc.getMessage());
							Response.setTipoMensaje(2);
						}
						updateProgress(10, 10);
						return Response;
					}
				};
				task.setOnSucceeded(new EventHandler<WorkerStateEvent>(){
					@Override
					public void handle(WorkerStateEvent event){
						progress.closeProgress();
						TaskResponse response = new TaskResponse();
						response = (TaskResponse)task.getValue();
						if(response.getTipoMensaje() == 2)
						{
							Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
						}
						else
						{
							Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
							getStage().close();
						}
					}
				});
				task.setOnFailed(new EventHandler<WorkerStateEvent>(){
					@Override
					public void handle(WorkerStateEvent event){
						progress.closeProgress();
						TaskResponse response = new TaskResponse();
						response = (TaskResponse)task.getValue();
						if(response.getTipoMensaje() == 2){
							Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
						}
					}
				});
				if(path!=""){
					progress.showProgess(task);
					new Thread(task).start();
				}
			}
			catch(Exception exc)
			{
				MessageController mensaje = new MessageController(getStage());
				mensaje.showMessage("Error en la aplicación " + exc.getMessage(), 2);
			}
			break;
		case 2:
			try
			{
				final ProgressController progress = new ProgressController(stage);
				final MessageController Mensaje = new MessageController(stage);
				FileChooser fileChooser = new FileChooser();
				//Set extension filter
		        FileChooser.ExtensionFilter extFilter = new FileChooser.ExtensionFilter("Archivos Texto (*.txt)", "*.txt");
		        fileChooser.getExtensionFilters().add(extFilter);
		        //Show save file dialog
		        File Archivo = fileChooser.showSaveDialog(stage);
		        final String path = Archivo != null ? Archivo.getAbsolutePath() : "";
				final Task<TaskResponse> task = new Task<TaskResponse>() {
					@Override
					protected TaskResponse call() throws Exception {
						TaskResponse Response = new TaskResponse();
						Catalogo entidad_nomina = cmbNomina.getItems().get(cmbNomina.getSelectionModel().getSelectedIndex());
						if(entidad_nomina.id == 1000)
							setNomina("");
						else
							setNomina(entidad_nomina.displayString);
						Catalogo entidad_comapnias = cmbRazonSocial.getItems().get(cmbRazonSocial.getSelectionModel().getSelectedIndex());
						if(entidad_comapnias.id == 1000)
							setRazonSocial("");
						else
							setRazonSocial(entidad_comapnias.displayString);
						Layout generadorLayout = new Layout(path);
						try
						{
							Response = generadorLayout.GeneraLayoutIncidencias(ObtenerFecha(dtpkFechaInicio.getValue()), 
									ObtenerFecha(dtpkFechaFin.getValue()),
									getNomina(),
									getRazonSocial()
									);
						}
						catch(Exception exc)
						{
							Response.setMensaje("Excepción: " + exc.getMessage());
							Response.setTipoMensaje(2);
						}
						updateProgress(10, 10);
						return Response;
					}
				};
				task.setOnSucceeded(new EventHandler<WorkerStateEvent>(){
					@Override
					public void handle(WorkerStateEvent event){
						progress.closeProgress();
						TaskResponse response = new TaskResponse();
						response = (TaskResponse)task.getValue();
						if(response.getTipoMensaje() == 2)
						{
							Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
						}
						else
						{
							Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
							getStage().close();
						}
					}
				});
				task.setOnFailed(new EventHandler<WorkerStateEvent>(){
					@Override
					public void handle(WorkerStateEvent event){
						progress.closeProgress();
						TaskResponse response = new TaskResponse();
						response = (TaskResponse)task.getValue();
						if(response.getTipoMensaje() == 2){
							Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
						}
					}
				});
				if(path != ""){
					progress.showProgess(task);
					new Thread(task).start();
				}
			}
			catch(Exception exc)
			{
				MessageController mensaje = new MessageController(getStage());
				mensaje.showMessage("Error en la aplicación " + exc.getMessage(), 2);
			}
			break;
		case 3:
			
			break;
		case 4:
			
			break;
		default:
			break;
		}
		
	}
	private void CargaCatalogoNomina()
	{
		final Task<TaskResponse> task = new Task<TaskResponse>(){

			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				ArrayList<Catalogo> CatalogoNomina = new ArrayList<Catalogo>();
				try {
					org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Nomina  = Servicio.obtenerCatalogoNomina();
					if(Nomina.length > 0){
						for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo modelo : Nomina){
							Catalogo catalogo = new Catalogo(modelo.getId(), modelo.getDescripcion());
							CatalogoNomina.add(catalogo);
						}
					}
					ObservableList<Catalogo> ListaNomina = FXCollections.observableArrayList(CatalogoNomina);
					cmbNomina.getItems().addAll(ListaNomina);
					Response.setMensaje("Catálogo de nómina obtenido correctamente");
					Response.setTipoMensaje(1);
				}
				catch (RemoteException e) {
					Response.setMensaje("Error al obtener el catálogo de nómina");
					Response.setTipoMensaje(2);
				}
				return Response;
			}
		};
		final MessageController Mensaje = new MessageController(stage);
		task.setOnSucceeded(new EventHandler<WorkerStateEvent>(){
			@Override
			public void handle(WorkerStateEvent event){
				TaskResponse response = new TaskResponse();
				response = (TaskResponse)task.getValue();
				if(response.getTipoMensaje() == 2){
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
				}
				else
				{
					for(Catalogo obj : cmbNomina.getItems()){
						if(obj.id.equals(1000)){
							cmbNomina.getSelectionModel().select(obj);
							break;
						}
					}
				}
			}
		});
		task.setOnFailed(new EventHandler<WorkerStateEvent>(){
			@Override
			public void handle(WorkerStateEvent event){
				TaskResponse response = new TaskResponse();
				response = (TaskResponse)task.getValue();
				if(response.getTipoMensaje() == 2){
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
				}
			}
		});
		javafx.application.Platform.runLater(task);
	}
	private void CargaCatalogoRazonSocial(){
		final Task<TaskResponse> task = new Task<TaskResponse>(){
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				ArrayList<Catalogo> CatalogoCompanias = new ArrayList<Catalogo>();
				try {
					org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Companias  = Servicio.obtenerCatalogoCompanias();
					if(Companias.length > 0){
						for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo modelo : Companias){
							Catalogo catalogo = new Catalogo(modelo.getId(), modelo.getDescripcion());
							CatalogoCompanias.add(catalogo);
						}
					}
					ObservableList<Catalogo> ListaCompanias = FXCollections.observableArrayList(CatalogoCompanias);
					cmbRazonSocial.getItems().addAll(ListaCompanias);
					Response.setMensaje("Catálogo de compañías obtenido correctamente");
					Response.setTipoMensaje(1);
				}
				catch (Exception e) {
					Response.setMensaje("Error al obtener el catálogo de compañías");
					Response.setTipoMensaje(2);
				}
				return Response;
			}
		};
		final MessageController Mensaje = new MessageController(stage);
		task.setOnSucceeded(new EventHandler<WorkerStateEvent>(){
			@Override
			public void handle(WorkerStateEvent event){
				TaskResponse response = new TaskResponse();
				response = (TaskResponse)task.getValue();
				if(response.getTipoMensaje() == 2){
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
				}
				else
				{
					for(Catalogo obj : cmbRazonSocial.getItems()){
						if(obj.id.equals(1000)){
							cmbRazonSocial.getSelectionModel().select(obj);
							break;
						}
					}
				}
			}
		});
		task.setOnFailed(new EventHandler<WorkerStateEvent>(){
			@Override
			public void handle(WorkerStateEvent event){
				TaskResponse response = new TaskResponse();
				response = (TaskResponse)task.getValue();
				if(response.getTipoMensaje() == 2){
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
				}
			}
		});
		javafx.application.Platform.runLater(task);
	}
	private Calendar ObtenerFecha(LocalDate Fecha){
		Calendar cal = Calendar.getInstance();
		cal.setTime(java.sql.Date.valueOf(Fecha));
		return cal;
	}
	private boolean ValidaForma(){
		boolean valido = true;
		MessageController Mensaje = new MessageController(getStage());
		if(dtpkFechaInicio.getValue() == null){
			valido = false;
			Mensaje.showMessage("Ingresa valor de fecha inicio", 2);
		}else if(dtpkFechaFin.getValue() == null){
			valido = false;
			Mensaje.showMessage("Ingresa valor de fecha fin", 2);
		}else if(dtpkFechaInicio.getValue().isAfter(dtpkFechaFin.getValue())){
			valido = false;
			Mensaje.showMessage("La fecha de inicio debe ser menor a la fecha fin", 2);
		}else if(cmbNomina.getSelectionModel().selectedItemProperty() == null){
			valido = false;
			Mensaje.showMessage("Seleccione un valor de nomina", 2);
		}else if(cmbRazonSocial.getSelectionModel().getSelectedItem() == null){
			valido = false;
			Mensaje.showMessage("Seleccione un valor de Razón Social", 2);
		}
		return valido;
	}
}
