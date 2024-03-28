using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Shared
{
    public class Meal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Allergen { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl {  get; set; } = string.Empty;
        [Column(TypeName ="decimal(18,2)")]

        public List<Image> Images { get; set; } = new List<Image>();
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public  List<MealVariant> Variants {get; set; } = new List<MealVariant>();

        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;



    }
}
