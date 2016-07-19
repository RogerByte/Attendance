package presentation.empleados;
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;

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
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.Tooltip;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.HBox;
import javafx.stage.Stage;
import javafx.util.Callback;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.*;
import org.tempuri.AttendanceServiceProxy;

public class Empleados extends AnchorPane{
	private Stage stage;
	private Usuario usuario;
	public Empleados(Stage ParentStage, Usuario usuario){
		this.usuario = usuario;
		stage = ParentStage;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/empleados/Empleados.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			txtNombreEmpleado.setOnAction (e->{
				FillGridEmpleados(txtNombreEmpleado.getText());
			});
			tcIdEmpleado.setCellValueFactory(new PropertyValueFactory<Empleado,String>("IEmpleadoId"));
			tcNumeroEmpleado.setCellValueFactory(new PropertyValueFactory<Empleado,String>("NumeroEmpleado"));
			tcNombreEmpleado.setCellValueFactory(new PropertyValueFactory<Empleado,String>("NombreEmpleado"));
			tcCompania.setCellValueFactory(new PropertyValueFactory<Empleado,String>("Compania"));
			tcNomina.setCellValueFactory(new PropertyValueFactory<Empleado,String>("Nomina"));
			ColumnaEditar();
			ColumnaEliminar();
			ColumnaSincronizar();
		}
		catch (IOException exception){
			throw new RuntimeException(exception);
		}
	}
	private ObservableList<Empleado> ListaEmpleados = FXCollections.observableArrayList();;
	private void setListaEmpleados(ArrayList<Empleado> ListaEmpleados){
		this.ListaEmpleados = FXCollections.observableArrayList(ListaEmpleados);
	}
	private ObservableList<Empleado> getListaEmpleados() {
		return ListaEmpleados;
	}
	@FXML TableView<Empleado> GridEmpleados;
	@FXML TableColumn<Empleado, String> tcIdEmpleado;
	@FXML TableColumn<Empleado, String> tcNumeroEmpleado;
	@FXML TableColumn<Empleado, String> tcNombreEmpleado;
	@FXML TableColumn<Empleado, String> tcEdit;
	@FXML TableColumn<Empleado, String> tcDelete;
	@FXML TableColumn<Empleado, String> tcSync;
	@FXML TableColumn<Empleado, String> tcCompania;
	@FXML TableColumn<Empleado, String> tcNomina;
	public void FillGridEmpleados(final String NombreEmpleado){
		final Task<TaskResponse> task = new Task<TaskResponse>(){
		    @Override public TaskResponse call() throws InterruptedException{
		    	TaskResponse Response = new TaskResponse();
		    	AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				try {
					Empleado[] EmpleadosArray = Servicio.obtenerListaEmpleados(NombreEmpleado).getEmpleados();
					if(EmpleadosArray != null)
					if(EmpleadosArray.length > 0){
						ArrayList<Empleado> Lista = new ArrayList<Empleado>();
						for(Empleado empleado : EmpleadosArray){
							Lista.add(empleado);
						}
						setListaEmpleados(Lista);
						GridEmpleados.setItems(getListaEmpleados());
					}
				} catch (RemoteException e){
					Response.setMensaje("Error en la comunicación con el servicio");
					Response.setTipoMensaje(2);
				}
		        updateProgress(10, 10);
				return Response;
		    }
		};
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
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
				if(response.getTipoMensaje() == 2)
				{
					Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
				}
			}
		});
		
		progress.showProgess(task);
		new Thread(task).start();
	}
	/**
	 * Permite cargar el botón con icono dentro del Grid para Editar un Empleado en los dispositivos de reloj checador
	 */
	private void ColumnaEditar(){
		final Empleados MainWindow = this;
		tcEdit.setCellValueFactory(new PropertyValueFactory<Empleado,String>("NumeroEmpleado"));
		tcEdit.setCellFactory(new Callback<TableColumn<Empleado, String>,TableCell<Empleado, String>>(){
			@Override public TableCell<Empleado, String> call(TableColumn<Empleado, String> param){                
				TableCell<Empleado, String> cell = new TableCell<Empleado, String>(){
					ImageView imageview = new ImageView();
					@Override public void updateItem(String item, boolean empty){                        
						if(item!=null){                            
							final HBox box= new HBox();
							box.setSpacing(10);
							imageview.setFitHeight(20);
							imageview.setFitWidth(20);
							imageview.setImage(new Image("/presentation/common/Images/Editar.png")); 
							box.getChildren().addAll(imageview);
							box.setAlignment(Pos.CENTER);
							setGraphic(box);
							setTooltip(new Tooltip("Editar Registro"));
							box.setOnMouseClicked(e-> {
								try {
									boolean EsExterno = ((Empleado)GridEmpleados.getSelectionModel().getSelectedItem()).getExterno();
									if(!EsExterno) {
										AltaEmpleado chdEmpleados = new AltaEmpleado((Empleado)GridEmpleados.getSelectionModel().getSelectedItem());
										chdEmpleados.setVentanaPadre(MainWindow);
										chdEmpleados.show();
									}
									else {
										AltaExterno chdEmpleadosExternos = new AltaExterno((Empleado)GridEmpleados.getSelectionModel().getSelectedItem());
										chdEmpleadosExternos.setVentanaPadre(MainWindow);
										chdEmpleadosExternos.show();
									}
								}
								catch (Exception exc) {
									MessageController mensaje = new MessageController(stage);
									mensaje.showMessage("Ocurrió un problema en la aplicación: " + exc.getMessage(), 2);
								}
							});
						}
						else {
							setGraphic(null);
						}
					}
				};
				return cell;
			}
		});
	}
	private void ColumnaEliminar(){
		tcDelete.setCellValueFactory(new PropertyValueFactory<Empleado,String>("NumeroEmpleado"));
		tcDelete.setCellFactory(new Callback<TableColumn<Empleado, String>,TableCell<Empleado, String>>(){
			@Override public TableCell<Empleado, String> call(TableColumn<Empleado, String> param){                
				TableCell<Empleado, String> cell = new TableCell<Empleado, String>(){
					ImageView imageview = new ImageView();
					@Override public void updateItem(String item, boolean empty){                        
						if(item!=null){                            
							final HBox box= new HBox();
							box.setSpacing(10);
							imageview.setFitHeight(20);
							imageview.setFitWidth(20);
							imageview.setImage(new Image("/presentation/common/Images/delete.png")); 
							box.getChildren().addAll(imageview);
							box.setAlignment(Pos.CENTER);
							setGraphic(box);
							setTooltip(new Tooltip("Eliminar Empleado"));
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									final MessageController mensaje = new MessageController((Stage)GridEmpleados.getScene().getWindow());
									mensaje.btnSalir.setOnAction(evt->{
										final int EmpleadoId = ((Empleado)GridEmpleados.getSelectionModel().getSelectedItem()).getIEmpleadoId();
										final Task<TaskResponse> task = new Task<TaskResponse>(){
										    @Override public TaskResponse call() throws InterruptedException{
										    	TaskResponse Response = new TaskResponse();
										    	AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
												try {
													if(Servicio.borraEmpleado(EmpleadoId)){
														Response.setMensaje("El empleado ha sido borrado correctamente");
														Response.setTipoMensaje(1);
													}
													else{
														Response.setMensaje("Existió un error en el borrado del empleado");
														Response.setTipoMensaje(2);
													}																
												} catch (RemoteException e){
													Response.setMensaje("Error en la comunicación con el servicio");
													Response.setTipoMensaje(2);
												}
										        updateProgress(10, 10);
												return Response;
										    }
										};
										final ProgressController progress = new ProgressController(stage);
										final MessageController Mensaje = new MessageController(stage);
										task.setOnSucceeded(evnt -> {
											progress.closeProgress();
											TaskResponse response = new TaskResponse();
											response = (TaskResponse)task.getValue();
											if(response.getTipoMensaje() == 2)
											{
												Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
											}
											FillGridEmpleados("");
										});
										task.setOnFailed(evnt -> {
											progress.closeProgress();
											TaskResponse response = new TaskResponse();
											response = (TaskResponse)task.getValue();
											if(response.getTipoMensaje() == 2) {
												Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
											}
											FillGridEmpleados("");
										});
										progress.showProgess(task);
										new Thread(task).start();										
										mensaje.close();
									});
									mensaje.btnCancelar.setOnMouseClicked(new EventHandler<MouseEvent>() {
										@Override
										public void handle(MouseEvent event){
											mensaje.close();
										}
									});
									mensaje.showMessage("¿Esta seguro que desea eliminar al empleado?", 3);
								}
							});
						}
						else {
							setGraphic(null);
						}
					}
				};
				return cell;
			}
		});
	}
	private void ColumnaSincronizar(){
		tcSync.setCellValueFactory(new PropertyValueFactory<Empleado,String>("NumeroEmpleado"));
		tcSync.setCellFactory(new Callback<TableColumn<Empleado, String>,TableCell<Empleado, String>>(){
			@Override public TableCell<Empleado, String> call(TableColumn<Empleado, String> param){                
				TableCell<Empleado, String> cell = new TableCell<Empleado, String>(){
					ImageView imageview = new ImageView();
					@Override public void updateItem(String item, boolean empty){                        
						if(item!=null){                            
							final HBox box= new HBox();
							box.setSpacing(10);
							imageview.setFitHeight(22);
							imageview.setFitWidth(18);
							imageview.setImage(new Image("/presentation/common/Images/sync.png")); 
							box.getChildren().addAll(imageview);
							box.setAlignment(Pos.CENTER);
							setGraphic(box);
							setTooltip(new Tooltip("Sincronizar empleado"));
							box.setOnMouseClicked(ev -> {
								MessageController mensaje = new MessageController((Stage)GridEmpleados.getScene().getWindow());
								try{
									SyncEmpleado chdSyncho = new SyncEmpleado((Empleado)GridEmpleados.getSelectionModel().getSelectedItem());
									chdSyncho.show();
								}
								catch (Exception ex){
									mensaje.showMessage("Ocurrió un problema en la aplicación: "+ ex.getMessage(), 2);
								}
							});
						}
						else {
							setGraphic(null);
						}
					}
				};
				return cell;
			}
		});
	}
	@FXML Button btnBuscarEmpleado = new Button();
	@FXML protected void btnBuscarEmpleado_OnAction(){
		FillGridEmpleados(txtNombreEmpleado.getText());
	}
	@FXML TextField txtNombreEmpleado = new TextField();
	@FXML Button btnAgregarEmpleado = new Button();
	@FXML protected void btnAgregarEmpleado_OnAction(){
		MessageController mensaje = new MessageController(stage);
		try{
			AltaEmpleado VentanaAltaEmpleado = new AltaEmpleado(true);
			VentanaAltaEmpleado.setUsuario(usuario.getIdUsuario());
			VentanaAltaEmpleado.setVentanaPadre(this);
			VentanaAltaEmpleado.show();
		}
		catch(Exception exc){
			mensaje.showMessage("Error en el sistema: " + exc.getMessage(), 2);
		}
	}
	@FXML Button btnExterno = new Button();
	@FXML protected void btnExterno_OnAction()
	{
		MessageController mensaje = new MessageController(stage);
		try{
			AltaExterno ChdExterno = new AltaExterno(true);
			ChdExterno.setUsuario(usuario.getIdUsuario());
			ChdExterno.setVentanaPadre(this);
			ChdExterno.show();
		}
		catch(Exception exc){
			mensaje.showMessage("Error en el sistema: " + exc.getMessage(), 2);
		}
	}
}
