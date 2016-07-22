package presentation;

import java.io.IOException;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.Usuario;
import presentation.Incidencias.ConfiguracionIncidencias;
import presentation.Incidencias.Incidencias;
import presentation.comedor.Comedor;
import presentation.comedor.ConfiguracionComedor;
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
			mnConfiguracionComedores.setOnAction((e)->{
				ConfiguracionComedor VentanaConfiguracion = new ConfiguracionComedor();
				VentanaConfiguracion.show();
			});
			mnConfiguracionIncidencias.setOnAction((e)->{
				ConfiguracionIncidencias configuracion = new ConfiguracionIncidencias();
				configuracion.show();
			});
			mnInfoIncidencias.setOnAction((e)->{
				LayoutWindow VentanaReporteLayoutIncidencias = new LayoutWindow(4);
				VentanaReporteLayoutIncidencias.show();
			});
			mnInfoComedor.setOnAction((e)->{
				LayoutWindow VentanaReporteLayoutIncidencias = new LayoutWindow(3);
				VentanaReporteLayoutIncidencias.show();
			});
		}
		catch (IOException exception){
			MessageController mensaje = new MessageController(stage);
			mensaje.showMessage("Error en la aplicación: " + exception.getMessage(), 2);
		}
	}
	private final Stage stage = new Stage();
	public void show(){
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Attendance");
		stage.setResizable(true);
		Thread CargaPantallaEmpleados = new Thread(new Runnable() {
			@Override
			public void run() {
				javafx.application.Platform.runLater(new Runnable() {
                    public void run() {
                    	IncrustaPanelEmpleados();
                    	stage.show();
                    }
                });
			}
		});
		Thread CargaPantallaHorarios = new Thread(new Runnable(){
			@Override
			public void run() {
				javafx.application.Platform.runLater(new Runnable() {
                    public void run() {
                    	IncrustaPanelHorarios();
                    }
                });
			}
		});
		Thread CargaPantallaComedores = new Thread(new Runnable(){
			@Override
			public void run() {
				javafx.application.Platform.runLater(new Runnable() {
                    public void run() {
                    	IncrustaPanelReporteComedor();
                    }
                });
			}
		});
		Thread CargaPantallaIncidencias = new Thread(new Runnable() {
			@Override
			public void run() {
				javafx.application.Platform.runLater(new Runnable() {
                    public void run() {
                    	IncrustaPanelIncidencias();
                    }
                });
			}
		});
		Thread CargaPantallaUsuarios = new Thread(new Runnable(){
			@Override
			public void run() {
				javafx.application.Platform.runLater(new Runnable() {
                    public void run() {
                    	IncrustaPanelUsuariosAdministradores();
                    }
                });
			}
		});
		CargaPantallaHorarios.start();
		CargaPantallaComedores.start();
		CargaPantallaIncidencias.start();
		CargaPantallaUsuarios.start();
		CargaPantallaEmpleados.start();
		try {
			CargaPantallaEmpleados.join();
			CargaPantallaHorarios.join();
			CargaPantallaComedores.join();
			CargaPantallaIncidencias.join();
			CargaPantallaUsuarios.join();
		} catch (Exception e) {
			MessageController Mensaje = new MessageController(stage);
			Mensaje.showMessage("Error en la aplicación: " + e.getMessage(), 2);
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
	@FXML private MenuItem mnConfiguracionComedores;
	@FXML private MenuItem mnConfiguracionIncidencias;
	@FXML private MenuItem mnInfoIncidencias;
	@FXML private MenuItem mnInfoComedor;
}
