CREATE PROCEDURE [dbo].[spAward_GetAllByMovieId]
    @movieId int
AS
BEGIN
    SELECT aw.*
    FROM Award aw
    INNER JOIN Movie m ON m.Id = aw.MovieId
    WHERE m.Id = @movieId
END