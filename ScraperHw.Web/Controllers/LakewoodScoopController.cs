using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScraperHw.Data;

namespace ScraperHw.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LakewoodScoopController : ControllerBase
    {


        [HttpGet]
        [Route("scrape")]
        public List<LakewoodScoopItem> Scrape()
        {
            return ScraperHw.Data.LakewoodScoopScraper.Scrape();
        }
    }
}
