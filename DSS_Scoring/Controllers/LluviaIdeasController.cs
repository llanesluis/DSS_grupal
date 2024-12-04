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
    public class LluviaIdeasController : ControllerBase
    {
        private MyDbContext _context;
        public LluviaIdeasController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todas las lluvias de ideas
        // GET /api/lluviaideas
        /// <summary>
        /// Obtener una lista de todas las lluvias de ideas
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/lluviaideas
        /// </remarks>
        /// <returns>Lista de lluvia de ideas</returns>
        [HttpGet]
        public ResponseAPI<IEnumerable<LluviaIdeaDTO>> GetLluviaIdeas()
        { 
          ResponseAPI<IEnumerable<LluviaIdeaDTO>> response = new ResponseAPI<IEnumerable<LluviaIdeaDTO>>();
          
          try{
            var lluviaIdeas = _context.LluviaIdeas.ToList();

            response.Success = true;
            response.Message = lluviaIdeas.Count > 0 ? "Lista de lluvia de ideas obtenida con éxito" : "No hay lluvia de ideas registradas";
            response.Data = lluviaIdeas.Adapt<IEnumerable<LluviaIdeaDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener la lluvia de ideas: {e.Message}";
          }

          return response; 
        }

        // Obtener la lista de lluvia de ideas de un proyecto
        // GET /api/lluviaideas/proyecto/{id}
        /// <summary>
        /// Obtener las lluvias de ideas asociadas a un proyecto
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/lluviaideas/proyecto/{id}
        /// 
        /// Ejemplo:
        /// GET http://localhost:xxxx/api/lluviaideas/proyecto/1
        /// </remarks>
        /// <param name="id">ID del proyecto</param>
        /// <returns>Lista de lluvia de ideas asociadas al proyecto</returns>
        [HttpGet("proyecto/{id}")]
        public ResponseAPI<IEnumerable<LluviaIdeaDTO>> GetLluviaIdeasPorProyecto(int id)
        { 
          ResponseAPI<IEnumerable<LluviaIdeaDTO>> response = new ResponseAPI<IEnumerable<LluviaIdeaDTO>>();
          
          try{
            var proyecto = _context.Proyectos.Find(id);

            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            var lluviaIdeas = _context.LluviaIdeas.Where(x => x.IdProyecto == id).ToList();

            response.Success = true;
            response.Message = lluviaIdeas.Count > 0 ? "Lista de lluvia de ideas obtenida con éxito" : "No hay registros de lluvia de ideas registradas para este proyecto";
            response.Data = lluviaIdeas.Adapt<IEnumerable<LluviaIdeaDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener la lluvia de ideas: {e.Message}";
          }

          return response; 
        }

        // Agregar una idea a la lluvia de ideas,
        // se necesita el id del usuario que participa y el id del proyecto en el que participa
        // POST /api/lluviaideas
        /// <summary>
        /// Crear una nueva idea para la lluvia de ideas
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// POST http://localhost:xxxx/api/lluviaideas
        /// 
        /// Body:
        /// {
        ///   "idProyecto": 1,
        ///   "idUsuario": 2,
        ///   "idea": "Propuesta para optimizar procesos",
        ///   "etapa": "alternativas"
        /// }
        /// </remarks>
        /// <param name="lluviaIdeas">Datos de la idea a crear</param>
        /// <returns>Idea creada</returns>
        [HttpPost]
        public ResponseAPI<LluviaIdeaDTO> CreateLluviaIdeas(LluviaIdeaDTO lluviaIdeas)
        { 
          ResponseAPI<LluviaIdeaDTO> response = new ResponseAPI<LluviaIdeaDTO>();
          
          try{
            var usuario = _context.Usuarios.Find(lluviaIdeas.IdUsuario);
            if(usuario == null) throw new Exception("Usuario no encontrado");

            var proyecto = _context.Proyectos.Find(lluviaIdeas.IdProyecto);
            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            // revisar la tabla de proyectos_usuarios para verificar que el usuario que propone la idea pertenece al proyecto
            bool isUserParticipant = _context.ProyectoUsuarios.Any(x => x.IdProyecto == lluviaIdeas.IdProyecto && x.IdUsuario == lluviaIdeas.IdUsuario);
            if(!isUserParticipant) throw new Exception("El usuario no es parte del proyecto, pide a un facilitador que lo agregue.");

            var lluviaIdeasNueva = lluviaIdeas.Adapt<LluviaIdea>();
            _context.LluviaIdeas.Add(lluviaIdeasNueva);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Idea creada con éxito";
            response.Data = lluviaIdeasNueva.Adapt<LluviaIdeaDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al crear la idea: {e.Message}";
          }

          return response; 
        }

       // NO ESTÁ PERMITIDO ELIMINAR UNA IDEA DE LA LLUVIA DE IDEAS
       // se necesita mantener un registro de todos las ideas propuestas en el sistema
    }
}
