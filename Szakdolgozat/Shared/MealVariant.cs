using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Szakdolgozat.Shared
{
    public class MealVariant
    {
        [JsonIgnore]
        public Meal Meal { get; set; }

        public int MealId { get; set; }

        public MealType MealType { get; set; }

        public int MealTypeId {  get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }

    }
}
