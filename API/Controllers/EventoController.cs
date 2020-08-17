using Domain.Entities;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [RoutePrefix("api/Evento")]
    public class EventoController : ApiController
    {
        private readonly EventoService service;

        public EventoController()
        {
            service = new EventoService();
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IEnumerable<Evento>> Buscar()
        {
            try
            {
                return await service.BuscarTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("BuscarPorId")]
        public async Task<Evento> BuscarPorId(int id)
        {
            try
            {
                return await service.BuscarPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Inserir")]
        public async Task<Evento> Inserir([FromBody]Evento value)
        {
            try
            {
                return await service.Adicionar(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<dynamic> Editar([FromBody]Evento value)
        {
            try
            {
                await service.Editar(value);

                return new { code = 200, message = "Item editado com sucesso" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("Deletar")]
        public async Task<dynamic> Deletar(int id)
        {
            try
            {
                await service.Deletar(id);

                return new { code = 200, message = "Item deletado com sucesso" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
