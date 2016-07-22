package presentation.empleados;

import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.*;

import org.controlsfx.control.textfield.TextFields;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Empleado;
import org.tempuri.AttendanceServiceProxy;

import ResponseContracts.AttendanceService.ServiceMessage;

import java.io.IOException;
import java.rmi.RemoteException;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.*;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.ComboBox;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class AltaEmpleado extends AnchorPane{
	private final Stage stage = new Stage();
	private int Usuario;
	public void setUsuario(int Value){
		this.Usuario = Value;
	}
	public int getUsuario(){
		return Usuario;
	}
	private Empleado empleado;
	public void setEmpleado(Empleado value){
		this.empleado = value;
	}
	public Empleado getEmpleado(){
		return empleado;
	}
	Empleados VentanaPadre;
	public void setVentanaPadre(Empleados MainWindow){
		this.VentanaPadre = MainWindow;
	}
	private boolean AltaEmpleado;
	public void setAltaEmpleado(boolean value){
		this.AltaEmpleado = value;
	}
	public boolean isAltaEmpleado(){
		return AltaEmpleado;
	}
	public AltaEmpleado(boolean AltaEmpleado){
		this.AltaEmpleado = AltaEmpleado;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/empleados/AltaEmpleado.fxml"));
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
	public AltaEmpleado(Empleado empleado){
		this(false);
		CargaControles(empleado);
		setEmpleado(empleado);
	}
	public void show(){
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Alta de Empleado");
		stage.setResizable(true);
		stage.show();
	}
	private void CargaControles(Empleado empleado){
		txtNumTarjeta.setText(empleado.getNumeroTarjeta());
		Catalogo emp = new Catalogo(empleado.getIEmpleadoId(), empleado.getNombreEmpleado());
		cmbEmpleados.getSelectionModel().select(emp);
		cmbEmpleados.setDisable(true);
		for(Catalogo obj : cmbHorario.getItems()){
			if(obj.id.equals(empleado.getHorarioId())){
				cmbHorario.getSelectionModel().select(obj);
				break;
			}
		}
		for(Catalogo obj : cmbManagers.getItems()){
			if(obj.id.equals(empleado.getManagerId())){
				cmbManagers.getSelectionModel().select(obj);
				break;
			}
		}
		chkIsManager.setSelected(empleado.getEsManager());
		txtNombreUsuario.setText(empleado.getNombreUsuario());
		txtPassword.setText(empleado.getPasswordAttendance());
		txtCorreo.setText(empleado.getCorreoElectronico());
	}
	private void InitializeComponents(){
		MessageController Mensaje = new MessageController(stage);
		
		AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
		//Cargar el catalogo de Empleados:
		try {
			ObservableList<Catalogo> ListaHorarios = FXCollections.observableArrayList();
			org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Horarios  = Servicio.obtenerCatalogoHorarios();
			if(Horarios.length > 0){
				for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo Horario : Horarios){
					Catalogo catalogo = new Catalogo(Horario.getId(), Horario.getDescripcion());
					ListaHorarios.add(catalogo);
				}
			}
			cmbHorario.getItems().addAll(ListaHorarios);
			
			ObservableList<Catalogo> ListaEmpleados = FXCollections.observableArrayList();
			org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Empleados  = Servicio.obtenerCatalogoEmpleados();
			if(Horarios.length > 0){
				for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo Empleado : Empleados){
					Catalogo catalogo = new Catalogo(Empleado.getId(), Empleado.getDescripcion());
					ListaEmpleados.add(catalogo);
				}
			}
			cmbEmpleados.getItems().addAll(ListaEmpleados);
			
			ObservableList<Catalogo> ListaManagers = FXCollections.observableArrayList();
			org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Managers  = Servicio.catalogoManagers();
			if(Horarios.length > 0){
				for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo Manager : Managers){
					Catalogo catalogo = new Catalogo(Manager.getId(), Manager.getDescripcion());
					ListaManagers.add(catalogo);
				}
			}
			cmbManagers.getItems().addAll(ListaManagers);
			
			cmbEmpleados.setEditable(true);
			TextFields.bindAutoCompletion(cmbEmpleados.getEditor(), cmbEmpleados.getItems());
			cmbManagers.setEditable(true);
			TextFields.bindAutoCompletion(cmbManagers.getEditor(), cmbManagers.getItems());
			
			cmbEmpleados.setOnMouseClicked(new EventHandler<MouseEvent>() {
				@Override
				public void handle(MouseEvent arg0) {
					cmbEmpleados.setStyle("-fx-border-width: 0");
				}
			});
			txtNumTarjeta.setOnKeyPressed(new EventHandler<KeyEvent>() {
				@Override
				public void handle(KeyEvent arg0) {
					txtNumTarjeta.setStyle("-fx-border-width: 0");
				}
			});
			cmbHorario.setOnMouseClicked(new EventHandler<MouseEvent>() {
				@Override
				public void handle(MouseEvent arg0) {
					cmbHorario.setStyle("-fx-border-width: 0");
				}
			});
			txtNombreUsuario.setOnKeyPressed(new EventHandler<KeyEvent>() {
				@Override
				public void handle(KeyEvent arg0) {
					txtNombreUsuario.setStyle("-fx-border-width: 0");
				}
			});
			txtPassword.setOnKeyPressed(new EventHandler<KeyEvent>() {
				@Override
				public void handle(KeyEvent arg0) {
					txtPassword.setStyle("-fx-border-width: 0");
				}
			});
			txtCorreo.setOnKeyPressed(new EventHandler<KeyEvent>() {
				@Override
				public void handle(KeyEvent arg0) {
					txtCorreo.setStyle("-fx-border-width: 0");
				}
			});
		} catch (RemoteException e) {
			Mensaje.showMessage(e.getMessage(), 2);
		}
	}
	private boolean ValidaContenido(){
		boolean Response = true;
		MessageController Mensaje = new MessageController(stage);
		try{
			if(cmbEmpleados.getSelectionModel().selectedItemProperty().getValue() == null){
				Response = false;
				cmbEmpleados.setStyle("-fx-border-color: red");
			}
			if(txtNumTarjeta.getText().isEmpty()){
				Response = false;
				txtNumTarjeta.setStyle("-fx-border-color: red");
			}
			if(cmbHorario.getSelectionModel().selectedItemProperty().getValue() == null){
				Response = false;
				cmbHorario.setStyle("-fx-border-color: red");
			}
			if(txtNombreUsuario.getText().isEmpty()){
				Response = false;
				txtNombreUsuario.setStyle("-fx-border-color: red");
			}
			if(txtPassword.getText().isEmpty()){
				Response = false;
				txtPassword.setStyle("-fx-border-color: red");
			}
			if(txtCorreo.getText().isEmpty()){
				Response = false;
				txtCorreo.setStyle("-fx-border-color: red");
			}
			if(!Response){
				Mensaje.showMessage("Favor de llenar los campos", 2);
			}
		}catch(Exception exc){
			Mensaje.showMessage(exc.getMessage(), 2);
		}
		return Response;
	}
	private Empleado ObtenEntidad()
	{
		Empleado empleado = new Empleado();
		if(!AltaEmpleado){
			empleado = getEmpleado();
		}
		Catalogo entidad = cmbEmpleados.getItems().get(cmbEmpleados.getSelectionModel().getSelectedIndex());
		empleado.setIEmpleadoId(entidad.id);
		empleado.setNombreEmpleado(entidad.displayString);
		empleado.setNumeroTarjeta(txtNumTarjeta.getText());
		entidad = cmbHorario.getItems().get(cmbHorario.getSelectionModel().getSelectedIndex());
		empleado.setHorarioId(entidad.id);
		empleado.setNombreUsuario(txtNombreUsuario.getText());
		empleado.setPasswordAttendance(txtPassword.getText());
		empleado.setCorreoElectronico(txtCorreo.getText());
		Catalogo manager = cmbManagers.getItems().get(cmbManagers.getSelectionModel().getSelectedIndex());
		empleado.setManagerId(manager.id);
		empleado.setEsManager(chkIsManager.isSelected());
		return empleado;
	}
	@FXML ComboBox<Catalogo> cmbEmpleados = new ComboBox<Catalogo>();
	@FXML TextField txtNumTarjeta = new TextField();
	@FXML ChoiceBox<Catalogo> cmbHorario = new ChoiceBox<Catalogo>();
	@FXML CheckBox chkIsManager = new CheckBox();
	@FXML ComboBox<Catalogo> cmbManagers = new ComboBox<Catalogo>();
	@FXML TextField txtNombreUsuario = new TextField();
	@FXML PasswordField txtPassword = new PasswordField();
	@FXML TextField txtCorreo = new TextField();
	@FXML Button btnCancelar = new Button();
	@FXML protected void btnCancelar_OnAction(){
		stage.close();
	}
	
	@FXML Button btnAceptar = new Button();
	@FXML protected void btnAceptar_OnAction(){
		if(ValidaContenido()){
			final Task<TaskResponse> task = new Task<TaskResponse>(){
				@Override public TaskResponse call() throws InterruptedException{
			    	TaskResponse Response = new TaskResponse();
			    	Empleado request = new Empleado();
			    	request = ObtenEntidad();
					AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
					if(AltaEmpleado){
						try {
							ServiceMessage ServiceResponse = Servicio.altaEmpleado(request);
							if(!ServiceResponse.getError()){
								Response.setMensaje("Empleado registrado correctamente");
								Response.setTipoMensaje(1);
							}
							else{
								Response.setMensaje("Hubo un problema al dar de alta el empleado, " + ServiceResponse.getMensajeRespuesta());
								Response.setTipoMensaje(2);
							}
						} catch (RemoteException e) {
							Response.setMensaje("Error en la comunicación con el servicio: " + e.getMessage());
							Response.setTipoMensaje(2);
						}
					}
					else
					{
						try {
							if(Servicio.actualizaEmpleado(request)){
								Response.setMensaje("Empleado actualizado correctamente");
								Response.setTipoMensaje(1);
							}
							else{
								Response.setMensaje("Hubo un problema al actualizar al empleado");
								Response.setTipoMensaje(2);
							}
						} catch (RemoteException e) {
							Response.setMensaje("Error en la comunicación con el servicio: " + e.getMessage());
							Response.setTipoMensaje(2);
						}
					}
			        updateProgress(10, 10);
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
						VentanaPadre.FillGridEmpleados("");
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
						VentanaPadre.FillGridEmpleados("");
						stage.close();
					}
				}
			});
			progress.showProgess(task);
			new Thread(task).start();
		}
	}
}
