using EisntFlix.Root.Enums;
using EisntFlix.Root.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models
{
    public class Film : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="API Id")]
        public int ApiId { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? Rating { get; set; }

        public double? Price { get; set; }

        [Display(Name ="Poster Image")]
        public string? ImageURL { get; set; }

        [Display(Name="Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Category")]
        public FilmCategory? FilmCategory { get; set; }

        //Streaming
        public int? StreamingId { get; set; }
        [ForeignKey("StreamingId")]
        public Streaming? Streaming { get; set; }

        //Producer
        public int? ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
        public string? Type { get; set; }

	}
}
