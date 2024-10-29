using DSS_Scoring.Data;
using DSS_Scoring.Models;
using DSS_Scoring.Shared;
using DSS_Scoring.Shared.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private MyDbContext _context;
        public ChatsController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todos los chats
        // GET /api/chats
        [HttpGet]
        public ResponseAPI<IEnumerable<ChatDTO>> GetChats()
        { 
          ResponseAPI<IEnumerable<ChatDTO>> response = new ResponseAPI<IEnumerable<ChatDTO>>();
          List<ChatDTO> listaChatsDTO = new List<ChatDTO>();
          
          try{
            var chats = _context.Chats.Include(c=>c.IdUsuarioNavigation).ToList();

            // Transformar individualmente cada chat para que contenga la información de usuario
            foreach(var chat in chats)
            {
              listaChatsDTO.Add( new ChatDTO {
                Id = chat.Id,
                IdUsuario = chat.IdUsuario,
                IdProyecto = chat.IdProyecto,
                Fecha = chat.Fecha,
                Hora = chat.Hora,
                Mensaje = chat.Mensaje,
                Usuario = chat.IdUsuarioNavigation.Adapt<UsuarioDTO>()
              });
            }


            response.Success = true;
            response.Message = listaChatsDTO.Count > 0 ? "Lista de chats obtenida con éxito" : "No hay chats registrados";
            response.Data = listaChatsDTO;
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los chats: {e.Message}";
          }

          return response; 
        }

        // Obtener un chat por su ID
        // GET /api/chats/{id}
        [HttpGet("{id}")]
        public ResponseAPI<ChatDTO> GetChatById(int id)
        { 
          ResponseAPI<ChatDTO> response = new ResponseAPI<ChatDTO>();
          
          try{
            var chat = _context.Chats.Include(c=> c.IdUsuarioNavigation).FirstOrDefault(c=> c.Id == id);
            
            if(chat == null) throw new Exception("Chat no encontrado");
            
            // Transformar manualmente la respuesta para que contenga la información de usuario
            ChatDTO chatDTO = chat.Adapt<ChatDTO>();
            chatDTO.Usuario = chat.IdUsuarioNavigation.Adapt<UsuarioDTO>();

            response.Success = true;
            response.Message = "Chat obtenido con éxito";
            response.Data = chatDTO;
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener el chat: {e.Message}";
          }

          return response; 
        }

        // Obtener los chats de un proyecto
        // GET /api/chats/proyecto/{id}
        [HttpGet("proyecto/{id}")]
        public ResponseAPI<IEnumerable<ChatDTO>> GetChatsByProyecto(int id)
        { 
          ResponseAPI<IEnumerable<ChatDTO>> response = new ResponseAPI<IEnumerable<ChatDTO>>();
          List<ChatDTO> listaChatsDTO = new List<ChatDTO>();
          
          try{
            var proyecto = _context.Proyectos.Find(id);
            
            if(proyecto == null) throw new Exception("Proyecto no encontrado");

            var chats = _context.Chats.Where(chat => chat.IdProyecto == id).Include(c=>c.IdUsuarioNavigation).ToList();

            // Transformar individualmente cada chat para que contenga la información de usuario
            foreach(var chat in chats){
              listaChatsDTO.Add( new ChatDTO{
                Id = chat.Id,
                IdUsuario = chat.IdUsuario,
                IdProyecto = chat.IdProyecto,
                Fecha = chat.Fecha,
                Hora = chat.Hora,
                Mensaje = chat.Mensaje,
                Usuario = chat.IdUsuarioNavigation.Adapt<UsuarioDTO>()
              });
            }

            response.Success = true;
            response.Message = listaChatsDTO.Count > 0 ? "Lista de chats obtenida con éxito" : "No hay chats registrados";
            response.Data = listaChatsDTO;
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al obtener los chats: {e.Message}";
          }

          return response; 
        }

        // Crear un chat
        // POST /api/chats
        [HttpPost]
        public ResponseAPI<ChatDTO> CreateChat(ChatDTO chat)
        { 
          ResponseAPI<ChatDTO> response = new ResponseAPI<ChatDTO>();
          
          try{
            var usuario = _context.Usuarios.Find(chat.IdUsuario);
            if(usuario == null) throw new Exception("Usuario no encontrado");

            var chatNuevo = chat.Adapt<Chat>();
            _context.Chats.Add(chatNuevo);
            _context.SaveChanges();

            response.Success = true;
            response.Message = "Chat creado con éxito";
            response.Data = chatNuevo.Adapt<ChatDTO>();
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al crear el chat: {e.Message}";
          }

          return response; 
        }

        // Editar un chat
        // PUT /api/chats/{id}
        [HttpPut("{id}")]
        public ResponseAPI<ChatDTO> EditChat(int id, ChatDTO chat)
        { 
          ResponseAPI<ChatDTO> response = new ResponseAPI<ChatDTO>();

          try{
            var chatExistente = _context.Chats.Include(c=> c.IdUsuarioNavigation).FirstOrDefault(c=> c.Id == id);

            if(chatExistente == null) throw new Exception("Chat no encontrado");
            
            if(chatExistente.IdUsuario != chat.IdUsuario || chatExistente.IdProyecto != chat.IdProyecto){
              throw new Exception("No se puede reasignar un chat");
            }


            chatExistente.Mensaje = chat.Mensaje;
           _context.SaveChanges(); 

           ChatDTO chatDTO = chatExistente.Adapt<ChatDTO>();
           chatDTO.Usuario = chatExistente.IdUsuarioNavigation.Adapt<UsuarioDTO>();

            response.Success = true;
            response.Message = "Chat editado con éxito";
            response.Data = chatDTO;
          } catch(Exception e){
            response.Success = false;
            response.Message = $"Error al editar el chat: {e.Message}";
          }

          return response;
        }

        // NO ESTÁ PERMITIDO ELIMINAR UN CHAT
        // se necesita mantener un registro de todos los chats en el sistema
    }
}
