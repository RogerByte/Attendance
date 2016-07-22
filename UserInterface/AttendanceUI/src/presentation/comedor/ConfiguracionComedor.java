package presentation.comedor;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Configuracion;
import org.tempuri.AttendanceServiceProxy;

import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class ConfiguracionComedor extends AnchorPane
{
	private final Stage stage = new Stage();
	public ConfiguracionComedor()
	{
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/comedor/ConfiguracionComedor.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
		}
		catch (Exception exception){
			MessageController mensaje = new MessageController(this.stage);
			mensaje.showMessage("Error en la aplicación: " + exception.getMessage(), 2);
		}
	}
	public void show()
	{
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Configuración Comedor");
		stage.setResizable(false);
		stage.show();
		LoadControls();
	}
	@FXML protected void btnCancelar_OnAction()
	{
		stage.close();
	}
	@FXML protected void btnAceptar_OnAction()
	{
		final MessageController Mensaje = new MessageController(this.stage);
		if(ValidaInformacion())
		{
			final ProgressController progress = new ProgressController(stage);
			final Task<TaskResponse> task = new Task<TaskResponse>(){
				@Override public TaskResponse call() throws InterruptedException{
					TaskResponse Response = new TaskResponse();
					AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
					Configuracion CostoEmpresa = new Configuracion();
					CostoEmpresa.setConfiguracionId(1);
					CostoEmpresa.setValue(txtCostoEmpresa.getText());
					Configuracion CostoRetencion = new Configuracion();
					CostoRetencion.setConfiguracionId(2);
					CostoRetencion.setValue(txtCostoRetencion.getText());
					try 
					{
						if(Servicio.actualizarConfiguracion(CostoEmpresa))
						{
							Response.setMensaje("Actualización Exitosa");
							Response.setTipoMensaje(1);
							if(!Servicio.actualizarConfiguracion(CostoRetencion))
							{
								Response.setMensaje("No se pudo actualizar el costo de retención");
								Response.setTipoMensaje(2);
							}
						}
						else
						{
							Response.setMensaje("No se pudo efectuar la actualización de las configuraciones");
							Response.setTipoMensaje(2);
						}
					}
					catch (Exception e) 
					{
						Response.setMensaje("Error de comunicación con el servicio: " + e.getMessage());
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
				}
			});
			progress.showProgess(task);
			new Thread(task).start();
		}
		else
		{
			Mensaje.showMessage("La información agregada no es válida", 2);
		}
	}
	
	@FXML Button btnAceptar = new Button();
	@FXML Button btnCancelar = new Button();
	@FXML TextField txtCostoEmpresa = new TextField();
	@FXML TextField txtCostoRetencion = new TextField();
	private boolean ValidaInformacion()
	{
		boolean Valido = true;
		if(txtCostoEmpresa.getText().isEmpty())
		{
			Valido = false;
			txtCostoEmpresa.setStyle("-fx-border-color: red");
		}
		if(!txtCostoEmpresa.getText().matches("[0-9]+(.[0-9][0-9]?)?"))
		{
			Valido = false;
			txtCostoEmpresa.setStyle("-fx-border-color: red");
		}
		if(txtCostoRetencion.getText().isEmpty())
		{
			Valido = false;
			txtCostoRetencion.setStyle("-fx-border-color: red");
		}
		if(!txtCostoRetencion.getText().matches("[0-9]+(.[0-9][0-9]?)?"))
		{
			Valido = false;
			txtCostoRetencion.setStyle("-fx-border-color: red");
		}
		return Valido;
	}
	private void LoadControls()
	{
		txtCostoEmpresa.setOnKeyTyped(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent arg0) {
				txtCostoEmpresa.setStyle("-fx-border-width: 0");			
			}
		});
		txtCostoRetencion.setOnKeyTyped(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent arg0) {
				txtCostoRetencion.setStyle("-fx-border-width: 0");			
			}
		});
		final MessageController Mensaje = new MessageController(stage);
		final ProgressController progress = new ProgressController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>(){
			@Override public TaskResponse call() throws InterruptedException{
				TaskResponse Response = new TaskResponse();
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				
				try 
				{
					Configuracion [] Configuraciones = Servicio.obtenerConfiguraciones();
					if(Configuraciones.length > 0)
					{
						txtCostoEmpresa.setText(Configuraciones[0].getValue());
						txtCostoRetencion.setText(Configuraciones[1].getValue());
					}
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
