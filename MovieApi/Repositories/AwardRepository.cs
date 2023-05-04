using Dapper;
using MovieApi.Context;
using MovieApi.Contracts;
using MovieApi.Models;
using System.Data;

namespace MovieApi.Repositories
{
    public class AwardRepository : IAwardRepository
    {
        private readonly DapperContext _context;

        public AwardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Award award)
        {
            var sql = @"INSERT INTO Award (Name, Year, MovieId) VALUES (@Name, @Year, @MovieId);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, award);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Award WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }

        public async Task<IEnumerable<Award>> GetAll()
        {
            var sql = "SELECT * FROM Award";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Award>(sql);
            }
        }

        public async Task<IEnumerable<Award>> GetAllByMovieId(int movieId)
        {
            var sql = "spAward_GetAllByMovieId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Award>(sql, new { movieId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Award?> GetAward(int id)
        {
            var sql = @"SELECT *
                        FROM Award aw
                        WHERE aw.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Award>(sql, new { id });
            }
        }

        public async Task<bool> Update(Award award)
        {
            var sql = @"UPDATE Award
                        SET Name = @Name, Year = @Year
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, award);
                return update == 1;
            }
        }
    }
}
