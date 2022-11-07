using System.Data.SqlClient;
using static PrimerTrabajoConAPI.Controllers.UsuarioController;
using PrimerTrabajoConAPI.Models;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

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
                            usuario.id = (int) dr.GetInt64(0);
                            usuario.Nombre = dr.GetString(1);
                            usuario.Apellido = dr.GetString(2);
                            usuario.NombreUsuario = dr.GetString(3);
                            usuario.Contraseña = dr.GetString(4);
                            usuario.Mail = dr.GetString(5);
                            listaUsuarios.Add(usuario);
                        }
                    }
                }
                connection.Close();
                return listaUsuarios;
            }
        }
        public static Usuario TraerUsuario(string userName)
        //Metodo al que se le ingresa un UserNAme y devuelve el objeto Usuario correspondiente (A)
        {
            var usuario = new Usuario();
            var listaUsuarios = new List<Usuario>();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario WHERE nombreUsuario='" + userName+"'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usuario.id = (int)dr.GetInt64(0);
                            usuario.Nombre = dr.GetString(1);
                            usuario.Apellido = dr.GetString(2);
                            usuario.NombreUsuario = dr.GetString(3);
                            usuario.Contraseña = dr.GetString(4);
                            usuario.Mail = dr.GetString(5);
                        }
                    }
                }
                connection.Close();
                return usuario;
            }
        }

        //Metodo para modificar la info de un usuario - Se puede modificar todo menos el ID
        public static bool  ModificarUsuario(Usuario usu)
        {

            string respuesta = string.Empty;
            bool validEmail;
            //coroboro que el email tenga un formato valido
            validEmail = IsMailValid(usu.Mail);

            if (validEmail != true)
            {
                return false;
            }
            bool existe = false;

            //Corroboro que el ID del usuario a modificar sea valido
            var usuario = new Usuario();
            var listaUsuarios = new List<Usuario>();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario WHERE id=" + usu.id, connection);
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
                var query = "UPDATE Usuario Set Nombre=@nombre, Apellido=@apellido, " +
                                " nombreUsuario=@nombreUsuario, Contraseña=@password, Mail=@mail WHERE id=@id";
                ModificarCrearUsuario(usu, query);
                return true;
            }
            else
            {
                return false;
            }
            
        }


        public static Usuario IniciarSesion(string userName, string password)
        //MEtodo al que se le ingresa un userNAme y un password y devuelve un obj Usuario
        //con toda la info del mismo o uno vacio si hay un error en alguno de los valores
        {
            var usuario = new Usuario();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario where NombreUsuario='" + userName + "' and Contraseña='" + password + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            long id = dr.GetInt64(0);
                            usuario = new Usuario(dr.GetInt64(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                        }
                    }
                    else
                    {
                        usuario = new Usuario();
                    }
                }
                connection.Close();
                return usuario;
            }
        }


        //Metodo para borrar un usuario de la DB
        public static bool  EliminarUsuario(int id)
        {
            bool valido = false;
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario where id=" + id , connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        valido = true;
                    }
                }
                connection.Close();
                if (valido)
                {
                    var query = "Delete Usuario WHERE id=" + id;
                    using (SqlConnection connectionDelete = new SqlConnection(connectionString))
                    {
                        connectionDelete.Open();
                        using (SqlCommand comandoUpdate = new SqlCommand(query, connectionDelete))
                        {
                            comandoUpdate.ExecuteNonQuery();
                        }
                        connectionDelete.Close();
                    }
                }
            }
            return valido;
        }

        //MEtodo para crear un usuario en la DB
        public static bool CrearUsuario(Usuario u)
        {
            bool valid = false;
            Usuario test = IniciarSesion(u.NombreUsuario, u.Contraseña);
            if (test.id==0)
            {
                valid = IsMailValid(u.Mail);
            }
            else
            {
                return false;
            }
            var tempUser = TraerUsuario(u.NombreUsuario);
            if (tempUser.id!=0)
            {
                return false;
            }
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario WHERE =mail'" + u.Mail + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        valid = false;
                    }
                }
                connection.Close();
            }
            if (valid)
            {
                if (u.Nombre.Length>0 && u.NombreUsuario.Length>0 && u.Apellido.Length>0 && u.Contraseña.Length>0)
                {
                    var query = "INSERT into Usuario values(@nombre, @apellido,@nombreUsuario, @password, @mail )";
                    ModificarCrearUsuario(u, query);
                    return true;

                }
                else
                { return false; }

            }
            else
            { return false; }
        }

        //MEtodo para modificar un usuario
        private static void ModificarCrearUsuario(Usuario usu, string query)
        {
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                var parametroId = new SqlParameter();
                parametroId.ParameterName = "id";
                parametroId.SqlDbType = System.Data.SqlDbType.Int;
                parametroId.Value = usu.id;
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
                parametroPassword.Value = usu.Contraseña;
                var parametroMail = new SqlParameter();
                parametroMail.ParameterName = "mail";
                parametroMail.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroMail.Value = usu.Mail;

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
                }
                connection.Close();
            }
        }

        //Metodo interno que uso para validar si el email ingresado es valido
        private static bool IsMailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return  true;

            }
            catch (FormatException)
            {
                return  false;
            }
        }


    }
}
