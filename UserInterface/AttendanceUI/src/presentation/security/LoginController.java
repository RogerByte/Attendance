package presentation.security;

import java.io.IOException;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.*;
import presentation.Attendance;
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
import javafx.application.Platform;
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
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import RequestContracts.AttendanceService.UsuarioRequest;
import ResponseContracts.AttendanceService.UsuarioResponse;

import org.tempuri.*;

public class LoginController extends AnchorPane
{
	@FXML private TextField txtUsuario;
	@FXML private PasswordField txtPassword;
	@FXML private Button btnCancelar;
	@FXML private Button btnIngresar;
	private Stage stage;
	private Usuario usuario;
	public Usuario getUsuario(){
		return usuario;
	}
	public void setUsuario(Usuario usuario){
		this.usuario = usuario;
	}
	public LoginController(Stage stage)
	{
		this.stage = stage;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/security/Login.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try
		{
			fxmlLoader.load();
		}
		catch (IOException exception)
		{
			throw new RuntimeException(exception);
		}
	}
	public void show()
	{
		Scene scene = new Scene(this);
		stage.centerOnScreen();
		stage.initStyle(StageStyle.UNDECORATED);
        stage.setResizable(false);
        stage.setTitle("Attendance");
        stage.setScene(scene);
		stage.show();
	}
	public void close()
	{
		stage.close();
	}
	@FXML protected void txtPassword_OnAction()
	{
		Logeo();
	}
	@FXML protected void btnCancelar_onClick()
	{
		Platform.exit();
	}
	@FXML protected void btnIngresar_onClick()
	{
		Logeo();
	}
	private void Logeo()
	{
		if(ValidateWindow())
		{
			final ProgressController progress = new ProgressController(stage);
			final Task<TaskResponse> task = new Task<TaskResponse>(){
			    @Override public TaskResponse call() throws InterruptedException{
			    	TaskResponse response = new TaskResponse();
					try{
						btnIngresar.setDisable(true);
						btnCancelar.setDisable(true);
						setUsuario(ValidaUsuario(txtUsuario.getText(), txtPassword.getText()));
						if (getUsuario().getIdUsuario() == null)
							throw new Exception("Error de comunicación con el servicio Attendance");
						if(getUsuario().getIdUsuario() > 0){
							response.setTipoMensaje(1);
						}
						else{
							response.setMensaje("El usuario o contraseña son incorrectos favor de verificar");
							response.setTipoMensaje(2);
						}
					}
					catch(Exception exc){
						response.setMensaje(exc.getMessage());
						response.setTipoMensaje(2);
					}
					finally{
						updateProgress(100, 100);
					}
					return response;
			    }
			};
			final MessageController mensaje = new MessageController(stage);
			task.setOnSucceeded(new EventHandler<WorkerStateEvent>(){
				@Override
				public void handle(WorkerStateEvent event){
					progress.closeProgress();
					TaskResponse response = new TaskResponse();
					response = (TaskResponse)task.getValue();
					if(response.getTipoMensaje() == 2){
						mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
					}
					if(response.getTipoMensaje() == 1){
						Attendance main = new Attendance(stage, usuario);
						close();
						main.show();
					}
					btnIngresar.setDisable(false);
					btnCancelar.setDisable(false);
				}
			});
			task.setOnFailed(new EventHandler<WorkerStateEvent>(){
				@Override
				public void handle(WorkerStateEvent event){
					progress.closeProgress();
					TaskResponse response = new TaskResponse();
					response = (TaskResponse)task.getValue();
					if(response.getTipoMensaje() == 2){
						mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
					}
					if(response.getTipoMensaje() == 1){
						Attendance main = new Attendance(stage, usuario);
						close();
						main.show();
					}
					btnCancelar.setDisable(false);
					btnCancelar.setDisable(false);
				}
			});
			progress.showProgess(task);
			new Thread(task).start();
		}
	}
	private boolean ValidateWindow()
	{
		MessageController mensaje = new MessageController(stage);
		if(txtUsuario.getText() == null || txtUsuario.getText().isEmpty())
		{
			mensaje.showMessage("Favor de ingresar un usuario válido", 2);
			return false;
		}
		if(txtPassword.getText() == null || txtPassword.getText().isEmpty())
		{
			mensaje.showMessage("Favor de ingresar una contraseña correcta", 2);
			return false;
		}
		else
		{
			return true;
		}
	}
	private Usuario ValidaUsuario(String Usuario, String Password){
		Usuario Response = new Usuario();
		try{
			AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
			RequestContracts.AttendanceService.UsuarioRequest Request = new UsuarioRequest();
			Request.setNombreUsuario(Usuario);
			Request.setPassword(Password);
			UsuarioResponse ServiceResponse = Servicio.validaUsuario(Request);
			Response = ServiceResponse.getUsuario();
		}
		catch(Exception exc){
			System.out.println(exc.getMessage());
		}
		return Response;
	}
	
}
