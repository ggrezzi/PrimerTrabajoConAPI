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
        private string _contraseña;
        private string _mail;


        //Properties

        public long id { get { return _idUsuario; } set { _idUsuario = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string NombreUsuario { get { return _nombreUsuario; } set { _nombreUsuario = value; } }
        public string Contraseña { get { return _contraseña; } set { _contraseña = value; } }
        public string Mail { get { return _mail; } set { _mail = value; } }

        //Constructor por defecto
        public Usuario()
        {
            _idUsuario = 0;
            _nombre = string.Empty;
            _apellido = string.Empty;
            _nombreUsuario = string.Empty;
            _contraseña = string.Empty;
            _mail = string.Empty; 

        }

        //Constructor con todos los datos del objeto Usuario
        public Usuario(long idUsuario, string nombre, string apellido, string nombreUsuario, string password, string email)
        {
            _idUsuario = idUsuario;
            _nombre = nombre;
            _apellido = apellido;
            _nombreUsuario = nombreUsuario;
            _contraseña = password;
            _mail = email;
        }


    }
}
