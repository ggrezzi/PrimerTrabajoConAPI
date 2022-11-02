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
        [HttpGet("{idUsuario}")]
        public List<Venta>  TraearVenta(int idUsuario)
        {
            return ADO_Venta.TraerVentas(idUsuario);
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
        [HttpDelete("{idVenta}")]
        public bool EliminarVenta(int idVenta)
        { 
            return ADO_Venta.EliminarVenta(idVenta);
            //return true;
        }
    }
}
