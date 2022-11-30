using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _9.gyakorlat.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoatController : ControllerBase
    {
        [HttpGet]
        [Route("questions/all")]
        public IActionResult M1()
        {
            Models.HajosContext hajosContext = new Models.HajosContext();
            var kerdesek = from x in hajosContext.Questions select x;
            return new JsonResult(kerdesek);

        }

        [HttpGet]
        [Route("questions/{sorszám}")]
        public IActionResult f2(int sorszám)
        {
            Models.HajosContext hajosContext = new Models.HajosContext();
            var kérdés = (from x in hajosContext.Questions
                        where x.QuestionId == sorszám
                        select x).FirstOrDefault();

            if (kérdés == null) return BadRequest("Nincs ilyen sorszámú kérdés");
           
            return  new JsonResult(kérdés);
        }
        


    }
}
