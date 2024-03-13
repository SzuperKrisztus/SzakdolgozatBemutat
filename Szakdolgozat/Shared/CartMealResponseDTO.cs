using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Shared
{
    public class CartMealResponseDTO
    {

        public int MealId { get; set; }
        public int MealTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MealType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
