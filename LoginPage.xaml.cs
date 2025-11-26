using proyecto_final_movil.Models;
using proyecto_final_movil.Services;
using System.Security.Cryptography;
using System.Text;

namespace proyecto_final_movil;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

 
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string usuario = UsuarioEntry.Text;
        string contraseña = ContraseñaEntry.Text;

        // Validar que no esten vacios
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos", "OK");
            return;
        }

        try
        {
            // Obtener cliente de Supabase
            var supabase = await SupabaseService.GetClientAsync();

            // Hash de la contraseña
            string hashContraseña = HashPassword(contraseña);

            // Buscar usuario en la base de datos
            var response = await supabase
                .From<Usuario>()
                .Where(u => u.NombreUsuario == usuario && u.ContraseniaHash == hashContraseña)
                .Get();

            if (response.Models.Count > 0)
            {
                var usuarioEncontrado = response.Models[0];
                await DisplayAlert("Exito", $"Bienvenido {usuarioEncontrado.NombreCompleto}!", "OK");
                // Navegar al menu principal
                await Navigation.PushAsync(new Menu());
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al iniciar sesion: {ex.Message}", "OK");
        }
    }


    private async void OnRegistrarseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    // Metodo para hashear la contraseña con SHA256
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}