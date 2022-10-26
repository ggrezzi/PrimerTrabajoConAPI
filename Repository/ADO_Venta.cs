using System.Data.SqlClient;
using static PrimerTrabajoConAPI.Controllers.UsuarioController;
using PrimerTrabajoConAPI.Models;
using System.Net.Mail;
using System.Reflection.Metadata;


namespace PrimerTrabajoConAPI.Repository
{
    public class ADO_Venta
    {
        public static bool CargarVenta(List<ProductoVendido> listaProd, int userId)
        {
            var connectionString = "Server=W0447;Database=Master; Trusted_connection=True;";
            int idVenta = 0;
            bool hayStock = true;
            string query = string.Empty;

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

            query = "INSERT into VENTA (Comentarios, IdUsuario) Values ('Venta prod'," + userId + ")";
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

            //Agrego todo slos productos a la tabla ProductoVendido
            foreach (ProductoVendido p in listaProd)
            {
                query = "INSERT into ProductoVendido (stock, IdProducto, IdVenta) Values (" + p.CantidadVendida + "," + p.IdProducto + "," + idVenta + ")";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand comandoUpdate = new SqlCommand(query, connection))
                    {
                        comandoUpdate.ExecuteNonQuery();
                    }
                    connection.Close();
                }
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
    }
}
