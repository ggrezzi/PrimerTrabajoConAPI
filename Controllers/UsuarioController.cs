using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimerTrabajoConAPI.Models;
using PrimerTrabajoConAPI.Repository;
using System.ComponentModel.DataAnnotations;

namespace PrimerTrabajoConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        //Trae un usuario dado un UserName
        [HttpGet("{nombreUsuario}")]
        public Usuario Consultar([Required] string nombreUsuario)
        {
            return ADO_Usuario.TraerUsuario(nombreUsuario);
        }

        //Inicio de Sesion
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario InicioSesion(string nombreUsuario, string contraseña)

        {
            return ADO_Usuario.IniciarSesion(nombreUsuario, contraseña);
        }

        //Modifica un usario - Se puede modificar cualquier dato en el objeto menos el ID.
        [HttpPut]
        public bool Modificar([FromBody] Usuario usuario)

        {
            return  ADO_Usuario.ModificarUsuario(usuario);
        }

        //Crea un usario
        [HttpPost]
        public bool CrearUsuario([FromBody] Usuario u)
        {
            return ADO_Usuario.CrearUsuario(u);
        }

        //Elimina un Usuario dado su ID.
        [HttpDelete("EliminarUsuario")]
        public bool EliminarUsuario([FromBody]int id)
        {
            return ADO_Usuario.EliminarUsuario(id);
        }



    }
}
