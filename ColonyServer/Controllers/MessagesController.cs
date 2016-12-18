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
           string stringregIds;
            // on 
            if (message.ReceiverNumber == null)
            {
                Group group = new Group();

              var regIds = group.GetTokensGroup(message.GroupId);
              stringregIds = string.Join("\",\"", regIds);

            }
            else
            {
                stringregIds = message.FindToken();
            }

            return Ok(message.sendMessage(stringregIds));


        }

     

    }
}
