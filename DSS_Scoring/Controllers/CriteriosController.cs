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
    public class CriteriosController : ControllerBase
    {
        private MyDbContext _context;
        public CriteriosController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todos los criterios
        // GET /api/criterios
        /// <summary>
        /// Obtener una lista de todos los criterios
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/criterios
        /// </remarks>
        /// <returns>Lista de criterios</returns>
        [HttpGet]
        public ResponseAPI<IEnumerable<CriterioDTO>> GetCriterios()
        { 
          ResponseAPI<IEnumerable<CriterioDTO>> response = new ResponseAPI<IEnumerable<CriterioDTO>>();
          
          try{
            var criterios = _context.Criterios.ToList();

            response.Success = true;
            response.Message = criterios.Count > 0 ? "Lista de criterios obtenida con éxito" : "No hay criterios registrados";
            response.Data = criterios.Adapt<IEnumerable<CriterioDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los criterios: {e.Message}";
          }

          return response; 
        }

        // Obtener un criterio por su ID
        // GET /api/criterios/{id}
        /// <summary>
        /// Obtener un criterio por su ID
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/criterios/{id}
        /// 
        /// Ejemplo:
        /// GET http://localhost:xxxx/api/criterios/1
        /// </remarks>
        /// <param name="id">ID del criterio</param>
        /// <returns>El criterio correspondiente al ID</returns>
        [HttpGet("{id}")]
        public ResponseAPI<CriterioDTO> GetCriterioById(int id)
        { 
          ResponseAPI<CriterioDTO> response = new ResponseAPI<CriterioDTO>();
          
          try{
            var criterio = _context.Criterios.Find(id);
            
            if(criterio == null) throw new Exception("Criterio no encontrado");

            response.Success = true;
            response.Message = "Criterio obtenido con éxito";
            response.Data = criterio.Adapt<CriterioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener el criterio: {e.Message}";
          }

          return response; 
        }

        // Obtener los criterios de un proyecto
        // GET /api/criterios/proyecto/{id}
        /// <summary>
        /// Obtener los criterios asociados a un proyecto
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// GET http://localhost:xxxx/api/criterios/proyecto/{id}
        /// 
        /// Ejemplo:
        /// GET http://localhost:xxxx/api/criterios/proyecto/1
        /// </remarks>
        /// <param name="id">ID del proyecto</param>
        /// <returns>Lista de criterios asociados al proyecto</returns>
        [HttpGet("proyecto/{id}")]
        public ResponseAPI<IEnumerable<CriterioDTO>> GetCriteriosByProyecto(int id)
        { 
          ResponseAPI<IEnumerable<CriterioDTO>> response = new ResponseAPI<IEnumerable<CriterioDTO>>();
          
          try{
            var proyecto = _context.Proyectos.Find(id);
            
            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            var criterios = _context.Criterios.Where(criterio => criterio.IdProyecto == id).ToList();

            response.Success = true;
            response.Message = criterios.Count > 0 ? "Lista de criterios obtenida con éxito" : "No hay criterios registrados";
            response.Data = criterios.Adapt<IEnumerable<CriterioDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los criterios: {e.Message}";
          }

          return response; 
        }

        // Crear un criterio
        // POST /api/criterios
        /// <summary>
        /// Crear un nuevo criterio
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// POST http://localhost:xxxx/api/criterios
        /// 
        /// Body:
        /// {
        ///   "idProyecto": 1,
        ///   "nombre": "Criterio de evaluación"
        /// }
        /// </remarks>
        /// <param name="criterio">Datos del nuevo criterio</param>
        /// <returns>Criterio creado</returns>
        [HttpPost]
        public ResponseAPI<CriterioDTO> CreateCriterio(CriterioDTO criterio)
        { 
          ResponseAPI<CriterioDTO> response = new ResponseAPI<CriterioDTO>();
          
          try{
            var criterioNuevo = criterio.Adapt<Criterio>();
            _context.Criterios.Add(criterioNuevo);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Criterio creado con éxito";
            response.Data = criterioNuevo.Adapt<CriterioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al crear el criterio: {e.Message}";
          }

          return response; 
        }

        // Editar un criterio
        // PUT /api/criterios/{id}
        /// <summary>
        /// Editar un criterio existente
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// PUT http://localhost:xxxx/api/criterios/{id}
        /// 
        /// Body:
        /// {
        ///   "idProyecto": 1,
        ///   "nombre": "Nuevo nombre del criterio"
        /// }
        /// </remarks>
        /// <param name="id">ID del criterio a editar</param>
        /// <param name="criterio">Datos actualizados del criterio</param>
        /// <returns>Criterio editado</returns>
        [HttpPut("{id}")]
        public ResponseAPI<CriterioDTO> EditCriterio(int id, CriterioDTO criterio)
        { 
          ResponseAPI<CriterioDTO> response = new ResponseAPI<CriterioDTO>();

          try{
            var criterioExistente = _context.Criterios.Find(id);

            if(criterioExistente == null) throw new Exception("Criterio no encontrado");

            
            criterioExistente.Nombre = criterio.Nombre;
            criterioExistente.IdProyecto = criterio.IdProyecto;

           _context.SaveChanges(); 

            response.Success = true;
            response.Message = "Criterio editado con éxito";
            response.Data = criterioExistente.Adapt<CriterioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al editar el criterio: {e.Message}";
          }

          return response;
        }

        // Eliminar un criterio
        // DELETE /api/criterios/{id}
        /// <summary>
        /// Eliminar un criterio
        /// </summary>
        /// <remarks>
        /// Solicitud de prueba:
        /// 
        /// DELETE http://localhost:xxxx/api/criterios/{id}
        /// 
        /// Ejemplo:
        /// DELETE http://localhost:xxxx/api/criterios/1
        /// </remarks>
        /// <param name="id">ID del criterio a eliminar</param>
        /// <returns>Criterio eliminado</returns>
        [HttpDelete("{id}")]
        public ResponseAPI<CriterioDTO> DeleteCriterio(int id)
        { 
          ResponseAPI<CriterioDTO> response = new ResponseAPI<CriterioDTO>();
          
          try{
            var criterio = _context.Criterios.Find(id);

            if(criterio == null) throw new Exception("Criterio no encontrado");

            _context.Criterios.Remove(criterio);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Criterio eliminado con éxito";
            response.Data = criterio.Adapt<CriterioDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al eliminar el criterio: {e.Message}";
          }

          return response;
        }
    }
}
