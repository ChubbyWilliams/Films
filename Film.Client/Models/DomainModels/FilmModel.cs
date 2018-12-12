using Film.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Film.Client.Models.DomainModels
{
    public sealed class FilmModel : IEquatable<FilmModel>
    {
        
        [Key]
        public Guid FilmId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ProductionYear { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string PosterUri { get; set; }
        [Required]
        public string UserId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as FilmModel);
        }

        public bool Equals(FilmModel other)
        {
            return other != null &&
                   Title == other.Title &&
                   ProductionYear == other.ProductionYear &&
                   Director == other.Director;
        }

        public override int GetHashCode()
        {
            var hashCode = -1233735591;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime>.Default.GetHashCode(ProductionYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Director);
            return hashCode;
        }

        public void UpdateFilmInfo(
            string title,
            string description,
            DateTime productionYear,
            string director,
            string posterUri,
            string userId
        )
        {
            if (UserCheck(userId))
            {
                Title = title;
                Description = description;
                ProductionYear = productionYear;
                Director = director;
                PosterUri = posterUri;
            }
            else
            {
                throw new FilmDomainRoleException();
            }

        }

        public bool UserCheck(string userId)
        {
            return userId == UserId;
        }

        public static bool operator ==(FilmModel domain1, FilmModel domain2)
        {
            return EqualityComparer<FilmModel>.Default.Equals(domain1, domain2);
        }

        public static bool operator !=(FilmModel domain1, FilmModel domain2)
        {
            return !(domain1 == domain2);
        }
    }
}
