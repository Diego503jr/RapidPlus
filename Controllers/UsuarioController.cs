using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Incluimos las librerias
using System.Data;
using System.Data.SqlClient;
using Rapid_Plus.Models;
using System.Windows;
using System.Data.Common;

namespace Rapid_Plus.Controllers
{
    class UsuarioController
    {
        private static string conexion = Properties.Settings.Default.DbRapidPlus;

        //Metodo para leer Usuario
        public static async Task<List<UsuarioModel>> MostrarUsuarios() 
        {
            List<UsuarioModel> lstUsuarios = new List<UsuarioModel>();

            try
            {
                using (var conDb = new SqlConnection(conexion)) 
                {
                    await conDb.OpenAsync();

                    using (var command = new SqlCommand("SPMOSTRARUSUARIOS", conDb)) 
                    { 
                        command.CommandType = CommandType.StoredProcedure;

                        using (DbDataReader dr = await command.ExecuteReaderAsync()) 
                        {
                            lstUsuarios = await LeerUsuariosAsync(dr);

                            if(await dr.NextResultAsync())
                            {
                                await LeerTelefonosAsync(dr, lstUsuarios);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar mostrar los registros" + ex.Message, "Validacion",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstUsuarios;
        }

        // Método auxiliar para leer usuarios
        private static async Task<List<UsuarioModel>> LeerUsuariosAsync(DbDataReader dr)
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();

            while (await dr.ReadAsync())
            {
                UsuarioModel usuario = new UsuarioModel
                {
                    UsuarioId = Convert.ToInt32(dr["IdUsuario"]),
                    Usuario = dr["Usuario"].ToString(),
                    Clave = dr["Clave"].ToString(),
                    Nombres = dr["NombreUsuario"].ToString(),
                    Apellidos = dr["ApellidoUsuario"].ToString(),
                    Rol = dr["Rol"].ToString(),
                    DUI = Convert.ToInt32(dr["DUI"]),
                    Sexo = dr["Sexo"].ToString(),
                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                    Estado = dr["Estado"].ToString()
                };
                usuarios.Add(usuario);
            }

            return usuarios;
        }

        // Método auxiliar para leer teléfonos
        private static async Task LeerTelefonosAsync(DbDataReader dr, List<UsuarioModel> usuarios)
        {
            while (await dr.ReadAsync())
            {
                int usuarioId = Convert.ToInt32(dr["IdUsuario"]);
                string telefono1 = dr["Telefono1"].ToString();
                string telefono2 = dr["Telefono2"].ToString();

                // Buscar el usuario y asignarle los teléfonos
                var usuario = usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
                if (usuario != null)
                {
                    usuario.Telefono1 = telefono1;
                    usuario.Telefono2 = telefono2;
                }
            }
        }

        //Metodo para crear Usuario
        public static async Task<int> CrearUsuario(UsuarioModel user, int idEstado) 
        {
            int res = -1;
            try
            {
                using (var conDb = new SqlConnection(conexion)) 
                {
                    await conDb.OpenAsync();

                    using (var command = conDb.CreateCommand()) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPCREARUSUARIO";

                        command.Parameters.AddWithValue("@Usuario", user.Usuario);
                        command.Parameters.AddWithValue("@Clave", user.Clave);
                        command.Parameters.AddWithValue("@Nombres", user.Nombres);
                        command.Parameters.AddWithValue("@Apellidos", user.Apellidos);
                        command.Parameters.AddWithValue("@IdRol", user.RolId);
                        command.Parameters.AddWithValue("@DUI", user.DUI);
                        command.Parameters.AddWithValue("@IdSexo", user.SexoId);
                        command.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);
                        command.Parameters.AddWithValue("@IdEstado", idEstado);
                        command.Parameters.AddWithValue("@Telefono1", user.Telefono1);
                        command.Parameters.AddWithValue("@Telefono2", user.Telefono2);

                        res = await command.ExecuteNonQueryAsync();
                        //Si la res es menor a 0 es porque ya existe el registro
                        if (res < 0) 
                        {
                            throw new Exception(" Ya existe este usuario");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar crear los registros" + ex.Message, "Validacion",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }

        //Metodo para editar Usuario
        public static async Task<int> EditarUsuario(UsuarioModel user, int idUsuario) 
        {
            int res = -1;

            try
            {
                using (var conDb = new SqlConnection(conexion))
                {
                    await conDb.OpenAsync();

                    using (var command = conDb.CreateCommand()) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPACTUALIZARUSUARIO";

                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        command.Parameters.AddWithValue("@Usuario", user.Usuario);
                        command.Parameters.AddWithValue("@Clave", user.Clave);
                        command.Parameters.AddWithValue("@Nombres", user.Nombres);
                        command.Parameters.AddWithValue("@Apellidos", user.Apellidos);
                        command.Parameters.AddWithValue("@IdRol", user.RolId);
                        command.Parameters.AddWithValue("@DUI", user.DUI);
                        command.Parameters.AddWithValue("@IdSexo", user.SexoId);
                        command.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);
                        command.Parameters.AddWithValue("@IdEstado", user.EstadoId);
                        command.Parameters.AddWithValue("@Telefono1", user.Telefono1);
                        command.Parameters.AddWithValue("@Telefono2", user.Telefono2);

                        res = await command.ExecuteNonQueryAsync();

                        //Si la res es menor a 0 es porque ya existe el registro
                        if (res < 0)
                        {
                            throw new Exception(" Ya existe este usuario");
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ocurrio un error al intentar editar los registros" + ex.Message, "Validacion",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }

        //Metodo para eliminar Usuario
        public static async Task<int> EliminarUsuario(int idUsuario, int idEstado) 
        { 
            int res = -1;

            try
            {
                using (var conDB = new SqlConnection(conexion)) 
                {
                    await conDB.OpenAsync();

                    using (var command = conDB.CreateCommand()) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SPELIMINARUSUARIO";

                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        command.Parameters.AddWithValue("@IdEstado", idEstado);

                        res = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ocurrio un error al intentar eliminar los registros" + ex.Message, "Validacion",
                           MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }
    }
}
