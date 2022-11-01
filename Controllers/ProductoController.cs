using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;

namespace PrimerTrabajoConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {

        //Traigo un producto dado el ID del mismo
        [HttpGet("TraerProducto")]
        public Producto Consultar(int id)
        {
            return ADO_Producto.TraerProducto(id);
        }

        //Creo un producto dada toda la info del mismo (el ID se crea automatico en la DB)
        [HttpPost("CrearProducto")]
        public string CrearProducto([FromBody] Producto producto)

        {
            return  ADO_Producto.CrearProducto(producto);
        }

        //Modifico un producto dada la info del objeto Producto
        [HttpPut("ModificarProducto")]
        public bool ModificarProducto([FromBody] Producto producto)

        {
            return ADO_Producto.ModificarProducto(producto);
        }

        //Elimino producto dado el ID del mismo
        [HttpDelete("EliminarProducto")]
        public bool EliminarProducto([FromBody] int id)

        {
            return ADO_Producto.EliminarProducto(id);
        }

    }
}
