CREATE PROCEDURE [dbo].[spMovieActor_IsActorInMovie]
    @movieId int,
    @actorId int
AS
BEGIN
    SELECT COUNT(*)
    FROM MovieActor ma
    INNER JOIN Movie m ON m.Id = ma.MovieId
    INNER JOIN Actor ac ON ac.Id = ma.ActorId
    WHERE m.Id = @movieId AND ac.Id = @actorId
END