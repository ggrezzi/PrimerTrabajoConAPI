using System.Data.SqlClient;

namespace PrimerTrabajoConAPI.Repository
{
    public class ADO_ProductoVendido
    {
        
        
        /*
        protected int _idProductoVendido;
        protected int _idVenta;
        protected int _cantidadVendida;
        protected int _idProducto;
        protected string _descripcion;
        protected double _precioDeVenta;
        protected double _precioDeCompra;
        protected int _stock;
        protected int _idUsuario;

        public ADO_ProductoVendido()
        //Constructor con toda la info
        {
            _idProductoVendido = 0;
            _idVenta = 0;
            _cantidadVendida = 0;
            _idProducto = 0;
            _descripcion = string.Empty;
            _precioDeVenta = 0;
            _precioDeCompra = 0;
            _stock = 0;
            _idUsuario = 0;
        }

        public ADO_ProductoVendido(int idProductoVendido, int cantidadVendida, int idVenta, int idProducto, string descripciones, double precioDeVenta, double precioDeCompra, int stock, int idUsuario)
        //Constructor con toda la info
        {
            //, int stock, int idUsuario)
            _idProductoVendido = idProductoVendido;
            _idVenta = idVenta;
            _cantidadVendida = cantidadVendida;
            _idProducto = idProducto;
            _descripcion = descripciones;
            _precioDeVenta = precioDeVenta;
            _precioDeCompra = precioDeCompra;
            _stock = stock;
            _idUsuario = idUsuario;

        }

        public List<ADO_ProductoVendido> TraerProductosVendidosPorIdVenta(int idVenta)
        //Metodo que recive un UserID y retorna una lista de productos vendidos asignados a ese usuario
        {
            List<ADO_ProductoVendido> listaProductosVendidos = new List<ADO_ProductoVendido> { };


            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from ProductoVendido  INNER JOIN " +
                    "Producto on ProductoVendido.IdProducto = Producto.Id WHERE idVenta = '" + idVenta + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //int idProductoVendido 0, int cantidadVendida 1, int idVenta 3, string idProducto 2, string descripciones 5, double precioDeVenta 6, double precioDeCompra7, int stock 8, int idUsuario 9
                            int test = (int)dr.GetInt64(0);
                            int test1 = dr.GetInt32(1);
                            int test2 = Convert.ToInt32(dr.GetValue(3));
                            int test3 = (int)dr.GetInt64(2);
                            string test4 = dr.GetString(5);
                            double test5 = Convert.ToDouble(dr.GetDecimal(6));
                            double test6 = Convert.ToDouble(dr.GetDecimal(7));
                            int test7 = Convert.ToInt32(dr.GetValue(8));
                            int test8 = Convert.ToInt32(dr.GetValue(9));


                            ADO_ProductoVendido p = new ADO_ProductoVendido((int)dr.GetInt64(0), dr.GetInt32(1), Convert.ToInt32(dr.GetValue(3)), (int)dr.GetInt64(2), dr.GetString(5), Convert.ToDouble(dr.GetDecimal(6)), Convert.ToDouble(dr.GetDecimal(7)), Convert.ToInt32(dr.GetValue(8)), Convert.ToInt32(dr.GetValue(9)));
                            listaProductosVendidos.Add(p);
                        }
                    }
                }
                connection.Close();
            }

            return listaProductosVendidos;
        }



        public List<ADO_Producto> TraerProductosVendidos(int idUsuario)
        //Metodo que recive un UserID y retorna una lista de productos vendidos asignados a ese usuario
        {
            string query = "";
            bool agregado = false;
            List<ADO_Producto> productos = new List<ADO_Producto> { };
            var listaProductos = ADO_Producto.TraerProducto(idUsuario);
            var listaProductosVendidos = productos;
            foreach (ADO_Producto p in listaProductos)
            {
                if (idUsuario == p.GetIdUsuario())
                {
                    if (agregado)
                    {
                        query = query + "," + p.GetIdProducto();
                    }
                    else
                    {
                        agregado = true;
                        query = p.GetIdProducto();
                    }
                }
            }

            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from ProductoVendido INNER JOIN Producto on producto.id = productoVendido.IdProducto"
                    + " where idProducto in (" + query + ")", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //string codigo, string descripcion, double precioDeVenta, double precioDeCompra, string categoria, int stock)
                            ADO_Producto p = new ADO_Producto(dr.GetInt64(0).ToString(), dr.GetString(5), Convert.ToDouble(dr.GetDecimal(7)), Convert.ToDouble(dr.GetDecimal(6)), Convert.ToInt32(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(8)));
                            listaProductosVendidos.Add(p);
                        }
                    }
                }
                connection.Close();
            }

            return listaProductosVendidos;
        }
        public int GetIdProductoVendido()
        {
            return _idProductoVendido;
        }
        public int GetIdVenta()
        {
            return _idVenta;
        }
        public int GetCantidadVendida()
        {
            return _cantidadVendida;
        }

        public string GetDescipcion()
        {
            return _descripcion;
        }
        */
    }
}
