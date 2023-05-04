CREATE PROCEDURE [dbo].[spMovie_GetMovieDetails]
	@Id int
AS
BEGIN
	SELECT m.*, g.*, ac.*, aw.*
    FROM Movie m
    LEFT JOIN MovieGenre mg ON mg.MovieId = m.Id
    LEFT JOIN Genre g ON g.Id = mg.GenreId
    LEFT JOIN MovieActor ma ON ma.MovieId = m.Id
    LEFT JOIN Actor ac ON ac.Id = ma.ActorId
    LEFT JOIN Award aw ON aw.MovieId = m.Id
    WHERE m.Id = @Id
END