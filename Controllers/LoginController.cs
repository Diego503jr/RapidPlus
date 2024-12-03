using Rapid_Plus.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Data.Common;
using Rapid_Plus.Views.Administrador;
using Rapid_Plus.Views.Cajero;
using Rapid_Plus.Views.JefeDeCocina;
using Rapid_Plus.Views.Mesero;

namespace Rapid_Plus.Controllers
{
    class LoginController
    {
        private static string conexion = Properties.Settings.Default.DbRapidPlus;

        public static int Login(UsuarioModel user) 
        {
            int idusuario = -1, idrol = -1;

            try
            {
                using (var conDB = new SqlConnection(conexion))
                {
                    conDB.Open();

                    using (var command = conDB.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPENCONTRARUSUARIO";

                        command.Parameters.AddWithValue("@USUARIO", user.Usuario);
                        command.Parameters.AddWithValue("@CLAVE", user.Clave);

                        // Obtiene los valores de salida
                        using (DbDataReader ddr = command.ExecuteReader())
                        {
                            if (ddr.HasRows)
                            {
                                while (ddr.Read())
                                {
                                    idusuario = int.Parse(ddr["IdUsuario"].ToString());
                                    idrol = int.Parse(ddr["IdRol"].ToString());

                                    // Verifica si el usuario fue encontrado (si userID y tipoUsuario no son -1)
                                    if (idusuario >= 0)
                                    {
                                        // Según el tipo de usuario, abre el dashboard correspondiente y pasa el ID de usuario
                                        switch (idrol)
                                        {
                                            case 1:
                                                DashboardAdmin dashboardAdmin = new DashboardAdmin(idusuario);
                                                dashboardAdmin.Show();
                                                break;
                                            case 2:
                                                DashboardMesero dashboardMesero = new DashboardMesero(idusuario);
                                                dashboardMesero.Show();
                                                break;
                                            case 3:
                                                DashboardCajero dashboardCajero = new DashboardCajero(idusuario);
                                                dashboardCajero.Show();
                                                break;
                                            case 4:
                                                DashboardJefeCocina dashboardJefeCocina = new DashboardJefeCocina();
                                                dashboardJefeCocina.Show();
                                                break;
                                            default:
                                                MessageBox.Show("Error inesperado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                                break;
                                        }
                                        return idusuario;
                                    }
                                }
                            }
                            else
                            {
                                        MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ocurrio un error al intentar iniciar sesion " + ex.Message, "Validacion",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
                return idusuario;
        }
    }
}
