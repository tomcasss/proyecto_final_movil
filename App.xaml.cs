namespace proyecto_final_movil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Iniciar la aplicación con LoginPage envuelta en NavigationPage
            return new Window(new NavigationPage(new LoginPage()));
        }
    }
}