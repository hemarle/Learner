using Learner.Data;
using Learner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Learner.Controllers
{
    [ApiController]
    [Route("walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;

        public WalksController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }
        [HttpGet]

        public async Task<IActionResult> GetWalks()
        {
            var response = await walkRepository.GetAllWalks();
            return Ok(response);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByID")]
        public async Task<IActionResult> GetWalkByID(Guid id)
        {
            var response = await walkRepository.GetWalkById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        //[Route("{id:guid}")]
        public async Task<IActionResult> CreateWalk(Learner.Models.DTO.Walk.AddWalk addWalk)
        {
            //Convert DTO to domain
            /* var body = new Models.Domain.Walk()
             {
                 Name = addWalk.Name,
                 Length = addWalk.Length,
             };

             var response =await walkRepository.CreateWalk(body);

             //Convert domain to DTO
             var responseDTO = new Learner.Models.DTO.Walk.Walk()
             {
                 Id = response.Id,
             Name=response.Name,
             Length=response.Length,

             };

 */
            var walk = new Models.Domain.Walk()
            {
                Length = addWalk.Length,
                regionID = addWalk.regionID,
                Name = addWalk.Name,
                WalkDifficultyID = addWalk.WalkDifficultyID

            };
            var response = await walkRepository.CreateWalk(walk);

            return CreatedAtAction(nameof(GetWalkByID), new { id = response.Id }, response);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateWalk(Guid id, Learner.Models.DTO.Walk.AddWalk updateWalk)
        {
            var responseModel = new Models.Domain.Walk()
            {
                Id = id,
                Name = updateWalk.Name,
                Length = updateWalk.Length,
                WalkDifficultyID = updateWalk.WalkDifficultyID,
                regionID = updateWalk.regionID

            };

            var response = await walkRepository.UpdateWalk(id, responseModel);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);

        }



        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveWalk(Guid id)
        {
            var request = await walkRepository.DeleteWalk(id);

            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);

        }
    }
}
