using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TaskManager.BL;
using TaskManager.DAL;
namespace TaskManager.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    
    public class TaskController : ApiController
    {
        public TaskCrud TaskDetailsGetter { get; set; }
        public TaskController()
        {
            TaskDetailsGetter = new TaskCrud();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaskInformation> Get()
        {
            return TaskDetailsGetter.GetAllTasks();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        [HttpGet]
         [ResponseType(typeof(TaskInformation))]
        public IHttpActionResult Get(int TaskId)
        {

            TaskInformation i = TaskDetailsGetter.GetTask(TaskId);
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
        public IHttpActionResult Post(int TaskId,TaskInformation t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TaskDetailsGetter.AddTask(t);
            return CreatedAtRoute("DefaultApi", new { id = t.TaskId }, t);
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int TaskId, TaskInformation t)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (TaskId != t.TaskId)
            {
                return BadRequest();
            }
            try
            {
                TaskDetailsGetter.UpdateTask(t);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailsGetter.IsTaskExist(TaskId))
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
        /// <param name="TaskId"></param>
        /// <returns></returns>
        [ResponseType(typeof(TaskInformation))]
        public IHttpActionResult Delete(int TaskId)
        {
            TaskInformation task = TaskDetailsGetter.GetTask(TaskId);
            if (task == null)
            {
                return NotFound();
            }
            TaskDetailsGetter.RemoveTask(TaskId);
            return Ok(task); 
        }
    }
}
