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
