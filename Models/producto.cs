using System.Data.SqlClient;

namespace PrimerTrabajoConAPI.Models

{
    public class Producto
    {
        private string _idProducto;
        private string _descripciones;
        private int _idUsuario;
        private double _precioDeCompra;
        private double _precioDeVenta;
        private int _stock;

        //Properties

        public string IdProducto { get { return _idProducto; } set { _idProducto = value; } }
        public string Descripciones { get { return _descripciones; } set { _descripciones = value; } }
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public double PrecioDeCompra { get { return _precioDeCompra; } set { _precioDeCompra = value; } }
        public double PrecioDeVenta { get { return _precioDeVenta; } set { _precioDeVenta = value; } }
        public int Stock { get { return _stock; } set { _stock = value; } }




        //Constructor por defecto

        public  Producto()
        {
            _idProducto = string.Empty;
            _descripciones = string.Empty;
            _precioDeCompra = 0;
            _precioDeVenta = 0;
            _idUsuario = 0;
            _stock = 0;


        }

        //Constructor con toda la info
        public Producto(string idProducto, string descripciones, double precioDeVenta, double precioDeCompra, int stock, int idUsuario)
        {
            _idProducto = idProducto;
            _descripciones = descripciones;
            _precioDeCompra = precioDeCompra;
            _precioDeVenta = precioDeVenta;
            _idUsuario = idUsuario;
            _stock = stock;
        }


    }
}


