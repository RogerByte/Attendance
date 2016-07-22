package presentation.horarios;
import java.rmi.RemoteException;
import org.datacontract.schemas._2004._07.AttendanceCore_Entities.*;
import org.tempuri.AttendanceServiceProxy;

import ResponseContracts.AttendanceService.EliminaHorarioResponse;
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.TableCell;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.Tooltip;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.HBox;
import javafx.stage.Stage;
import javafx.util.Callback;

public class Horarios extends AnchorPane{
	private Stage stage;
	private Usuario usuario;
	public Horarios(Stage ParentStage, Usuario usuario){
		this.usuario = usuario;
		stage = ParentStage;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/horarios/Horarios.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try{
			fxmlLoader.load();
			tcIdHorario.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioId"));
			tcDescripcionHoriario.setCellValueFactory(new PropertyValueFactory<Horario,String>("DescripcionHorario"));
			ColumnaEditar();
			ColumnaEliminar();
			ColumnaLunes();
			ColumnaMartes();
			ColumnaMiercoles();
			ColumnaJueves();
			ColumnaViernes();
			ColumnaSabado();
			ColumnaDomingo();
		}
		catch (Exception exception){
			MessageController mensaje = new MessageController(stage);
			mensaje.showMessage("Error en la aplicación: " + exception.getMessage(), 2);
		}
	}
	
	@FXML Button btnAgregarHorario = new Button();
	@FXML protected void btnAgregarHorario_OnAction(){
		MessageController Mensaje = new MessageController(stage);
		try{
			AltaHorario VentanaAgregar = new AltaHorario();
			VentanaAgregar.setUsuario(usuario.getIdUsuario());
			VentanaAgregar.setVentanaPadre(this);
			VentanaAgregar.show();
		}catch(Exception exc){
			Mensaje.showMessage(exc.getCause().toString(), 2);
		}
	}
	@FXML TableView<Horario> GridHorarios = new TableView<Horario>();
	@FXML TableColumn<Horario, String> tcIdHorario = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcDescripcionHoriario = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcLunes = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcMartes = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcMiercoles = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcJueves = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcViernes = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcSabado = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcDomingo = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcEdit = new TableColumn<Horario, String>();
	@FXML TableColumn<Horario, String> tcDelete = new TableColumn<Horario, String>();
	public void FillGridHorarios(){
		
		final Task<TaskResponse> task = new Task<TaskResponse>(){
		    @Override public TaskResponse call() throws InterruptedException{
		    	TaskResponse Response = new TaskResponse();
		    	AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
				try {
					Horario[] Horarios = Servicio.obtenerListaHorarios();
					if(Horarios != null)
					if(Horarios.length > 0){
				    	ObservableList<Horario> lst = FXCollections.observableArrayList(Horarios);
				    	GridHorarios.setItems(lst);
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
		task.setOnSucceeded(evt -> {
			progress.closeProgress();
			TaskResponse response = new TaskResponse();
			response = (TaskResponse)task.getValue();
			if(response.getTipoMensaje() == 2){
				Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
			}
		});
		task.setOnFailed(evt->{
			progress.closeProgress();
			TaskResponse response = new TaskResponse();
			response = (TaskResponse)task.getValue();
			if(response.getTipoMensaje() == 2) {
				Mensaje.showMessage(response.getMensaje(), response.getTipoMensaje());
			}
		});
		progress.showProgess(task);
		Platform.runLater(task);
	}
	private void ColumnaLunes(){
		tcLunes.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioLunes"));
	}
	private void ColumnaMartes(){
		tcMartes.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioMartes"));
	}
	private void ColumnaMiercoles(){
		tcMiercoles.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioMiercoles"));
	}
	private void ColumnaJueves(){
		tcJueves.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioJueves"));
	}
	private void ColumnaViernes(){
		tcViernes.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioViernes"));
	}
	private void ColumnaSabado(){
		tcSabado.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioSabado"));
	}
	private void ColumnaDomingo(){
		tcDomingo.setCellValueFactory(new PropertyValueFactory<Horario,String>("HorarioDomingo"));
	}
	private void ColumnaEditar(){
		final Horarios MainWindow = this;
		tcEdit.setCellValueFactory(new PropertyValueFactory<Horario,String>("DescripcionHorario"));
		
		tcEdit.setCellFactory(new Callback<TableColumn<Horario, String>,TableCell<Horario, String>>(){
			@Override public TableCell<Horario, String> call(TableColumn<Horario, String> param){                
				TableCell<Horario, String> cell = new TableCell<Horario, String>(){
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
							box.setOnMouseClicked(evt->{
								try{
									AltaHorario chdHorarios = new AltaHorario();
									chdHorarios.setUsuario(usuario.getIdUsuario());
									chdHorarios.CargarHorario((Horario)GridHorarios.getSelectionModel().getSelectedItem());
									chdHorarios.setVentanaPadre(MainWindow);
									chdHorarios.show();
								}
								catch (Exception e){
									MessageController mensaje = new MessageController(stage);
									mensaje.showMessage("Ocurrió un problema en la aplicación: "+ e.getMessage(), 2);
								}
							});
						}
						else{
							setGraphic(null);
						}
					}
				};
				return cell;
			}
		});
	}
	private void ColumnaEliminar(){
		tcDelete.setCellValueFactory(new PropertyValueFactory<Horario,String>("DescripcionHorario"));
		tcDelete.setCellFactory(new Callback<TableColumn<Horario, String>,TableCell<Horario, String>>(){
			@Override public TableCell<Horario, String> call(TableColumn<Horario, String> param){                
				TableCell<Horario, String> cell = new TableCell<Horario, String>(){
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
									final MessageController mensaje = new MessageController((Stage)GridHorarios.getScene().getWindow());
									try{
										mensaje.btnSalir.setOnAction(evt->{
											int HorarioId = 0;
											HorarioId = ((Horario)GridHorarios.getSelectionModel().getSelectedItem()).getHorarioId();
											AttendanceServiceProxy Service = new AttendanceServiceProxy();
											EliminaHorarioResponse Response = new EliminaHorarioResponse();
											try {
												MessageController VentanaRespesta = new MessageController(mensaje.getStage());
												Response = Service.eliminaHorario(HorarioId);
												if(Response.getEliminado()){
													VentanaRespesta.showMessage(Response.getRespuesta(), 1);
												}
												else{
													VentanaRespesta.showMessage(Response.getRespuesta(), 2);
												}
											} catch (RemoteException e) {
												System.out.print(e.getMessage());
											}
											FillGridHorarios();
											mensaje.close();
										});
										mensaje.btnCancelar.setOnMouseClicked(evt->{
											mensaje.close();
										});
										mensaje.showMessage("¿Esta seguro que desea eliminar el horario?", 3);
									}
									catch (Exception e){
										mensaje.showMessage("Ocurrió un problema en la aplicación: "+ e.getMessage(), 2);
									}
								}
							});
						}
						else{
							setGraphic(null);
						}
					}
				};
				return cell;
			}
		});
	}
}
