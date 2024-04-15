
namespace Szakdolgozat.Server.Services.MealService
{
    public class MealService : IMealService
    {
        private DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MealService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Meal>> CreateMeal(Meal meal)
        {
            foreach (var variant in meal.Variants)
            {
                variant.MealType = null;
            }
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Meal> { Data = meal };
        }

        public async Task<ServiceResponse<bool>> DeleteMeal(int mealId)
        {
            var dbMeal = await _context.Meals.FindAsync(mealId);
            if (dbMeal == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Az étel nem található"
                };
            }

            // Ellenőrizzük, hogy az étel szerepel-e valamelyik megrendelésben
            bool isMealInOrder = await _context.OrderItems.AnyAsync(oi => oi.MealId == mealId);

            if (isMealInOrder)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Nem törölhető, az étel szerepel egy vagy több megrendelésben"
                };
            }

            // Az étel nem szerepel egyetlen megrendelésben sem, így törölhetjük
            dbMeal.Deleted = true;
            _context.Meals.Remove(dbMeal);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Az ételt sikeresen törlődött"
            };
        }

        public async Task<ServiceResponse<List<Meal>>> GetAdminMeals()
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await _context.Meals
                    .Where(p => !p.Deleted)
                    .Include(p => p.Variants.Where(v => !v.Deleted))
                    .ThenInclude(v => v.MealType)
                    .Include(p => p.Images)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Meal>>> GetMealsAsync()
        {
            var response = new ServiceResponse<List<Meal>>()
            {
                Data = await _context.Meals.Where(p => p.Visible && !p.Deleted)
                .Include(p => p.Variants
                .Where(v => v.Visible && !v.Deleted))
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Meal>> GetMealAsync(int mealId)
        {
            var response = new ServiceResponse<Meal>();
            Meal meal = null;

            if (_httpContextAccessor.HttpContext.User.IsInRole("admin"))
            {
                meal = await _context.Meals
                .Include(p => p.Variants.Where(v => !v.Deleted))
                .ThenInclude(v => v.MealType)
                .FirstOrDefaultAsync(p => p.Id == mealId && !p.Deleted);
            }
            else
            {
                meal = await _context.Meals
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(v => v.MealType)
                .FirstOrDefaultAsync(p => p.Id == mealId && p.Visible && !p.Deleted);
            }


            if (meal == null)
            {
                response.Success = false;
                response.Message = "Hoppá, ez az étel nem létezik.";
            }
            else
            {
                response.Data = meal;
            }
            return response;
        }

        /*public async Task<ServiceResponse<List<Meal>>> GetMealsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await _context.Meals
                    .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                    .ToListAsync()

            };

            return response;
        }*/

        public async Task<ServiceResponse<List<Meal>>> GetMealsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await _context.Meals
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) && p.Visible && !p.Deleted)
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Meal>>> SearchMeals(string searchText)
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await FindMealsBySearchText(searchText)
            };
            return response;


        }

        private async Task<List<Meal>> FindMealsBySearchText(string searchText)
        {
            return await _context.Meals
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) ||
                                    p.Allergen.ToLower().Contains(searchText.ToLower()) && p.Visible && !p.Deleted)
                                .Include(p => p.Variants)
                                .ToListAsync();
        }


        public async Task<ServiceResponse<Meal>> UpdateMeal(Meal meal)
        {
            var dbMeal = await _context.Meals
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == meal.Id);

            if (dbMeal == null)
            {
                return new ServiceResponse<Meal>
                {
                    Success = false,
                    Message = "Meal not found."
                };
            }

            dbMeal.Name = meal.Name;
            dbMeal.Description = meal.Description;
            dbMeal.Allergen = meal.Allergen;
            dbMeal.ImageUrl = meal.ImageUrl;
            dbMeal.CategoryId = meal.CategoryId;
            dbMeal.Visible = meal.Visible;


            var mealImages = dbMeal.Images;
            _context.Images.RemoveRange(mealImages);

            dbMeal.Images = meal.Images;

            foreach (var variant in meal.Variants)
            {
                var dbVariant = await _context.MealVariants
                    .SingleOrDefaultAsync(v => v.MealId == variant.MealId &&
                        v.MealTypeId == variant.MealTypeId);
                if (dbVariant == null)
                {
                    variant.MealType = null;
                    _context.MealVariants.Add(variant);
                }
                else
                {
                    dbVariant.MealTypeId = variant.MealTypeId;
                    dbVariant.Price = variant.Price;
                    dbVariant.OriginalPrice = variant.OriginalPrice;
                    dbVariant.Visible = variant.Visible;
                    dbVariant.Deleted = variant.Deleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Meal> { Data = meal };
        }

        public async Task<ServiceResponse<List<string>>> GetMealSearchSuggestions(string searchText)
        {
            var meals = await FindMealsBySearchText(searchText);

            List<string> result = new List<string>();
            foreach (var meal in meals)
            {
                if (meal.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(meal.Name);
                }

                if (meal.Description != null)
                {
                    var punctuation = meal.Description.Where(char.IsPunctuation)
                        .Distinct()
                        .ToArray();
                    var words = meal.Description.Split()
                        .Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
                if (meal.Allergen != null)
                {
                    var punctuation = meal.Allergen.Where(char.IsPunctuation)
                        .Distinct()
                        .ToArray();
                    var words = meal.Allergen.Split()
                        .Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }
    }
}