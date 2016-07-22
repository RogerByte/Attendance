package presentation.comedor;

import java.io.IOException;
import java.util.Calendar;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.DetalleComida;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.EmpleadoComidas;
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

public class DetalleComidas extends AnchorPane
{
	EmpleadoComidas Empleado;
	public void setEmpleado(EmpleadoComidas value)
	{
		this.Empleado = value;
	}
	public EmpleadoComidas getEmpleado()
	{
		return Empleado;
	}
	Calendar FechaInicio;
	Calendar FechaFin;
	private final Stage stage = new Stage();
	public DetalleComidas(EmpleadoComidas Entidad, Calendar FechaInicio, Calendar FechaFin)
	{
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/comedor/DetalleComidas.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			setEmpleado(Entidad);
			lblNumeroEmpleado.setText(Empleado.getNumeroEmpleado().toString());
			lblNombreEmpleado.setText(Empleado.getNombreEmpleado().toUpperCase());
			lblNomina.setText(Empleado.getNomina().toUpperCase());
			lblCompania.setText(Empleado.getCompania().toUpperCase());
			tcFechaRegistro.setCellValueFactory(new PropertyValueFactory<DetalleComida,String>("FechaRegistro"));
			tcLugar.setCellValueFactory(new PropertyValueFactory<DetalleComida,String>("LugarRegistro"));
			this.FechaInicio = FechaInicio;
			this.FechaFin = FechaFin;
		}
		catch (IOException exception){
			throw new RuntimeException(exception);
		}
	}
	public void show(){
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Detalle de comidas");
		stage.setResizable(true);
		stage.show();
		FillGrid();
	}
	@FXML Label lblNumeroEmpleado = new Label();
	@FXML Label lblNombreEmpleado = new Label();
	@FXML Label lblNomina = new Label();
	@FXML Label lblCompania = new Label();
	@FXML TableView<DetalleComida> GridDetalleComidas = new TableView<DetalleComida>();
	@FXML TableColumn<DetalleComida, String> tcFechaRegistro = new TableColumn<DetalleComida, String>();
	@FXML TableColumn<DetalleComida, String> tcLugar = new TableColumn<DetalleComida, String>();
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
					DetalleComida[] Details = Servicio.obtenerDetalleComidas(Empleado.getEmpleadoId(), FechaInicio, FechaFin);
					ObservableList<DetalleComida> lst = FXCollections.observableArrayList(Details);
					GridDetalleComidas.setItems(lst);
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
