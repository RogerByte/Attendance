package presentation.usuarios;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Usuario;
import org.tempuri.AttendanceServiceProxy;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;

public class EditarUsuario extends AnchorPane
{	
	public EditarUsuario() 
	{
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/usuarios/EditarUsuario.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try
		{
			fxmlLoader.load();
		}
		catch(Exception exc)
		{
			throw new RuntimeException(exc);
		}
		TipoConfiguracion = 0; //Refiere a un Alta
		user = new Usuario();
		LoadControls();
	}
	public void setVentanaPadre(UsuariosAdmin ParentWindow){
		this.ParentWindow = ParentWindow;
	}
	private UsuariosAdmin ParentWindow;
	public EditarUsuario(Usuario user)
	{
		this();
		setUser(user);
		TipoConfiguracion = 1; //Refiere a un cambio
		LoadControls();
	}
	private int TipoConfiguracion;
	private Usuario user;
	public Usuario getUser(){
		return this.user;
	}
	public void setUser(Usuario user){
		this.user = user;
	}
	private void LoadControls() {
		txtNombre.textProperty().addListener(new ChangeListener<String>() {
			@Override
			public void changed(ObservableValue<? extends String> observable, String oldValue, String newValue) {
				txtNombre.setStyle("-fx-border-width: 0");
			}
		});
		txtUsuario.textProperty().addListener(new ChangeListener<String>(){

			@Override
			public void changed(ObservableValue<? extends String> arg0, String arg1, String arg2) {
				txtUsuario.setStyle("-fx-border-width: 0");				
			}
		});
		txtPassword.textProperty().addListener(new ChangeListener<String>() {

			@Override
			public void changed(ObservableValue<? extends String> arg0, String arg1, String arg2) {
				txtPassword.setStyle("-fx-border-width: 0");
			}
		});
		if(TipoConfiguracion == 1) {
			txtNombre.setText(getUser().getNombre());
			txtUsuario.setText(getUser().getNombreUsuario());
			txtPassword.setText(getUser().getPassword());
		}
	}
	private final Stage stage = new Stage();
	@FXML TextField txtNombre = new TextField();
	@FXML TextField txtUsuario = new TextField();
	@FXML PasswordField txtPassword = new PasswordField();
	@FXML Button btnAceptar = new Button();
	@FXML Button btnCancelar = new Button();
	@FXML protected void btnAceptar_OnAction(){
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>() {
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				try
				{
					Response = GuardaUsuario(getUser());
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
					ParentWindow.FillGrid("");
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
				else
				{
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
					ParentWindow.FillGrid("");
					stage.close();
				}
			}
		});
		progress.showProgess(task);
		new Thread(task).start();
	}
	@FXML protected void btnCancelar_OnAction(){
		stage.close();
	}
	public void show()
	{
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Configuración de usuarios administrativos");
		stage.setResizable(false);
		stage.show();
	}
	private boolean ValidaForma()
	{
		boolean Valido = true;
		try
		{
			if(txtNombre.getText().isEmpty())
			{
				txtNombre.setStyle("-fx-border-color: red");
				Valido = false;
			} 
			if(txtUsuario.getText().isEmpty())
			{
				txtUsuario.setStyle("-fx-border-color: red");
				Valido = false;
			} 
			if(txtPassword.getText().isEmpty())
			{
				txtPassword.setStyle("-fx-border-color: red");
				Valido = false;
			}
		}
		catch(Exception exc)
		{
			Valido = false;
		}
		return Valido;
	}
	private TaskResponse GuardaUsuario(Usuario Entidad){
		TaskResponse Response = new TaskResponse();
		try	{
			if(ValidaForma()){
				Entidad.setNombre(txtNombre.getText());
				Entidad.setNombreUsuario(txtUsuario.getText());
				Entidad.setPassword(txtPassword.getText());
				AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				switch(TipoConfiguracion)
				{
					case 0: //Alta de usuario
						if(Servicio.altaUsuario(Entidad))
						{
							Response.setTipoMensaje(1);
							Response.setMensaje("¡Alta de usuario correcta!");
						}
						else
						{
							Response.setTipoMensaje(2);
							Response.setMensaje("Ocurrió un error durante el alta de usuario...");
						}
						break;
					case 1: //Actualización de usuario
						if(Servicio.cambioUsuario(Entidad))
						{
							Response.setTipoMensaje(1);
							Response.setMensaje("¡Usuario Actualizado correctamente!");
						}
						else
						{
							Response.setTipoMensaje(2);
							Response.setMensaje("Ocurrió un error durante la actualización del usuario...");
						}
						break;
					default:
						Response.setTipoMensaje(2);
						Response.setMensaje("Error de integridad de la aplicación");
						break;
				}
			}
			else
			{
				Response.setTipoMensaje(2);
				Response.setMensaje("Favor de llenar todos los campos");
			}
		}
		catch(Exception exc){
			Response.setTipoMensaje(2);
			Response.setMensaje("Excepción encontrada: " + exc.getMessage());
		}
		return Response;
	}
}
