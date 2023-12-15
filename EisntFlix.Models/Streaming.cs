using EisntFlix.Root.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EisntFlix.Models
{
    public class Streaming : IEntityBase
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Streaming logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Streaming name is required")]
        public string Name { get; set; }
        [Display(Name = "Description")]

        [Required(ErrorMessage = "Streaming description is required")]
        public string? Description { get; set; }

        //Relationship
        public List<Movie>? Movies { get; set; }
        public List<Serie>? Series { get; set; }


    }
}
