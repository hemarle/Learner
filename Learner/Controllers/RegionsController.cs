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

            //return dto
            /*  var regionsDTO = new List<Models.DTO.Region>();
              regions.ToList().ForEach(region =>
              {

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
                  regionsDTO.Add(regionDTO);  
              });*/
            var regionDTO= mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionDTO);
        }

    }
}
