using Dapper;
using MovieApi.Context;
using MovieApi.Contracts;
using MovieApi.Models;
using System.Data;

namespace MovieApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DapperContext _context;
        public MovieRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Movie movie)
        {
            var sql = @"INSERT INTO Movie (Title, Director, Duration, ReleaseDate, Rate) VALUES (@Title, @Director, @Duration, @ReleaseDate, @Rate);
                        SELECT SCOPE_IDENTITY();";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, movie);
            }
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var sql = "SELECT * FROM Movie";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Movie>(sql);
            }
        }

        public async Task<IEnumerable<Movie>> GetAllByActorId(int actorId)
        {
            var sql = "spMovie_GetAllByActorId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Movie>(sql, new { actorId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Movie>> GetAllByGenreId(int genreId)
        {
            var sql = "spMovie_GetAllByGenreId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Movie>(sql, new { genreId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Movie?> GetMovie(int id)
        {
            var sql = "spMovie_GetMovieDetails";

            using (var connection = _context.CreateConnection())
            {
                var movie = await connection.QueryAsync<Movie, Genre, Actor, Award, Movie>(
                    sql,
                    map: (movie, genre, actor, award) =>
                    {
                        movie.Genres.Add(genre);
                        movie.Actors.Add(actor);
                        movie.Awards.Add(award);
                        return movie;
                    },
                    param: new { id },
                    commandType: CommandType.StoredProcedure);

                var movieres = movie.GroupBy(m => m.Id).Select(mg =>
                {
                    var firstmovie = mg.First();
                    firstmovie.Genres = mg.SelectMany(a => a.Genres).ToList();
                    firstmovie.Actors = mg.SelectMany(a => a.Actors).ToList();
                    firstmovie.Awards = mg.SelectMany(a => a.Awards).ToList();
                    return firstmovie;
                }).First();

                return movieres;
            }
        }
        public async Task<Movie?> GetMovieOnly(int id)
        {
            var sql = @"SELECT *
                        FROM Movie m
                        WHERE m.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Movie?>(sql, new { id });
            }
        }

        public async Task<bool> IsActorInMovie(int movieId, int actorId)
        {
            var sql = "spMovieActor_IsActorInMovie";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { movieId, actorId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> AddActorInMovie(int movieId, int actorId)
        {
            var sql = @"INSERT INTO MovieActor (MovieId, ActorId) VALUES (@movieId, @actorId)";

            using (var connection = _context.CreateConnection())
            {
                var res = await connection.ExecuteAsync(sql, new { movieId, actorId });
                return res == 1;
            }
        }

        public async Task<bool> IsGenreInMovie(int movieId, int genreId)
        {
            var sql = @"spMovieGenre_IsGenreInMovie";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(sql, new { movieId, genreId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> AddGenreInMovie(int movieId, int genreId)
        {
            var sql = @"INSERT INTO MovieGenre (MovieId, GenreId) VALUES (@movieId, @genreId)";

            using (var connection = _context.CreateConnection())
            {
                var res = await connection.ExecuteAsync(sql, new { movieId, genreId });
                return res == 1;
            }
        }

        public async Task<bool> Update(Movie movie)
        {
            var sql = @"UPDATE Movie
                        SET ReleaseDate = @ReleaseDate, Rate = @Rate
                        WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var update = await connection.ExecuteAsync(sql, movie);
                return update == 1;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Movie WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { id });
                return delete == 1;
            }
        }

        public async Task<bool> DeleteGenreInMovie(int movieId, int genreId)
        {
            var sql = @"DELETE FROM MovieGenre
                        WHERE MovieId = @movieId AND GenreId = @genreId";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { movieId, genreId });
                return delete == 1;
            }
        }

        public async Task<bool> DeleteActorInMovie(int movieId, int actorId)
        {
            var sql = @"DELETE FROM MovieActor
                        WHERE MovieId = @movieId AND ActorId = @actorId";

            using (var connection = _context.CreateConnection())
            {
                var delete = await connection.ExecuteAsync(sql, new { movieId, actorId });
                return delete == 1;
            }
        }
    }
}
