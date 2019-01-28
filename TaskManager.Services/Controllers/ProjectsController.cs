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
    public class ProjectsController : ApiController
    {

        public ProjectCrud ProjectsDetailGetter { get; set; }
        public ProjectsController()
        {
            ProjectsDetailGetter = new ProjectCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> Get()
        {
            return ProjectsDetailGetter.GetAllProject();
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

            Project i = ProjectsDetailGetter.GetProject(ProjectId);
            if (i != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Projects Id : " + ProjectId + " Not Found");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public HttpResponseMessage Post([FromBody] Project i)
        {
            string Result = null;
            try
            {
                Result = ProjectsDetailGetter.AddProject(i);
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
        public HttpResponseMessage Put([FromBody] Project i)
        {
            string Result = null;
            try
            {
                Result = ProjectsDetailGetter.UpdateProject(i);
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
        /// <param name="ProjectId">ProjectId</param>
        /// <returns></returns>
        [ResponseType(typeof(Project))]
        public HttpResponseMessage Delete(int ProjectId)
        {
            string Result = null;
            try
            {
                Result = ProjectsDetailGetter.RemoveProject(ProjectId);
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
