using Dapper;
using Domain.Entities;
using Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class EventoRepository : IBaseRepository<Evento>
    {
        private IDbConnection Connection { get; set; }

        public EventoRepository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        }

        public async Task<Evento> Add(Evento entity)
        {
            Connection.Open();
            var sql = $@" INSERT INTO Evento
                          (Id, NomeResponsavel, DataInicio, DataFim, IdSala)
                          OUTPUT INSERTED.Id
                          VALUES (NEXT VALUE FOR EventoSQ, '{entity.NomeResponsavel}', '{entity.DataInicio}', '{entity.DataFim}', {entity.IdSala}) ";
            try
            {
                entity.Id = await Connection.QuerySingleAsync<int>(sql, new { Evento = entity });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Connection.Close();
            
            return entity;
        }

        public async Task<Evento> Update(Evento entity)
        {
            Connection.Open();
            var sql = $@" UPDATE Evento
                          SET NomeResponsavel = '{entity.NomeResponsavel}',
	                          DataInicio = '{entity.DataInicio}',
                              DataFim = '{entity.DataFim}',
                              IdSala = {entity.IdSala}
                          WHERE Id = {entity.Id} ";
            try
            {
                await Connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Connection.Close();

            return entity;
        }

        public async Task<bool> Delete(long id)
        {
            Connection.Open();
            var sql = $@" DELETE FROM Evento
                          WHERE Id = {id} ";
            try
            {
                await Connection.ExecuteAsync(sql);

                Connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Evento entity)
        {
            try
            {
                return await Delete(entity.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Evento Find(long pk)
        {
            Connection.Open();
            var sql = $@" SELECT Ev.*,
	                             Sl.Nome AS Sala
                          FROM Evento AS Ev
                          INNER JOIN Sala AS Sl ON Ev.IdSala = Sl.Id
                          WHERE Ev.Id = {pk} ";
            try
            {
                var data = Connection.Query<Evento>(sql).FirstOrDefault();
                Connection.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Evento> FindAsync(long pk)
        {
            Connection.Open();
            var sql = $@" SELECT Ev.*,
	                             Sl.Nome AS Sala
                          FROM Evento AS Ev
                          INNER JOIN Sala AS Sl ON Ev.IdSala = Sl.Id
                          WHERE Ev.Id = {pk} ";
            try
            {
                var data = await Connection.QueryAsync<Evento>(sql);
                Connection.Close();

                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Evento> FindAll()
        {
            Connection.Open();
            var sql = $@" SELECT Ev.*,
	                             Sl.Nome AS Sala
                          FROM Evento AS Ev
                          INNER JOIN Sala AS Sl ON Ev.IdSala = Sl.Id ";
            try
            {
                var data = Connection.Query<Evento>(sql).AsQueryable();
                Connection.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Evento>> FindAllAsync()
        {
            Connection.Open();
            var sql = $@" SELECT Ev.*,
	                             Sl.Nome AS Sala
                          FROM Evento AS Ev
                          INNER JOIN Sala AS Sl ON Ev.IdSala = Sl.Id ";
            try
            {
                var data = await Connection.QueryAsync<Evento>(sql);
                Connection.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Evento> Find(Expression<Func<Evento, bool>> predicate)
        {
            try
            {
                var data = FindAll().Where(predicate);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Evento>> FindAsync(Expression<Func<Evento, bool>> predicate)
        {
            try
            {
                var data = await FindAllAsync();

                data = data.AsQueryable().Where(predicate);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
