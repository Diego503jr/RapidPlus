using Rapid_Plus.Controllers;
using Rapid_Plus.Models;
using Rapid_Plus.Models.Mesero;
using Rapid_Plus.Views.JefeDeCocina;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Rapid_Plus.Views.Mesero
{
    /// <summary>
    /// Lógica de interacción para TomarOrden.xaml
    /// </summary>
    public partial class TomarOrden : Page
    {
        public TomarOrden()
        {
            InitializeComponent();
            IniciarTemporizador();

        }

        #region VARIABLES LOCALES
        private int idMesa = 0;
        private int idOrden = 0;
        private int idplatillo = 0; //Datagrid de platillos
        private int idPlatilloOrden = 0; //Para datagrid platillos por orden
        private int idCategoria = 0;
        private int idDetalleOrden = 0;
        private int idEstadoMesa = 0;
        private bool agregando = false, editando = false;
        private DispatcherTimer timer;

        #endregion

        #region MÉTODOS PERSONALIZADOS
        //Muestra mesas asignadas (ocupadas)
        private void CargarNumeroMesa()
        {
            idEstadoMesa = 2;
            cmbMesa.ItemsSource = OrdenController.ObtenerMesas(idEstadoMesa);
            cmbMesa.DisplayMemberPath = "Mesa";
            cmbMesa.SelectedValuePath = "Mesa";
        }

        //Muestra categorías de platillos
        private void CargarCategorias()
        {
            using (var conDb = new SqlConnection(Properties.Settings.Default.DbRapidPlus))
            {
                conDb.Open();
                using (var command = new SqlCommand("SELECT IdCategoria, Categoria FROM Categoria", conDb))
                {
                    SqlDataReader dr = command.ExecuteReader();
                    var estados = new List<dynamic>();
                    while (dr.Read())
                    {
                        estados.Add(new { Id = dr.GetInt32(0), Categoria = dr.GetString(1) });
                    }

                    cmbPlatillo.ItemsSource = estados;
                }
            }
            cmbPlatillo.DisplayMemberPath = "Categoria";
            cmbPlatillo.SelectedValuePath = "Id";

        }
        private void IniciarTemporizador()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tik;
            timer.Start();
        }
        private void Timer_Tik(object sender, EventArgs e)
        {
            if(cmbMesa.SelectedIndex == -1)
            {
                CargarNumeroMesa();
            }
            CargarCategorias();
            MostrarOrden();
        }

        //Limpia objetos 
        private void LimpiarObjetos()
        {
            cmbMesa.SelectedIndex = -1;
            cmbPlatillo.SelectedIndex = -1;
            txtCantidad.Clear();
            idPlatilloOrden = -1;
            txbEstado.Text = null;
            txbOrden.Text = null;
            txbPlatillo.Text = null;
            dgPlatillos.DataContext = null;
            idplatillo = -1;
            idPlatilloOrden = -1;
            dgOrdenes.DataContext = null;

        }

        //Obtiene le número de mesa seleccionado en el combobox
        private int IdMesa()
        {

            if (cmbMesa.SelectedIndex != -1)
            {
                idMesa = (int)cmbMesa.SelectedValue;
            }
            else
            {
                idMesa = -1;
            }
            return idMesa;
        }

        //Valida campos completos del formulario
        private bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;
            if (string.IsNullOrEmpty(txbOrden.Text))
            {
                estado = false;
                mensaje += "Número de orden\n";
            }
            if (idplatillo == -1 && idPlatilloOrden == -1 && string.IsNullOrEmpty(txbPlatillo.Text))
            {
                estado = false;
                mensaje += "Platillo\n";
            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                estado = false;
                mensaje += "Cantidad";
            }

            if (!estado)
            {
                MessageBox.Show("Debe completar los campos:\n" + mensaje, "Validación de formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return estado;
        }
        private void MostrarOrden() 
        {
            idMesa = IdMesa();
            var detalle = DetalleOrdenController.ObtenerDetalleOrden(idMesa);
            var ordenes = OrdenController.MostrarOrdenesPorMesa(idMesa);

            if (detalle != null)
            {
                dgOrdenes.DataContext = ordenes;

                txbOrden.Text = detalle.IdOrden.ToString();
                txbEstado.Text = detalle.EstadoOrden;
            }
            else
            {
                txbOrden.Text = string.Empty;
            }
        }

        //Activa o desactiva campos y botones
        private void ControlAcciones()
        {
            //Click a Nuevo
            if(agregando)
            {
                btnGuardar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                cmbMesa.IsEnabled = true;
                cmbPlatillo.IsEnabled = true;
                txtCantidad.IsEnabled = true;
                dgPlatillos.IsEnabled = true;
                dgOrdenes.IsEnabled = false;


                btnEditar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnNuevo.IsEnabled = false;
            }
            if (editando )
            {
                dgOrdenes.IsEnabled = true;
                txtCantidad.IsEnabled = true;
                btnGuardar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                cmbMesa.IsEnabled = true;

                cmbPlatillo.IsEnabled = false;
                dgPlatillos.IsEnabled= false;
                btnNuevo.IsEnabled= false;
                btnEditar.IsEnabled= false;
                btnEliminar.IsEnabled= false;
            }
            if(!agregando && !editando)
            {
                btnNuevo.IsEnabled = true;
                btnEditar.IsEnabled = true;
                btnEliminar.IsEnabled =true;
                dgOrdenes.IsEnabled=true;
                cmbMesa.IsEnabled=true;

                txtCantidad.IsEnabled= false;
                dgPlatillos.IsEnabled = false;
                btnCancelar.IsEnabled = false;
                btnGuardar.IsEnabled=false;
                cmbPlatillo.IsEnabled=false;
            }
        }

        #endregion

        #region EVENTOS

        //Cargas por defecto
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarObjetos();
            CargarNumeroMesa();
            CargarCategorias();

            agregando = false;
            editando = false;
            ControlAcciones();

        }


        //Muestra elementos según categoria de platillo seleccionada
        private void cmbPlatillo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPlatillo.SelectedIndex != -1)
            {
                idCategoria = (int)cmbPlatillo.SelectedValue;
                var platillos = PlatilloController.MostrarPlatillos(idCategoria);
                if (platillos != null)
                {
                    dgPlatillos.DataContext = platillos;

                }
                else
                {
                    MessageBox.Show("No hay Platillos disponibles.");
                }
            }
            
        }

        //Muestra elementos según mesa seleccionada
        private void cmbMesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarOrden();
        }


        //Obtiene información de registros seleccionados
        private void dgPlatillos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlatilloModel platillo = (PlatilloModel)dgPlatillos.SelectedItem;
            if (platillo == null)
            {
                return;
            }
            txbPlatillo.Text = platillo.Platillo;
            idplatillo = platillo.PlatilloId;

        }

        private void dgOrdenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrdenesModel ordenes = (OrdenesModel)dgOrdenes.SelectedItem;
            if (ordenes == null)
            {
                return;
            }
            txtCantidad.Text = ordenes.Cantidad.ToString();
            txbPlatillo.Text = ordenes.NombrePlatillo;

            idPlatilloOrden = ordenes.IdPlatilloOrden;
            idDetalleOrden = ordenes.IdDetalleOrden;
            idOrden = ordenes.IdOrden;
        }

        //Evento se activa al desplegar el combobox, verifica si no existen elementos
        private void cmbMesa_DropDownOpened(object sender, EventArgs e)
        {
            //Si no existen elementos en el combobox
            if (cmbMesa.Items.Count  <= 0)
            {
                MessageBox.Show("No hay mesas asignadas", "Mesas", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //Valida que el ingreso sea solo texto
        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Validación para poder ingresar solo números
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        #endregion

        //BOTONES
        #region BOTONES
        //Nuevo detalleOrden
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            //Agregando
            agregando = true;
            editando = false;
            ControlAcciones();
            txtCantidad.Clear();
            dgOrdenes.SelectedIndex = -1;
            txbPlatillo.Text = null;
        }

        //Guardar el detalle de la orden
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = null;
            if (ValidarFormulario())
            {
                DetalleOrdenModel detalle = new DetalleOrdenModel();
                detalle.IdOrden = Convert.ToInt32(txbOrden.Text);
                detalle.Cantidad = Convert.ToInt32(txtCantidad.Text);
                detalle.IdEstado = 1;
                detalle.IdEstadoOrden = 1;
                detalle.IdPlatillo = idplatillo;
                detalle.IdPlatilloOrden = idPlatilloOrden;

                //Verifica si se está agregando o editando
                if (agregando)
                {
                    agregando = false;
                    editando = false;
                    ControlAcciones();

                    LimpiarObjetos();
                    idOrden = DetalleOrdenController.CrearDetalleOrden(detalle);
                    mensaje = "Orden creada con éxito";
                }
                else //Se está editando
                {
                    agregando = false;
                    editando = false;
                    ControlAcciones();
                    LimpiarObjetos();
                    idOrden = DetalleOrdenController.ActualizarDetalleOrden(detalle, idDetalleOrden);
                    mensaje = "Orden actualizada";
                }

                if (idOrden > 0)
                {
                    MessageBox.Show(mensaje, "Validación de formulario", MessageBoxButton.OK, MessageBoxImage.Information);
                    agregando = false;
                    editando = false;
                    ControlAcciones();
                }


            }
        }

        //Actualiza los datos de un detalle de orden
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            //Editando
            agregando = false;
            editando = true;
            ControlAcciones();

        }

        //Elimina un detalle de orden
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Verifica que los campos estén completos para eliminar
            if (idDetalleOrden > 0 && idOrden > 0 && idPlatilloOrden != -1)
            {
                if (MessageBox.Show("¿Desea eliminar el detalle de la orden?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    DetalleOrdenController.EliminarDetalleOrden(idDetalleOrden, idOrden);
                    MessageBox.Show("Detalle de orden eliminado con éxito.", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

                    LimpiarObjetos();
                    agregando = false;
                    editando = false;
                    ControlAcciones();
                }
                else
                {
                    agregando = false;
                    editando = false;
                    ControlAcciones();
                    LimpiarObjetos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un detalle de orden válido para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                agregando = false;
                editando = false;
                ControlAcciones();
            }
        }

        //Cancela la operación
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea cancelar la operación", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarObjetos();
                agregando = false;
                editando = false;
                ControlAcciones();

            }
        }
        #endregion



    }
}
