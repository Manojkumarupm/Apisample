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

namespace ProjectManager.Services.Controllers
{
    public class ProjectsController : ApiController
    {
        public ProjectCrud ProjectDetailsGetter { get; set; }
        public ProjectsController()
        {
            ProjectDetailsGetter = new ProjectCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> Get()
        {
            return ProjectDetailsGetter.GetAllProject();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Project))]
        public IHttpActionResult Get(int ProjectId)
        {

            Project i = ProjectDetailsGetter.GetProject(ProjectId);
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
        public IHttpActionResult Post( Project t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProjectDetailsGetter.AddProject(t);
            return CreatedAtRoute("DefaultApi", new { id = t.ProjectId }, t);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int ProjectId, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ProjectId != project.ProjectId)
            {
                return BadRequest();
            }
            try
            {
                ProjectDetailsGetter.UpdateProject(project);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectDetailsGetter.IsProjectExist(ProjectId))
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
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [ResponseType(typeof(Project))]
        public IHttpActionResult Delete(int ProjectId)
        {
            Project Project = ProjectDetailsGetter.GetProject(ProjectId);
            if (Project == null)
            {
                return NotFound();
            }
            ProjectDetailsGetter.RemoveProject(ProjectId);
            return Ok(Project);
        }
    }
}
