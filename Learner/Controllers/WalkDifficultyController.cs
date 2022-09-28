using Learner.Models.Domain;
using Learner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Learner.Controllers
{
    [ApiController]
    [Route("difficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultys walkDifficultys;

        public WalkDifficultyController(IWalkDifficultys walkDifficultys)
        {
            this.walkDifficultys = walkDifficultys;
        }

        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            var request= await walkDifficultys.GetAll();
            return Ok(request);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetById")]
    public async Task<IActionResult> GetByID(Guid id)
        {
            var req= await walkDifficultys.GetByID(id);
            if (req == null)
            {
                return NotFound();
            }
            return Ok(req);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalk([FromBody] Models.DTO.WalkDifficulty.WalkDifficulty walkDifficulty)
        {
            //DTO To Domain
            var responseDomain = new Models.Domain.WalkDifficulty()
            {
                Code = walkDifficulty.Code,
            };
            var response =await  walkDifficultys.CreateNew(responseDomain);
            return CreatedAtAction(nameof(GetByID), new { id = response.Id }, response);
        }
        
               [HttpPut]
               [Route("{id:guid}")]
             public async Task<IActionResult> UpdateWalk(Guid id, Models.DTO.WalkDifficulty.WalkDifficulty walkDifficulty)
               {
            //Covert DTO to Model

            var requestModel = new Models.Domain.WalkDifficulty()
            {
                Id = id,
                Code = walkDifficulty.Code,
            };
                   var request = await walkDifficultys.UpdatePost(id, requestModel);
                   if (request == null)
                   {
                       return NotFound();
                   }
                   return Ok(request);
               }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var request =await walkDifficultys.DeletePost(id);
        if (request == null)
            {
                return NotFound();
            }
            return Ok(request);

        }

    }
}
