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
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductoByUserID(int idUsuario)
        {
            return ADO_Producto.TraerProductoByUserID(idUsuario);
        }

        //Creo un producto dada toda la info del mismo (el ID se crea automatico en la DB)
        [HttpPost]
        public string CrearProducto([FromBody] Producto producto)

        {
            return  ADO_Producto.CrearProducto(producto);
        }

        //Modifico un producto dada la info del objeto Producto
        [HttpPut]
        public bool ModificarProducto([FromBody] Producto producto)

        {
            return ADO_Producto.ModificarProducto(producto);
        }

        //Elimino producto dado el ID del mismo
        [HttpDelete("{idProducto}")]
        public bool EliminarProducto(int idProducto)

        {
            return ADO_Producto.EliminarProducto(idProducto);
        }





    }
}
