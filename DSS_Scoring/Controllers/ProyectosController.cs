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
    public class ProyectosController : ControllerBase
    {
        private MyDbContext _context;
        public ProyectosController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todos los proyectos
        // GET /api/proyectos
        [HttpGet]
        public ResponseAPI<IEnumerable<ProyectoDTO>> GetProyectos()
        { 
          ResponseAPI<IEnumerable<ProyectoDTO>> response = new ResponseAPI<IEnumerable<ProyectoDTO>>();
          
          try{
            var proyectos = _context.Proyectos.ToList();

            response.Success = true;
            response.Message = proyectos.Count > 0 ? "Lista de proyectos obtenida con éxito" : "No hay proyectos registrados";
            response.Data = proyectos.Adapt<IEnumerable<ProyectoDTO>>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los proyectos: {e.Message}";
          }

          return response; 
        }

        // Obtener un proyecto por su ID
        // GET /api/proyectos/{id}
        [HttpGet("{id}")]
        public ResponseAPI<ProyectoDTO> GetUserById(int id)
        { 
          ResponseAPI<ProyectoDTO> response = new ResponseAPI<ProyectoDTO>();
          
          try{
            var proyecto = _context.Proyectos.Find(id);
            
            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            response.Success = true;
            response.Message = "Proyecto obtenido con éxito";
            response.Data = proyecto.Adapt<ProyectoDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener el proyecto: {e.Message}";
          }

          return response; 
        }

        // Crear un proyecto
        // POST /api/proyectos
        [HttpPost]
        public ResponseAPI<ProyectoDTO> CreateProyecto(ProyectoDTO proyecto)
        { 
          ResponseAPI<ProyectoDTO> response = new ResponseAPI<ProyectoDTO>();
          
          try{
            var usuario = _context.Usuarios.Find(proyecto.IdFacilitador);
            if(usuario == null) throw new Exception("Usuario no encontrado");

            bool isUserAuthorized = usuario.Rol == "facilitador" || usuario.Rol == "admin";
            if(!isUserAuthorized) throw new Exception("El usuario no es un facilitador o admin, solo los usuarios con rol 'facilitador' o 'admin' pueden manipular proyectos");	

            var proyectoNuevo = proyecto.Adapt<Proyecto>();
            _context.Proyectos.Add(proyectoNuevo);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Proyecto creado con éxito";
            response.Data = proyectoNuevo.Adapt<ProyectoDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al crear el proyecto: {e.Message}";
          }

          return response; 
        }

        // Editar un proyecto
        // PUT /api/proyectos/{id}
        [HttpPut("{id}")]
        public ResponseAPI<ProyectoDTO> EditProyecto(int id, ProyectoDTO proyecto)
        { 
          ResponseAPI<ProyectoDTO> response = new ResponseAPI<ProyectoDTO>();

          try{
            var proyectoExistente = _context.Proyectos.Find(id);

            if(proyectoExistente == null) throw new Exception("Proyecto no encontrado");

            var usuario = _context.Usuarios.Find(proyecto.IdFacilitador);
            if(usuario == null) throw new Exception("Usuario no encontrado");

            bool isUserAuthorized = usuario.Rol == "facilitador" || usuario.Rol == "admin";
            if(!isUserAuthorized) throw new Exception("El usuario no es un facilitador o admin, solo los usuarios con rol 'facilitador' o 'admin' pueden manipular proyectos");	

            proyectoExistente.Nombre = proyecto.Nombre;
            proyectoExistente.Objetivo = proyecto.Objetivo;
            proyectoExistente.IdFacilitador = proyecto.IdFacilitador;

           _context.SaveChanges(); 

            response.Success = true;
            response.Message = "Proyecto editado con éxito";
            response.Data = proyectoExistente.Adapt<ProyectoDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al editar el proyecto: {e.Message}";
          }

          return response;
        }

        // Eliminar un proyecto
        // DELETE /api/proyectos/{id}
        [HttpDelete("{id}")]
        public ResponseAPI<ProyectoDTO> DeleteProyecto(int id)
        { 
          ResponseAPI<ProyectoDTO> response = new ResponseAPI<ProyectoDTO>();
          
          try{
            var proyecto = _context.Proyectos.Find(id);

            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            _context.Proyectos.Remove(proyecto);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Proyecto eliminado con éxito";
            response.Data = proyecto.Adapt<ProyectoDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al eliminar el proyecto: {e.Message}";
          }

          return response;
        }
    }
}
