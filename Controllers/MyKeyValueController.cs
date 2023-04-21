using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MyKeyValueController : Controller
    {
        private readonly MyKeyValueAPIDbContext dbContext;

        public MyKeyValueController(MyKeyValueAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetMyKeyValues()
        {
             return Ok ( await dbContext.MyKeyValues.ToListAsync());
           // return View();
        }

        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> AddKeyValueRequest(AddKeyValueRequest addKeyValueRequest)
        {
            var myKeyValue = new MyKeyValue()
            {
                Id = Guid.NewGuid(),
                Key = addKeyValueRequest.Key,
                Value = addKeyValueRequest.Value

            };
            await dbContext.MyKeyValues.AddAsync(myKeyValue);
            await dbContext.SaveChangesAsync();

            return Ok(myKeyValue);
        }

        [HttpPatch] 
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateMyKeyValue([FromRoute] Guid id , UpdateMyKeyValue updateMyKeyValue)
        {
            var myKeyValue = await dbContext.MyKeyValues.FindAsync(id);

            if (myKeyValue != null)
            {
               myKeyValue.Key = updateMyKeyValue.Key;
                myKeyValue.Value = updateMyKeyValue.Value;

                await dbContext.SaveChangesAsync();

                return Ok(myKeyValue);
            }

           
            return NotFound();
           
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteMyKeyValue([FromRoute] Guid id)
        {
            var myKeyValue = await dbContext.MyKeyValues.FindAsync(id);

            if (myKeyValue != null)
            {
               dbContext.Remove(myKeyValue);
                await dbContext.SaveChangesAsync();
                return Ok(myKeyValue);
            }
            return NotFound();

        }
    }
}
