using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;

namespace PrimerTrabajoConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        public VentaController()
        {
        }

        //Cargo venta - Recibo lista de prod vendidos x json y un User ID por url
        //Retorno TRUE si se pudo cargar o FALSE si no se pudo cargar.
        [HttpPost("cargarVenta")]
        public bool CargarVenta([FromBody] List<ProductoVendido> listaProd, int userId)
        {
            return ADO_Venta.CargarVenta(listaProd, userId);

        }

    }
}
