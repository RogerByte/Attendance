package presentation;

import java.io.IOException;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Usuario;
import presentation.Incidencias.Incidencias;
import presentation.comedor.Comedor;
import presentation.common.MessageController;
import presentation.empleados.Empleados;
import presentation.horarios.Horarios;
import presentation.reportes.LayoutWindow;
import presentation.usuarios.UsuariosAdmin;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.StackPane;
import javafx.stage.Stage;
import javafx.concurrent.Task;
import javafx.fxml.*;

public class Attendance extends AnchorPane{
	private Usuario Usuario;
	public Attendance(Stage stage, Usuario usuario){
		this.Usuario = usuario;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/Attendance.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			mnLayoutComedor.setOnAction((e)->{
				LayoutWindow VentanaReporteLayoutComedor = new LayoutWindow(1);
				VentanaReporteLayoutComedor.show();
			});
			mnLayoutIncidencias.setOnAction((e)->{
				LayoutWindow VentanaReporteLayoutIncidencias = new LayoutWindow(2);
				VentanaReporteLayoutIncidencias.show();
			});
		}
		catch (IOException exception){
			throw new RuntimeException(exception);
		}
	}
	private final Stage stage = new Stage();
	public void show(){
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Attendance");
		stage.setResizable(true);
		Task<Void> CargarPantallaEmpleados = new Task<Void>() {
			@Override
			protected Void call() throws Exception {
				IncrustaPanelEmpleados();
				return null;
			}
	    };
	    Task<Void> CargarPantallaHorarios = new Task<Void>() {
			@Override
			protected Void call() throws Exception {
				IncrustaPanelHorarios();
				return null;
			}
	    };
	    Task<Void> CargarPantallaComedores = new Task<Void>() {
			@Override
			protected Void call() throws Exception {
				IncrustaPanelReporteComedor();
				return null;
			}
	    };
	    Task<Void> CargarPantallaIncidencias = new Task<Void>() {
			@Override
			protected Void call() throws Exception {
				IncrustaPanelIncidencias();
				return null;
			}
	    };
	    Task<Void> CargarPantallaAdministradores = new Task<Void>() {
			@Override
			protected Void call() throws Exception {
				IncrustaPanelUsuariosAdministradores();
				return null;
			}
	    };
		Thread Empleados = new Thread(CargarPantallaEmpleados);
		Thread Horarios = new Thread(CargarPantallaHorarios);
		Thread Comedores = new Thread(CargarPantallaComedores);
		Thread Incidencias = new Thread(CargarPantallaIncidencias);
		Thread Administradores = new Thread(CargarPantallaAdministradores);
		Empleados.start();
		Horarios.start();
		Comedores.start();
		Incidencias.start();
		Administradores.start();
		try{
			Empleados.join();
			Horarios.join();
			Comedores.join();
			Incidencias.join();
			Administradores.join();
			stage.show();
		}
		catch(Exception exc){
			MessageController Mensaje = new MessageController(stage);
			Mensaje.showMessage("Error al intentar cargar la aplicación: " + exc.getMessage(), 2);
		}
		
	}
	private void IncrustaPanelEmpleados(){
		Empleados PanelEmpleados = new Empleados(stage, Usuario);
		spEmpleados.getChildren().add(PanelEmpleados);
		//PanelEmpleados.FillGridEmpleados("");
	}
	private void IncrustaPanelHorarios(){
		Horarios PanelHorarios = new Horarios(stage, Usuario);
		spHorarios.getChildren().add(PanelHorarios);
		PanelHorarios.FillGridHorarios();
	}
	private void IncrustaPanelReporteComedor(){
		Comedor PanelComedor = new Comedor(stage, Usuario);
		spReporteComedor.getChildren().add(PanelComedor);
	}
	private void IncrustaPanelIncidencias(){
		Incidencias PanelIncidencias = new Incidencias(stage, Usuario);
		spIncidencias.getChildren().add(PanelIncidencias);
	}
	private void IncrustaPanelUsuariosAdministradores(){
		UsuariosAdmin PanelUsuariosAdmin = new UsuariosAdmin(stage, Usuario);
		spUsuariosAdministradores.getChildren().add(PanelUsuariosAdmin);
	}
	@FXML private Tab TabEmpleados;
	@FXML private StackPane spEmpleados;
	@FXML private StackPane spHorarios;
	@FXML private StackPane spReporteComedor;
	@FXML private StackPane spIncidencias;
	@FXML private StackPane spUsuariosAdministradores;
	@FXML private MenuItem mnLayoutComedor;
	@FXML private MenuItem mnLayoutIncidencias;
}
