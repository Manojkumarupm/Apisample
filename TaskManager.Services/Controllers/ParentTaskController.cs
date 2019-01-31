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
    public class ParentTaskController : ApiController
    {
        public ParentTaskCrud ParentTaskDetailsGetter { get; set; }
        public ParentTaskController()
        {
            ParentTaskDetailsGetter = new ParentTaskCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParentTask> Get()
        {
            return ParentTaskDetailsGetter.GetAllParentTask();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentTaskId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ParentTask))]
        public IHttpActionResult Get(int ParentTaskId)
        {

            ParentTask i = ParentTaskDetailsGetter.GetParentTask(ParentTaskId);
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
        public IHttpActionResult Post(int ParentTaskId, ParentTask t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ParentTaskDetailsGetter.AddParentTask(t);
            return CreatedAtRoute("DefaultApi", new { id = t.ParentId }, t);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int ParentTaskId, ParentTask ParentTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ParentTaskId != ParentTask.ParentId)
            {
                return BadRequest();
            }
            try
            {
                ParentTaskDetailsGetter.UpdateParentTask(ParentTask);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentTaskDetailsGetter.IsParentTaskExist(ParentTaskId))
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
        /// <param name="ParentTaskId"></param>
        /// <returns></returns>
        [ResponseType(typeof(ParentTask))]
        public IHttpActionResult Delete(int ParentTaskId)
        {
            ParentTask ParentTask = ParentTaskDetailsGetter.GetParentTask(ParentTaskId);
            if (ParentTask == null)
            {
                return NotFound();
            }
            ParentTaskDetailsGetter.RemoveParentTask(ParentTaskId);
            return Ok(ParentTask);
        }
    }
}
