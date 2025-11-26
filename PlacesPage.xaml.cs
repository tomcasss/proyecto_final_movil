namespace proyecto_final_movil;

public partial class PlacesPage : ContentPage
{
	public PlacesPage()
	{
		InitializeComponent();
	}

    public async void OnVerMapaClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        Frame parentFrame = (Frame)button.Parent.Parent.Parent;
        StackLayout stackLayout = (StackLayout)parentFrame.Content;
        StackLayout infoStack = (StackLayout)((Grid)stackLayout.Children[0]).Children[1];
        Label titleLabel = (Label)infoStack.Children[0];

        await DisplayAlert("Mapa", $"Abriendo mapa de: {titleLabel.Text}", "OK");
    }

    public async void OnMasInfoClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        Frame parentFrame = (Frame)button.Parent.Parent.Parent;
        StackLayout stackLayout = (StackLayout)parentFrame.Content;
        StackLayout infoStack = (StackLayout)((Grid)stackLayout.Children[0]).Children[1];
        Label titleLabel = (Label)infoStack.Children[0];
        Label descLabel = (Label)infoStack.Children[1];

        await DisplayAlert(titleLabel.Text, descLabel.Text, "Cerrar");

    }
}