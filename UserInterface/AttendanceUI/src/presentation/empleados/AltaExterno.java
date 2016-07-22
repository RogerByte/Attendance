package presentation.empleados;

import java.io.IOException;
import java.rmi.RemoteException;
import java.util.ArrayList;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Empleado;
import org.tempuri.AttendanceServiceProxy;

import ResponseContracts.AttendanceService.ServiceMessage;
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.Catalogo;
import presentation.common.entities.TaskResponse;

public class AltaExterno extends AnchorPane{
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
	public AltaExterno(boolean AltaEmpleado){
		this.AltaEmpleado = AltaEmpleado;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/empleados/AltaExterno.fxml"));
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
	public AltaExterno(Empleado empleado){
		this(false);
		CargaControles(empleado);
		setEmpleado(empleado);
	}
	public void show(){
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Alta de Empleado Externo");
		stage.setResizable(false);
		stage.show();
	}
	private void CargaControles(Empleado empleado){
		txtNumTarjeta.setText(empleado.getNumeroTarjeta());
		txtNombreEmpleado.setText(empleado.getNombreEmpleado());
		txtNombreEmpleado.setDisable(true);
		for(Catalogo obj : cmbHorario.getItems()){
			if(obj.id.equals(empleado.getHorarioId())){
				cmbHorario.getSelectionModel().select(obj);
				break;
			}
		}
	}
	private void InitializeComponents(){
		MessageController Mensaje = new MessageController(stage);
		ArrayList<Catalogo> CatalogoHorarios = new ArrayList<Catalogo>();
		ArrayList<Catalogo> CatalogoEmpleados = new ArrayList<Catalogo>();
		AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
		//Cargar el catalogo de Empleados:
		try {
			org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Horarios  = Servicio.obtenerCatalogoHorarios();
			if(Horarios.length > 0){
				for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo Horario : Horarios){
					Catalogo catalogo = new Catalogo(Horario.getId(), Horario.getDescripcion());
					CatalogoHorarios.add(catalogo);
				}
			}
			ObservableList<Catalogo> ListaHorarios = FXCollections.observableArrayList(CatalogoHorarios);
			cmbHorario.getItems().addAll(ListaHorarios);
			
			org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo [] Empleados  = Servicio.obtenerCatalogoEmpleados();
			if(Horarios.length > 0){
				for(org.datacontract.schemas._2004._07.AttendanceCore_Entities.Catalogo Empleado : Empleados){
					Catalogo catalogo = new Catalogo(Empleado.getId(), Empleado.getDescripcion());
					CatalogoEmpleados.add(catalogo);
				}
			}
			txtNombreEmpleado.setOnMouseClicked(new EventHandler<MouseEvent>() {
				@Override
				public void handle(MouseEvent arg0) {
					txtNombreEmpleado.setStyle("-fx-border-width: 0");
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
		} catch (RemoteException e) {
			Mensaje.showMessage(e.getMessage(), 2);
		}
	}
	private boolean ValidaContenido(){
		boolean Response = true;
		MessageController Mensaje = new MessageController(stage);
		try{
			if(txtNombreEmpleado.getText().isEmpty()){
				Response = false;
				txtNombreEmpleado.setStyle("-fx-border-color: red");
			}
			if(txtNumTarjeta.getText().isEmpty()){
				Response = false;
				txtNumTarjeta.setStyle("-fx-border-color: red");
			}
			if(cmbHorario.getSelectionModel().selectedItemProperty().getValue() == null){
				Response = false;
				cmbHorario.setStyle("-fx-border-color: red");
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
		empleado.setNombreEmpleado(txtNombreEmpleado.getText());
		empleado.setNumeroTarjeta(txtNumTarjeta.getText());
		empleado.setExterno(true);
		Catalogo entidad = cmbHorario.getItems().get(cmbHorario.getSelectionModel().getSelectedIndex());
		empleado.setHorarioId(entidad.id);
		return empleado;
	}
	@FXML TextField txtNombreEmpleado = new TextField();
	@FXML TextField txtNumTarjeta = new TextField();
	@FXML ChoiceBox<Catalogo> cmbHorario = new ChoiceBox<Catalogo>();
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
							ServiceMessage ServiceResponse = Servicio.altaEmpleadoExterno(request);
							if(!ServiceResponse.getError()){
								Response.setMensaje("Emmpleado registrado correctamente");
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
			final MessageController Mensaje = 
					new MessageController(stage);
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
