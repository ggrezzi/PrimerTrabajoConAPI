using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;

namespace PrimerTrabajoConAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : Controller
    {
        //Ingreso in ID de un producto vendido y retorno el objeto productoVendido
        [HttpGet("TraerProductoVendido")]
        public ProductoVendido Consultar(int id)
        {
            return ADO_ProductoVendido.TraerProductoVendido(id);
        }


        //Elimino producto dado el IVendidoD del mismo
        [HttpDelete("EliminarProductoVendido")]
        public bool EliminarProductoVendido([FromBody] int id)

        {
            return ADO_ProductoVendido.EliminarProductoVendido(id);
        }
        //Modifico el producto vendido ingresado
        [HttpPut("ModificarProductoVendido")]
        public bool ModificarProductoVendido(ProductoVendido p)

        {
            return ADO_ProductoVendido.ModificarProductoVendido(p);
        }
        //Creo el producto vendido ingresado
        [HttpPost("CrearProductoVendido")]
        public bool CrearProductoVendido(ProductoVendido p)

        {
            return ADO_ProductoVendido.CrearProductoVendido(p);
        }

    }
}
