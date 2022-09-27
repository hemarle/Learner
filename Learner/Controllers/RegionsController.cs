using AutoMapper;
using Learner.Models.Domain;
using Learner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Learner.Controllers
{
    [ApiController]
    [Route ("regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            var regions = await regionRepository.GetAll();

            //Convert Domain to DTO
            var regionDTO= mapper.Map<List<Models.DTO.Region>>(regions);
           

           return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
           var region=  await regionRepository.GetRegion(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO= mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request (DTO) to Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };
            
            //Pass details to Repo
          region= await regionRepository.AddRegion(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                ID = region.ID,
                Name = region.Name,
                Area = region.Area,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.ID }, regionDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get Region from the db
            var region= await regionRepository.DeleteRegion(id);
            //If null return 404
            if (region == null)
            {
                return NotFound();
            }
            //Conver response back to DTO
            var regionDTO = new Models.DTO.Region() {
            ID = region.ID,
            Area = region.Area,
            Code=region.Code,
            Lat=region.Lat,
            Long=region.Long,
            Name = region.Name,
            Population = region.Population
            };
            regionDTO = mapper.Map<Models.DTO.Region>(region);

            //response ok
            return Ok(regionDTO);  
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,[FromBody] Learner.Models.DTO.UpdateRegionRequest updateRegionRequest)
        {

            //Convert DTo to Domain
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };

            //Update Region using Repositor
           region= await regionRepository.UpdateRegion(id, region);
            //If null, Not Fount
            if (region == null)
            {
                return NotFound();
            }
            //Convert Domain to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            //Return OK
            return Ok(regionDTO);
        }
    }
}
