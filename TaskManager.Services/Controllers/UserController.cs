using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BL;
using TaskManager.DAL;

namespace TaskManager.Services.Controllers
{
    public class UserController : ApiController
    {
        public UserCrud UserDetailsGetter { get; set; }
        public UserController()
        {
            UserDetailsGetter = new UserCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return UserDetailsGetter.GetAllUser();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(int UserId)
        {

            User i = UserDetailsGetter.GetUser(UserId);
            if (i != null)
            {
                return Ok(i);
            }
            else
            {
                return NotFound();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Post( User t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDetailsGetter.AddUser(t);
            return CreatedAtRoute("DefaultApi", new { id = t.UserId }, t);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int UserId, User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UserId != User.UserId)
            {
                return BadRequest();
            }
            try
            {
                UserDetailsGetter.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailsGetter.IsUserExist(UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public IHttpActionResult Delete(int UserId)
        {
            User User = UserDetailsGetter.GetUser(UserId);
            if (User == null)
            {
                return NotFound();
            }
            UserDetailsGetter.RemoveUser(UserId);
            return Ok(User);
        }
    }
}
