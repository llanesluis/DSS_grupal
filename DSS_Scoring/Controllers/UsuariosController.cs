using DSS_Scoring.Data;
using DSS_Scoring.Models;
using DSS_Scoring.Shared;
using DSS_Scoring.Shared.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private MyDbContext _context;
        public UsuariosController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todos los usuarios
        // GET /api/usuarios
        /// <summary>
        /// Obtener una lista de todos los usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet]
        public ResponseAPI<IEnumerable<UsuarioDTO>> GetUsuarios()
        { 
          ResponseAPI<IEnumerable<UsuarioDTO>> response = new ResponseAPI<IEnumerable<UsuarioDTO>>();
          
          try{
            var usuarios = _context.Usuarios.ToList();

            response.Success = true;
            response.Message = usuarios.Count > 0 ? "Lista de usuarios obtenida con éxito" : "No hay usuarios registrados";
            response.Data = usuarios.Adapt<IEnumerable<UsuarioDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los usuarios: {e.Message}";
          }

          return response; 
        }

        // Obtener un usuario por su ID
        // GET /api/usuarios/{id}
        /// <summary>
        /// Obtener un usuario por su ID
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/usuarios/1
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>El usuario que coincida con el ID</returns>
        [HttpGet("{id}")]
        public ResponseAPI<UsuarioDTO> GetUsuariosPorId(int id)
        { 
          ResponseAPI<UsuarioDTO> response = new ResponseAPI<UsuarioDTO>();
          
          try{
            var usuario = _context.Usuarios.Find(id);
            
            if(usuario == null) throw new Exception("Usuario no encontrado");

            response.Success = true;
            response.Message = "Usuario obtenido con éxito";
            response.Data = usuario.Adapt<UsuarioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener el usuario: {e.Message}";
          }

          return response; 
        }

        // Crear un usuario
        // POST /api/usuarios
        /// <summary>
        /// Crear un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// POST http://localhost:xxxx/api/usuarios
        /// {
        ///     "nombre": "Juan Pérez",
        ///     "email": "juan.perez@ejemplo.com",
        ///     "password": "123456",
        ///     "rol": "facilitador"
        /// }
        /// </remarks>
        /// <param name="usuario">Objeto con la información del usuario a crear.</param>
        /// <returns>El usuario creado.</returns>
        /// <response code="200">Usuario creado con éxito.</response>
        /// <response code="400">Error en los datos del usuario o el correo ya existe.</response>
        /// <response code="500">Error interno al procesar la solicitud.</response>
        [HttpPost]
        public ResponseAPI<UsuarioDTO> CreateUsuario(UsuarioDTO usuario)
        { 
          ResponseAPI<UsuarioDTO> response = new ResponseAPI<UsuarioDTO>();
          
          try{

            // Checar si ya existe un usuario con ese email
            var usuarioExistente = _context.Usuarios.Where(u=>u.Email == usuario.Email).Any(); 
            if(usuarioExistente) throw new Exception("Este correo ya fue registrado");

            var usuarioNuevo = usuario.Adapt<Usuario>();
            _context.Usuarios.Add(usuarioNuevo);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Usuario creado con éxito";
            response.Data = usuarioNuevo.Adapt<UsuarioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al crear el usuario: {e.Message}";
          }

          return response; 
        }

        // Editar un usuario
        // PUT /api/usuarios/{id}
        /// <summary>
        /// Editar un usuario existente.
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// PUT http://localhost:xxxx/api/usuarios/{id}
        /// {
        ///     "id": 1,
        ///     "nombre": "Nombre Actualizado",
        ///     "email": "correo@ejemplo.com",
        ///     "password": "nueva_contraseña",
        ///     "rol": "facilitador"
        /// }
        /// </remarks>
        /// <param name="id">ID del usuario a editar.</param>
        /// <param name="usuario">Datos actualizados del usuario.</param>
        /// <returns>El usuario actualizado.</returns>
        [HttpPut("{id}")]
        public ResponseAPI<UsuarioDTO> EditUsuario(int id, UsuarioDTO usuario)
        { 
          ResponseAPI<UsuarioDTO> response = new ResponseAPI<UsuarioDTO>();

          try{
            var usuarioExistente = _context.Usuarios.Find(id);

            if(usuarioExistente == null) throw new Exception("Usuario no encontrado");

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Password = usuario.Password;
            usuarioExistente.Rol = usuario.Rol;

           _context.SaveChanges(); 

            response.Success = true;
            response.Message = "Usuario editado con éxito";
            response.Data = usuarioExistente.Adapt<UsuarioDTO>();

          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al editar el usuario: {e.Message}";
          }

          return response;
        }

        // Eliminar un usuario
        // DELETE /api/usuarios/{id}
        /// <summary>
        /// Eliminar un usuario por su ID.
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// DELETE http://localhost:xxxx/api/usuarios/{id}
        /// </remarks>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>El usuario eliminado.</returns>
        [HttpDelete("{id}")]
        public ResponseAPI<UsuarioDTO> DeleteUsuario(int id)
        { 
          ResponseAPI<UsuarioDTO> response = new ResponseAPI<UsuarioDTO>();
          
          try{
            var usuario = _context.Usuarios.Find(id);

            if(usuario == null) throw new Exception("Usuario no encontrado");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Usuario eliminado con éxito";
            response.Data = usuario.Adapt<UsuarioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al eliminar el usuario: {e.Message}";
          }

          return response;
        }
    }
}
