CREATE PROCEDURE [dbo].[spMovieGenre_IsGenreInMovie]
	@movieId int,
	@genreId int
AS
BEGIN
	SELECT COUNT(*)
    FROM MovieGenre mg
    INNER JOIN Movie m ON m.Id = mg.MovieId
    INNER JOIN Genre g ON g.Id = mg.GenreId
    WHERE m.Id = @movieId AND g.Id = @genreId
END