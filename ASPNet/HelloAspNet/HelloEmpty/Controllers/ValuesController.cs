using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloEmpty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloEmpty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public List<HelloMessage> Get()
        {
            List<HelloMessage> messages = new List<HelloMessage>();
            messages.Add(new HelloMessage() { Messsage = "Hello Web API 1 !" });
            messages.Add(new HelloMessage() { Messsage = "Hello Web API 2 !" });
            messages.Add(new HelloMessage() { Messsage = "Hello Web API 3 !" });

            return messages;
        }
    }
}
