using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.PokemonOwners.Any())
            {
                var pokemonOwners = new List<PokemonOwner>()
                {
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Pikachu",
                            BirthDate = new DateTime(1993,1,1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category(){ Name = "Electric"} }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() {Title="Pikachu",Text="Is best",
                                Reviewer = new Reviewer(){FirstName="Teddy",LastName="Smith" } },
                                new Review() {Title="Pikachu",Text="Is medium",
                                Reviewer = new Reviewer(){FirstName="Meddy",LastName="Bith" } },
                                new Review() {Title="Pikachu",Text="Is worst",
                                Reviewer = new Reviewer(){FirstName="Feddy",LastName="Fith" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Jack",
                            LastName = "Jackery",
                            Gym = "Bricks Gym",
                            Country = new Country()
                            {
                                Name = "Kunto"
                            }
                        }
                    },
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Raichu",
                            BirthDate = new DateTime(1996, 5, 12),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category(){ Name = "Electric"} }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() {Title="Raichu",Text="Is best",
                                Reviewer = new Reviewer(){FirstName="Bobby",LastName="Lake" } },
                                new Review() {Title="Raichu",Text="Is medium",
                                Reviewer = new Reviewer(){FirstName="Hobby",LastName="Bake" } },
                                new Review() {Title="Raichu",Text="Is worst",
                                Reviewer = new Reviewer(){FirstName="Fobby",LastName="Fake" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Tom",
                            LastName = "Tommery",
                            Gym = "Wet Gym",
                            Country = new Country()
                            {
                                Name = "Blomberg"
                            }
                        }
                    },
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Piechu",
                            BirthDate = new DateTime(2000, 2, 1),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category(){ Name = "Food"} }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review() {Title="Piechu",Text="Is best",
                                Reviewer = new Reviewer(){FirstName="Hebby",LastName="Hodge" } },
                                new Review() {Title="Piechu",Text="Is medium",
                                Reviewer = new Reviewer(){FirstName="Tebby",LastName="Dodge" } },
                                new Review() {Title="Piechu",Text="Is worst",
                                Reviewer = new Reviewer(){FirstName="Sebby",LastName="Sodge" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Leg",
                            LastName = "Leggy",
                            Gym = "Arm Gym",
                            Country = new Country()
                            {
                                Name = "PooPlace"
                            }
                        }
                    }
                };
                dataContext.PokemonOwners.AddRange(pokemonOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
