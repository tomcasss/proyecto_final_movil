using Supabase;

namespace proyecto_final_movil.Services;

public class SupabaseService
{
    private static Supabase.Client? _client;
    private const string SupabaseUrl = "https://vhqycikuscmrnkejswyu.supabase.co";
    private const string SupabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZocXljaWt1c2Ntcm5rZWpzd3l1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjQxMTY1MDAsImV4cCI6MjA3OTY5MjUwMH0.dthzHoySfo082ruEugVMkTPUe7totdHFLGg5IUkRk8Y";

    public static async Task<Supabase.Client> GetClientAsync()
    {
        if (_client == null)
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _client = new Supabase.Client(SupabaseUrl, SupabaseKey, options);
            await _client.InitializeAsync();
        }

        return _client;
    }
}
