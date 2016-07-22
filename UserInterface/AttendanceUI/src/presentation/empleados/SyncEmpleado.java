package presentation.empleados;
import java.io.IOException;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Empleado;
import org.tempuri.AttendanceServiceProxy;

import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.*;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.RadioButton;
import javafx.scene.control.ToggleGroup;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class SyncEmpleado extends AnchorPane{
	private Empleado empleado;
	public Empleado getEmpleado(){
		return empleado;
	}
	private final Stage stage = new Stage();
	
	public SyncEmpleado(Empleado empleado)
	{
		this.empleado = empleado;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/empleados/SyncEmpleado.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			InitializeComponents();
		}
		catch (IOException exception){
			throw new RuntimeException(exception);
		}
	}
	private void InitializeComponents()
	{
		rbtEntradaCoorporativo.setToggleGroup(group);
		rbtEntradaCEDIS.setToggleGroup(group);
		rbtComedorCoorporativo.setToggleGroup(group);
		rbtComedorCEDIS.setToggleGroup(group);
		lblEmpleado.setText(lblEmpleado.getText() + " " + empleado.getNombreEmpleado());
	}
	public void show(){
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Sincroniza Empleado");
		stage.setResizable(true);
		stage.show();
	}
	@FXML private RadioButton rbtEntradaCoorporativo = new RadioButton();
	@FXML private RadioButton rbtComedorCoorporativo = new RadioButton();
	@FXML private RadioButton rbtEntradaCEDIS = new RadioButton();
	@FXML private RadioButton rbtComedorCEDIS = new RadioButton();
	@FXML private Label lblEmpleado = new Label();
	final ToggleGroup group = new ToggleGroup();
	@FXML private Button btnSync = new Button();
	@FXML protected void btnSync_OnAction(){
		final Task<TaskResponse> task = new Task<TaskResponse>(){
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				int RelojChecador = 0;
				if(rbtEntradaCoorporativo.isSelected())
					RelojChecador = 1;
				if(rbtComedorCoorporativo.isSelected())
					RelojChecador = 2;
				if(rbtEntradaCEDIS.isSelected())
					RelojChecador = 3;
				if(rbtComedorCEDIS.isSelected())
					RelojChecador = 4;
				if(RelojChecador > 0)
				{
					try
					{
						boolean Sincronizado = false;
						Sincronizado = Servicio.sincronizaEmpleado(getEmpleado(), RelojChecador);
						if(Sincronizado)
						{
							Response.setMensaje("¡Sincronización exitosa!");
							Response.setTipoMensaje(1);
						}
						else
						{
							Response.setMensaje("Ocurrió un error de sincronización");
							Response.setTipoMensaje(2);
						}
					}
					catch(Exception exc)
					{
						Response.setMensaje(exc.getMessage());
						Response.setTipoMensaje(2);
					}
				}
				else
				{
					Response.setMensaje("Favor de seleccionar una opción");
					Response.setTipoMensaje(2);
				}
				updateProgress(10,10);
				return Response;
			}
			
		};
		final MessageController Mensaje = new MessageController(stage);
		final ProgressController progress = new ProgressController(stage);
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
					stage.close();
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
					stage.close();
				}
			}
		});
		progress.showProgess(task);
		new Thread(task).start();
	}
}
