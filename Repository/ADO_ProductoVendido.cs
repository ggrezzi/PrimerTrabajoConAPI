using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using PrimerTrabajoConAPI.Models;

namespace PrimerTrabajoConAPI.Repository
{
    public class ADO_ProductoVendido
    {
        
        
        public static List<ProductoVendido> TraerProductosVendidosPorIdVenta(int idVenta)
        //Metodo que recive una ID de una venta y retorna una lista de productos vendidos en esa transaccion
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido> { };


            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from ProductoVendido  WHERE idVenta = '" + idVenta + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ProductoVendido p = new ProductoVendido((int)dr.GetInt64(0), dr.GetInt32(1), Convert.ToInt32(dr.GetValue(3)), (int)dr.GetInt64(2));
                            listaProductosVendidos.Add(p);
                        }
                    }
                }
                connection.Close();
            }

            return listaProductosVendidos;
        }



        public static List<Producto> TraerProductosVendidos(int idUsuario)
        //Metodo que recive un UserID y retorna una lista de productos vendidos asignados a ese usuario
        {
            string query = "";
            bool agregado = false;
            List<Producto> productos = new List<Producto> { };
            var listaProductos = ADO_Producto.TraerProductoByUserID(idUsuario);
            var listaProductosVendidos = productos;
            foreach (Producto p in listaProductos)
            {
                if (idUsuario == p.IdUsuario)
                {
                    if (agregado)
                    {
                        query = query + "," + p.IdProducto;
                    }
                    else
                    {
                        agregado = true;
                        query = p.IdProducto;
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
                            Producto p = new Producto(dr.GetInt64(0).ToString(), dr.GetString(5), Convert.ToDouble(dr.GetDecimal(7)), Convert.ToDouble(dr.GetDecimal(6)), Convert.ToInt32(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(8)));
                            listaProductosVendidos.Add(p);
                        }
                    }
                }
                connection.Close();
            }

            return listaProductosVendidos;
        }

        public static bool EliminarProductoVendido(int id)
        {
            //Devuelvo el stock a la tabla del producto 
            //Estoy asumiendo que al cancelar la venta se estan devolviendo los productos
            ProductoVendido p = TraerProductoVendido(id);
            Producto prod = ADO_Producto.TraerProducto(p.IdProducto);
            prod.Stock = p.CantidadVendida + prod.Stock;
            ADO_Producto.ModificarProducto(prod);

            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            string query = "DELETE FROM ProductoVendido Where idProducto=" + id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand comandoCreate = new SqlCommand(query, connection))
                {
                    comandoCreate.ExecuteNonQuery();
                }
            connection.Close();
            }
            return true;
            
        }

        public static bool ModificarProductoVendido(ProductoVendido p)
        {

            //Metodo para modificar un producto Vendido. Solo se puede modificar la cantidad vendida.
            try
            {
                string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
                string query = "UPDATE ProductoVendido Set Stock = " + p.CantidadVendida + ",idProducto=" + p.IdProducto +
                    " WHERE id=" + p.IdProductoVendido;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand comandoCreate = new SqlCommand(query, connection))
                    {
                        comandoCreate.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        //MEtodo al que dado un ID de producto retorna el objego ProductoVendido con la info cargada
        public static ProductoVendido TraerProductoVendido (int idProducto)
        {
            ProductoVendido producto = new ProductoVendido();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("Select * from ProductoVendido Where producto.id =" + idProducto);
                    
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            producto = new ProductoVendido((int)dr.GetInt64(0), dr.GetInt32(1), Convert.ToInt32(dr.GetValue(3)), (int)dr.GetInt64(2));
                        }
                    }
                }
                connection.Close();
            }
            return producto;
        }

        //Metodo para cargar un productovendido a la DB
        public static bool CrearProductoVendido(ProductoVendido p)
        {
            var connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            string query = "INSERT into ProductoVendido (stock, IdProducto, IdVenta) Values (" + p.CantidadVendida + "," + p.IdProducto + "," + p.IdVenta + ")";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand comandoUpdate = new SqlCommand(query, connection))
                {
                    comandoUpdate.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }



    }
}
