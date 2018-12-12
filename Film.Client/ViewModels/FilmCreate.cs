using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Film.Client.ViewModels
{
    public class FilmCreate
    {
        public Guid FilmId { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public DateTime ProductionYear { get; set; }
        [Required] public string PosterUri { get; set; }
        [Required] public string Director { get; set; }
        public HttpPostedFileBase Upload { get; set; }
    }
}