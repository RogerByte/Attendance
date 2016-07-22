package presentation.Incidencias;

import java.io.IOException;
import java.time.LocalDate;
import java.util.Calendar;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.EmpleadoFaltas;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Usuario;
import org.tempuri.AttendanceServiceProxy;

import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
import ResponseContracts.AttendanceService.ServiceMessage;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.DatePicker;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.Tooltip;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.HBox;
import javafx.stage.Stage;
import javafx.util.Callback;

public class Incidencias extends AnchorPane{
	private Usuario usuario;
	public Usuario getUsuario(){
		return usuario;
	}
	private String Compania;
	public void setCompania(String Compania){
		this.Compania = Compania;
	}
	public String getCompania(){
		return Compania;
	}
	private String Nomina;
	public void setNomina(String Nomina){
		this.Nomina = Nomina;
	}
	public String getNomina(){
		return Nomina;
	}
	private Stage stage;
	public Incidencias(Stage ParentStage, Usuario usuario)
	{
		this.usuario = usuario;
		stage = ParentStage;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/Incidencias/Incidencias.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			LoadControls();
		}
		catch (IOException exception){
			throw new RuntimeException(exception);
		}
	}
	private void LoadControls()
	{
		dtpkFechaInicio.setShowWeekNumbers(false);
		dtpkFechaFinal.setShowWeekNumbers(false);
		txtNumEmpleadoIncidencias.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent arg0) {
				String texto = txtNumEmpleadoIncidencias.getText();
				if (!texto.matches("[0-9]*")){
					txtNumEmpleadoIncidencias.setText(texto.substring(0, texto.length() - 1));
				}
			}
		});
		setNomina("");
		setCompania("");
		tcNumeroEmpleado.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas,Integer>("NumeroEmpleado"));
		tcNombreEmpleado.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas,String>("NombreEmpleado"));
		tcCompania.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas,String>("Compania"));
		tcNomina.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas,String>("Nomina"));
		tcFaltas.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas, Integer>("NumeroFaltas"));
		ColumnaDetalle();
		ColumnaRetardos();
	}
	private TaskResponse FillGrid()
	{
		TaskResponse Response = new TaskResponse();
		if(ValidaFechas())
		{
			AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
			try 
			{
				setNomina("");
				setCompania("");
				EmpleadoFaltas[] Empleados = Servicio.obtenerFaltas(Integer.parseInt(txtNumEmpleadoIncidencias.getText().isEmpty() ? "0" : txtNumEmpleadoIncidencias.getText()), "", getCompania(), getNomina(), DateToCalendar(dtpkFechaInicio.getValue()), DateToCalendar(dtpkFechaFinal.getValue()));
				ObservableList<EmpleadoFaltas> lst = FXCollections.observableArrayList(Empleados);
				GridIncidencias.setItems(lst);
			}
			catch (Exception e) 
			{
				Response.setMensaje("Error de comunicación con el servicio: " + e.getMessage());
				Response.setTipoMensaje(2);
			}
			Response.setMensaje("Busqueda realizada correctamente");
			Response.setTipoMensaje(1);
		}
		else
		{
			Response.setMensaje("Favor de seleccionar las fechas para efectuar la búsqueda");
			Response.setTipoMensaje(2);
		}
		return Response;
	}
	@FXML TableView<EmpleadoFaltas> GridIncidencias = new TableView<EmpleadoFaltas>();
	@FXML TableColumn<EmpleadoFaltas, Integer> tcNumeroEmpleado = new TableColumn<EmpleadoFaltas, Integer>();
	@FXML TableColumn<EmpleadoFaltas, String> tcNombreEmpleado = new TableColumn<EmpleadoFaltas, String>();
	@FXML TableColumn<EmpleadoFaltas, String> tcCompania = new TableColumn<EmpleadoFaltas, String>();
	@FXML TableColumn<EmpleadoFaltas, String> tcNomina = new TableColumn<EmpleadoFaltas, String>();
	@FXML TableColumn<EmpleadoFaltas, Integer> tcFaltas = new TableColumn<EmpleadoFaltas, Integer>();
	@FXML TableColumn<EmpleadoFaltas, String> tcDetail = new TableColumn<EmpleadoFaltas, String>();
	@FXML TableColumn<EmpleadoFaltas, String> tcRetardos = new TableColumn<EmpleadoFaltas, String>();
	private static Calendar DateToCalendar(LocalDate date)
	{ 
		Calendar cal = Calendar.getInstance();
		cal.setTime(java.sql.Date.valueOf(date));
		return cal;
	}
	@FXML DatePicker dtpkFechaInicio;
	@FXML DatePicker dtpkFechaFinal;
	@FXML TextField txtNumEmpleadoIncidencias = new TextField();
	@FXML Button btnActualizaInformacion = new Button();
	@FXML protected void btnActualizaInformacion_OnAction()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>(){
			@Override public TaskResponse call() throws InterruptedException{
		    	TaskResponse Response = new TaskResponse();
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				try {
					ServiceMessage Mensaje = new ServiceMessage();
					Mensaje = Servicio.actualizarRegistrosEntrada();
						Response.setMensaje(Mensaje.getMensajeRespuesta());
						if(Mensaje.getError())
							Response.setTipoMensaje(2);
						else
							Response.setTipoMensaje(1);
					}
				catch (Exception e) {
					Response.setMensaje("Error en la comunicación con el servicio: " + e.getMessage());
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
				else{
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
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
				else{
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
				}
			}
		});
		progress.showProgess(task);
		new Thread(task).start();
	}
	@FXML protected void txtNumEmpleadoIncidencias_OnAction()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>() {
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				try
				{
					Response = FillGrid();
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
		progress.showProgess(task);
		new Thread(task).start();
	}
	@FXML Button btnBuscar = new Button();
	@FXML protected void btnBuscar_OnAction()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>() {
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				try
				{
					Response = FillGrid();
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
		progress.showProgess(task);
		new Thread(task).start();
	}
	private void ColumnaDetalle()
	{
		tcDetail.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas,String>("NombreEmpleado"));
		tcDetail.setCellFactory(new Callback<TableColumn<EmpleadoFaltas, String>,TableCell<EmpleadoFaltas, String>>(){
			@Override public TableCell<EmpleadoFaltas, String> call(TableColumn<EmpleadoFaltas, String> param){                
				TableCell<EmpleadoFaltas, String> cell = new TableCell<EmpleadoFaltas, String>(){
					ImageView imageview = new ImageView();
					@Override public void updateItem(String item, boolean empty){                        
						if(item!=null){                            
							final HBox box= new HBox();
							box.setSpacing(10);
							imageview.setFitHeight(20);
							imageview.setFitWidth(20);
							imageview.setImage(new Image("/presentation/common/Images/calendar.png")); 
							box.getChildren().addAll(imageview);
							box.setAlignment(Pos.CENTER);
							setGraphic(box);
							setTooltip(new Tooltip("Detalle de registro"));
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									MessageController mensaje = new MessageController((Stage)GridIncidencias.getScene().getWindow());
									try
									{
										DetalleIncidencias Detalle = new DetalleIncidencias((EmpleadoFaltas)GridIncidencias.getSelectionModel().getSelectedItem(),  DateToCalendar(dtpkFechaInicio.getValue()), DateToCalendar(dtpkFechaFinal.getValue()));
										Detalle.show();
									}
									catch (Exception e){
										mensaje.showMessage("Ocurrió un problema en la aplicación: "+ e.getMessage(), 2);
									}
								}
							});
						}
						else
							setGraphic(null);
					}
				};
				return cell;
			}
		});
	}
	private void ColumnaRetardos()
	{
		tcRetardos.setCellValueFactory(new PropertyValueFactory<EmpleadoFaltas,String>("NombreEmpleado"));
		tcRetardos.setCellFactory(new Callback<TableColumn<EmpleadoFaltas, String>,TableCell<EmpleadoFaltas, String>>(){
			@Override public TableCell<EmpleadoFaltas, String> call(TableColumn<EmpleadoFaltas, String> param){                
				TableCell<EmpleadoFaltas, String> cell = new TableCell<EmpleadoFaltas, String>(){
					ImageView imageview = new ImageView();
					@Override public void updateItem(String item, boolean empty){                        
						if(item!=null){                            
							final HBox box= new HBox();
							box.setSpacing(10);
							imageview.setFitHeight(20);
							imageview.setFitWidth(20);
							imageview.setImage(new Image("/presentation/common/Images/Retardo.png")); 
							box.getChildren().addAll(imageview);
							box.setAlignment(Pos.CENTER);
							setGraphic(box);
							setTooltip(new Tooltip("Retardos"));
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									MessageController mensaje = new MessageController((Stage)GridIncidencias.getScene().getWindow());
									try
									{
										Retardos VentanaRetardos = new Retardos((EmpleadoFaltas)GridIncidencias.getSelectionModel().getSelectedItem(),  DateToCalendar(dtpkFechaInicio.getValue()), DateToCalendar(dtpkFechaFinal.getValue()));
										VentanaRetardos.show();
									}
									catch (Exception e){
										mensaje.showMessage("Ocurrió un problema en la aplicación: "+ e.getMessage(), 2);
									}
								}
							});
						}
						else
							setGraphic(null);
					}
				};
				return cell;
			}
		});
	}
	private boolean ValidaFechas()
	{
		boolean valido = true;
		if (dtpkFechaInicio.getValue() == null)
		{
			valido = false;
		}
		if (dtpkFechaFinal.getValue() == null)
		{
			valido = false;
		}
		return valido;
	}
	
}
