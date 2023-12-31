﻿namespace PokemonReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public Reviewer Reviewer { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
