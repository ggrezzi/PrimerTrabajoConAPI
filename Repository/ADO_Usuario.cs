using System.Data.SqlClient;
using static PrimerTrabajoConAPI.Controllers.UsuarioController;
using PrimerTrabajoConAPI.Models;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace PrimerTrabajoConAPI.Repository
{
    public class ADO_Usuario
    {
        public static List<Usuario> TraerUsuario()
        //Metodo al que se le ingresa un UserNAme y devuelve el objeto Usuario correspondiente (A)
        {
            var listaUsuarios = new List<Usuario>() ;
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var usuario = new Usuario();
                            usuario.Id = (int) dr.GetInt64(0);
                            usuario.Nombre = dr.GetString(1);
                            usuario.Apellido = dr.GetString(2);
                            usuario.NombreUsuario = dr.GetString(3);
                            usuario.Password = dr.GetString(4);
                            usuario.Email = dr.GetString(5);
                            listaUsuarios.Add(usuario);
                        }
                    }
                }
                connection.Close();
                return listaUsuarios;
            }
        }
        public static Usuario TraerUsuario(int id)
        //Metodo al que se le ingresa un UserNAme y devuelve el objeto Usuario correspondiente (A)
        {
            var usuario = new Usuario();
            var listaUsuarios = new List<Usuario>();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario WHERE id=" + id, connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usuario.Id = (int)dr.GetInt64(0);
                            usuario.Nombre = dr.GetString(1);
                            usuario.Apellido = dr.GetString(2);
                            usuario.NombreUsuario = dr.GetString(3);
                            usuario.Password = dr.GetString(4);
                            usuario.Email = dr.GetString(5);
                        }
                    }
                }
                connection.Close();
                return usuario;
            }
        }

        //Metodo para modificar la info de un usuario - Se puede modificar todo menos el ID
        public static string  ModificarUsuario(Usuario usu)
        {

            string respuesta = string.Empty;
            bool validEmail;
            //coroboro que el email tenga un formato valido
            try
            {
                MailAddress m = new MailAddress(usu.Email);
                validEmail = true;

            }
            catch (FormatException)
            {
                validEmail = false;
            }
            if (validEmail != true)
            {
                respuesta = "wrong email format";
                return respuesta;
            }
            bool existe = false;

            //Corroboro que el ID del usuario a modificar sea valido
            var usuario = new Usuario();
            var listaUsuarios = new List<Usuario>();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario WHERE id=" + usu.Id, connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        existe = true;
                    }
                }
                connection.Close();
            }

            if (existe)
                //Habiendo corroborado el ID, modifico todo.
            {
                connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
                var query = "UPDATE Usuario Set Nombre=@nombre, Apellido=@apellido, " +
                                " nombreUsuario=@nombreUsuario, Contraseña=@password, Mail=@mail WHERE id=@id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    var parametroId = new SqlParameter();
                    parametroId.ParameterName = "id";
                    parametroId.SqlDbType = System.Data.SqlDbType.Int;
                    parametroId.Value = usu.Id;
                    var parametroNombre = new SqlParameter();
                    parametroNombre.ParameterName = "nombre";
                    parametroNombre.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroNombre.Value = usu.Nombre;
                    var parametroApellido = new SqlParameter();
                    parametroApellido.ParameterName = "apellido";
                    parametroApellido.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroApellido.Value = usu.Apellido;
                    var parametroNomUsuario = new SqlParameter();
                    parametroNomUsuario.ParameterName = "nombreUsuario";
                    parametroNomUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroNomUsuario.Value = usu.NombreUsuario;
                    var parametroPassword = new SqlParameter();
                    parametroPassword.ParameterName = "password";
                    parametroPassword.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroPassword.Value = usu.Password;
                    var parametroMail = new SqlParameter();
                    parametroMail.ParameterName = "mail";
                    parametroMail.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroMail.Value = usu.Email;

                    connection.Open();
                    using (SqlCommand comandoUpdate = new SqlCommand(query, connection))
                    {
                        comandoUpdate.Parameters.Add(parametroId);
                        comandoUpdate.Parameters.Add(parametroNombre);
                        comandoUpdate.Parameters.Add(parametroApellido);
                        comandoUpdate.Parameters.Add(parametroNomUsuario);
                        comandoUpdate.Parameters.Add(parametroPassword);
                        comandoUpdate.Parameters.Add(parametroMail);
                        comandoUpdate.ExecuteNonQuery();
                        respuesta = "usuario actualizado";
                    }
                    connection.Close();
                }
                respuesta = "usuario modificado correctamente";
            }
            else
            {
                respuesta = "usuario no encontrado";
            }
            return respuesta;
        }
    }
}
