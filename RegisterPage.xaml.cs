using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using proyecto_final_movil.Models;
using proyecto_final_movil.Services;

namespace proyecto_final_movil;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

   
    private async void OnRegistrarClicked(object sender, EventArgs e)
    {

        string nombre = NombreEntry?.Text?.Trim() ?? string.Empty;
        string email = EmailEntry?.Text?.Trim() ?? string.Empty;
        string usuario = UsuarioEntry?.Text?.Trim() ?? string.Empty;
        string contraseña = ContraseñaEntry?.Text ?? string.Empty;
        string confirmarContraseña = ConfirmarContraseñaEntry?.Text ?? string.Empty;

        // Validaciones basicas
        if (string.IsNullOrWhiteSpace(nombre) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(usuario) ||
            string.IsNullOrWhiteSpace(contraseña) ||
            string.IsNullOrWhiteSpace(confirmarContraseña))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
            return;
        }

        if (!EsEmailValido(email))
        {
            await DisplayAlert("Error", "Introduce un email valido.", "OK");
            return;
        }

        if (contraseña != confirmarContraseña)
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
            return;
        }

        if (contraseña.Length < 6)
        {
            await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres.", "OK");
            return;
        }

        try
        {
            // Obtener cliente de Supabase
            var supabase = await SupabaseService.GetClientAsync();

            // Verificar si el usuario ya existe
            var existeUsuario = await supabase
                .From<Usuario>()
                .Where(u => u.NombreUsuario == usuario)
                .Get();

            if (existeUsuario.Models.Count > 0)
            {
                await DisplayAlert("Error", "El nombre de usuario ya existe.", "OK");
                return;
            }

            // Verificar si el email ya existe
            var existeEmail = await supabase
                .From<Usuario>()
                .Where(u => u.Correo == email)
                .Get();

            if (existeEmail.Models.Count > 0)
            {
                await DisplayAlert("Error", "El correo electrónico ya está registrado.", "OK");
                return;
            }

            // Crear nuevo usuario
            var nuevoUsuario = new Usuario
            {
                NombreCompleto = nombre,
                Correo = email,
                NombreUsuario = usuario,
                ContraseniaHash = HashPassword(contraseña),
                FechaCreacion = DateTime.Now
            };

            // Insertar en la base de datos
            await supabase.From<Usuario>().Insert(nuevoUsuario);

            await DisplayAlert("Exito", "Cuenta creada exitosamente!", "OK");


            if (Navigation is not null && Navigation.NavigationStack is not null && Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopAsync();
            }
            else if (Shell.Current is not null)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrio un error: {ex.Message}", "OK");
        }
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

    // Click del enlace "Inicia sesion" desde TapGestureRecognizer (usa TappedEventArgs)
    private async void OnVolverLoginClicked(object sender, TappedEventArgs e)
    {
        try
        {
            if (Navigation is not null && Navigation.NavigationStack is not null && Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopAsync();
            }
            else if (Shell.Current is not null)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo navegar: {ex.Message}", "OK");
        }
    }

    // Validacion simple de email
    private bool EsEmailValido(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Patron simple, suficiente para validaciones basicas
        const string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, patron, RegexOptions.IgnoreCase);
    }
}