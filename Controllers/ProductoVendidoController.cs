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
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductoVendido(int idUsuario)
        {
            return ADO_ProductoVendido.TraerProductosVendidos(idUsuario);
        }

        /*
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
        */

    }
}
