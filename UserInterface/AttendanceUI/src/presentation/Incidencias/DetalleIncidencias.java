package presentation.Incidencias;

import java.util.Calendar;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.DetalleFaltas;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.EmpleadoFaltas;
import org.tempuri.AttendanceServiceProxy;

import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class DetalleIncidencias extends AnchorPane
{
	public DetalleIncidencias(EmpleadoFaltas Entidad, Calendar FechaInicio, Calendar FechaFin)
	{
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/Incidencias/DetalleIncidencias.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			setEmpleado(Entidad);
			lblNumeroEmpleado.setText(Empleado.getNumeroEmpleado().toString());
			lblNombreEmpleado.setText(Empleado.getNombreEmpleado().toUpperCase());
			lblNomina.setText(Empleado.getNomina().toUpperCase());
			lblCompania.setText(Empleado.getCompania().toUpperCase());
			tcFechaRegistro.setCellValueFactory(new PropertyValueFactory<DetalleFaltas,String>("Fecha"));
			setFechaInicio(FechaInicio);
			setFechaFin(FechaFin);
		}
		catch(Exception exc)
		{
			MessageController Mensaje = new MessageController(stage);
			Mensaje.showMessage("Error en la aplicación: " + exc.getMessage(), 2);
		}
	}
	private final Stage stage = new Stage();
	private EmpleadoFaltas Empleado;
	public void setEmpleado(EmpleadoFaltas Empleado)
	{
		this.Empleado = Empleado;
	}
	public EmpleadoFaltas getEmpleado()
	{
		return Empleado;
	}
	private Calendar FechaInicio;
	public void setFechaInicio(Calendar FechaInicio)
	{
		this.FechaInicio = FechaInicio;
	}
	public Calendar getFechaInicio()
	{
		return FechaInicio;
	}
	private Calendar FechaFin;
	public void setFechaFin(Calendar FechaFin)
	{
		this.FechaFin = FechaFin;
	}
	public Calendar getFechaFin()
	{
		return FechaFin;
	}
	@FXML Label lblNumeroEmpleado = new Label();
	@FXML Label lblNombreEmpleado = new Label();
	@FXML Label lblCompania = new Label();
	@FXML Label lblNomina = new Label();
	@FXML TableView<DetalleFaltas> GridDetalleFaltas = new TableView<DetalleFaltas>();
	@FXML TableColumn<DetalleFaltas, String> tcFechaRegistro = new TableColumn<DetalleFaltas, String>();
	public void show()
	{
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Detalle de Faltas");
		stage.setResizable(true);
		stage.show();
		FillGrid();
	}
	public void FillGrid()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		
		final Task<TaskResponse> task = new Task<TaskResponse>(){
			@Override public TaskResponse call() throws InterruptedException{
				TaskResponse Response = new TaskResponse();
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				try 
				{
					DetalleFaltas[] Details = Servicio.obtenerDetalleFaltas(getEmpleado().getEmpleadoId(), getFechaInicio(), getFechaFin());
					ObservableList<DetalleFaltas> lst = FXCollections.observableArrayList(Details);
					GridDetalleFaltas.setItems(lst);
				}
				catch (Exception e) 
				{
					Response.setMensaje("Error de comunicación con el servicio: " + e.getMessage());
					Response.setTipoMensaje(2);
				}
				Response.setMensaje("Busqueda realizada correctamente");
				Response.setTipoMensaje(1);
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
}
