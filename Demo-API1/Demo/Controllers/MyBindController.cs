using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//Binding  API
    public class MyBindController : ControllerBase
    {
        //Bind primitive type ==>Route Segment /id/ ==>Query String ?id=1
        //Bind Complex Type   ==>Request Body
        //[HttpGet("{id:int}")]//api/Mybind/1?name=ahmed
        //public IActionResult Get1(int id,string name)//,string name,Department dept)
        //{
        //    return Ok();
        //}
        
        [HttpPost]
        public IActionResult Add([FromBody]Department dept,string name)//Request body
        {
            return Ok();
        }
        [HttpGet("{id:int}")]//api/Mybind/1?name=ahmed
        public IActionResult Get2(int id,[FromBody] string name)//,string name,Department dept)
        {
            return Ok();
        }
        [HttpGet("{Name:alpha}/{Manager:alpha}/{age:int}")]
        //http://localhost:16123/api/MyBind/xyz/asd
        //http://localhost:16123/api/MyBind?name=asdas&manager=sdsf
        public IActionResult Get3([FromBody]int id,[FromRoute]int age,[FromRoute]Department dept)//,string name,Department dept)
        {
            return Ok();
        }
    }
}
