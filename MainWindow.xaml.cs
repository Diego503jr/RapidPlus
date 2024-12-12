using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Rapid_Plus.Models;
using System;
using System.ComponentModel;
using Rapid_Plus.Controllers;

namespace Rapid_Plus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region VARIABLES LOCALES
        // Variable para almacenar el estado de visibilidad de la contraseña
        private bool isPasswordVisible = false;
        private int idUsuario;
        //private int xClick, yClick;

        #endregion

        #region METODOS

        //Validar formulario
        bool ValidarFormulario() 
        {
            bool estado = true;
            string msj = null;

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                estado = false;
                msj += "Usuario vacio\n";
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                estado = false;
                msj += "Clave vacio\n";
            }

            if (!estado) 
            {
                MessageBox.Show("Debe cumplir estos campos:\n" + msj,
              "Validacion de formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return estado;
        }

        // Métodos para alternar la visibilidad de la contraseña
        void VisualizarClave()
        {
            // Cambia a TextBox para mostrar la contraseña en texto claro
            txtPassword.Visibility = Visibility.Collapsed; 
            txtPasswordVisible.Visibility = Visibility.Visible;
            txtPasswordVisible.Text = txtPassword.Password;
        }

        void OcultarClave()
        {
            // Cambia a TextBox para ocultar la contraseña en texto claro
            txtPassword.Visibility = Visibility.Visible;    
            txtPasswordVisible.Visibility = Visibility.Collapsed; 
            txtPassword.Password = txtPasswordVisible.Text;
        }

        #endregion

        #region EVENTOS BOTONES
        private void BtnCerrarLogin_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnIngresar_Click(object sender, RoutedEventArgs e)
        {

            if(ValidarFormulario())
            {
                //Recuperamos los datos
                UsuarioModel user = new UsuarioModel();
                user.Usuario = txtCorreo.Text;
                user.Clave = txtPassword.Password;

                idUsuario = await LoginController.Login(user);

                if(idUsuario > -1)
                {
                    this.Close();
                }
            }
        }

        private void btnVisualizar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisualizarClave();
        }

        private void btnVisualizar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OcultarClave();
        }

        #endregion

    }
}