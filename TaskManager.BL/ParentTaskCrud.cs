using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL;

namespace TaskManager.BL
{
    public class ParentTaskCrud
    {
        /// <summary>
        /// Getting All the Parent Information from the database
        /// </summary>
        /// <returns>List of Parent</returns>
        public IEnumerable<ParentTask> GetAllParentTask()
        {
            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.ParentTasks.ToList();
            }
        }
        /// <summary>
        /// Getting the specific Task from the database
        /// </summary>
        /// <param name="ParentTaskId">ParentTaskId</param>
        /// <returns>Parent</returns>
        public ParentTask GetParentTask(int ParentTaskId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                ParentTask i = PE.ParentTasks.Where(x => x.ParentId == ParentTaskId).FirstOrDefault();
                return i;
            }
        }
        /// <summary>
        /// Add a particular Parent to database
        /// </summary>
        /// <param name="i">Parent</param>
        /// <returns>Status of the Operation</returns>
        public ParentTask AddParentTask(ParentTask i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.ParentTasks.Add(i);
                    PE.SaveChanges();
                    return i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update a particular ParentTask to database.
        /// </summary>
        /// <param name="i">ParentTask</param>
        /// <returns>Status of The ParentTask</returns>
        public ParentTask UpdateParentTask(ParentTask i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    ParentTask value = PE.ParentTasks.Where(x => x.ParentId == i.ParentId).FirstOrDefault();
                    value.ParentTask1 = i.ParentTask1;
                    PE.SaveChanges();
                    return i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Remove Parent from the database
        /// </summary>
        /// <param name="ParentTaskId">ParentTaskId</param>
        /// <returns>Status of the Operation</returns>
        public ParentTask RemoveParentTask(int ParentTaskId)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    ParentTask I = PE.ParentTasks.Where(x => x.ParentId == ParentTaskId).FirstOrDefault();

                    PE.Entry(I).State = System.Data.Entity.EntityState.Deleted;
                    PE.SaveChanges();
                    return I;
                }


            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Check if ParentTask exist or not
        /// </summary>
        /// <param name="ParentTaskId"></param>
        /// <returns></returns>
        public bool IsParentTaskExist(int ParentTaskId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                bool Result = PE.ParentTasks.Count(x => x.ParentId == ParentTaskId) > 0;
                return Result;
            }
        }
    }
}
