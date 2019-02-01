using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.Services.Controllers;
using System.Net.Http;
using System.Web.Http;
using TaskManager.DAL;
using System.Collections.Generic;
using System.Net;
using TaskManager.BL;

namespace TaskManager.UnitTestProject
{
    [TestClass]
    public class TaskInformationControllerTest
    {

        [TestMethod]
        public void GetUserList()
        {    //Arrange
            UserCrud userBusiness = new UserCrud();
            //Act         
            User user = userBusiness.GetUser(1);
            //Assert
            if (user != null)
                Assert.AreEqual(1, user.UserId);
            else
                Assert.IsNotNull(user);
        }
        [TestMethod]
        public void PostUserTest()
        {    //Arrange
            UserCrud user = new UserCrud();
            User usObj = new User();
            usObj.UserId = 0;
            usObj.FirstName = "FirstName";
            usObj.LastName = "LastName";
            usObj.EmployeeId = 1234;

            //Act
            usObj = user.AddUser(usObj);

            //Assert
            Assert.AreNotEqual(0, usObj.UserId);
        }
        [TestMethod]
        public void GetParentTaskTest()
        {    //Arrange
            ParentTaskCrud parentTask = new ParentTaskCrud();
            //Act         
            ParentTask parent = parentTask.GetParentTask(1);
            //Assert
            if (parentTask != null)
                Assert.AreEqual(1, parent.ParentId);
            else
                Assert.IsNotNull(parentTask);
        }

        [TestMethod]
        public void PostParentTaskTest()
        {    //Arrange
            ParentTaskCrud parentTask = new ParentTaskCrud();
            ParentTask parent = new ParentTask();
            parent.ParentTask1 = "Parent task 1";
            //Act
            parentTask.AddParentTask(parent);

            //Assert
            Assert.AreNotEqual(0, parent.ParentId);
        }

        [TestMethod]
        public void GetProjectTest()
        {    //Arrange
            ProjectCrud projectcrud = new ProjectCrud();
            //Act                  
            Project project = projectcrud.GetProject(1);
            //Assert
            if (project != null)
                Assert.AreEqual(1, project.ProjectId);
            else
                Assert.IsNotNull(project);
        }

        [TestMethod]
        public void GetTaskTest()
        {    //Arrange
            TaskCrud taskcrud = new TaskCrud();
            //Act                  
            TaskInformation task = taskcrud.GetTask(1);
            //Assert
            if (task != null)
                Assert.AreEqual(1, task.TaskId);
            else
                Assert.IsNotNull(task);
        }
    }
}
