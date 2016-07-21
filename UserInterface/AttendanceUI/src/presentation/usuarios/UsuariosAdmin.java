package presentation.usuarios;

import java.rmi.RemoteException;

import org.datacontract.schemas._2004._07.AttendanceCore_Entities.*;
import org.tempuri.AttendanceServiceProxy;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.concurrent.Task;
import javafx.concurrent.WorkerStateEvent;
import javafx.event.ActionEvent;
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
import presentation.common.MessageController;
import presentation.common.ProgressController;
import presentation.common.entities.TaskResponse;
public class UsuariosAdmin extends AnchorPane
{
	@FXML TableView<Usuario> GridUsuariosAdmin = new TableView<Usuario>();
	@FXML TableColumn<Usuario, String> tcDetail = new TableColumn<Usuario, String>();
	@FXML TableColumn<Usuario, String> tcDelete = new TableColumn<Usuario, String>();
	@FXML TableColumn<Usuario, String> colNombre = new TableColumn<Usuario, String>();
	@FXML TableColumn<Usuario, String> colUsuario = new TableColumn<Usuario, String>();
	@FXML TableColumn<Usuario, String> colFechaSesion = new TableColumn<Usuario,String>();
	@FXML TextField txtNombreUsuario = new TextField();
	@FXML Button btnBuscar = new Button();
	@FXML Button btnAgregarUsuario = new Button();
	private Stage stage;
	private Usuario usuario;
	public Usuario getUsuario(){
		return usuario;
	}
	public UsuariosAdmin(Stage stage, Usuario usuario)
	{
		this.stage = stage;
		this.usuario = usuario;
		FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/presentation/usuarios/UsuariosAdmin.fxml"));
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try
		{
			fxmlLoader.load();
			LoadControls();
			FillGrid("");
		}
		catch(Exception exc)
		{
			throw new RuntimeException(exc);
		}
	}
	private void LoadControls()
	{
		colNombre.setCellValueFactory(new PropertyValueFactory<Usuario, String>("Nombre"));
		colUsuario.setCellValueFactory(new PropertyValueFactory<Usuario, String>("NombreUsuario"));
		colFechaSesion.setCellValueFactory(new PropertyValueFactory<Usuario, String>("FechaSesion"));
		ColumnaEditar();
		ColumnaEliminar();
	}
	private void ColumnaEditar(){
		final UsuariosAdmin MainWindow = this;
		tcDetail.setCellValueFactory(new PropertyValueFactory<Usuario,String>("Nombre"));
		tcDetail.setCellFactory(new Callback<TableColumn<Usuario, String>,TableCell<Usuario, String>>(){
			@Override public TableCell<Usuario, String> call(TableColumn<Usuario, String> param){                
				TableCell<Usuario, String> cell = new TableCell<Usuario, String>(){
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
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									MessageController mensaje = new MessageController((Stage)GridUsuariosAdmin.getScene().getWindow());
									try{
										EditarUsuario chdUsuarios = new EditarUsuario((Usuario)GridUsuariosAdmin.getSelectionModel().getSelectedItem());
										chdUsuarios.setVentanaPadre(MainWindow);
										chdUsuarios.show();
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
	}
	private void ColumnaEliminar(){
		tcDelete.setCellValueFactory(new PropertyValueFactory<Usuario,String>("NombreUsuario"));
		tcDelete.setCellFactory(new Callback<TableColumn<Usuario, String>,TableCell<Usuario, String>>(){
			@Override public TableCell<Usuario, String> call(TableColumn<Usuario, String> param){                
				TableCell<Usuario, String> cell = new TableCell<Usuario, String>(){
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
							setTooltip(new Tooltip("Eliminar Usuario"));
							box.setOnMouseClicked(new EventHandler<MouseEvent>(){
								@Override public void handle(MouseEvent event){
									final MessageController mensaje = new MessageController((Stage)GridUsuariosAdmin.getScene().getWindow());
									mensaje.btnSalir.setOnAction(new EventHandler<ActionEvent>() {
										@Override
										public void handle(ActionEvent arg0) {
											final int UsuarioId = ((Usuario)GridUsuariosAdmin.getSelectionModel().getSelectedItem()).getIdUsuario();
											final Task<TaskResponse> task = new Task<TaskResponse>(){
											    @Override public TaskResponse call() throws InterruptedException{
											    	TaskResponse Response = new TaskResponse();
											    	AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
													try {
														if(Servicio.bajaUsuario(UsuarioId)){
															Response.setMensaje("El usuario ha sido borrado correctamente");
															Response.setTipoMensaje(1);
														}
														else{
															Response.setMensaje("Existió un error en el borrado del usuario");
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
													FillGrid("");
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
													FillGrid("");
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
									mensaje.showMessage("¿Esta seguro que desea eliminar al usuario?", 3);
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
	}
	public TaskResponse FillGrid(String Nombre)
	{
		TaskResponse Response = new TaskResponse();
		try
		{
			AttendanceServiceProxy Servicio = new AttendanceServiceProxy();
			Usuario[] Usuarios = Servicio.consultaUsuarios(Nombre);
			ObservableList<Usuario> lst = FXCollections.observableArrayList(Usuarios);
			GridUsuariosAdmin.setItems(lst);
			Response.setTipoMensaje(1);
			Response.setMensaje("Carga Exitosa");
		}
		catch(Exception exc)
		{
			Response.setTipoMensaje(2);
			Response.setMensaje(exc.getMessage());
		}
		return Response;
	}
	@FXML protected void txtNombreUsuario_OnAction()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>() {
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				try
				{
					Response = FillGrid(txtNombreUsuario.getText());
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
	@FXML protected void btnBuscar_OnAction()
	{
		final ProgressController progress = new ProgressController(stage);
		final MessageController Mensaje = new MessageController(stage);
		final Task<TaskResponse> task = new Task<TaskResponse>() {
			@Override
			protected TaskResponse call() throws Exception {
				TaskResponse Response = new TaskResponse();
				try
				{
					Response = FillGrid(txtNombreUsuario.getText());
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
	@FXML protected void btnAgregarUsuario_OnAction()
	{
		EditarUsuario chdUsuarios = new EditarUsuario();
		chdUsuarios.setVentanaPadre(this);
		chdUsuarios.show();
	}
}
