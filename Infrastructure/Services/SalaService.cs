using Domain.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SalaService
    {
        private readonly SalaRepository repository;

        public SalaService()
        {
            repository = new SalaRepository();
        }

        public async Task<IEnumerable<Sala>> BuscarTodos()
        {
            return await repository.FindAllAsync();
        }

        public async Task<Sala> BuscarPorId(long id)
        {
            return await repository.FindAsync(id);
        }

        public async Task<Sala> Adicionar(Sala entity)
        {
            return await repository.Add(entity);
        }

        public async Task<Sala> Editar(Sala entity)
        {
            return await repository.Update(entity);
        }

        public async Task<bool> Deletar(long id)
        {
            return await repository.Delete(id);
        }
    }
}
