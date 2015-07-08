using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICCardSystem.Models;
using ICCardSystem.Models.VModels;
using ICCardSystem.Utility;

namespace ICCardSystem.Controllers
{
    public class DataReportController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /DataReport/     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BusinessReport()
        {
            return View();
        }

        //
        // GET: /DataReport/Details/5

        public ActionResult FilingReport()
        {
            return View();
        }

        public ActionResult IndustryReport()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DataReport/Create

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult BusinessStatistics(StatisticsView statis)
        {
            if (statis.FOperator == "business")
            {
                var yyslList = baseDAL.Get<yysj>().Where(
                    c => c.skrq >= statis.FSDate
                    && c.skrq <= statis.FEDate
                    ).ToList();
                return Json(yyslList);
            }
            return null;
        }

        //
        // POST: /DataReport/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DataReport/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DataReport/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DataReport/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DataReport/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
