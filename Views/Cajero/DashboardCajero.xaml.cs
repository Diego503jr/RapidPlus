using Rapid_Plus.Views.Administrador;
using Rapid_Plus.Views.Mesero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rapid_Plus.Views.Cajero
{
    /// <summary>
    /// Lógica de interacción para DashboardCajero.xaml
    /// </summary>
    public partial class DashboardCajero : Window
    {

        #region DECLARACION DE VARIABLES Y CLASES
        
        private FacturarOrden factura;
        private int idUsuario = -1;

        #endregion
        
        public DashboardCajero(int usuarioId)
        {
            InitializeComponent();
            LoadDarkMode();
            this.idUsuario = usuarioId;
        }



        #region METODOS MODO OSCURO

        private void ToggleTheme(bool isDarkMode)
        {
            var themeUri = new Uri(isDarkMode
                ? "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml"
                : "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");

            // Replace the first merged dictionary with the selected theme
            var dictionaries = Application.Current.Resources.MergedDictionaries;
            dictionaries[0] = new ResourceDictionary { Source = themeUri };
        }

        private void ThemeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleTheme(true);
        }

        private void ThemeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleTheme(false);
        }

        void LoadDarkMode()
        {
            // Cargar el modo oscuro
            bool isDarkMode = Properties.Settings.Default.DarkMode;
            ThemeToggleButton.IsChecked = isDarkMode;
            ToggleTheme(isDarkMode);
        }

        #endregion

        #region METODOS MENU

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            if (factura == null)
            {
                factura = new FacturarOrden();
            }
            frContent.NavigationService.Navigate(factura);
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            GestionClientes clientes = new GestionClientes();
            frContent.NavigationService.Navigate(clientes);
        }

        private void btnCrearOrden_Click(object sender, RoutedEventArgs e)
        {
            CrearOrden crearOrden = new CrearOrden(idUsuario);
            frContent.NavigationService.Navigate(crearOrden);
        }

        private void btnVerOrden_Click(object sender, RoutedEventArgs e)
        {
            VerOrden verOrden = new VerOrden();
            frContent.NavigationService.Navigate(verOrden);
        }

        private void btnGestionar_Click(object sender, RoutedEventArgs e)
        {
            TomarOrden tomarOrden = new TomarOrden();
            frContent.NavigationService.Navigate(tomarOrden);
        }

        //Colocamos la factura para inicializar al entrar a Cajero
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (factura == null)
            {
                factura = new FacturarOrden();
            }
            frContent.NavigationService.Navigate(factura);
        }

        //Metodo para cerrar sesión

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (
                MessageBox.Show("Desea Cerrar Sesión?",
                "Cerrar sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information) == MessageBoxResult.Yes
               )
            {
                MainWindow login = new MainWindow();
                login.Show();
                this.Close();
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion
    }
}
