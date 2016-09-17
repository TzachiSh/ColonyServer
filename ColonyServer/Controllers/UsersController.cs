using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ColonyServer.App_Data;
using ColonyServer.Models;

namespace ColonyServer.Controllers
{
    public class UsersController : ApiController
    {
        AlertDialog alertDialog = new AlertDialog(); 
        User user = new User();
        
        // GET: api/Users/5
        //Find user 
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            user=user.FindUser(id);
            
            if ( user != null)
            {
                alertDialog.Code = "user_success";
                alertDialog.Message = user.UserName;
                
            }
            else
            {
                alertDialog.Code = "user_error";
                alertDialog.Message = "User not Found";
            }
            return Ok(alertDialog);

            
            }

        [HttpPost]
        // Login user 
        public IHttpActionResult PostLogin(User login)
        {

            if (ModelState.IsValid)
            {


                try
                {
                    
                    alertDialog.Message = user.FindUser(login.UserName).LoginUser(login);
                    if (alertDialog.Message.Equals("User login"))
                    {
                        alertDialog.Code = "log_success";
                    }
                    else
                    {
                        alertDialog.Code = "log_error";
                    }
                }
                catch (NullReferenceException)
                {
                    alertDialog.Message = "User not found";
                    alertDialog.Code = "log_error";

                    return Ok(alertDialog);
                }
            }
            else
            {
                alertDialog.Message = string.Join(" \n", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
                alertDialog.Code = "log_error";

            }
            return Ok(alertDialog);
        }

       

        // PuT: api/Users
        //Add new user
        [ResponseType(typeof(User))]
        public IHttpActionResult PutUser(User user)
        {
            if (ModelState.IsValid)
            {

                if (!user.NewUser())
                {
                    alertDialog.Message = "User exsist";
                    alertDialog.Code = "reg_error";

                }
                else
                {
                    alertDialog.Message = "user Created";
                    alertDialog.Code = "reg_success";

                }
            }
            else
            {
                alertDialog.Message = string.Join(" \n", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
                alertDialog.Code = "reg_error";


            }
                    
          
            return Ok(alertDialog);
        }

        // DELETE: api/Users/name
        //Delete user
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
           
            if(!user.FindUser(id).DeleteUser())
            {
                return NotFound();

            }
            return Ok("user Delteted !");
        }

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