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

namespace Rapid_Plus.Reports
{
    /// <summary>
    /// Lógica de interacción para ReporteFechas.xaml
    /// </summary>
    public partial class ReporteFechas : Window
    {
        public ReporteFechas()
        {
            InitializeComponent();
        }

        void LimpiarFormulario()
        {
            dtpInicio.SelectedDate = null;
            dtpFin.SelectedDate = null;
        }

        bool ValidarFormulario() 
        {
            bool estado = true;
            string msj = null;

            if (dtpInicio.SelectedDate == null && dtpFin.SelectedDate != null)
            {
                estado = false;
                msj += "debe seleccionar Fecha de Inicio\n";
            }
            else if(dtpInicio.SelectedDate != null && dtpFin.SelectedDate == null) 
            {
                estado = false;
                msj += "Debe seleccionar Fecha de Finalizacion\n";
            }

            if(!estado)
            {
                MessageBox.Show($"Debe Cumplir: \n{msj}", "Validacion de Formulario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            return estado;
        }

        //Parametros para abrir el reporte
        private void btnGnRFecha_Click(object sender, RoutedEventArgs e)
        {
            //Asignar valores se ingresen o no
            DateTime? fechaInicio = dtpInicio.SelectedDate;
            DateTime? fechaFin = dtpFin.SelectedDate;

            rptFechas rpt = new rptFechas();
            vwRapidPlus vw = new vwRapidPlus();

            // Cargar el reporte
            rpt.Load(@"rptFechas.rpt");

            // Verificar fechas y asignar parámetros
            if (ValidarFormulario())
            {
                rpt.SetParameterValue("@FechaInicio", fechaInicio ?? (object)DBNull.Value);
                rpt.SetParameterValue("@FechaFin", fechaFin ?? (object)DBNull.Value);
            }
            else
            {
                return;
            }

            // Asignar el reporte al visor
            vw.crvReportRapidPlus.ViewerCore.ReportSource = rpt;
            vw.ShowDialog();

            //Limpiamos los campos
            LimpiarFormulario();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Verifica que el botón izquierdo está presionado
            {
                this.DragMove();
            }
        }
    }
}
