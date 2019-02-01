
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL;

namespace TaskManager.BL
{
    public class ProjectCrud
    {
        /// <summary>
        /// Getting All the Project Information from the database
        /// </summary>
        /// <returns>List of Project</returns>
        public IEnumerable<Project> GetAllProject()
        {
            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.Projects.ToList();
            }
        }
        /// <summary>
        /// Getting the specific Task from the database
        /// </summary>
        /// <param name="ProjectId">ProjectId</param>
        /// <returns>Project</returns>
        public Project GetProject(int ProjectId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                Project i = PE.Projects.Where(x => x.ProjectId == ProjectId).FirstOrDefault();
                return i;
            }
        }
        /// <summary>
        /// Add a particular Project to database
        /// </summary>
        /// <param name="i">Project</param>
        /// <returns>Status of the Operation</returns>
        public Project AddProject(Project i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.Projects.Add(i);
                    PE.SaveChanges();

                    return i; 
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update a particular Project to database.
        /// </summary>
        /// <param name="i">Project</param>
        /// <returns>Status of The Project</returns>
        public Project UpdateProject(Project i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    Project value = PE.Projects.Where(x => x.ProjectId == i.ProjectId).FirstOrDefault();
                    value.Priority = i.Priority;
                    value.StartDate = i.StartDate;
                    value.EndDate = i.EndDate;
                    value.ProjectDescription   = i.ProjectDescription;
                    value.ManagerUserId = i.ManagerUserId; 
                    PE.SaveChanges();
                    return value;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Remove Project from the database
        /// </summary>
        /// <param name="ProjectId">ProjectId</param>
        /// <returns>Status of the Operation</returns>
      
        public Project RemoveProject(int ProjectId)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    Project I = PE.Projects.Where(x => x.ProjectId == ProjectId).FirstOrDefault();
                     
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
        /// Check if Project exist or not
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public bool IsProjectExist(int ProjectId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                bool Result = PE.Projects.Count(x => x.ProjectId == ProjectId) > 0;
                return Result;
            }
        }
    }
}
