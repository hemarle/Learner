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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultys walkDifficultys;

        public WalksController(IWalkRepository walkRepository, IRegionRepository regionRepository, IWalkDifficultys walkDifficultys)
        {
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.walkDifficultys = walkDifficultys;
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
            var validateWalk = await ValidateWalks(addWalk);

            if (! (validateWalk))
            {
                return BadRequest(ModelState);
            }
            
            var walk = new Models.Domain.Walk()
            {
                Length = addWalk.Length,
                Name = addWalk.Name,
                regionID = addWalk.regionID,
                WalkDifficultyID = addWalk.WalkDifficultyID

            };
            var response = await walkRepository.CreateWalk(walk);

            var responseDTO = new Learner.Models.DTO.Walk.AddWalk()
            {
                regionID= response.regionID,
                Name = response.Name,   
                WalkDifficultyID = response.WalkDifficultyID,
                Length=response.Length,
                
            };

            return CreatedAtAction(nameof(GetWalkByID), new { id = response.Id }, responseDTO);
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
        #region Private
        private async Task<bool> ValidateWalks(Learner.Models.DTO.Walk.AddWalk addWalk)
        {
            var allRegions = await regionRepository.GetRegion(addWalk.regionID);
            var allWalksDifficultys = await walkDifficultys.GetByID(addWalk.WalkDifficultyID);
            if (allRegions == null)
            {
                ModelState.AddModelError("Region Id", "Region Id doesn't exist");
            }
            if (allWalksDifficultys == null)
            {
                ModelState.AddModelError("WalkDifficulty", "WalkDifficulty Id doesn't exist");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;



        }
        #endregion
    }



}
