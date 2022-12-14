using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using PrimerTrabajoConAPI.Models;

namespace PrimerTrabajoConAPI.Repository
{
    public class ADO_Producto
    {
        public static Producto TraerProducto(int id)
        //Metodo que recibe un ID y retorna el producto con ese ID
        {

            var p = new Producto();
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var comando = new SqlCommand("Select * from Producto where id ='" + id + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //string codigo, string descripcion, double precioDeVenta, double precioDeCompra, string categoria, int stock)

                            p = new Producto((int) dr.GetInt64(0), dr.GetString(1), Convert.ToDouble(dr.GetDecimal(2)), Convert.ToDouble(dr.GetDecimal(3)), Convert.ToInt32(dr.GetValue(4)), Convert.ToInt32(dr.GetValue(5)));

                            
                        }
                    }
                }
                connection.Close();
                return p;
            }
        }
        
        public static List<Producto> TraerProductoByUserID(int idUsuario)
        //Metodo que recibe un UserID y retorna una lista de productos asignados a ese usuario
        {
            List<Producto> productos = new List<Producto> { };
            var listaProductos = productos;

            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var comando = new SqlCommand("Select * from Producto where idUsuario ='" + idUsuario + "'", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Producto p = new Producto((int) dr.GetInt64(0), dr.GetString(1), Convert.ToDouble(dr.GetDecimal(2)), Convert.ToDouble(dr.GetDecimal(3)), Convert.ToInt32(dr.GetValue(4)), Convert.ToInt32(dr.GetValue(5)));
                            listaProductos.Add(p);
                        }
                    }
                }
                connection.Close();
                return listaProductos;
            }
        }
        
        public static bool CrearProducto(Producto p)
        //Metodo para crear u producto desde 0
        {
            //Primero reviso que los datos ingresados sean validos (stock, descripcion, user ID)
            string error = ValidarDatos(p);
            if (string.IsNullOrEmpty(error))
            {
                string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
                var query = "INSERT into Producto values (@desc, @costo, @venta, @stock, @idUsuario)";
                ModificarCrearProducto(p, query);
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var comando = new SqlCommand("SELECT IDENT_CURRENT ('Producto')", connection);
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                error = "ID del nuevo producto = " + dr.GetValue(0);
                            }
                        }
                    }
                    connection.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ModificarProducto (Producto p)
        //Metodo para Modificar un producto

        {
            string error = ValidarDatos(p);
            if (string.IsNullOrEmpty(error))
            {
                var query = "UPDATE Producto Set Descripciones = @desc, " +
                "Costo=@costo, PrecioVenta=@venta, Stock=@stock, idUSuario=@idUsuario " +
                "WHERE id=" + p.Id;
                ModificarCrearProducto(p, query);
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
        public static bool EliminarProducto(int id)
        // Metodo para Eliminar un producto dado su ID
        {
            bool eliminado = true;

            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var comando = new SqlCommand("Select * from Producto where id =" + id, connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (!dr.HasRows)
                    {
                        eliminado = false;
                        
                    }
                }
                connection.Close();
                if (eliminado==false)
                {
                    return eliminado;
                }
            }
            ADO_ProductoVendido.EliminarProductoVendido(id);
           
            string query = "DELETE FROM Producto Where id=" + id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand comandoCreate = new SqlCommand(query, connection))
                {
                    comandoCreate.ExecuteNonQuery();
                }
                connection.Close();
            }
            return eliminado;
        }


        public static string ValidarDatos (Producto p)
        //Metodo que valida si la info de un producto es correcta.

        {
            //Primero reviso que los datos ingresados sean validos (stock, descripcion, user ID)
            //Metodo aparte porque se usa en otros dos metodos y asi se evita repetir codigo.
            string error = string.Empty;
            if (p.Descripciones == "" || p.Descripciones == String.Empty)
            {
                error = "Descripcion vacio";
            }
            if (p.Stock <= 0)
            {
                error = "Stock no puede ser Menor a 1";
            }
            if (p.IdUsuario <= 0)
            {
                error = "El ID del Usuario asignado no puede ser <1";
            }
            else
            {
                string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var comando = new SqlCommand("Select * from Usuario where id =" + p.IdUsuario, connection);
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            error = "El ID del Usuario asignado no existe";
                        }
                    }
                    connection.Close();
                }
            }
            if (p.PrecioVenta <= p.Costo)
            {
                error = "El precio de venta debe ser mayor al costo del producto";
            }
            if (p.Costo <= 0)
            {
                error = "EL costo del producto no puede ser menor o igual a 0";
            }
            return error;
        }


        private static void ModificarCrearProducto(Producto p, string query)
        //Metodo interno usado para no repetir codigo al Modificar o crear un producto

        {
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var parametroDesc = new SqlParameter();
                parametroDesc.ParameterName = "desc";
                parametroDesc.SqlDbType = System.Data.SqlDbType.Char;
                parametroDesc.Value = p.Descripciones;
                var parametroCosto = new SqlParameter();
                parametroCosto.ParameterName = "costo";
                parametroCosto.SqlDbType = System.Data.SqlDbType.Decimal;
                parametroCosto.Value = p.Costo;
                var parametroVenta = new SqlParameter();
                parametroVenta.ParameterName = "venta";
                parametroVenta.SqlDbType = System.Data.SqlDbType.Decimal;
                parametroVenta.Value = p.PrecioVenta;
                var parametroStock = new SqlParameter();
                parametroStock.ParameterName = "stock";
                parametroStock.SqlDbType = System.Data.SqlDbType.Int;
                parametroStock.Value = p.Stock;
                var parametroIdUsuario = new SqlParameter();
                parametroIdUsuario.ParameterName = "idUsuario";
                parametroIdUsuario.SqlDbType = System.Data.SqlDbType.Int;
                parametroIdUsuario.Value = p.IdUsuario;
                connection.Open();
                using (SqlCommand comandoCreate = new SqlCommand(query, connection))
                {
                    comandoCreate.Parameters.Add(parametroDesc);
                    comandoCreate.Parameters.Add(parametroCosto);
                    comandoCreate.Parameters.Add(parametroVenta);
                    comandoCreate.Parameters.Add(parametroStock);
                    comandoCreate.Parameters.Add(parametroIdUsuario);
                    comandoCreate.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
