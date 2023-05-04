CREATE PROCEDURE [dbo].[spMovie_GetAllByGenreId]
	@genreId int
AS
BEGIN
	SELECT m.*
    FROM Movie m
    INNER JOIN MovieGenre mg ON mg.MovieId = m.Id
    INNER JOIN Genre g ON g.Id = mg.GenreId
    WHERE g.Id = @genreId
END
