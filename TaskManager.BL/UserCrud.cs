using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL;

namespace TaskManager.BL
{
   public class UserCrud
    {
        /// <summary>
        /// Getting All the User Information from the database
        /// </summary>
        /// <returns>List of Users</returns>
        public IEnumerable<Users> GetAllUser()
        {
            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.Userss.ToList();
            }
        }
        /// <summary>
        /// Getting the specific User from the database
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>User</returns>
        public Users GetUser(int UserId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                Users i = PE.Userss.Where(x => x.TaskId == UserId).FirstOrDefault();
                return i;
            }
        }
        /// <summary>
        /// Add a particular User to database
        /// </summary>
        /// <param name="i">User</param>
        /// <returns>Status of the Operation</returns>
        public string AddUser(Users i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.Userss.Add(i);
                    PE.SaveChanges();

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Failed" + ex.Message;
            }
        }
        /// <summary>
        /// Update a particular User to database.
        /// </summary>
        /// <param name="i">User</param>
        /// <returns>Status of The User</returns>
        public string UpdateUser(Users i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    Users value = PE.Userss.Where(x => x.UserId == i.UserId).FirstOrDefault();
                    value.ProjectId = i.ProjectId;
                    value.LastName = i.LastName;
                    value.FirstName = i.FirstName;
                    value.EmployeeId = i.EmployeeId; 
                    PE.SaveChanges();
                    return "Updated";
                }
            }
            catch (Exception ex)
            {
                return "failed : " + ex.Message;
            }
        }
        /// <summary>
        /// Remove User from the database
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>Status of the Operation</returns>
        public string RemoveUser(int UserId)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    Users I = PE.Userss.Where(x => x.UserId   == UserId).FirstOrDefault();
                    if (I == null)
                    {
                        return "User with Id " + UserId + " Not found";
                    }
                    else
                    {
                        PE.Entry(I).State = System.Data.Entity.EntityState.Deleted;
                        PE.SaveChanges();
                        return "Success";
                    }
                }

            }
            catch (Exception ex)
            {
                return "Failed : " + ex.Message;
            }

        }
        /// <summary>
        /// Check if User exist or not
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool IsUserExist(int UserId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                bool Result = PE.Userss.Count(x => x.UserId == UserId) > 0;
                return Result;
            }
        }
    }
}
