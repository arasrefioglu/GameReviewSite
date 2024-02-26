﻿using GameReviewSite.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewSite.Entities.Concrete
{
    public class Game : AEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int ReleaseYear { get; set; }
        public double EditorRating { get; set; }

        public virtual ICollection<UserRating> Ratings { get; set; }
        public virtual ICollection<GameComment> Comments { get; set; }
        public int GameGenreId { get; set; }
        public virtual GameGenre Genre { get; set; }

        public virtual ICollection<GamePrice> Prices { get; set; }
    }
}
