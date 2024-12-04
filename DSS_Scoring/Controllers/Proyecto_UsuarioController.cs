using DSS_Scoring.Components.Pages;
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
    public class Proyecto_UsuarioController : ControllerBase
    {
        private MyDbContext _context;
        public Proyecto_UsuarioController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener el registro de relación entre un proyecto y un usuario
        // GET /api/proyecto_usuario
        /// <summary>
        /// Obtener todas las relaciones entre proyectos y usuarios
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/proyecto_usuario
        /// </remarks>
        /// <returns>Lista de relaciones proyecto-usuario</returns>
        [HttpGet]
        public ResponseAPI<IEnumerable<ProyectoUsuarioDTO>> GetProyectosYUsuarios()
        { 
          ResponseAPI<IEnumerable<ProyectoUsuarioDTO>> response = new ResponseAPI<IEnumerable<ProyectoUsuarioDTO>>();
          
          try{
            var proyectos_usuarios = _context.ProyectoUsuarios.ToList();

            response.Success = true;
            response.Message = proyectos_usuarios.Count > 0 ? "El registro de relación entre un proyecto y un usuario fue obtenido con éxito" : "No hay relaciones registradas";
            response.Data = proyectos_usuarios.Adapt<IEnumerable<ProyectoUsuarioDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener el registro de relación entre un proyecto y un usuario: {e.Message}";
          }

          return response; 
        }

        // Obtener los usuarios que pertenecen a un proyecto
        // GET /api/proyecto_usuario/proyecto/{id}
        /// <summary>
        /// Obtener los usuarios que pertenecen a un proyecto
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/proyecto_usuario/proyecto/{id}
        /// 
        /// Ejemplo:
        /// GET http://localhost:xxxx/api/proyecto_usuario/proyecto/1
        /// </remarks>
        /// <param name="id">ID del proyecto</param>
        /// <returns>Lista de usuarios asociados al proyecto</returns>
        [HttpGet("proyecto/{id}")]
        public ResponseAPI<IEnumerable<UsuarioDTO>> GetUsuariosPorProyecto(int id)
        { 
          // TODO: Checar este codigo, es para regresar los usuarios que pertenecen a un proyecto, con toda su información.
          ResponseAPI<IEnumerable<UsuarioDTO>> response = new ResponseAPI<IEnumerable<UsuarioDTO>>();

          try{
            var proyecto = _context.Proyectos.Find(id);

            if(proyecto == null) throw new Exception("El proyecto no existe");

            var proyecto_usuarios = _context.ProyectoUsuarios.Where(x => x.IdProyecto == id).Select(x=> x.IdUsuarioNavigation).ToList();

            if(proyecto_usuarios.Count == 0) throw new Exception("No hay usuarios registrados para este proyecto");

            var usuariosIds = proyecto_usuarios.Select(x=> x.Id).ToList();

            var usuarios = _context.Usuarios.Where(x => usuariosIds.Contains(x.Id)).ToList();

            response.Success = true;
            response.Message = usuarios.Count > 0 ? "Lista de usuarios obtenida con éxito" : "No hay usuarios registrados";
            response.Data = usuarios.Adapt<IEnumerable<UsuarioDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los usuarios: {e.Message}";
          }

          return response; 
        }

        // Crear una relación entre un proyecto y un usuario
        // POST /api/proyecto_usuario
        /// <summary>
        /// Crear una relación entre un proyecto y un usuario
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// POST http://localhost:xxxx/api/proyecto_usuario
        /// 
        /// Body:
        /// {
        ///   "idProyecto": 1,
        ///   "idUsuario": 2
        /// }
        /// </remarks>
        /// <param name="proyecto_usuario">Datos de la relación a crear</param>
        /// <returns>Relación creada</returns>
        [HttpPost]
        public ResponseAPI<ProyectoUsuarioDTO> CreateProyectoYUsuario(ProyectoUsuarioDTO proyecto_usuario)
        { 
          ResponseAPI<ProyectoUsuarioDTO> response = new ResponseAPI<ProyectoUsuarioDTO>();
          
          try{
            var usuario = _context.Usuarios.Find(proyecto_usuario.IdUsuario);
            if(usuario == null) throw new Exception("Usuario no encontrado");

            var proyecto = _context.Proyectos.Find(proyecto_usuario.IdProyecto);
            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            // verificar que la relación no existe, si no existe procederá a crearla
            bool relationAlreadyExists = _context.ProyectoUsuarios.Any(x => x.IdProyecto == proyecto_usuario.IdProyecto && x.IdUsuario == proyecto_usuario.IdUsuario);
            if(relationAlreadyExists) throw new Exception("La relación entre el proyecto y el usuario ya existe");

            var proyectoUsuarioNuevo = proyecto_usuario.Adapt<ProyectoUsuario>();
            _context.ProyectoUsuarios.Add(proyectoUsuarioNuevo);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Relación entre un proyecto y un usuario creada con éxito";
            response.Data = proyectoUsuarioNuevo.Adapt<ProyectoUsuarioDTO>();
          } catch(Exception e){
            response.Success = false;            
            response.Message = $"Error al crear la relación entre un proyecto y un usuario: {e.Message}";
          }

          return response; 
        }

        // Eliminar una relación entre un proyecto y un usuario
        // DELETE /api/proyecto_usuario/{id}
        /// <summary>
        /// Eliminar una relación entre un proyecto y un usuario
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// DELETE http://localhost:xxxx/api/proyecto_usuario/{id}
        /// 
        /// Ejemplo:
        /// DELETE http://localhost:xxxx/api/proyecto_usuario/1
        /// </remarks>
        /// <param name="id">ID de la relación a eliminar</param>
        /// <returns>Relación eliminada</returns>
        [HttpDelete("{id}")]
        public ResponseAPI<ProyectoUsuarioDTO> DeleteProyectoYUsuario(int id)
        { 
          ResponseAPI<ProyectoUsuarioDTO> response = new ResponseAPI<ProyectoUsuarioDTO>();
          
          try{
            var proyectoUsuario = _context.ProyectoUsuarios.Find(id);

            if(proyectoUsuario == null) throw new Exception("La relación entre el proyecto y el usuario no existe");

            _context.ProyectoUsuarios.Remove(proyectoUsuario);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "La relación entre el proyecto y el usuario fue eliminada con éxito";
            response.Data = proyectoUsuario.Adapt<ProyectoUsuarioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al eliminar la relación entre el proyecto y el usuario: {e.Message}";
          }

          return response;
        }
    }
}
