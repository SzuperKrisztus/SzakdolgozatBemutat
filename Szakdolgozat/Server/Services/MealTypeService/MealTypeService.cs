namespace Szakdolgozat.Server.Services.MealTypeService
{
    public class MealTypeService : IMealTypeService
    {
        private readonly DataContext _context;

        public MealTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<MealType>>> AddMealType(MealType mealType)
        {
            mealType.Editing = mealType.IsNew = false;
            _context.MealTypes.Add(mealType);
            await _context.SaveChangesAsync();

            return await GetMealTypes();
        }

        public async Task<ServiceResponse<List<MealType>>> GetMealTypes()
        {
            var mealTypes = await _context.MealTypes.ToListAsync();
            return new ServiceResponse<List<MealType>> { Data = mealTypes };
        }

        public async Task<ServiceResponse<List<MealType>>> UpdateMealType(MealType mealType)
        {
            var dbMealType = await _context.MealTypes.FindAsync(mealType.Id);
            if (dbMealType == null)
            {
                return new ServiceResponse<List<MealType>>
                {
                    Success = false,
                    Message = "Meal Type not found."
                };
            }

            dbMealType.Name = mealType.Name;
            await _context.SaveChangesAsync();

            return await GetMealTypes();
        }
    }
}
