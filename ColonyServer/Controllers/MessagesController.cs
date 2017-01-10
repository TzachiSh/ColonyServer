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
        [Route("api/Messages/{isGroup}")]
        public IHttpActionResult Post([FromUri]Boolean isGroup ,[FromBody] Message message)
        {
           string stringregIds;
            // on 
            if (isGroup)
            {
              Group group = new Group();
              var regIds = group.GetTokensGroup(Int32.Parse(message.ReceiverNumber) , message.SenderName);

             stringregIds = string.Join("\",\"", regIds);

            }
            else
            {
                stringregIds = message.FindToken();
            }

            return Ok(message.sendMessage(stringregIds,isGroup));


        }

     

    }
}
