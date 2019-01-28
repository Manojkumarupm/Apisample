using System;
using System.Collections.Generic;
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
        public IEnumerable<Users> Get()
        {
            return UserDetailsGetter.GetAllUser();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Users))]
        public HttpResponseMessage Get(int UserId)
        {

            Users i = UserDetailsGetter.GetUser(UserId);
            if (i != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Id : " + UserId + " Not Found");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public HttpResponseMessage Post([FromBody] Users i)
        {
            string Result = null;
            try
            {
                Result = UserDetailsGetter.AddUser(i);
                if (Result.Equals("Success"))
                {
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.Created, i);
                    return Message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put([FromBody] Users i)
        {
            string Result = null;
            try
            {
                Result = UserDetailsGetter.UpdateUser(i);
                if (Result.Equals("Success"))
                {
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, i);
                    return Message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Result);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        [ResponseType(typeof(Users))]
        public HttpResponseMessage Delete(int UserId)
        {
            string Result = null;
            try
            {
                Result = UserDetailsGetter.RemoveUser(UserId);
                if (Result.Equals("Success"))
                {
                    HttpResponseMessage Message = Request.CreateResponse(HttpStatusCode.OK, Result);
                    return Message;
                }
                else if (Result.Contains(" Not found"))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, Result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Result);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
