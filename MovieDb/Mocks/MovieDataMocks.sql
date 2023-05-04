
--insert mocks to Actor
insert into Actor (Name, Gender, Birthday) values ('Merwin Rizziello', 'Male', '1981-08-12');
insert into Actor (Name, Gender, Birthday) values ('Charles Sopp', 'Male', '1989-12-25');
insert into Actor (Name, Gender, Birthday) values ('Sarina Williment', 'Female', '1985-08-26');
insert into Actor (Name, Gender, Birthday) values ('Clarabelle Molyneaux', 'Female', '1986-01-31');
insert into Actor (Name, Gender, Birthday) values ('Cesar Matitiaho', 'Male', '1996-11-11');
insert into Actor (Name, Gender, Birthday) values ('Mellie O'' Hern', 'Female', '1982-11-01');
insert into Actor (Name, Gender, Birthday) values ('Gregoor MacParland', 'Male', '1994-09-03');
insert into Actor (Name, Gender, Birthday) values ('Ulrich Stronough', 'Male', '1996-12-23');
insert into Actor (Name, Gender, Birthday) values ('Bartram Stannus', 'Male', '1996-06-30');
insert into Actor (Name, Gender, Birthday) values ('Mellisa Willerstone', 'Female', '1999-07-13');
GO

--insert mocks to Genre
insert into Genre (Name)
values
('Action'),
('Comedy'),
('Drama')
GO

--insert mocks to Movie
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Devil''s Double, The', 'Karalynn Smogur', 255, '2016-11-20', 88);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Alien Predator (Mutant II) (Falling, The)', 'Hermione Masser', 118, '2015-05-28', 73);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Graveyard Shift (Stephen King''s Graveyard Shift)', 'Janette Birkett', 299, '2017-11-05', 96);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Magic Voyage of Sindbad, The (Sadko)', 'Reyna Webland', 173, '2019-01-19', 56);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Dexter the Dragon & Bumble the Bear', 'Anallise Worgen', 118, '2021-07-19', 96);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Creature', 'Kikelia Swinnerton', 142, '2021-03-21', 69);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Hong Kong Confidential (Amaya)', 'Steffane Loveard', 193, '2018-05-06', 73);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Year One, The (L''an 01)', 'Beniamino Blencowe', 145, '2006-01-12', 43);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Better Tomorrow II, A (Ying hung boon sik II)', 'Nanete Lipsett', 273, '2021-11-09', 43);
insert into Movie (Title, Director, Duration, ReleaseDate, Rate) values ('Shakes the Clown', 'Delphinia Ericssen', 88, '2006-12-20', 71);
GO

--insert mocks to Award
insert into Award (Name, Year, MovieId) values ('cras pellentesque volutpat dui', 2020, 10);
insert into Award (Name, Year, MovieId) values ('in hac habitasse platea', 2015, 10);
insert into Award (Name, Year, MovieId) values ('habitasse platea dictumst maecenas ut', 2010, 3);
insert into Award (Name, Year, MovieId) values ('diam erat fermentum', 2004, 10);
insert into Award (Name, Year, MovieId) values ('quis libero nullam sit amet', 2002, 7);
GO

--MovieActors
insert into MovieActor(MovieId, ActorId)
values
(1,1), (1,2),
(2,2), (2,3),
(3,3), (3,4),
(4,4), (4,5),
(5,5), (5,6),
(6,6), (6,7),
(7,7), (7,8),
(8,8), (8,9),
(9,9)
GO

--MovieGenres
insert into MovieGenre (MovieId, GenreId)
values
(1,1),
(2,1), (2,2),
(3,2),
(4,2), (4,3),
(5,1),
(6,1), (6,2),
(7,2),
(8,2), (8,3),
(9,3)