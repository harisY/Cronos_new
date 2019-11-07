using Cronos.Models;
using Cronos.Models.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronos.Services;

namespace Cronos.Controllers
{
    public class DashboardController : Controller
    {
        private DeptService objDept;
        public DashboardController()
        {
           
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboardv1()
        {
            
            return View();
        }

        public ActionResult Dashboardv2()
        {
            return View();
        }

        public ActionResult GetMessages()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            //return PartialView("_MessagesList", _messageRepository.GetAllMessages());
            deptRepos deptrepos = new deptRepos();
            string Dept = deptrepos.getDeptByUser(User.Identity.Name);
            var data = _messageRepository.GetAllMessages(Dept);
            string Jumlah = "0";
            foreach (var item in data)
            {
                Jumlah = item.Jumlah;
            }
            return Json(new {jumlah = Jumlah, success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCompletTask()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            //return PartialView("_MessagesList", _messageRepository.GetAllMessages());
            deptRepos deptrepos = new deptRepos();
            string Dept = deptrepos.getDeptByUser(User.Identity.Name);
            var data = _messageRepository.GetCompletedTask(Dept);
            string Jumlah = "0";
            foreach (var item in data)
            {
                Jumlah = item.Jumlah;
            }
            return Json(new { jumlah = Jumlah, success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCreatedTask()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            //return PartialView("_MessagesList", _messageRepository.GetAllMessages());
            deptRepos deptrepos = new deptRepos();
            string Dept = deptrepos.getDeptByUser(User.Identity.Name);
            var data = _messageRepository.GetCreatedTask(Dept);
            string Jumlah = "0";
            foreach (var item in data)
            {
                Jumlah = item.Jumlah;
            }
            return Json(new { jumlah = Jumlah, success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCreatedTaskCompleted()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            //return PartialView("_MessagesList", _messageRepository.GetAllMessages());
            deptRepos deptrepos = new deptRepos();
            string Dept = deptrepos.getDeptByUser(User.Identity.Name);
            var data = _messageRepository.GetCreatedTaskCompleted(Dept);
            string Jumlah = "0";
            foreach (var item in data)
            {
                Jumlah = item.Jumlah;
            }
            return Json(new { jumlah = Jumlah, success = true, message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}