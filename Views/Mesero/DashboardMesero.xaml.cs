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

namespace Rapid_Plus.Views.Mesero
{
    /// <summary>
    /// Lógica de interacción para DashboardMesero.xaml
    /// </summary>
    public partial class DashboardMesero : Window
    {

        #region Instancia de las páginas

        //Crear objeto de cada page
        private TomarOrden tomarOrden;
        private VerOrden verOrden;
        private CrearOrden crearOrden;
        private GestionClientes clientes;
        #endregion

        public DashboardMesero(int usuarioId)
        {
            InitializeComponent();
            //Id de Usuario logueado
            crearOrden = new CrearOrden(usuarioId);
        }

        #region NAVEGACIÓN HACIA LAS PÁGINAS
        //Abrir páginas con NavigationService
        private void btnTomarOrden_Click(object sender, RoutedEventArgs e)
        {
            frContent.NavigationService.Navigate(crearOrden);
        }

        private void btnVerOrden_Click(object sender, RoutedEventArgs e)
        {
            if(verOrden == null)
            {
                verOrden = new VerOrden();
            }
            frContent.NavigationService.Navigate(verOrden);
        }

        private void btnGestionar_Click(object sender, RoutedEventArgs e)
        {
            if(tomarOrden == null)
            {
                tomarOrden = new TomarOrden();
            }
            frContent.NavigationService.Navigate(tomarOrden);
        }
        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            if (clientes == null)
            {
                clientes = new GestionClientes();
            }
            frContent.NavigationService.Navigate(clientes);
        }
        #endregion


        #region EVENTOS
        //Cerrar Sesión
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Desea Cerrar Sesión?", 
                "Cerrar sesión", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                MainWindow login = new MainWindow();
                login.Show();
                this.Close();
            }

        }

        //Cerrar Ventana
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Página inicial por defecto
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (verOrden == null)
            {
                verOrden = new VerOrden();
            }
            frContent.NavigationService.Navigate(verOrden);
        }

        //Minimizar ventana
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion


    }
}
