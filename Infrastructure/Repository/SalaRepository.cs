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
    public class SalaRepository : IBaseRepository<Sala>
    {
        private IDbConnection Connection { get; set; }

        public SalaRepository()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        }

        public async Task<Sala> Add(Sala entity)
        {
            Connection.Open();
            var sql = $@" INSERT INTO Sala 
                          (Id, Nome)
                          OUTPUT INSERTED.Id
                          VALUES (NEXT VALUE FOR SalaSQ, '{entity.Nome}') ";
            try
            {
                entity.Id = await Connection.QuerySingleAsync<int>(sql, new { Sala = entity });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Connection.Close();
            
            return entity;
        }

        public async Task<Sala> Update(Sala entity)
        {
            Connection.Open();
            var sql = $@" UPDATE Sala 
                          SET Nome = '{entity.Nome}'
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
            var sql = $@" DELETE FROM Sala
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

        public async Task<bool> Delete(Sala entity)
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

        public Sala Find(long pk)
        {
            Connection.Open();
            var sql = $@" SELECT *
                          FROM Sala
                          WHERE Id = {pk} ";
            try
            {
                var data = Connection.Query<Sala>(sql).FirstOrDefault();
                Connection.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sala> FindAsync(long pk)
        {
            Connection.Open();
            var sql = $@" SELECT *
                          FROM Sala
                          WHERE Id = {pk} ";
            try
            {
                var data = await Connection.QueryAsync<Sala>(sql);
                Connection.Close();

                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Sala> FindAll()
        {
            Connection.Open();
            var sql = $@" SELECT *
                          FROM Sala ";
            try
            {
                var data = Connection.Query<Sala>(sql).AsQueryable();
                Connection.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Sala>> FindAllAsync()
        {
            Connection.Open();
            var sql = $@" SELECT *
                          FROM Sala ";
            try
            {
                var data = await Connection.QueryAsync<Sala>(sql);
                Connection.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Sala> Find(Expression<Func<Sala, bool>> predicate)
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

        public async Task<IEnumerable<Sala>> FindAsync(Expression<Func<Sala, bool>> predicate)
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
