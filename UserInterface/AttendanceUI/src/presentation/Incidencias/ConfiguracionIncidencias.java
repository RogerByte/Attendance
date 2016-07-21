package presentation.Incidencias;

import java.rmi.RemoteException;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.DiaFeriado;
import org.tempuri.AttendanceServiceProxy;

import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.Tooltip;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.HBox;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.util.Callback;

public class ConfiguracionIncidencias extends AnchorPane
{	
	public ConfiguracionIncidencias()
	{
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/Incidencias/ConfiguracionIncidencias.fxml"));
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
	private final Stage stage = new Stage();
	@FXML TextField txtNumeroRetardos = new TextField();
	@FXML TextField txtHorasTolerancia = new TextField();
	@FXML TextField txtMinutosTolerancia = new TextField();
	@FXML Button btn_AgregarDia = new Button();
	@FXML protected void btn_AgregarDia_OnAction()
	{
		final ConfiguracionIncidencias MainWindow = this;
		presentation.Incidencias.DiaFeriado DiaFeriado = new presentation.Incidencias.DiaFeriado(MainWindow);
		DiaFeriado.show();
	}
	@FXML TableView<DiaFeriado> GridDiasFeriados = new TableView<DiaFeriado>();
	@FXML TableColumn<DiaFeriado, String> tcDescripcion = new TableColumn<DiaFeriado, String>();
	@FXML TableColumn<DiaFeriado, String> tcFecha = new TableColumn<DiaFeriado, String>();
	@FXML TableColumn<DiaFeriado, String> tcEditar = new TableColumn<DiaFeriado, String>();
	@FXML TableColumn<DiaFeriado, String> tcBorrar = new TableColumn<DiaFeriado, String>();
	@FXML Button btnCancelar = new Button();
	@FXML protected void btnCancelar_OnAction()	
	{
		stage.close();
	}
	@FXML Button btnAceptar = new Button();
	@FXML protected void btnAceptar_OnAction()
	{
		ActualizaConfiguracion();
	}
	@FXML Button btnAjustaHora;
	public void show()
	{
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.centerOnScreen();
		stage.setTitle("Configuración de incidencias");
		stage.setResizable(false);
		stage.show();
		LoadControls();
		FillGrid();
	}
	public void FillGrid()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>() {
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				try
				{
					AttendanceServiceProxy Service = new AttendanceServiceProxy();
					DiaFeriado[] Dias = Service.obtenerDiasFeriados();
					if(Dias.length > 0)
					{
						ObservableList<DiaFeriado> lst = FXCollections.observableArrayList(Dias);
						GridDiasFeriados.setItems(lst);
						Response.setMensaje("OK");
						Response.setTipoMensaje(1);
					}
					else
					{
						Response.setMensaje("No fue posible obtener ningún dato");
						Response.setTipoMensaje(2);
					}
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
	private boolean ValidaEntradaConfiguracion()
	{
		boolean Valido = true;
		try
		{
			if(txtHorasTolerancia.getText().isEmpty())
			{
				txtHorasTolerancia.setStyle("-fx-border-color: red");
				txtHorasTolerancia.setText("00");
				Valido = false;
			} 
				else if(!txtHorasTolerancia.getText().matches("[0-9]{1,2}"))
				{
					txtHorasTolerancia.setStyle("-fx-border-color: red");
					txtHorasTolerancia.setText("00");
					Valido = false;
				}
				else if(Integer.parseInt(txtHorasTolerancia.getText()) > 23)
				{
					txtHorasTolerancia.setStyle("-fx-border-color: red");
					txtHorasTolerancia.setText("00");
					Valido = false;
				}
			if(txtMinutosTolerancia.getText().isEmpty())
			{
				txtMinutosTolerancia.setStyle("-fx-border-color: red");
				txtMinutosTolerancia.setText("00");
				Valido = false;
			} 
				else if(!txtMinutosTolerancia.getText().matches("[0-9]{2}"))
				{
					txtMinutosTolerancia.setStyle("-fx-border-color: red");
					txtMinutosTolerancia.setText("00");
					Valido = false;
				}
				else if(Integer.parseInt(txtMinutosTolerancia.getText()) > 60)
				{
					txtMinutosTolerancia.setStyle("-fx-border-color: red");
					txtMinutosTolerancia.setText("00");
					Valido = false;
				}
			if(txtNumeroRetardos.getText().isEmpty())
			{
				txtNumeroRetardos.setStyle("-fx-border-color: red");
				txtNumeroRetardos.setText("3");
				Valido = false;
			}
				else if(!txtNumeroRetardos.getText().matches("[0-9]{1,}"))
				{
					txtNumeroRetardos.setStyle("-fx-border-color: red");
					txtNumeroRetardos.setText("3");
					Valido = false;
				}
				else if(Integer.parseInt(txtNumeroRetardos.getText()) > 20) 
				{
					txtNumeroRetardos.setStyle("-fx-border-color: red");
					txtNumeroRetardos.setText("3");
					Valido = false;
				}
		}
		catch(Exception exc)
		{
			Valido = false;
		}
		return Valido;
	}
	private void ActualizaConfiguracion()
	{
		if(ValidaEntradaConfiguracion())
		{
			final Task<TaskResponse> task = new Task<TaskResponse>(){
				@Override public TaskResponse call() throws InterruptedException{
			    	TaskResponse Response = new TaskResponse();
					AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
					if (txtHorasTolerancia.getText().length() < 2)
						txtHorasTolerancia.setText("0" + txtHorasTolerancia.getText());
					try 
					{
						if (Servicio.actualizaTiempoTolerancia(txtHorasTolerancia.getText() + ":" + txtMinutosTolerancia.getText()))
						{
							if(Servicio.actualizaDiasRetardo(Integer.parseInt(txtNumeroRetardos.getText())))
							{
								Response.setMensaje("¡Actualización Correcta!");
								Response.setTipoMensaje(1);
							}
							else
							{
								Response.setMensaje("Actualización Parcial, sólo el tiempo de tolerancia fue actualizado...");
								Response.setTipoMensaje(2);
							}
						}
						else
						{
							Response.setMensaje("Error durante la actualización");
							Response.setTipoMensaje(2);
						}
					} 
					catch (RemoteException e) 
					{
						Response.setTipoMensaje(2);
						Response.setMensaje("Error en la comunicación con el servicio: " + e.getMessage());
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
						stage.close();
					}
				}
			});
			progress.showProgess(task);
			new Thread(task).start();
		}
		else
		{
			MessageController Mensaje = new MessageController(stage);
			Mensaje.showMessage("Error al validar los datos de entrada, favor de ingresar correctamente los valores para poder actualizar la configuración", 2);
		}
	}
	private void LoadControls()
	{
		final ConfiguracionIncidencias ParentWindow = this;
		tcDescripcion.setCellValueFactory(new PropertyValueFactory<DiaFeriado,String>("Descripcion"));
		tcFecha.setCellValueFactory(new PropertyValueFactory<DiaFeriado,String>("Fecha"));
		tcEditar.setCellValueFactory(new PropertyValueFactory<DiaFeriado,String>("Descripcion"));
		tcEditar.setCellFactory(new Callback<TableColumn<DiaFeriado, String>,TableCell<DiaFeriado, String>>(){
			@Override public TableCell<DiaFeriado, String> call(TableColumn<DiaFeriado, String> param){                
				TableCell<DiaFeriado, String> cell = new TableCell<DiaFeriado, String>(){
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
							setTooltip(new Tooltip("Editar Fecha Feriada"));
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									MessageController mensaje = new MessageController((Stage)GridDiasFeriados.getScene().getWindow());
									try
									{
										presentation.Incidencias.DiaFeriado VentanaDiasFeriados = new presentation.Incidencias.DiaFeriado(ParentWindow, (org.datacontract.schemas._2004._07.AttendanceCore_Entities.DiaFeriado)GridDiasFeriados.getSelectionModel().getSelectedItem());
										VentanaDiasFeriados.show();
									}
									catch (Exception e){
										mensaje.showMessage("Ocurrió un problema en la aplicación: "+ e.getMessage(), 2);
									}
								}
							});
						}
						else
							setGraphic(null);
					}
				};
				return cell;
			}
		});
		tcBorrar.setCellValueFactory(new PropertyValueFactory<DiaFeriado,String>("Descripcion"));
		tcBorrar.setCellFactory(new Callback<TableColumn<DiaFeriado, String>,TableCell<DiaFeriado, String>>(){
			@Override public TableCell<DiaFeriado, String> call(TableColumn<DiaFeriado, String> param){                
				TableCell<DiaFeriado, String> cell = new TableCell<DiaFeriado, String>(){
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
							setTooltip(new Tooltip("Eliminar Fecha Feriada"));
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									final MessageController mensaje = new MessageController((Stage)GridDiasFeriados.getScene().getWindow());
									mensaje.btnSalir.setOnAction(new EventHandler<ActionEvent>() {
										@Override
										public void handle(ActionEvent arg0) {
											final int DiaFeriadoId = ((DiaFeriado)GridDiasFeriados.getSelectionModel().getSelectedItem()).getDiaFeriadoId();
											final Task<TaskResponse> task = new Task<TaskResponse>(){
											    @Override public TaskResponse call() throws InterruptedException{
											    	TaskResponse Response = new TaskResponse();
											    	AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
													try {
														if(Servicio.borrarDiaFeriado(DiaFeriadoId)){
															Response.setMensaje("El día feriado ha sido borrado correctamente");
															Response.setTipoMensaje(1);
														}
														else{
															Response.setMensaje("Existió un error en el borrado del día feriado");
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
													FillGrid();
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
													FillGrid();
												}
											});
											progress.showProgess(task);
											new Thread(task).start();										
											mensaje.close();
										}
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
						else
							setGraphic(null);
					}
				};
				return cell;
			}
		});
		txtHorasTolerancia.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent arg0) {
				txtHorasTolerancia.setStyle("-fx-border-width: 0");				
			}
		});
		txtMinutosTolerancia.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent arg0) {
				txtMinutosTolerancia.setStyle("-fx-border-width: 0");
			}
		});
		txtNumeroRetardos.setOnKeyReleased(new EventHandler<KeyEvent>() {
			@Override
			public void handle(KeyEvent arg0) {
				txtNumeroRetardos.setStyle("-fx-border-width: 0");
			}
		});
		AttendanceServiceProxy Service = new AttendanceServiceProxy();
		try 
		{
			String TiempoTolerancia = Service.obtenerTiempoTolerancia();
			String DatosTiempoTolerancia [] = TiempoTolerancia.split(":");
			txtHorasTolerancia.setText(DatosTiempoTolerancia[0]);
			txtMinutosTolerancia.setText(DatosTiempoTolerancia[1]);
			txtNumeroRetardos.setText(Service.obtenerDiasRetardo());
			btnAjustaHora.setOnAction((evt) -> {
				try{
					final ProgressController progress = new ProgressController(stage);
					final Task<TaskResponse> task = new Task<TaskResponse>(){
						@Override public TaskResponse call() throws InterruptedException{
					    	TaskResponse Response = new TaskResponse();
					    	AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
							try {
								Servicio.setTime();														
							} catch (RemoteException e){
								Response.setMensaje("Error en la comunicación con el servicio");
								Response.setTipoMensaje(2);
							}
					        updateProgress(10, 10);
							return Response;
					    }
					};
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
							else
							{
								Mensaje.showMessage("La hora se ajustado correctamente según el servidor", 1);
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
				}catch(Exception ex){System.out.println(ex.getMessage());}
			});
		}
		
		catch (RemoteException e) 
		{
			
		}
	}
}
