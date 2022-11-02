using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;

namespace PrimerTrabajoConAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InicioController : Controller
    {

        //Traigo un producto dado el ID del mismo
        [HttpGet("Nombre")]
        public string TraerNombre()
        {
            return "Carpinteria Batimuebles";
        }


    }
}

