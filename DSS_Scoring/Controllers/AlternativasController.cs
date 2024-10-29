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
    public class AlternativasController : ControllerBase
    {
        private MyDbContext _context;
        public AlternativasController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todas las alternativas
        // GET /api/alternativas
        [HttpGet]
        public ResponseAPI<IEnumerable<AlternativaDTO>> GetAlternativas()
        { 
          ResponseAPI<IEnumerable<AlternativaDTO>> response = new ResponseAPI<IEnumerable<AlternativaDTO>>();
          
          try{
            var alternativas = _context.Alternativas.ToList();

            response.Success = true;
            response.Message = alternativas.Count > 0 ? "Lista de alternativas obtenida con éxito" : "No hay alternativas registradas";
            response.Data = alternativas.Adapt<IEnumerable<AlternativaDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener las alternativas: {e.Message}";
          }

          return response; 
        }

        // Obtener un alternativa por su ID
        // GET /api/alternativas/{id}
        [HttpGet("{id}")]
        public ResponseAPI<AlternativaDTO> GetUserById(int id)
        { 
          ResponseAPI<AlternativaDTO> response = new ResponseAPI<AlternativaDTO>();
          
          try{
            var alternativa = _context.Alternativas.Find(id);
            
            if(alternativa == null) throw new Exception("Alternativa no encontrada");

            response.Success = true;
            response.Message = "Alternativa obtenida con éxito";
            response.Data = alternativa.Adapt<AlternativaDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener la alternativa: {e.Message}";
          }

          return response; 
        }

        // Obtener las alternativas de un proyecto
        // GET /api/alternativas/proyecto/{id}
        [HttpGet("proyecto/{id}")]
        public ResponseAPI<IEnumerable<AlternativaDTO>> GetAlternativasByProyecto(int id)
        { 
          ResponseAPI<IEnumerable<AlternativaDTO>> response = new ResponseAPI<IEnumerable<AlternativaDTO>>();
          
          try{
            var proyecto = _context.Proyectos.Find(id);
            
            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            var alternativas = _context.Alternativas.Where(alternativa => alternativa.IdProyecto == id).ToList();

            response.Success = true;
            response.Message = alternativas.Count > 0 ? "Lista de alternativas obtenida con éxito" : "No hay alternativas registradas";
            response.Data = alternativas.Adapt<IEnumerable<AlternativaDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener las alternativas: {e.Message}";
          }

          return response; 
        }

        // Crear un alternativa
        // POST /api/alternativas
        [HttpPost]
        public ResponseAPI<AlternativaDTO> CreateAlternativa(AlternativaDTO alternativa)
        { 
          ResponseAPI<AlternativaDTO> response = new ResponseAPI<AlternativaDTO>();
          
          try{
            var alternativaNuevo = alternativa.Adapt<Alternativa>();
            _context.Alternativas.Add(alternativaNuevo);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Alternativa creada con éxito";
            response.Data = alternativaNuevo.Adapt<AlternativaDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al crear la alternativa: {e.Message}";
          }

          return response; 
        }

        // Editar un alternativa
        // PUT /api/alternativas/{id}
        [HttpPut("{id}")]
        public ResponseAPI<AlternativaDTO> EditAlternativa(int id, AlternativaDTO alternativa)
        { 
          ResponseAPI<AlternativaDTO> response = new ResponseAPI<AlternativaDTO>();

          try{
            var alternativaExistente = _context.Alternativas.Find(id);

            if(alternativaExistente == null) throw new Exception("Alternativa no encontrada");

            
            alternativaExistente.Nombre = alternativa.Nombre;
            alternativaExistente.IdProyecto = alternativa.IdProyecto;

           _context.SaveChanges(); 

            response.Success = true;
            response.Message = "Alternativa editada con éxito";
            response.Data = alternativaExistente.Adapt<AlternativaDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al editar la alternativa: {e.Message}";
          }

          return response;
        }

        // Eliminar un alternativa
        // DELETE /api/alternativas/{id}
        [HttpDelete("{id}")]
        public ResponseAPI<AlternativaDTO> DeleteAlternativa(int id)
        { 
          ResponseAPI<AlternativaDTO> response = new ResponseAPI<AlternativaDTO>();
          
          try{
            var alternativa = _context.Alternativas.Find(id);

            if(alternativa == null) throw new Exception("Alternativa no encontrada");

            _context.Alternativas.Remove(alternativa);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Alternativa eliminada con éxito";
            response.Data = alternativa.Adapt<AlternativaDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al eliminar la alternativa: {e.Message}";
          }

          return response;
        }
    }
}
