using ColonyServer.App_Data;
using ColonyServer.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace ColonyServer.Controllers
{
    public class MessagesController : ApiController
    {
        

        // POST: api/Messages
        public IHttpActionResult Post(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            return Ok(message.sendMessage());
        }

    }
}
