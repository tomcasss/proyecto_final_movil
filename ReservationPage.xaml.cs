using System.Collections.ObjectModel;

namespace proyecto_final_movil;

public partial class ReservationPage : ContentPage
{
    public ObservableCollection<Alojamiento> Opciones { get; set; }

    public ReservationPage()
	{
		InitializeComponent();
        Opciones = new ObservableCollection<Alojamiento>
            {
                new Alojamiento
                {
                    Nombre = "Cabana Rio Tortuguero",
                    Ubicacion = "Frente al canal",
                    Precio = "45000 por noche",
                    Imagen = "cabana1.jpg"
                },
                new Alojamiento
                {
                    Nombre = "Bungalow Jardin",
                    Ubicacion = "Cerca del muelle",
                    Precio = "38000 por noche",
                    Imagen = "bungalow1.jpg"
                },
                new Alojamiento
                {
                    Nombre = "Eco Lodge Premium",
                    Ubicacion = "Selva tropical",
                    Precio = "60000 por noche",
                    Imagen = "eco1.jpg"
                }
            };

        cvOpciones.ItemsSource = Opciones;

        btnReservar.Clicked += BtnReservar_Clicked;
    }

    private async void BtnReservar_Clicked(object sender, EventArgs e)
    {
        if (cvOpciones.SelectedItem == null)
        {
            await DisplayAlert("Atencion", "Seleccione un alojamiento", "OK");
            return;
        }

        var reserva = (Alojamiento)cvOpciones.SelectedItem;

        await DisplayAlert(
            "Reserva confirmada",
            "Has reservado: " + reserva.Nombre + "\n" +
            "Entrada: " + dpEntrada.Date.ToString("dd/MM/yyyy") + "\n" +
            "Salida: " + dpSalida.Date.ToString("dd/MM/yyyy") + "\n" +
            "Huespedes: " + pkHuespedes.SelectedItem + "\n" +
            "Tipo: " + pkTipo.SelectedItem,
            "OK"
        );
    }
}

public class Alojamiento
{
    public string Nombre { get; set; }
    public string Ubicacion { get; set; }
    public string Precio { get; set; }
    public string Imagen { get; set; }
}

	
