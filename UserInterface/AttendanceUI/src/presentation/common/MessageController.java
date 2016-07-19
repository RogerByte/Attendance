package presentation.common;
import java.io.IOException;
import java.net.URL;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
public class MessageController extends AnchorPane
{
	@FXML private ImageView imgMessage;
	@FXML private Label lblMessageContent;
	@FXML public Button btnSalir;
	@FXML public Button btnCancelar;
	private Stage parentStage;
	private final Stage stage = new Stage();
	public boolean confirmed = false;
	public boolean result;
	public MessageController(Stage parentStage) {
		this.parentStage = parentStage;
		URL location = getClass().getResource("/presentation/common/Message.fxml");
		FXMLLoader fxmlLoader = new FXMLLoader();
		fxmlLoader.setLocation(location);
		fxmlLoader.setRoot(this);
		fxmlLoader.setController(this);
		try	{
			Parent root = (Parent)fxmlLoader.load();
			root.getStylesheets().add(this.getClass().getResource("/presentation/common/Images/JMetroLightTheme.css").toExternalForm());
			btnCancelar.setOnAction(e->{stage.close();});
			btnSalir.setOnAction(e->{stage.close();});
		}
		catch (IOException exception) {
			throw new RuntimeException(exception);
		}
	}
	@FXML protected void btnSalir_OnClick(MouseEvent event)	{
		stage.close();
	}
	@FXML protected void btnCancelar_OnClick(MouseEvent event) {
	    stage.close();
	}
	public Stage getStage() {
		return this.stage;
	}
	public void close()	{
		this.stage.close();
	}
	private void loadComponent() {
		stage.initModality(Modality.APPLICATION_MODAL);
		stage.setScene(new Scene(this));
		stage.setWidth(345);
		stage.setHeight(221);
		stage.setResizable(false);
		stage.setX(parentStage.getX() + parentStage.getWidth() / 2 - stage.getWidth() / 2);
		stage.setY(parentStage.getY() + parentStage.getHeight() / 2 - stage.getHeight() / 2);
		stage.show();
	}
	/**
	 * @author Lázaro Adrián González Montoya
	 * @param Mensaje Mensaje para mostrar en la ventana
	 * @param config <br>
	 * 1 - Correcto <br>
	 * 2 - Error <br>
	 * 3 - Warning <br>
	 */
	public void showMessage(String Mensaje, int config)	{
		loadComponent();
		lblMessageContent.setText(Mensaje);
		confirmed = false;
		Image imagen;
		try {
			switch(config) {
				case 1:
					imagen = new Image("/presentation/common/Images/checked_64.png");				
					imgMessage.setImage(imagen);
					btnCancelar.setVisible(false);
					break;
				case 2:
					imagen = new Image("/presentation/common/Images/error_64.png");
					imgMessage.setImage(imagen);
					btnCancelar.setVisible(false);
					break;
				case 3:
					imagen = new Image("/presentation/common/Images/question.png");
					imgMessage.setImage(imagen);
				default:
					break;
			}
		}
		catch(Exception ex)	{
			ex.printStackTrace();
		}
	}
}
