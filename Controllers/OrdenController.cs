using Rapid_Plus.Models.Mesero;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Rapid_Plus.Models;
using Rapid_Plus.Views.JefeDeCocina;

namespace Rapid_Plus.Controllers
{
    internal class OrdenController
    {
        private static string conexion = Properties.Settings.Default.DbRapidPlus;

        //CREAR ORDEN
        public static int CrearOrden(OrdenesModel orden)
        {
            int res = -1;
            try
            {
                using (var conDb = new SqlConnection(conexion))
                {
                    conDb.Open();

                    using (var command = conDb.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPCREARORDEN";

                        command.Parameters.AddWithValue("@IdCliente", orden.IdCliente);
                        command.Parameters.AddWithValue("@IdUsuario", orden.IdUsuario);
                        command.Parameters.AddWithValue("@FechaOrden", orden.FechaOrden);
                        command.Parameters.AddWithValue("@Total", orden.Total);
                        command.Parameters.AddWithValue("@IdMesa", orden.Mesa);
                        command.Parameters.AddWithValue("@IdEstadoOrden", orden.IdEstadoOrden);

                        res = command.ExecuteNonQuery();
                        if (res <= 0)
                        {
                            throw new Exception("El cliente ha sido asignado a otra mesa.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Validación orden",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }

        //MOSTRAR LISTADO DE ORDENES FILTRANDO POR ESTADO (LISTO/ PENDIENTE/ CANCELADO)
        public static List<OrdenesModel> MostrarOrdenes(int? IdEstado = null)
        {
            List<OrdenesModel> lstOrdenes = new List<OrdenesModel>();

            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPMOSTRARORDENES";
                        if (IdEstado.HasValue)
                        {
                            command.Parameters.AddWithValue("@ESTADO", IdEstado.Value);
                        }
                        else
                        {
                            // Si no se pasa un valor, asegurarse de que el parámetro sea NULL
                            command.Parameters.AddWithValue("@ESTADO", DBNull.Value);
                        }
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorrer el dataReader
                            while (dr.Read())
                            {
                                OrdenesModel ordenes = new OrdenesModel();
                                ordenes.IdOrden = int.Parse(dr["IDORDEN"].ToString());
                                ordenes.Cantidad = int.Parse(dr["CANTIDAD"].ToString());
                                ordenes.NombrePlatillo = dr["PLATILLO"].ToString();
                                ordenes.Mesa = int.Parse(dr["MESA"].ToString()); //Número de mesa
                                ordenes.EstadoOrden = dr["ESTADOORDEN"].ToString();

                                //Agregar a la lista inicial
                                lstOrdenes.Add(ordenes);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar mostrar las ordenes:" + ex.Message, "Validaccion ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstOrdenes;

        }

       
        //MOSTRAR LISTADO DE ORDENES FILTRANDO POR MESA
        public static List<OrdenesModel> MostrarOrdenesPorMesa(int idMesa, int? IdEstadoOrden = null)
        {
            List<OrdenesModel> lstOrdenes = new List<OrdenesModel>();

            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    
                    using (var command = con.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPMOSTRARORDENESPORMESA";
                        command.Parameters.AddWithValue("@IDMESA", idMesa);
                        if (IdEstadoOrden.HasValue)
                        {
                            command.Parameters.AddWithValue("@IDESTADOORDEN", IdEstadoOrden.Value);
                        }
                        else
                        {
                            // Si no se pasa un valor, asegurarse de que el parámetro sea NULL
                            command.Parameters.AddWithValue("@IDESTADOORDEN", DBNull.Value);
                        }
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            //Recorrer el dataReader
                            while (dr.Read())
                            {
                                OrdenesModel ordenes = new OrdenesModel();
                                ordenes.IdOrden = int.Parse(dr["IDORDEN"].ToString());
                                ordenes.IdPlatilloOrden = int.Parse(dr["IDPLATILLOORDEN"].ToString());
                                ordenes.IdDetalleOrden = int.Parse(dr["IDDETALLEORDEN"].ToString());
                                ordenes.NombrePlatillo = dr["PLATILLO"].ToString();
                                ordenes.Orden = dr["DESCRIPCION"].ToString();
                                ordenes.IdMesa = int.Parse(dr["MESA"].ToString());
                                ordenes.Mesa = int.Parse(dr["NUMMESA"].ToString());
                                ordenes.Cantidad = int.Parse(dr["CANTIDAD"].ToString());
                                ordenes.EstadoOrden = dr["ESTADOORDEN"].ToString();

                                //Agregar a la lista inicial
                                lstOrdenes.Add(ordenes);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar mostrar las ordenes:" + ex.Message, "Validaccion ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstOrdenes;

        }
        //Obtener mesas según esatado (libres u ocupadas)
        internal static List<MesasModel> ObtenerMesas(int idEstadoMesa)
        {
            List<MesasModel> mesas = new List<MesasModel>();
            try
            {
                using (var con = new SqlConnection(conexion))
                {
                    con.Open();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPMOSTRARMESAESTADO";
                        command.Parameters.AddWithValue("@IDESTADO", idEstadoMesa);
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mesas.Add(new MesasModel
                                {
                                    Mesa = reader.GetInt32(0)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros de mesas: " + ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return mesas;
        }
    }
}
