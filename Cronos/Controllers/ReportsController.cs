using Cronos.Models.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cronos.Models;
using Cronos.Entity;
using Cronos.Services;
using Cronos.Repository;
using System.Configuration;

namespace Cronos.Controllers
{
    public class ReportsController : Controller
    {
        private DeptService objDept;

        public ReportsController()
        {
            this.objDept = new DeptService();
        }
        // GET: Reports
         
       
        //==========================Baru=====================================
        public ActionResult LaporanCustomFA(string facility, int period, int yearperiod)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomFA.rpt";
            this.HttpContext.Session["Param"] = period;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportCustomFA(facility, period, yearperiod);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexCustomFA()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["nPeriod"] = report.GetPeriod();
            ViewBag.CompanyName = report.CompanyName();
            GetListReportFA("MONDRIAN",1,2018);
            return View();
        }
        public ActionResult GetListReportFA(string facility, int period, int yearperiod)
        {
            ReportIMrepo report = new ReportIMrepo();
            DataTable dt = new DataTable();
            dt = report.PrintReportCustomFA_table(facility, period, yearperiod);
            return PartialView("PartialGrid/_GridFA", dt);
        }
        public ActionResult LaporanCustomIncoming(string facility, string tgl1, string tgl2, string bctype)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomIncoming.rpt";
            this.HttpContext.Session["Param1"] = tgl1;
            this.HttpContext.Session["Param2"] = tgl2;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportCustomIncoming(facility, tgl1, tgl2, bctype);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexCustomIncoming()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["bctypeIncoming"] = report.GetIncomingBcType();
            ViewBag.CompanyName = report.CompanyName();
            return View();
        }

        public ActionResult LaporanCustomOutgoing(string facility, string tgl1, string tgl2, string bctype)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomOutgoing.rpt";
            this.HttpContext.Session["Param1"] = tgl1;
            this.HttpContext.Session["Param2"] = tgl2;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportCustomOutgoing(facility, tgl1, tgl2, bctype);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexCustomOutgoing()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["bctypeOutgoing"] = report.GetOutgoingBcType();
            ViewBag.CompanyName = report.CompanyName();
            return View();
        }

        public ActionResult LaporanRawMaterial(string facility, int period, int yearperiod)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomRM.rpt";
            this.HttpContext.Session["Param"] = period;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportRawMaterial(facility, period, yearperiod);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexRawMaterial()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["nPeriod"] = report.GetPeriod();
            ViewBag.CompanyName = report.CompanyName();
            return View();
        }

        public ActionResult LaporanFG(string facility, int period, int yearperiod)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomFG.rpt";
            this.HttpContext.Session["Param"] = period;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportFG(facility, period, yearperiod);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexFG()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["nPeriod"] = report.GetPeriod();
            ViewBag.CompanyName = report.CompanyName();
            return View();
        }

        public ActionResult LaporanScrap(string facility, int period, int yearperiod)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomScrap.rpt";
            this.HttpContext.Session["Param"] = period;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportScrap(facility, period, yearperiod);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexScrap()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["nPeriod"] = report.GetPeriod();
            ViewBag.CompanyName = report.CompanyName();
            return View();
        }

        public ActionResult LaporanWIP(string facility, int period, int yearperiod)
        {

            ReportIMrepo report = new ReportIMrepo();
            string Title = ConfigurationManager.AppSettings["ReportTitle"].ToString();
            this.HttpContext.Session["NamaLaporan"] = "CustomWIP.rpt";
            this.HttpContext.Session["Param"] = period;
            this.HttpContext.Session["Title"] = Title;
            this.HttpContext.Session["Datasource"] = report.PrintReportWIP(facility, period, yearperiod);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexWIP()
        {
            ReportIMrepo report = new ReportIMrepo();
            ViewData["nPeriod"] = report.GetPeriod();
            ViewBag.CompanyName = report.CompanyName();
            return View();
        }
    }

}
