namespace proyecto_final_movil;

public partial class ToursPage : ContentPage
{
	public ToursPage()
	{
		InitializeComponent();
	}

    private async void OnReservarClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        StackLayout parentStack = (StackLayout)button.Parent;

        // Obtener informacion del tour
        Grid headerGrid = (Grid)parentStack.Children[0];
        StackLayout infoStack = (StackLayout)headerGrid.Children[1];
        Label titleLabel = (Label)infoStack.Children[0];

        Grid priceGrid = (Grid)parentStack.Children[2];
        Label priceLabel = (Label)priceGrid.Children[1];

        // Mostrar confirmacion
        bool answer = await DisplayAlert(
            "Confirmar Reservacion",
            $"Deseas reservar el tour '{titleLabel.Text}'?\nPrecio: {priceLabel.Text}",
            "Si",
            "No"
        );

        if (answer)
        {
            await DisplayAlert("Reservacion Exitosa!",
                $"Tu reservacion para '{titleLabel.Text}' ha sido confirmada.\n\nRecibiras un correo de confirmacion pronto.",
                "OK");

        }
    }
}