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


    }
}
