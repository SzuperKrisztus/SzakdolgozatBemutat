using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Shared
{
    public class MealsSearchResultDTO
    {
        public List<Meal> Meals { get; set; } = new List<Meal>();

        public int Number { get; set; }
    }
}
