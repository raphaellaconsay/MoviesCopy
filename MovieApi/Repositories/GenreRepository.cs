using Dapper;
using MovieApi.Context;
using MovieApi.Contracts;
using MovieApi.Models;
using System.Data;

namespace MovieApi.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DapperContext _context;
        public GenreRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Genre genre)
        {
            var sql = @"INSERT INTO Genre (Name) VALUES (@Name);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, genre);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Genre WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var sql = "SELECT * FROM Genre";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Genre>(sql);
            }
        }

        public async Task<IEnumerable<Genre>> GetAllByMovieId(int movieId)
        {
            var sql = "spGenre_GetAllByMovieId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Genre>(sql, new { movieId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Genre?> GetGenre(int id)
        {
            var sql = @"SELECT *
                        FROM Genre g
                        WHERE g.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Genre>(sql, new { id });
            }
        }

        public async Task<Genre?> GetGenre(string name)
        {
            var sql = @"SELECT *
                        FROM Genre g
                        WHERE g.Name = @Name";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Genre>(sql, new { name });
            }
        }

        public async Task<bool> Update(Genre genre)
        {
            var sql = @"UPDATE Genre
                        SET Name = @Name 
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, genre);
                return update == 1;
            }
        }
    }

}
