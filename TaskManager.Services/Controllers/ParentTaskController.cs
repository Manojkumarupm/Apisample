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
    public class ParentTaskController : ApiController
    {
        public ParentTaskCrud ParentTaskCrudDetailGetter { get; set; }
        public ParentTaskController()
        {
            ParentTaskCrudDetailGetter = new ParentTaskCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParentTask> Get()
        {
            return ParentTaskCrudDetailGetter.GetAllParentTask();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId">ProjectId</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Project))]
        public HttpResponseMessage Get(int ProjectId)
        {

            ParentTask i = ParentTaskCrudDetailGetter.GetParentTask(ProjectId);
            if (i != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ParentTaskCrud Id : " + ProjectId + " Not Found");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public HttpResponseMessage Post([FromBody] ParentTask i)
        {
            string Result = null;
            try
            {
                Result = ParentTaskCrudDetailGetter.AddParentTask(i);
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
        public HttpResponseMessage Put([FromBody] ParentTask i)
        {
            string Result = null;
            try
            {
                Result = ParentTaskCrudDetailGetter.UpdateParentTask(i);
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
        /// <param name="ParentTaskId">ParentTaskId</param>
        /// <returns></returns>
        [ResponseType(typeof(Project))]
        public HttpResponseMessage Delete(int ParentTaskId)
        {
            string Result = null;
            try
            {
                Result = ParentTaskCrudDetailGetter.RemoveParentTask(ParentTaskId);
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
