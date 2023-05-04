using MovieApi.Context;
using MovieApi.Contracts;
using MovieApi.Models;
using Dapper;
using System.Data;

namespace MovieApi.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly DapperContext _context;

        public ActorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Actor actor)
        {
            var sql = @"INSERT INTO Actor (Name, Gender, Birthday) VALUES (@Name, @Gender, @Birthday);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, actor);
            }
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var sql = "SELECT * FROM Actor";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Actor>(sql);
            }
        }

        public async Task<Actor?> GetActor(int id)
        {
            var sql = @"SELECT * 
                        FROM Actor ac 
                        WHERE ac.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Actor>(sql, new { id });
            }
        }

        public async Task<IEnumerable<Actor>> GetAllByMovieId(int movieId)
        {
            var sql = "spActor_GetAllByMovieId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Actor>(sql, new { movieId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Update(Actor actor)
        {
            var sql = @"UPDATE Actor
                        SET Name = @Name 
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, actor);
                return update == 1;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Actor WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }
    }
}
