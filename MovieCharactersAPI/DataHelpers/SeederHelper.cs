using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCharactersAPI.Model;

namespace MovieCharactersAPI.DataHelpers
{
    /// <summary>
    /// The class <c>SeederHelper</c> used to seed a database with data using the models Character, Franchise, and Movie.
    /// </summary>
    public class SeederHelper
    {
        /// <summary>
        /// Create four Characters
        /// </summary>
        /// <returns>List of Characters</returns>
        public static List<Character> SeedCharacterHelper()
        {
            List<Character> characters = new List<Character>()
            {
                new Character(){
                    CharacterId = 1,
                    FullName = "Luke Skywaker",
                    Alias = "Wormie",
                    Gender = "Male",
                    Picture = "https://tse3.mm.bing.net/th/id/OIP.uPTRLR8uspCiafiunUqKfQHaMJ?pid=ImgDet&rs=1",
                    },
                new Character(){
                    CharacterId = 2,
                    FullName = "Chewbacca",
                    Alias = "Chewie",
                    Gender = "Male",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/1/12/Chewbaca_%28Peter_Mayhew%29.png",
                    },
                new Character(){
                    CharacterId = 3,
                    FullName = "Han Solo",
                    Alias = "Captain Solo",
                    Gender = "Male",
                    Picture = "https://upload.wikimedia.org/wikipedia/en/b/be/Han_Solo_depicted_in_promotional_image_for_Star_Wars_%281977%29.jpg",
                    },
                new Character(){
                    CharacterId = 4,
                    FullName = "Frodo Baggins",
                    Alias = "Mr Underhill",
                    Gender = "Male",
                    Picture = "https://th.bing.com/th/id/R.230fcc34a11720fccce4ec86c0e345e9?rik=cOfjmuogT%2b8NDA&pid=ImgRaw",
                    },
            };
            return characters;
        }
        
        /// <summary>
        /// Create two Franchises
        /// </summary>
        /// <returns>List of Franchises</returns>
        public static List<Franchise> SeedFranchiseHelper()
        {
            List<Franchise> franchises = new List<Franchise>()
            {
                new Franchise()
                {
                    FranchiseId = 1,
                    Name = "Star Wars",
                    Description = "Star Wars is an American epic space opera[1] multimedia franchise created by George Lucas, which began with the eponymous 1977 film[b] and quickly " +
                    "became a worldwide pop-culture phenomenon. The franchise has been expanded into various films and other media, including television series, video games, novels, " +
                    "comic books, theme park attractions, and themed areas, comprising an all-encompassing fictional universe.[c] In 2020, its total value was estimated at US$70 billion, " +
                    "and it is currently the fifth-highest-grossing media franchise of all time.",
                },
                new Franchise()
                {
                    FranchiseId = 2,
                    Name = "The Lord of the Rings",
                    Description = "The Lord of the Rings is a series of three epic fantasy adventure films directed by Peter Jackson, based on the novel written by J. R. R. Tolkien. " +
                    "The films are subtitled The Fellowship of the Ring (2001), The Two Towers (2002), and The Return of the King (2003). Produced and distributed by New Line Cinema with " +
                    "the co-production of WingNut Films, it is an international venture between New Zealand and the United States. The films feature an ensemble cast including Elijah Wood, " +
                    "Ian McKellen, Liv Tyler, Viggo Mortensen, Sean Astin, Cate Blanchett, John Rhys-Davies, Christopher Lee, Billy Boyd, Dominic Monaghan, Orlando Bloom, Hugo Weaving, Andy " +
                    "Serkis, and Sean Bean.",
                }
            };
            return franchises;
        }

        /// <summary>
        /// Create three movies
        /// </summary>
        /// <returns>List of Movies</returns>
        public static List<Movie> SeedMovieHelper()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    MovieId = 1,
                    MovieTitle = "Episode I - The Phantom Menace",
                    Genre = "Science fiction",
                    ReleaseYear = 1999,
                    Director = "George Lucas",
                    FranchiseId = 1,
                    Trailer = "https://www.imdb.com/video/vi2143788569?playlistId=tt0120915&ref_=tt_ov_vi",               
                },
                new Movie()
                {
                    MovieId = 2,
                    MovieTitle = "The Fellowship of the Ringe",
                    Genre = "Adventure",
                    ReleaseYear = 2001,
                    Director = "Peter Jackson",
                    FranchiseId = 2,
                    Trailer = "https://www.youtube.com/watch?v=V75dMMIW2B4",
                },
                new Movie()
                {
                    MovieId = 3,
                    MovieTitle = "The Two Towers",
                    Genre = "Adventure",
                    ReleaseYear = 2002,
                    Director = "Peter Jackson",
                    FranchiseId = 2,
                    Trailer = "https://www.youtube.com/watch?v=LbfMDwc4azU",
                }
            };
            return movies;
        }
        
        public static List<CharacterMovie> CharacterMovies()
        {
            List<CharacterMovie> characterMovies = new List<CharacterMovie>()
            {
                new CharacterMovie()
                {
                    CharacterId = 4,
                    MovieId = 2,
                },
                 new CharacterMovie()
                {
                    CharacterId = 4,
                    MovieId = 3,
                },
                new CharacterMovie()
                {
                    CharacterId = 1,
                    MovieId = 1,
                },
                new CharacterMovie()
                {
                    CharacterId = 2,
                    MovieId = 1,
                },
                 new CharacterMovie()
                {
                    CharacterId = 3,
                    MovieId = 2,
                },
            };

            return characterMovies;
        }

    }
}
