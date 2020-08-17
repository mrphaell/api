using Domain.Entities;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Sala")]
    public class SalaController : ApiController
    {
        private readonly SalaService service;

        public SalaController()
        {
            service = new SalaService();
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IEnumerable<Sala>> Buscar()
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
        public async Task<Sala> BuscarPorId(int id)
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
        public async Task<Sala> Inserir([FromBody]Sala value)
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
        public async Task<dynamic> Editar([FromBody]Sala value)
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
