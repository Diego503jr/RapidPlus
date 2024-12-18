using Rapid_Plus.Controllers;
using Rapid_Plus.Models.Mesero;
using Rapid_Plus.Views.JefeDeCocina;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Rapid_Plus.Views.Mesero
{
    /// <summary>
    /// Lógica de interacción para VerOrden.xaml
    /// </summary>
    public partial class VerOrden : Page
    {
        public VerOrden()
        {
            InitializeComponent();
            IniciarTemporizador();
        }

        #region DECLARACIÓN DE VARIABLES LOCALES
        private DispatcherTimer timer;
        private int idMesa = 0;
        private int idEstadoOrden = 0;
        #endregion

        #region MÉTODOS PERSONALIZADOS
        //Lista las ordenes en un datagrid
        void MostrarOrdenes()
        {
            int idEstadoOrden = 1; //Estado pendiente
            dgOrdenes.DataContext = OrdenController.MostrarOrdenes(idEstadoOrden);
        }
        
        //Carga las mesas en el combobox
        private void CargarNumeroMesa()
        {
            using (var conDb = new SqlConnection(Properties.Settings.Default.DbRapidPlus))
            {
                conDb.Open();
                using (var command = new SqlCommand("SPMOSTRARMESASPENDIENTES", conDb))
                {
                    SqlDataReader dr = command.ExecuteReader();
                    var mesas = new List<dynamic>();
                    while (dr.Read())
                    {
                        mesas.Add(new { IdMesa = dr.GetInt32(0), Mesa = dr.GetInt32(1) });
                    }

                    cmbMesa.ItemsSource = mesas;
                }
            }

            //Define que campos mostrar
            cmbMesa.DisplayMemberPath = "Mesa";
            cmbMesa.SelectedValuePath = "IdMesa";
        }

        //Refresca la página
        private void IniciarTemporizador()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tik;
            timer.Start();
        }

        //Limpia los objetos de la página
        private void limpiarObjetos()
        {
            cmbMesa.SelectedIndex = -1;
        }
        #endregion

        #region EVENTOS

        //Carga por 'defecto'
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            limpiarObjetos();   
            MostrarOrdenes();
            CargarNumeroMesa();
        }

        //Refrescar página
        private void Timer_Tik(object sender, EventArgs e)
        {
            if (cmbMesa.SelectedIndex == -1)
            {
                //Muestra todas las ordenes
                MostrarOrdenes();
            }
            else
            {
                //Muestra las ordenes según la mesa
                MostrarOrden();
            }
        }

        //Llamada la método con filtro de ordenes según mesa
        private void cmbMesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarOrden();
        }

        //Muestra ordenes según selección del combobox de mesa
        private void MostrarOrden()
        {
            idMesa = IdMesa();
            idEstadoOrden = 1;
            var detalle = DetalleOrdenController.ObtenerDetalleOrden(idMesa);
            var ordenes = OrdenController.MostrarOrdenesPorMesa(idMesa, idEstadoOrden);

            if (detalle != null)
            {
                dgOrdenes.DataContext = ordenes;
            }
            else
            {
                dgOrdenes.DataContext = null;
            }
        }

        //Obtiene el número de mesa seleccionado en el combobox
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

        #region BOTONES

        //Limpia objetos y muestra todas las ordenes pendientes
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarObjetos();
            MostrarOrdenes();
        }
        #endregion
    }
    #endregion




}
