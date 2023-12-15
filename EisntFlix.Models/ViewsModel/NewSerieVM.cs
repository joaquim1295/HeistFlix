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
    public class NewSerieVM
    {
        public NewSerieVM()
        {

        }
        public NewSerieVM(Serie SerieDetails)
        {
            Id = SerieDetails.Id;
            Name = SerieDetails.Name;
            Description = SerieDetails.Description;
            Rating = SerieDetails.Rating;
            StartDate = SerieDetails.StartDate;
            EndDate = SerieDetails.EndDate;
            ImageURL = SerieDetails.ImageURL;
            MovieCategory = SerieDetails.FilmCategory;
            StreamingId = SerieDetails.StreamingId;
            ProducerId = SerieDetails.ProducerId;
            ActorIds = SerieDetails.Actors_Series.Select(n => n.ActorId).ToList();
        }



        [Key]
        public int Id { get; set; }

        [Display(Name = "Serie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Serie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price €")]
        public double? Price { get; set; }
        public float? Rating { get; set; }

        [Display(Name = "Serie poster URL")]
        [Required(ErrorMessage = "Serie poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Serie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Serie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Serie category is required")]
        public FilmCategory? MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Serie actor(s) is required")]
        public List<int>? ActorIds { get; set; }

        [Display(Name = "Select a Streaming")]
        [Required(ErrorMessage = "Serie Streaming is required")]
        public int? StreamingId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Serie producer is required")]
        public int? ProducerId { get; set; }

    }
}

