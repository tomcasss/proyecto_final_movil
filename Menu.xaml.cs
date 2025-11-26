namespace proyecto_final_movil;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    private async void Geo_tap(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlacesPage());
    }

    private async void Eco_tap(object sender, EventArgs e)
    {

    }

    private async void Map_tap(object sender, EventArgs e)
    {

    }

    private async void His_tap(object sender, EventArgs e)
    {

    }

    private async void Book_tap(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReservationPage());
    }

    private async void Help_tap(object sender, EventArgs e)
    {

    }

    private async void Activities_tap(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToursPage());
    }
}