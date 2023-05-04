CREATE PROCEDURE [dbo].[spMovie_GetAllByActorId]
	@actorId int
AS
BEGIN
	SELECT m.*
    FROM Movie m
    INNER JOIN MovieActor ma ON ma.MovieId = m.Id
    INNER JOIN Actor a ON a.Id = ma.ActorId
    WHERE a.Id = @actorId
END