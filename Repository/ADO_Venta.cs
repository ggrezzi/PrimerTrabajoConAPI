using System.Data.SqlClient;
using static PrimerTrabajoConAPI.Controllers.UsuarioController;
using PrimerTrabajoConAPI.Models;
using System.Net.Mail;
using System.Reflection.Metadata;


namespace PrimerTrabajoConAPI.Repository
{
    public class ADO_Venta
    {
        //MEtodo usado para cargar as ventas realizadas de una lista de productos 
        public static bool CargarVenta(List<ProductoVendido> listaProd, int userId, string comentario)
        {
            var connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            int idVenta = 0;
            bool hayStock = true;

            //Confirmo que haya stock de todos los productos antes de agregar nada a la DB
            foreach (ProductoVendido p in listaProd)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var comando = new SqlCommand("Select Stock from Producto Where Id= " + p.IdProducto, connection);
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var prodStock = dr.GetInt32(0);
                                if (prodStock < p.CantidadVendida)
                                {
                                    hayStock = false;
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            if (hayStock == false)
            {
                return false;
            }

            //Una vez que se que hay stock (de lo contrario hubieramos retornado FALSE en la parte anterior - Agrego la Venta

            string query = "INSERT into VENTA (Comentarios, IdUsuario) Values ('"+comentario+"'," + userId + ")";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand comandoUpdate = new SqlCommand(query, connection))
                {
                    comandoUpdate.ExecuteNonQuery();
                }
                connection.Close();
            }
            //Una vez agregada la venta, me fijo cual es el ID que le quedo asignado
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("SELECT IDENT_CURRENT ('Venta')", connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            idVenta = (int) dr.GetDecimal(0);
                        }
                    }
                }
                connection.Close();
            }

            //Agrego todos los productos a la tabla ProductoVendido
            foreach (ProductoVendido p in listaProd)
            {
                p.IdVenta = idVenta;
                ADO_ProductoVendido.CrearProductoVendido(p);

                //Luego de agregado el producto, descuento la cantidad vendida del stock en la tabla PRoducto
                query = "DECLARE @stock int " +
                        "SET @stock = (Select Stock from Producto Where id="+p.IdProducto+") " +
                        "UPDATE Producto set stock=(@stock-"+p.CantidadVendida + ") Where id="+p.IdProducto;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand comandoUpdate = new SqlCommand(query, connection))
                    {
                        comandoUpdate.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            return true;

        }

        static public List<Venta> TraerVentas(int userId)
        //Metodo al que se le ingresa un UserID y retorna la lista de ventas correspondiente
        //a ese usuario
        {
            List<Venta> ventas = new List<Venta> { };
            var traerVentas = ventas;
            string connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("SELECT * from Venta Where IdUsuario = " + userId, connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            // ProductoVendido temporaryProd = new ProductoVendido;
                            Venta p = new Venta((int)Convert.ToInt64(dr.GetValue(2)), (int)Convert.ToInt64(dr.GetValue(0)), dr.GetString(1), ADO_ProductoVendido.TraerProductosVendidosPorIdVenta((int)Convert.ToInt64(dr.GetValue(0))));
                            traerVentas.Add(p);
                        }
                    }
                }
                connection.Close();

            }
            return traerVentas;
        }



        public static bool EliminarVenta(int id)
        {
            //elimino la lista de productos vendidos
            //vuelvo a cargar el stock
            //elimino la venta
            bool existeVenta = false;
            var connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comando = new SqlCommand("SELECT * from Venta Where id = " +id, connection);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        existeVenta = true;
                    }
                }
                connection.Close();

            }

            if (!existeVenta)
            {
                return false;
            }
            List<ProductoVendido> listaProductos = new List<ProductoVendido>();
            listaProductos = ADO_ProductoVendido.TraerProductosVendidosPorIdVenta(id);
            foreach (ProductoVendido p in listaProductos)
            {
                ADO_ProductoVendido.EliminarProductoVendido(p.IdProductoVendido);
            }
            
            string query = "DELETE Venta where id=" + id;
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
