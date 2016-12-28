using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ColonyServer.App_Data;
using ColonyServer.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ColonyServer.Controllers
{
    public class GroupsController : ApiController
    {
        private ColonyDBContext db = new ColonyDBContext();

        Group group = new Group();
        User user = new User();

        // GET: api/Group/5
        //groupDetails
        [Route("api/Groups/{groupId}")]
        [ResponseType(typeof(Group))]
        public IHttpActionResult GetGroup(int groupId)
        {

            var groupDetails = group.GroupDetails(groupId);

            return Ok(groupDetails);
        }

        ////PUT: api/Group/AddUser/5
        [Route("api/Groups/{groupId}/AddUser/")]
        [HttpPost]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult AddUserToGroup(int groupId,[FromBody] String jsUsersNumber)
        {
            var usersNumber = JsonConvert.DeserializeObject<List<String>>(jsUsersNumber);

            group = group.AddUsersToGroup(groupId, usersNumber);      

            return Ok();
        }


        // POST: api/Groups
        //create group
        [ResponseType(typeof(Group))]
        [HttpPost]
        public IHttpActionResult NewGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!group.NewGroup())
            {
                return Ok("Error");

            }

            //return CreatedAtRoute("DefaultApi", new { id = group.GroupId }, group);
            return Ok(group.GroupId);
        }

        //// DELETE: api/Groups/5
        //[ResponseType(typeof(Group))]
        //public IHttpActionResult DeleteGroup(int id)
        //{
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Groups.Remove(group);
        //    db.SaveChanges();

        //    return Ok(group);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


    }
}