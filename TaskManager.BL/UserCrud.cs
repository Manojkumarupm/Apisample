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
        /// <returns>List of User</returns>
        public IEnumerable<User> GetAllUser()
        {
            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                return PE.Users.ToList();
            }
        }
        /// <summary>
        /// Getting the specific User from the database
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>User</returns>
        public User GetUser(int UserId)
        {

            using (CapsuleEntities PE = new CapsuleEntities())
            {
                PE.Configuration.ProxyCreationEnabled = false;
                User i = PE.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                return i;
            }
        }
        /// <summary>
        /// Add a particular User to database
        /// </summary>
        /// <param name="i">User</param>
        /// <returns>Status of the Operation</returns>
        public User AddUser(User i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    PE.Users.Add(i);
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
        /// Update a particular User to database.
        /// </summary>
        /// <param name="i">User</param>
        /// <returns>Status of The User</returns>
        public User UpdateUser(User i)
        {
            try
            {
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    User value = PE.Users.Where(x => x.UserId == i.UserId).FirstOrDefault();
                    value.ProjectId = i.ProjectId;
                    value.LastName = i.LastName;
                    value.FirstName = i.FirstName;
                    value.EmployeeId = i.EmployeeId;
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
        /// Remove User from the database
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>Status of the Operation</returns>
        public User RemoveUser(int UserId)
        {
            try
            {
                User I = null;
                using (CapsuleEntities PE = new CapsuleEntities())
                {
                    PE.Configuration.ProxyCreationEnabled = false;
                    I = PE.Users.Where(x => x.UserId == UserId).FirstOrDefault();

                    PE.Entry(I).State = System.Data.Entity.EntityState.Deleted;
                    PE.SaveChanges();

                }
                return I;
            }

            catch (Exception)
            {
                throw;
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
                bool Result = PE.Users.Count(x => x.UserId == UserId) > 0;
                return Result;
            }
        }
    }
}
