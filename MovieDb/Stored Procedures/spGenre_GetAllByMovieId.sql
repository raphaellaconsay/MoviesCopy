CREATE PROCEDURE [dbo].[spGenre_GetAllByMovieId]
    @movieId int
AS
BEGIN
    SELECT g.*
    FROM Genre g
    INNER JOIN MovieGenre mg ON mg.GenreId = g.Id
    INNER JOIN Movie m ON m.Id = mg.MovieId
    WHERE m.Id = @movieId
END