using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Szakdolgozat.Server.Services.MealTypeService;

namespace Szakdolgozat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class MealTypeController : ControllerBase
    {
        private readonly IMealTypeService _mealTypeService;

        public MealTypeController(IMealTypeService mealTypeService)
        {
            _mealTypeService = mealTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<MealType>>>> GetMealTypes()
        {
            var response = await _mealTypeService.GetMealTypes();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<MealType>>>> AddMealType(MealType mealType)
        {
            var response = await _mealTypeService.AddMealType(mealType);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<MealType>>>> UpdateMealType(MealType mealType)
        {
            var response = await _mealTypeService.UpdateMealType(mealType);
            return Ok(response);
        }
    }
}
