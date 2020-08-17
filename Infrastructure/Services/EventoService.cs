using Domain.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EventoService
    {
        private readonly EventoRepository repository;
        private readonly SalaRepository salaRepository;

        public EventoService()
        {
            repository = new EventoRepository();
            salaRepository = new SalaRepository();
        }

        public async Task<IEnumerable<Evento>> BuscarTodos()
        {
            return await repository.FindAllAsync();
        }

        public async Task<Evento> BuscarPorId(long id)
        {
            return await repository.FindAsync(id);
        }

        public async Task<Evento> Adicionar(Evento entity)
        {
            var sala = salaRepository.Find(entity.IdSala);

            if (sala != null)
            {
                var eventos = repository.Find(ev => (entity.DataInicio.Date >= ev.DataInicio.Date && entity.DataInicio.Date <= ev.DataFim.Date) || // Evento novo inicia durante outro evento
                                                   (entity.DataFim.Date >= ev.DataInicio.Date && entity.DataFim.Date <= ev.DataFim.Date) || // Evento novo termina durante outro evento
                                                   (entity.DataInicio.Date < ev.DataInicio.Date && entity.DataFim.Date > ev.DataFim.Date)).ToList(); // Evento novo começa antes e termina depois de um evento
                if (!eventos.Any())
                {
                    return await repository.Add(entity);
                }
                else
                {
                    throw new Exception("A duração de um evento não pode sobrepor outro");
                }
            }
            else
            {
                throw new Exception("Sala não encontrada");
            }
        }

        public async Task<Evento> Editar(Evento entity)
        {
            var sala = salaRepository.Find(entity.IdSala);

            if (sala != null)
            {
                var eventos = repository.Find(ev => (entity.DataInicio.Date >= ev.DataInicio.Date && entity.DataInicio.Date <= ev.DataFim.Date) || // Evento novo inicia durante outro evento
                                                    (entity.DataFim.Date >= ev.DataInicio.Date && entity.DataFim.Date <= ev.DataFim.Date) || // Evento novo termina durante outro evento
                                                    (entity.DataInicio.Date < ev.DataInicio.Date && entity.DataFim.Date > ev.DataFim.Date)).ToList(); // Evento novo começa antes e termina depois de um evento

                var eventosId = eventos.Select(ev => ev.Id).ToList();
                if (!eventos.Any() || (eventos.Count() == 1 && eventosId.Contains(entity.Id))) // Não encontrou evento na data selecionada ou permitir alterações no evento que não seja data
                {
                    return await repository.Update(entity);
                }
                else
                {
                    throw new Exception("A duração de um evento não pode sobrepor outro");
                }
            }
            else
            {
                throw new Exception("Sala não encontrada");
            }
        }

        public async Task<bool> Deletar(long id)
        {
            return await repository.Delete(id);
        }
    }
}
