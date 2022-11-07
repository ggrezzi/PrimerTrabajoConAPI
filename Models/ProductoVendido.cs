using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PrimerTrabajoConAPI.Models
{
    public class ProductoVendido
    {
        private int _id;
        private int _idVenta;
        private int _cantidadVendida;
        private int _idProducto;

        //Properties

        public int Id { get { return _id; } set { _id = value; } }
        public int IdVenta { get { return _idVenta; } set { _idVenta = value; } }
        public int CantidadVendida { get { return _cantidadVendida; } set { _cantidadVendida = value; } }
        public int IdProducto { get { return _idProducto; } set { _idProducto = value; } }



        public ProductoVendido()
        //Constructor x defecto
        {
            _id = 0;
            _idVenta = 0;
            _cantidadVendida = 0;
            _idProducto = 0;
        
        }

        public ProductoVendido(int idProductoVendido, int cantidadVendida, int idVenta, int idProducto)
        //Constructor con toda la info
        {
            _id = idProductoVendido;
            _idVenta = idVenta;
            _cantidadVendida = cantidadVendida;
            _idProducto = idProducto;
        }

    }


}

