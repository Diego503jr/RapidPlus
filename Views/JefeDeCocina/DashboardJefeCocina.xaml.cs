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

namespace Rapid_Plus.Views.JefeDeCocina
{
    /// <summary>
    /// Lógica de interacción para DashboardJefeCocina.xaml
    /// </summary>
    public partial class DashboardJefeCocina : Window
    {
        public DashboardJefeCocina()
        {
            InitializeComponent();
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

        #region EVENTOS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           EstadoOrden estadoOrden = new EstadoOrden();
            frContent.NavigationService.Navigate(estadoOrden);
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar Sesión?", "Cerrar sesión", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                MainWindow login = new MainWindow();
                login.Show();
                this.Close();
            }
        }
        #endregion

        #region MÉTODO PARA MOVER VENTANA
        private void cDashboardJC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        #endregion

    }
}
