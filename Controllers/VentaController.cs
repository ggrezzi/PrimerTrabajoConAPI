using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;

namespace PrimerTrabajoConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        //Traigo Venta - REcibo un ID de usuario y traigo una lista de ventas de ese usuario
        [HttpGet("TrearVenta")]
        public List<Venta>  TraearVenta(int userId)
        {
            return ADO_Venta.TraerVentas(userId);
            //return true;
        }

        //Cargo venta - Recibo lista de prod vendidos x json y un User ID por url
        //Retorno TRUE si se pudo cargar o FALSE si no se pudo cargar.
        [HttpPost("cargarVenta")]
        public bool CargarVenta([FromBody] List<ProductoVendido> listaProd, int userId, string comentario)
        {
            return ADO_Venta.CargarVenta(listaProd, userId, comentario);
            //return true;
        }

        //Elimino Venta - Recibo un ID de venta y la borro, si da error devuelvo FALSE
        [HttpDelete("EliminarVenta")]
        public bool EliminarVenta(int id)
        { 
            return ADO_Venta.EliminarVenta(id);
            //return true;
        }
    }
}
