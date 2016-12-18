using ColonyServer.App_Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ColonyServer.Models
{
    public class Group
    {
   
        public int GroupId { get; set;  }

        [Required]
        public string GroupName { get; set; }
        public string Created { get; set; }

        public virtual ICollection<User> Users{get; set;}

        private ColonyDBContext db = new ColonyDBContext();

        public bool NewGroup()
        {

            db.Groups.Add(this);

            if (GroupExists(this.GroupId))
            {

                return false;
            }
            try
            {
                db.SaveChanges();
                return true;

            }
            catch (DbUpdateException)
            {
                throw;

            }

        }

        public Group AddUsersToGroup(int groupId ,List<string> usersNumber)
        {
            var group = db.Groups.Find(groupId);
            User user = new User();
     
            foreach (var userNumber in usersNumber)
            {
                user = db.Users.FirstOrDefault(u=>u.Number == userNumber);
                //user.Groups = null;

                group.Users.Add(user);

                
            }
            db.Entry(group).State = EntityState.Modified;
            try
            {
                db.SaveChanges();

                return group;
            }
            catch (System.Exception)
            {

                throw;
            }
            

        }

        public IEnumerable GroupDetails(int idGroup)        
        {
            
            var group = db.Groups.Where(g => g.GroupId == idGroup)
                .Select(g => new { g.GroupId, g.GroupName, g.Created, Users = g.Users
                .Select(u => new List<string> { u.UserName,u.Number })});
            return  group;
        }

        public List<string> GetTokensGroup(int idGroup)
        {
            List<string> tokens = new List<string>();
            var group = db.Groups.Where(g => g.GroupId == idGroup).SelectMany(g => g.Users.Select(u => u.Token)).ToList();
 
            return group;
            
            
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }

      


    }

}