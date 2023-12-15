using EisntFlix.Root.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EisntFlix.Models.ViewsModel
{
    public class NewMovieVM 
    {
        public NewMovieVM() { }

        public NewMovieVM(Movie movieDetails)
        {
            Id = movieDetails.Id;
            Name = movieDetails.Name;
            Description = movieDetails.Description;
            Rating = movieDetails.Rating;
            StartDate = movieDetails.StartDate;
            EndDate = movieDetails.EndDate;
            ImageURL = movieDetails.ImageURL;
            MovieCategory = movieDetails.FilmCategory;
            StreamingId = movieDetails.StreamingId;
            ProducerId = movieDetails.ProducerId;
            ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList();
        }



        [Key]
        public int Id { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price €")]
        public double? Price { get; set; }

        public float? Rating { get; set; }

        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public FilmCategory? MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int>? ActorIds { get; set; }

        [Display(Name = "Select a Streaming")]
        [Required(ErrorMessage = "Movie Streaming is required")]
        public int? StreamingId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int? ProducerId { get; set; }
    }
}
