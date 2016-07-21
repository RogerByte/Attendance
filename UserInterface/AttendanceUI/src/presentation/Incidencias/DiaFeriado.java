package presentation.Incidencias;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

import org.tempuri.AttendanceServiceProxy;

import presentation.common.MessageController;
import presentation.common.entities.TaskResponse;
import eu.schudt.javafx.controls.calendar.DatePicker;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.ProgressBar;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.GridPane;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class DiaFeriado extends AnchorPane
{
	public DiaFeriado()
	{
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/Incidencias/DiaFeriado.fxml"));
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
	}
	public DiaFeriado(ConfiguracionIncidencias ParentWindow, org.datacontract.schemas._2004._07.AttendanceCore_Entities.DiaFeriado Entidad)
	{
		this();
		setParentWindow(ParentWindow);
		setEntidad(Entidad);
		setTipo(2);
		LoadControls();
	}
	public DiaFeriado(ConfiguracionIncidencias ParentWindow)
	{
		this();
		setParentWindow(ParentWindow);
		setTipo(1);
		LoadControls();
	}
	private int Tipo;
	public void setTipo(int Value)
	{
		Tipo = Value;
	}
	public int getTipo()
	{
		return Tipo;
	}
	private final Stage stage = new Stage();
	private ConfiguracionIncidencias ParentWindow;
	public void setParentWindow(ConfiguracionIncidencias ParentWindow)
	{
		this.ParentWindow = ParentWindow;
	}
	public ConfiguracionIncidencias getParentWindow()
	{
		return ParentWindow;
	}
	private org.datacontract.schemas._2004._07.AttendanceCore_Entities.DiaFeriado Entidad;
	public void setEntidad(org.datacontract.schemas._2004._07.AttendanceCore_Entities.DiaFeriado Entidad)
	{
		this.Entidad = Entidad;
	}
	public org.datacontract.schemas._2004._07.AttendanceCore_Entities.DiaFeriado getEntidad()
	{
		return Entidad;
	}
	
	@FXML TextField txtDescripcion = new TextField();
	@FXML GridPane gdpFecha = new GridPane();
	@FXML Button btnAceptar = new Button();
	@FXML Button btnCancelar = new Button();
	private DatePicker dtpkFecha = new DatePicker();
	@FXML ProgressBar BarraProgreso = new ProgressBar();
	@FXML protected void btnAceptar_OnAction()
	{
		if(ValidaEntrada())
		{
			final Task<TaskResponse> task = new Task<TaskResponse>()
			{
				@Override
				protected TaskResponse call() throws Exception {
					TaskResponse Response = new TaskResponse();
					AttendanceServiceProxy Service = new AttendanceServiceProxy();
					try
					{
						if(Tipo == 1)//Refierese a una inserción
						{
							Calendar FechaFeriada = Calendar.getInstance();
							FechaFeriada.setTime(dtpkFecha.selectedDateProperty().getValue());
							Service.insertarDiaFeriado(txtDescripcion.getText(), FechaFeriada);
							updateProgress(100, 100);
							Response.setTipoMensaje(1);
							Response.setMensaje("¡Inserción correcta!");
						}
						else //De otra forma se trata de una actualización
						{
							Calendar FechaFeriada = Calendar.getInstance();
							FechaFeriada.setTime(dtpkFecha.selectedDateProperty().getValue());
							Service.actualizaDiaFeriado(getEntidad().getDiaFeriadoId(), txtDescripcion.getText(), FechaFeriada);
							updateProgress(100, 100);
							Response.setTipoMensaje(1);
							Response.setMensaje("¡Actualización correcta!");
						}
					}
					catch(Exception exc)
					{
						Response.setMensaje("Error en la aplicación: " + exc.getMessage());
						Response.setTipoMensaje(2);
					}
					return Response;
				}
			};
			final MessageController Mensaje = new MessageController(stage);
			task.setOnSucceeded(new EventHandler<WorkerStateEvent>(){
				@Override
				public void handle(WorkerStateEvent event){
					TaskResponse response = new TaskResponse();
					response = (TaskResponse)task.getValue();
					if(response.getTipoMensaje() == 2)
					{
						Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
					}
					else{
						Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
						getParentWindow().FillGrid();
						stage.close();
					}
				}
			});
			task.setOnFailed(new EventHandler<WorkerStateEvent>(){
				@Override
				public void handle(WorkerStateEvent event){
					TaskResponse response = new TaskResponse();
					response = (TaskResponse)task.getValue();
					if(response.getTipoMensaje() == 2){
						Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
					}
					else{
						Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
						getParentWindow().FillGrid();
						stage.close();
					}
				}
			});
			BarraProgreso.progressProperty().bind(task.progressProperty());
			new Thread(task).start();
		}
		else
		{
			MessageController Mensaje = new MessageController(stage);
			Mensaje.showMessage("Ingrese los campos para poder dar de alta una fecha feriada", 2);
		}
			
	}
	@FXML protected void btnCancelar_OnAction()
	{
		stage.close();
	}
	public void show()
	{
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Dias Feriados");
		stage.setResizable(false);
		stage.show();
	}
	private void LoadControls()
	{
		try 
		{
			Locale loc = new Locale("es","MX");
			dtpkFecha = new DatePicker(loc);
			dtpkFecha.setDateFormat(new SimpleDateFormat("yyyy-MM-dd"));
			dtpkFecha.getCalendarView().todayButtonTextProperty().set("Today");
			dtpkFecha.getCalendarView().setShowWeeks(false);
			dtpkFecha.getStylesheets().add("/presentation/DatePicker.css");
			gdpFecha.add(dtpkFecha, 0, 0);
			txtDescripcion.setOnKeyReleased(new EventHandler<KeyEvent>() {
				@Override
				public void handle(KeyEvent arg0) {
					txtDescripcion.setStyle("-fx-border-width: 0");
				}
			});
			if (getTipo() == 2)
			{
				txtDescripcion.setText(getEntidad().getDescripcion());
				Calendar cal = Calendar.getInstance();
				SimpleDateFormat sdf = new SimpleDateFormat("d MMM yyyy", loc);
				cal.setTime(sdf.parse(getEntidad().getFecha()));
				dtpkFecha.selectedDateProperty().setValue(cal.getTime());
			}
		}
		catch(Exception exc)
		{
			MessageController Mensaje = new MessageController(stage);
			Mensaje.showMessage("Existió un error en la aplicación: " + exc.getMessage(), 2);
		}
	}
	private boolean ValidaEntrada()
	{
		boolean Valido = true;
		try
		{
			if(txtDescripcion.getText().isEmpty())
			{
				Valido = false;
				txtDescripcion.setStyle("-fx-border-color: red");
			}
			if (dtpkFecha.selectedDateProperty().getValue() == null)
			{
				Valido = false;
			}
		}
		catch(Exception exc)
		{
			Valido = false;
		}
		return Valido;
	}
}
