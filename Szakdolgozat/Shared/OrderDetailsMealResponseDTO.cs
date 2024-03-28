using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Shared
{
    public class OrderDetailsMealResponseDTO
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        public string MealType { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
