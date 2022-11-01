using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;

namespace PrimerTrabajoConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        
        //Trae un usuario dado un ID
        [HttpGet("TraerUsuario")]
        public Usuario Consultar(int id)
        {
            return ADO_Usuario.TraerUsuario(id);
        }

        //Modifica un usario - Se puede modificar cualquier dato en el objeto menos el ID.
        [HttpPut("ModificarUsuario")]
        public string Modificar([FromBody] Usuario usuario)

        {
            return  ADO_Usuario.ModificarUsuario(usuario);
        }

        //Modifica un usario - Se puede modificar cualquier dato en el objeto menos el ID.
        [HttpPost("CrearUsuario")]
        public bool CrearUsuario([FromBody] Usuario u)
        {
            return ADO_Usuario.CrearUsuario(u);
        }

        //Modifica un usario - Se puede modificar cualquier dato en el objeto menos el ID.
        [HttpDelete("EliminarUsuario")]
        public bool EliminarUsuario([FromBody] int id)
        {
            return ADO_Usuario.EliminarUsuario(id);
        }



    }
}
