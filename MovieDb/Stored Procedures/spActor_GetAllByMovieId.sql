CREATE PROCEDURE [dbo].[spActor_GetAllByMovieId]
    @movieId int
AS
BEGIN
    SELECT ac.*
    FROM Actor ac
    INNER JOIN MovieActor ma ON ma.ActorId = ac.Id
    INNER JOIN Movie m ON m.Id = ma.MovieId
    WHERE m.Id = @movieId
END