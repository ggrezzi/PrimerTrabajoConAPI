using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PrimerTrabajoConAPI.Models
{
    public class Usuario
    {
        private long _idUsuario;
        private string _nombre;
        private string _apellido;
        private string _nombreUsuario;
        private string _password;
        private string _email;


        //Properties

        public long Id { get { return _idUsuario; } set { _idUsuario = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string NombreUsuario { get { return _nombreUsuario; } set { _nombreUsuario = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Email { get { return _email; } set { _email = value; } }

        //Constructor por defecto
        public Usuario()
        {
            _idUsuario = 0;
            _nombre = string.Empty;
            _apellido = string.Empty;
            _nombreUsuario = string.Empty;
            _password = string.Empty;
            _email = string.Empty; 

        }

        //Constructor con todos los datos del objeto Usuario
        public Usuario(long idUsuario, string nombre, string apellido, string nombreUsuario, string password, string email)
        {
            _idUsuario = idUsuario;
            _nombre = nombre;
            _apellido = apellido;
            _nombreUsuario = nombreUsuario;
            _password = password;
            _email = email;
        }


        /*
        public long GetId ()
            //Devuelve ID
        {
            return _idUsuario;
        }
        public String GetName()
            //Devuelve el Nombre del usuario
        {
            return _nombre;
        }
        public String GetApellido()
            //Devuelve el Apellido del Usuario
        {
            return _apellido;
        }
        public String GetEmail()
            //Devuelve el Email del usuario
        {
            return _email;
        }
        public String GetNombreUsuario()
            //Devuelve el userName
        {
            return _nombreUsuario;
        }
        public  Usuario TraerUsuario(string nombreUsuario)
            //Metodo al que se le ingresa un UserNAme y devuelve el objeto Usuario correspondiente (A)
        {
            var usuario = new Usuario();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario where NombreUSuario ='"+nombreUsuario+"'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usuario = new Usuario(dr.GetInt64(0),dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
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

        public Usuario TraerUsuario(int idUsuario)
        //Metodo al que se le ingresa un UserNAme y devuelve el objeto Usuario correspondiente (A)
        {
            var usuario = new Usuario();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario where id ='" + idUsuario + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
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


        public string GetPassword()
            //MEtodo que devuelve el password del usuario
        {
            return _password;
        }

        public Usuario InicioDeSesion (string userName, string password)
            //MEtodo al que se le ingresa un userNAme y un password y devuelve un obj Usuario
            //con toda la info del mismo o uno vacio si hay un error en alguno de los valores
        {
            var usuario = new Usuario();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from Usuario where NombreUsuario='"+userName+ "' and Contraseña='"+password+"'", connection);
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
        }*/
    }
}
