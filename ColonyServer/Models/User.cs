using ColonyServer.App_Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ColonyServer.Models
{
    
    public class User
    {
        private ColonyDBContext db = new ColonyDBContext();

        [Key]
        public int UserId { get; set; }

        [RegularExpression("([a-zA-Z0-9.&'-]+)", ErrorMessage = "Enter only alphabets and numbers of User")]
        [Required ,MinLength(4)]
        public string UserName { get; set; }

        [RegularExpression("([a-zA-Z0-9.&'-]+)", ErrorMessage = "Enter only alphabets and numbers of Password")]
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public User()
        {
            Created = DateTime.UtcNow;  
        }

        public bool NewUser()
        {

            db.Users.Add(this);

            if (UserExists(this.UserName))
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

        public User FindUser(string userName)
        {

                User user = db.Users.FirstOrDefault(acc => acc.UserName == userName);
                return user;
                  
         }

        public string LoginUser(User user)
        {
            
            try
            {
                
                if (user.Password != this.Password)
                {
                   
                    return "Password Incorect";

                }
                if (this.Token != user.Token)
                {

                    var usr = db.Users.FirstOrDefault(acc => acc.UserName == user.UserName);
                    usr.Token = user.Token;
                    db.SaveChanges();

                }

                
            }
            catch (NullReferenceException)
            {
               
                return this.UserName + " not Found";
            }
            return "User login";



        }

        public bool DeleteUser()
        {
            if (this == null)
            {
                return false;
            }


            db.Users.Remove(this);
            db.SaveChanges();

            return true;

        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.UserName == id) > 0;

        }


    }
  
}