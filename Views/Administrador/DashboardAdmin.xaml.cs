using Rapid_Plus.Reports;
using Rapid_Plus.Views.Cajero;
using Rapid_Plus.Views.JefeDeCocina;
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

namespace Rapid_Plus.Views.Administrador
{
    /// <summary>
    /// Lógica de interacción para DashboardAdmin.xaml
    /// </summary>
    public partial class DashboardAdmin : Window
    {

        #region DECLARACION DE VARIABLES Y CLASES
        private Contactos contacto;
        int idUsuario = -1;

        #endregion

        public DashboardAdmin(int usuarioId)
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

        #region METODO PARA MOVER VENTANA

        private void cDashboardA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        #endregion

        #region METODOS FORMULARIO

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (contacto == null) // Inicializar solo si es necesario
            {
                contacto = new Contactos();
            }

            frContent.NavigationService.Navigate(contacto);
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            frContent.NavigationService.Navigate(menu);
        }

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (contacto == null) // Inicializar solo si es necesario
            {
                contacto = new Contactos();
            }
            frContent.NavigationService.Navigate(contacto);
        }

        private void btnMas_Click(object sender, RoutedEventArgs e)
        {
            Configuraciones configuracion = new Configuraciones();
            frContent.NavigationService.Navigate(configuracion);
        }

        private void btnOrdenesFinal_Click(object sender, RoutedEventArgs e)
        {
            VerOrdenesT verOrdenes = new VerOrdenesT();
            frContent.NavigationService.Navigate(verOrdenes);
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            ParametroReporte prReport = new ParametroReporte();
            prReport.ShowDialog();
        }

        private void btnTomarOrden_Click(object sender, RoutedEventArgs e)
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

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            GestionClientes clientes = new GestionClientes();
            frContent.NavigationService.Navigate(clientes);
        }

        private void btnOrdenes_Click(object sender, RoutedEventArgs e)
        {
            EstadoOrden estadoOrden = new EstadoOrden();
            frContent.NavigationService.Navigate(estadoOrden);
        }

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            FacturarOrden factura = new FacturarOrden();
            frContent.NavigationService.Navigate(factura);
        }

        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (
                MessageBox.Show("Desea Cerrar Sesión?",
                "Cerrar sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                MainWindow login = new MainWindow();
                login.Show();
                this.Close();
            }
        }

        #endregion

    }
}
