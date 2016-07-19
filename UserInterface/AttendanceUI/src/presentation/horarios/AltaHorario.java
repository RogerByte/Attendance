package presentation.horarios;

import java.io.IOException;
import java.rmi.RemoteException;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Horario;
import org.tempuri.AttendanceServiceProxy;

import RequestContracts.AttendanceService.AltaHorarioRequest;
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
import javafx.scene.control.CheckBox;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class AltaHorario extends AnchorPane{
	private final Stage stage = new Stage();
	private int Usuario;
	public int getUsuario(){
		return Usuario;
	}
	public void setUsuario(int Usuario){
		this.Usuario = Usuario;
	}
	private int HorarioId;
	public void setHorarioId(int value){
		HorarioId = value;
	}
	public int getHorarioId(){
		return HorarioId;
	}
	public AltaHorario(){
		HorarioId = 0;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/horarios/AltaHorario.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			InicializaControles();
		}
		catch (IOException exception){
			throw new RuntimeException(exception);
		}
	}
	public void show(){
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Alta de Horario");
		stage.setResizable(false);
		stage.show();
	}
	private void ObtenHorario(String HorarioRequest, TextField HoraEntrada, TextField MinutoEntrada, TextField HoraSalida, TextField MinutoSalida){
		String [] horario = HorarioRequest.split("-");
		String HorarioEntrada = horario[0].trim();
		String HorarioSalida = horario[1].trim();
		HoraEntrada.setText(HorarioEntrada.split(":")[0]);
		MinutoEntrada.setText(HorarioEntrada.split(":")[1]);
		HoraSalida.setText(HorarioSalida.split(":")[0]);
		MinutoSalida.setText(HorarioSalida.split(":")[1]);
		HoraEntrada.setDisable(false);
		MinutoEntrada.setDisable(false);
		HoraSalida.setDisable(false);
		MinutoSalida.setDisable(false);
	}
	@FXML Label Titulo = new Label();
	public void CargarHorario(Horario horario){
		Titulo.setText("Editar Horario");
		setHorarioId(horario.getHorarioId());
		txtDescripcion.setText(horario.getDescripcionHorario());
		if(horario.getLunes()){
			chkLunes.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioLunes(), LunesHoraEnt, LunesMinutoEnt, LunesHoraSal, LunesMinutoSal);
		}
		if(horario.getMartes()){
			chkMartes.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioMartes(), MartesHoraEnt, MartesMinutoEnt, MartesHoraSal, MartesMinutoSal);
		}
		if(horario.getMiercoles()){
			chkMiercoles.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioMiercoles(), MiercolesHoraEnt, MiercolesMinutoEnt, MiercolesHoraSal, MiercolesMinutoSal);
		}
		if(horario.getJueves()){
			chkJueves.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioJueves(), JuevesHoraEnt, JuevesMinutoEnt, JuevesHoraSal, JuevesMinutoSal);
		}
		if(horario.getViernes()){
			chkViernes.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioViernes(), ViernesHoraEnt, ViernesMinutoEnt, ViernesHoraSal, ViernesMinutoSal);
		}
		if(horario.getSabado()){
			chkSabado.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioSabado(), SabadoHoraEnt, SabadoMinutoEnt, SabadoHoraSal, SabadoMinutoSal);
		}
		if(horario.getDomingo()){
			chkDomingo.selectedProperty().setValue(true);
			ObtenHorario(horario.getHorarioDomingo(), DomingoHoraEnt, DomingoMinutoEnt, DomingoHoraSal, DomingoMinutoSal);
		}
	}
	private void CompletaDosDigitos(TextField Elemento){
		if(Integer.parseInt(Elemento.getText()) < 10 && Elemento.getText().length() == 1){
			Elemento.setText("0" + Elemento.getText());
		}
	}
	private final Horario ObtenerModelo(){
		Horario Modelo = new Horario();
		try {
			Modelo.setDescripcionHorario(txtDescripcion.getText());
			Modelo.setLunes(chkLunes.selectedProperty().getValue());
			Modelo.setMartes(chkMartes.selectedProperty().getValue());
			Modelo.setMiercoles(chkMiercoles.selectedProperty().getValue());
			Modelo.setJueves(chkJueves.selectedProperty().getValue());
			Modelo.setViernes(chkViernes.selectedProperty().getValue());
			Modelo.setSabado(chkSabado.selectedProperty().getValue());
			Modelo.setDomingo(chkDomingo.selectedProperty().getValue());
			
			if(Modelo.getLunes()){
				Modelo.setHorarioLunes(LunesHoraEnt.getText() + ":" + LunesMinutoEnt.getText() + " - " + LunesHoraSal.getText() + ":" + LunesMinutoSal.getText());
			}
			if(Modelo.getMartes()){
				Modelo.setHorarioMartes(MartesHoraEnt.getText() + ":" + MartesMinutoEnt.getText() + " - " + MartesHoraSal.getText() + ":" + MartesMinutoSal.getText());
			}
			if(Modelo.getMiercoles()){
				Modelo.setHorarioMiercoles(MiercolesHoraEnt.getText() + ":" + MiercolesMinutoEnt.getText() + " - " + MiercolesHoraSal.getText() + ":" + MiercolesMinutoSal.getText());
			}
			if(Modelo.getJueves()){
				Modelo.setHorarioJueves(JuevesHoraEnt.getText() + ":" + JuevesMinutoEnt.getText() + " - " + JuevesHoraSal.getText() + ":" + JuevesMinutoSal.getText());
			}
			if(Modelo.getViernes()){
				Modelo.setHorarioViernes(ViernesHoraEnt.getText() + ":" + ViernesMinutoEnt.getText() + " - " + ViernesHoraSal.getText() + ":" + ViernesMinutoSal.getText());
			}
			if(Modelo.getSabado()){
				Modelo.setHorarioSabado(SabadoHoraEnt.getText() + ":" + SabadoMinutoEnt.getText() + " - " + SabadoHoraSal.getText() + ":" + SabadoMinutoSal.getText());
			}
			if(Modelo.getDomingo()){
				Modelo.setHorarioDomingo(DomingoHoraEnt.getText() + ":" + DomingoMinutoEnt.getText() + " - " + DomingoHoraSal.getText() + ":" + DomingoMinutoSal.getText());
			}
			Modelo.setHorarioId(getHorarioId());
		}
		catch(Exception exc){
			System.out.println("Error en la aplicación, al generar la entidad horario: " + exc.getMessage());
		}
		return Modelo;
	}
	private boolean IntegridadEntradaSalida(TextField HoraEntrada, TextField MinutoEntrada, TextField HoraSalida, TextField MinutoSalida){
		boolean IntegridadCorrecta = false;
		try {
            DateFormat dateFormat = new SimpleDateFormat("HH:mm");
            Date horaIni;
            Date horaFin;
            horaIni = dateFormat.parse(HoraEntrada.getText() + ":" + MinutoEntrada.getText());
            horaFin = dateFormat.parse(HoraSalida.getText() + ":" + MinutoSalida.getText());
            if (horaIni.compareTo(horaFin) < 0) {
            	IntegridadCorrecta = true;
            }
            else{
            	IntegridadCorrecta = false;
            	HoraEntrada.setStyle("-fx-border-color: red");
            	MinutoEntrada.setStyle("-fx-border-color: red");
            	HoraSalida.setStyle("-fx-border-color: red");
            	MinutoSalida.setStyle("-fx-border-color: red");
            }
        } catch (ParseException ex) {
            System.out.println(ex.getMessage());
        }
		return IntegridadCorrecta;
	}
	private boolean ValidaContenido(){
		boolean Response = true;
		MessageController Mensaje = new MessageController(stage);
		try
		{
			if(txtDescripcion.getText().isEmpty()){
				txtDescripcion.setStyle("-fx-border-color: red");
				Response = false;
			}
			if(chkLunes.selectedProperty().getValue()){
				if(LunesHoraEnt.getText().isEmpty()){
					LunesHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(LunesMinutoEnt.getText().isEmpty()){
					LunesMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(LunesHoraSal.getText().isEmpty()){
					LunesHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(LunesMinutoSal.getText().isEmpty()){
					LunesMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(LunesHoraEnt);
					CompletaDosDigitos(LunesMinutoEnt);
					CompletaDosDigitos(LunesHoraSal);
					CompletaDosDigitos(LunesMinutoSal);
					Response = IntegridadEntradaSalida(LunesHoraEnt, LunesMinutoEnt, LunesHoraSal, LunesMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
			if(chkMartes.selectedProperty().getValue()){
				if(MartesHoraEnt.getText().isEmpty()){
					MartesHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(MartesMinutoEnt.getText().isEmpty()){
					MartesMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(MartesHoraSal.getText().isEmpty()){
					MartesHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(MartesMinutoSal.getText().isEmpty()){
					MartesMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(MartesHoraEnt);
					CompletaDosDigitos(MartesMinutoEnt);
					CompletaDosDigitos(MartesHoraSal);
					CompletaDosDigitos(MartesMinutoSal);
					Response = IntegridadEntradaSalida(MartesHoraEnt, MartesMinutoEnt, MartesHoraSal, MartesMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
			if(chkMiercoles.selectedProperty().getValue()){
				if(MiercolesHoraEnt.getText().isEmpty()){
					MiercolesHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(MiercolesMinutoEnt.getText().isEmpty()){
					MiercolesMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(MiercolesHoraSal.getText().isEmpty()){
					MiercolesHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(MiercolesMinutoSal.getText().isEmpty()){
					MiercolesMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(MiercolesHoraEnt);
					CompletaDosDigitos(MiercolesMinutoEnt);
					CompletaDosDigitos(MiercolesHoraSal);
					CompletaDosDigitos(MiercolesMinutoSal);
					Response = IntegridadEntradaSalida(MiercolesHoraEnt, MiercolesMinutoEnt, MiercolesHoraSal, MiercolesMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
			if(chkJueves.selectedProperty().getValue()){
				if(JuevesHoraEnt.getText().isEmpty()){
					JuevesHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(JuevesMinutoEnt.getText().isEmpty()){
					JuevesMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(JuevesHoraSal.getText().isEmpty()){
					JuevesHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(JuevesMinutoSal.getText().isEmpty()){
					JuevesMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(JuevesHoraEnt);
					CompletaDosDigitos(JuevesMinutoEnt);
					CompletaDosDigitos(JuevesHoraSal);
					CompletaDosDigitos(JuevesMinutoSal);
					Response = IntegridadEntradaSalida(JuevesHoraEnt, JuevesMinutoEnt, JuevesHoraSal, JuevesMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
			if(chkViernes.selectedProperty().getValue()){
				if(ViernesHoraEnt.getText().isEmpty()){
					ViernesHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(ViernesMinutoEnt.getText().isEmpty()){
					ViernesMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(ViernesHoraSal.getText().isEmpty()){
					ViernesHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(ViernesMinutoSal.getText().isEmpty()){
					ViernesMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(ViernesHoraEnt);
					CompletaDosDigitos(ViernesMinutoEnt);
					CompletaDosDigitos(ViernesHoraSal);
					CompletaDosDigitos(ViernesMinutoSal);
					Response = IntegridadEntradaSalida(ViernesHoraEnt, ViernesMinutoEnt, ViernesHoraSal, ViernesMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
			if(chkSabado.selectedProperty().getValue()){
				if(SabadoHoraEnt.getText().isEmpty()){
					SabadoHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(SabadoMinutoEnt.getText().isEmpty()){
					SabadoMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(SabadoHoraSal.getText().isEmpty()){
					SabadoHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(SabadoMinutoSal.getText().isEmpty()){
					SabadoMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(SabadoHoraEnt);
					CompletaDosDigitos(SabadoMinutoEnt);
					CompletaDosDigitos(SabadoHoraSal);
					CompletaDosDigitos(SabadoMinutoSal);
					Response = IntegridadEntradaSalida(SabadoHoraEnt, SabadoMinutoEnt, SabadoHoraSal, SabadoMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
			if(chkDomingo.selectedProperty().getValue()){
				if(DomingoHoraEnt.getText().isEmpty()){
					DomingoHoraEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(DomingoMinutoEnt.getText().isEmpty()){
					DomingoMinutoEnt.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(DomingoHoraSal.getText().isEmpty()){
					DomingoHoraSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(DomingoMinutoSal.getText().isEmpty()){
					DomingoMinutoSal.setStyle("-fx-border-color: red");
					Response = false;
				}
				if(Response){
					CompletaDosDigitos(DomingoHoraEnt);
					CompletaDosDigitos(DomingoMinutoEnt);
					CompletaDosDigitos(DomingoHoraSal);
					CompletaDosDigitos(DomingoMinutoSal);
					Response = IntegridadEntradaSalida(DomingoHoraEnt, DomingoMinutoEnt, DomingoHoraSal, DomingoMinutoSal);
					if(!Response)
						Mensaje.showMessage("Revisar congruencia de horarios, recuerde que el formato es sobre 24 horas", 2);
				}
				else{
					Mensaje.showMessage("Favor de llenar todos los campos", 2);
				}
			}
		}
		catch(Exception exc)
		{
			Mensaje.showMessage(exc.getMessage(), 2);
		}
		return Response;
	}
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
					Horario horario = ObtenerModelo();
					AltaHorarioRequest Request = new AltaHorarioRequest();
					Request.setHorario(horario);
					Request.setUsuario(getUsuario());
					AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
					if(Request.getHorario().getHorarioId() > 0){
						try {
							if(Servicio.actualizaHorario(Request)){
								Response.setMensaje("Horario actualizado exitosamente");
								Response.setTipoMensaje(1);
							}
							else{
								Response.setMensaje("Hubo un problema al actualizar el horario");
								Response.setTipoMensaje(2);
							}
						} catch (RemoteException e) {
							Response.setMensaje("Error en la comunicación con el servicio: " + e.getMessage());
							Response.setTipoMensaje(2);
						}
					}
					else{
						try {
							if(Servicio.insertaHorario(Request)){
								Response.setMensaje("Horario dado de alta exitosamente");
								Response.setTipoMensaje(1);
							}
							else{
								Response.setMensaje("Hubo un problema al dar de alta el horario");
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
						VentanaPadre.FillGridHorarios();
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
						VentanaPadre.FillGridHorarios();
						stage.close();
					}
				}
			});
			progress.showProgess(task);
			new Thread(task).start();
		}
	}
	Horarios VentanaPadre;
	public void setVentanaPadre(Horarios MainWindow){
		this.VentanaPadre = MainWindow;
	}
	@FXML CheckBox chkLunes = new CheckBox();
	@FXML protected void chkLunes_OnAction(){
		if(chkLunes.selectedProperty().getValue()){
			LunesHoraEnt.setDisable(false);
			LunesHoraEnt.setStyle("-fx-border-width: 0");
			LunesMinutoEnt.setDisable(false);
			LunesMinutoEnt.setStyle("-fx-border-width: 0");
			LunesHoraSal.setDisable(false);
			LunesHoraSal.setStyle("-fx-border-width: 0");
			LunesMinutoSal.setDisable(false);
			LunesMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			LunesHoraEnt.setStyle("-fx-border-width: 0");
			LunesMinutoEnt.setStyle("-fx-border-width: 0");
			LunesHoraSal.setStyle("-fx-border-width: 0");
			LunesMinutoSal.setStyle("-fx-border-width: 0");
			LunesHoraEnt.setDisable(true);
			LunesHoraEnt.setText("");
			LunesMinutoEnt.setDisable(true);
			LunesMinutoEnt.setText("");
			LunesHoraSal.setDisable(true);
			LunesHoraSal.setText("");
			LunesMinutoSal.setDisable(true);
			LunesMinutoSal.setText("");
		}
	}
	@FXML CheckBox chkMartes = new CheckBox();
	@FXML protected void chkMartes_OnAction(){
		if(chkMartes.selectedProperty().getValue()){
			MartesHoraEnt.setDisable(false);
			MartesHoraEnt.setStyle("-fx-border-width: 0");
			MartesMinutoEnt.setDisable(false);
			MartesMinutoEnt.setStyle("-fx-border-width: 0");
			MartesHoraSal.setDisable(false);
			MartesHoraSal.setStyle("-fx-border-width: 0");
			MartesMinutoSal.setDisable(false);
			MartesMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			MartesHoraEnt.setStyle("-fx-border-width: 0");
			MartesMinutoEnt.setStyle("-fx-border-width: 0");
			MartesHoraSal.setStyle("-fx-border-width: 0");
			MartesMinutoSal.setStyle("-fx-border-width: 0");
			MartesHoraEnt.setDisable(true);
			MartesHoraEnt.setText("");
			MartesMinutoEnt.setDisable(true);
			MartesMinutoEnt.setText("");
			MartesHoraSal.setDisable(true);
			MartesHoraSal.setText("");
			MartesMinutoSal.setDisable(true);
			MartesMinutoSal.setText("");
		}
		
	}
	@FXML CheckBox chkMiercoles = new CheckBox();
	@FXML protected void chkMiercoles_OnAction(){
		if(chkMiercoles.selectedProperty().getValue()){
			MiercolesHoraEnt.setDisable(false);
			MiercolesHoraEnt.setStyle("-fx-border-width: 0");
			MiercolesMinutoEnt.setDisable(false);
			MiercolesMinutoEnt.setStyle("-fx-border-width: 0");
			MiercolesHoraSal.setDisable(false);
			MiercolesHoraSal.setStyle("-fx-border-width: 0");
			MiercolesMinutoSal.setDisable(false);
			MiercolesMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			MiercolesHoraEnt.setStyle("-fx-border-width: 0");
			MiercolesMinutoEnt.setStyle("-fx-border-width: 0");
			MiercolesHoraSal.setStyle("-fx-border-width: 0");
			MiercolesMinutoSal.setStyle("-fx-border-width: 0");
			MiercolesHoraEnt.setDisable(true);
			MiercolesHoraEnt.setText("");
			MiercolesMinutoEnt.setDisable(true);
			MiercolesMinutoEnt.setText("");
			MiercolesHoraSal.setDisable(true);
			MiercolesHoraSal.setText("");
			MiercolesMinutoSal.setDisable(true);
			MiercolesMinutoSal.setText("");
		}
		
	}
	@FXML CheckBox chkJueves = new CheckBox();
	@FXML protected void chkJueves_OnAction(){
		if(chkJueves.selectedProperty().getValue()){
			JuevesHoraEnt.setDisable(false);
			JuevesHoraEnt.setStyle("-fx-border-width: 0");
			JuevesMinutoEnt.setDisable(false);
			JuevesMinutoEnt.setStyle("-fx-border-width: 0");
			JuevesHoraSal.setDisable(false);
			JuevesHoraSal.setStyle("-fx-border-width: 0");
			JuevesMinutoSal.setDisable(false);
			JuevesMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			JuevesHoraEnt.setStyle("-fx-border-width: 0");
			JuevesMinutoEnt.setStyle("-fx-border-width: 0");
			JuevesHoraSal.setStyle("-fx-border-width: 0");
			JuevesMinutoSal.setStyle("-fx-border-width: 0");
			JuevesHoraEnt.setDisable(true);
			JuevesHoraEnt.setText("");
			JuevesMinutoEnt.setDisable(true);
			JuevesMinutoEnt.setText("");
			JuevesHoraSal.setDisable(true);
			JuevesHoraSal.setText("");
			JuevesMinutoSal.setDisable(true);
			JuevesMinutoSal.setText("");
		}
		
	}
	@FXML CheckBox chkViernes = new CheckBox();
	@FXML protected void chkViernes_OnAction(){
		if(chkViernes.selectedProperty().getValue()){
			ViernesHoraEnt.setDisable(false);
			ViernesHoraEnt.setStyle("-fx-border-width: 0");
			ViernesMinutoEnt.setDisable(false);
			ViernesMinutoEnt.setStyle("-fx-border-width: 0");
			ViernesHoraSal.setDisable(false);
			ViernesHoraSal.setStyle("-fx-border-width: 0");
			ViernesMinutoSal.setDisable(false);
			ViernesMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			ViernesHoraEnt.setStyle("-fx-border-width: 0");
			ViernesMinutoEnt.setStyle("-fx-border-width: 0");
			ViernesHoraSal.setStyle("-fx-border-width: 0");
			ViernesMinutoSal.setStyle("-fx-border-width: 0");
			ViernesHoraEnt.setDisable(true);
			ViernesHoraEnt.setText("");
			ViernesMinutoEnt.setDisable(true);
			ViernesMinutoEnt.setText("");
			ViernesHoraSal.setDisable(true);
			ViernesHoraSal.setText("");
			ViernesMinutoSal.setDisable(true);
			ViernesMinutoSal.setText("");
		}
	}
	@FXML CheckBox chkSabado = new CheckBox();
	@FXML protected void chkSabado_OnAction(){
		if(chkSabado.selectedProperty().getValue()){
			SabadoHoraEnt.setDisable(false);
			SabadoHoraEnt.setStyle("-fx-border-width: 0");
			SabadoMinutoEnt.setDisable(false);
			SabadoMinutoEnt.setStyle("-fx-border-width: 0");
			SabadoHoraSal.setDisable(false);
			SabadoHoraSal.setStyle("-fx-border-width: 0");
			SabadoMinutoSal.setDisable(false);
			SabadoMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			SabadoHoraEnt.setStyle("-fx-border-width: 0");
			SabadoMinutoEnt.setStyle("-fx-border-width: 0");
			SabadoHoraSal.setStyle("-fx-border-width: 0");
			SabadoMinutoSal.setStyle("-fx-border-width: 0");
			SabadoHoraEnt.setDisable(true);
			SabadoHoraEnt.setText("");
			SabadoMinutoEnt.setDisable(true);
			SabadoMinutoEnt.setText("");
			SabadoHoraSal.setDisable(true);
			SabadoHoraSal.setText("");
			SabadoMinutoSal.setDisable(true);
			SabadoMinutoSal.setText("");
		}
	}
	@FXML CheckBox chkDomingo = new CheckBox();
	@FXML protected void chkDomingo_OnAction(){
		if(chkDomingo.selectedProperty().getValue()){
			DomingoHoraEnt.setDisable(false);
			DomingoHoraEnt.setStyle("-fx-border-width: 0");
			DomingoMinutoEnt.setDisable(false);
			DomingoMinutoEnt.setStyle("-fx-border-width: 0");
			DomingoHoraSal.setDisable(false);
			DomingoHoraSal.setStyle("-fx-border-width: 0");
			DomingoMinutoSal.setDisable(false);
			DomingoMinutoSal.setStyle("-fx-border-width: 0");
		}
		else{
			DomingoHoraEnt.setStyle("-fx-border-width: 0");
			DomingoMinutoEnt.setStyle("-fx-border-width: 0");
			DomingoHoraSal.setStyle("-fx-border-width: 0");
			DomingoMinutoSal.setStyle("-fx-border-width: 0");
			DomingoHoraEnt.setDisable(true);
			DomingoHoraEnt.setText("");
			DomingoMinutoEnt.setDisable(true);
			DomingoMinutoEnt.setText("");
			DomingoHoraSal.setDisable(true);
			DomingoHoraSal.setText("");
			DomingoMinutoSal.setDisable(true);
			DomingoMinutoSal.setText("");
		}
	}
	
	@FXML TextField LunesHoraEnt = new TextField();
	@FXML TextField MartesHoraEnt = new TextField();
	@FXML TextField MiercolesHoraEnt = new TextField();
	@FXML TextField JuevesHoraEnt = new TextField();
	@FXML TextField ViernesHoraEnt = new TextField();
	@FXML TextField SabadoHoraEnt = new TextField();
	@FXML TextField DomingoHoraEnt = new TextField();
	
	@FXML TextField LunesMinutoEnt = new TextField();
	@FXML TextField MartesMinutoEnt = new TextField();
	@FXML TextField MiercolesMinutoEnt = new TextField();
	@FXML TextField JuevesMinutoEnt = new TextField();
	@FXML TextField ViernesMinutoEnt = new TextField();
	@FXML TextField SabadoMinutoEnt = new TextField();
	@FXML TextField DomingoMinutoEnt = new TextField();
	
	@FXML TextField LunesHoraSal = new TextField();
	@FXML TextField MartesHoraSal = new TextField();
	@FXML TextField MiercolesHoraSal = new TextField();
	@FXML TextField JuevesHoraSal = new TextField();
	@FXML TextField ViernesHoraSal = new TextField();
	@FXML TextField SabadoHoraSal = new TextField();
	@FXML TextField DomingoHoraSal = new TextField();
	
	@FXML TextField LunesMinutoSal = new TextField();
	@FXML TextField MartesMinutoSal = new TextField();
	@FXML TextField MiercolesMinutoSal = new TextField();
	@FXML TextField JuevesMinutoSal = new TextField();
	@FXML TextField ViernesMinutoSal = new TextField();
	@FXML TextField SabadoMinutoSal = new TextField();
	@FXML TextField DomingoMinutoSal = new TextField();
	
	@FXML TextField txtDescripcion = new TextField();
	
	private void InicializaControles(){
		LunesHoraEnt.setDisable(true);
		LunesMinutoEnt.setDisable(true);
		LunesHoraSal.setDisable(true);
		LunesMinutoSal.setDisable(true);
		
		MartesHoraEnt.setDisable(true);
		MartesMinutoEnt.setDisable(true);
		MartesHoraSal.setDisable(true);
		MartesMinutoSal.setDisable(true);
		
		MiercolesHoraEnt.setDisable(true);
		MiercolesMinutoEnt.setDisable(true);
		MiercolesHoraSal.setDisable(true);
		MiercolesMinutoSal.setDisable(true);
		
		JuevesHoraEnt.setDisable(true);
		JuevesMinutoEnt.setDisable(true);
		JuevesHoraSal.setDisable(true);
		JuevesMinutoSal.setDisable(true);
		
		ViernesHoraEnt.setDisable(true);
		ViernesMinutoEnt.setDisable(true);
		ViernesHoraSal.setDisable(true);
		ViernesMinutoSal.setDisable(true);
		
		SabadoHoraEnt.setDisable(true);
		SabadoMinutoEnt.setDisable(true);
		SabadoHoraSal.setDisable(true);
		SabadoMinutoSal.setDisable(true);
		
		DomingoHoraEnt.setDisable(true);
		DomingoMinutoEnt.setDisable(true);
		DomingoHoraSal.setDisable(true);
		DomingoMinutoSal.setDisable(true);
		
		txtDescripcion.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				txtDescripcion.setStyle("-fx-border-width: 0");
			}
		});
		LunesHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!LunesHoraEnt.getText().matches("[0-9]{1,2}"))
					LunesHoraEnt.setText("");
				if(LunesHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(LunesHoraEnt.getText()) > 24)
					LunesHoraEnt.setText("");
				LunesHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		LunesMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!LunesMinutoEnt.getText().matches("[0-9]{1,2}"))
					LunesMinutoEnt.setText("");
				if(LunesMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(LunesMinutoEnt.getText()) > 60)
					LunesMinutoEnt.setText("");
				LunesMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		LunesHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!LunesHoraSal.getText().matches("[0-9]{1,2}"))
					LunesHoraSal.setText("");
				if(LunesHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(LunesHoraSal.getText()) > 24)
					LunesHoraSal.setText("");
				LunesHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		LunesMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!LunesMinutoSal.getText().matches("[0-9]{1,2}"))
					LunesMinutoSal.setText("");
				if(LunesMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(LunesMinutoSal.getText()) > 60)
					LunesMinutoSal.setText("");
				LunesMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
		MartesHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!MartesHoraEnt.getText().matches("[0-9]{1,2}"))
					MartesHoraEnt.setText("");
				if(MartesHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(MartesHoraEnt.getText()) > 24)
					MartesHoraEnt.setText("");
				MartesHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		MartesMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!MartesMinutoEnt.getText().matches("[0-9]{1,2}"))
					MartesMinutoEnt.setText("");
				if(MartesMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(MartesMinutoEnt.getText()) > 60)
					MartesMinutoEnt.setText("");
				MartesMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		MartesHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!MartesHoraSal.getText().matches("[0-9]{1,2}"))
					MartesHoraSal.setText("");
				if(MartesHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(MartesHoraSal.getText()) > 24)
					MartesHoraSal.setText("");
				MartesHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		MartesMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!MartesMinutoSal.getText().matches("[0-9]{1,2}"))
					MartesMinutoSal.setText("");
				if(MartesMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(MartesMinutoSal.getText()) > 60)
					MartesMinutoSal.setText("");
				MartesMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
		MiercolesHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!MiercolesHoraEnt.getText().matches("[0-9]{1,2}"))
					MiercolesHoraEnt.setText("");
				if(MiercolesHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(MiercolesHoraEnt.getText()) > 24)
					MiercolesHoraEnt.setText("");
				MiercolesHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		MiercolesMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!MiercolesMinutoEnt.getText().matches("[0-9]{1,2}"))
					MiercolesMinutoEnt.setText("");
				if(MiercolesMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(MiercolesMinutoEnt.getText()) > 60)
					MiercolesMinutoEnt.setText("");
				MiercolesMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		MiercolesHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!MiercolesHoraSal.getText().matches("[0-9]{1,2}"))
					MiercolesHoraSal.setText("");
				if(MiercolesHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(MiercolesHoraSal.getText()) > 24)
					MiercolesHoraSal.setText("");
				MiercolesHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		MiercolesMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!MiercolesMinutoSal.getText().matches("[0-9]{1,2}"))
					MiercolesMinutoSal.setText("");
				if(MiercolesMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(MiercolesMinutoSal.getText()) > 60)
					MiercolesMinutoSal.setText("");
				MiercolesMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
		JuevesHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!JuevesHoraEnt.getText().matches("[0-9]{1,2}"))
					JuevesHoraEnt.setText("");
				if(JuevesHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(JuevesHoraEnt.getText()) > 24)
					JuevesHoraEnt.setText("");
				JuevesHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		JuevesMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!JuevesMinutoEnt.getText().matches("[0-9]{1,2}"))
					JuevesMinutoEnt.setText("");
				if(JuevesMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(JuevesMinutoEnt.getText()) > 60)
					JuevesMinutoEnt.setText("");
				JuevesMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		JuevesHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!JuevesHoraSal.getText().matches("[0-9]{1,2}"))
					JuevesHoraSal.setText("");
				if(JuevesHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(JuevesHoraSal.getText()) > 24)
					JuevesHoraSal.setText("");
				JuevesHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		JuevesMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!JuevesMinutoSal.getText().matches("[0-9]{1,2}"))
					JuevesMinutoSal.setText("");
				if(JuevesMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(JuevesMinutoSal.getText()) > 60)
					JuevesMinutoSal.setText("");
				JuevesMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
		ViernesHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!ViernesHoraEnt.getText().matches("[0-9]{1,2}"))
					ViernesHoraEnt.setText("");
				if(ViernesHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(ViernesHoraEnt.getText()) > 24)
					ViernesHoraEnt.setText("");
				ViernesHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		ViernesMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!ViernesMinutoEnt.getText().matches("[0-9]{1,2}"))
					ViernesMinutoEnt.setText("");
				if(ViernesMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(ViernesMinutoEnt.getText()) > 60)
					ViernesMinutoEnt.setText("");
				ViernesMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		ViernesHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!ViernesHoraSal.getText().matches("[0-9]{1,2}"))
					ViernesHoraSal.setText("");
				if(ViernesHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(ViernesHoraSal.getText()) > 24)
					ViernesHoraSal.setText("");
				ViernesHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		ViernesMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!ViernesMinutoSal.getText().matches("[0-9]{1,2}"))
					ViernesMinutoSal.setText("");
				if(ViernesMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(ViernesMinutoSal.getText()) > 60)
					ViernesMinutoSal.setText("");
				ViernesMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
		SabadoHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!SabadoHoraEnt.getText().matches("[0-9]{1,2}"))
					SabadoHoraEnt.setText("");
				if(SabadoHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(SabadoHoraEnt.getText()) > 24)
					SabadoHoraEnt.setText("");
				SabadoHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		SabadoMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!SabadoMinutoEnt.getText().matches("[0-9]{1,2}"))
					SabadoMinutoEnt.setText("");
				if(SabadoMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(SabadoMinutoEnt.getText()) > 60)
					SabadoMinutoEnt.setText("");
				SabadoMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		SabadoHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!SabadoHoraSal.getText().matches("[0-9]{1,2}"))
					SabadoHoraSal.setText("");
				if(SabadoHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(SabadoHoraSal.getText()) > 24)
					SabadoHoraSal.setText("");
				SabadoHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		SabadoMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!SabadoMinutoSal.getText().matches("[0-9]{1,2}"))
					SabadoMinutoSal.setText("");
				if(SabadoMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(SabadoMinutoSal.getText()) > 60)
					SabadoMinutoSal.setText("");
				SabadoMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
		DomingoHoraEnt.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!DomingoHoraEnt.getText().matches("[0-9]{1,2}"))
					DomingoHoraEnt.setText("");
				if(DomingoHoraEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(DomingoHoraEnt.getText()) > 24)
					DomingoHoraEnt.setText("");
				DomingoHoraEnt.setStyle("-fx-border-width: 0");
			}
		});
		DomingoMinutoEnt.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!DomingoMinutoEnt.getText().matches("[0-9]{1,2}"))
					DomingoMinutoEnt.setText("");
				if(DomingoMinutoEnt.getText().matches("[0-9]{1,2}") && Integer.parseInt(DomingoMinutoEnt.getText()) > 60)
					DomingoMinutoEnt.setText("");
				DomingoMinutoEnt.setStyle("-fx-border-width: 0");
			}
		});
		DomingoHoraSal.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent event){
				if(!DomingoHoraSal.getText().matches("[0-9]{1,2}"))
					DomingoHoraSal.setText("");
				if(DomingoHoraSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(DomingoHoraSal.getText()) > 24)
					DomingoHoraSal.setText("");
				DomingoHoraSal.setStyle("-fx-border-width: 0");
			}
		});
		DomingoMinutoSal.setOnKeyReleased(new EventHandler<KeyEvent>(){
			@Override
			public void handle(KeyEvent event){
				if(!DomingoMinutoSal.getText().matches("[0-9]{1,2}"))
					DomingoMinutoSal.setText("");
				if(DomingoMinutoSal.getText().matches("[0-9]{1,2}") && Integer.parseInt(DomingoMinutoSal.getText()) > 60)
					DomingoMinutoSal.setText("");
				DomingoMinutoSal.setStyle("-fx-border-width: 0");
			}
		});
	}
}
