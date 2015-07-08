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
    public class StatisticsController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /Statistics/
             
        public ActionResult UserQuery()
        {
            var djglList = baseDAL.Get<djgl>().ToList();
            var jdlxList = baseDAL.Get<jdlx>().ToList();
            var zxglList = baseDAL.Get<zxgl>().ToList();
            ViewBag.djglList = djglList;
            ViewBag.jdlxList = jdlxList;
            ViewBag.zxglList = zxglList;
            return View();
        }

        [HttpPost]
        public ActionResult KHQuery(SearchView kpbh)
        {
            List<yhxx> yhxxlist = null;
            int number = int.Parse(kpbh.Condition);
            yhxxlist = baseDAL.Get<yhxx>().Where(c => c.kpbh == number).ToList();

            //ViewBag.yhxxlist = yhxxlist;

            return Json(yhxxlist);
        }

        [HttpPost]
        public ActionResult HMQuery(SearchView hm)
        {
            List<yhxx> yhxxlist = null;
            String str = hm.Condition;
            yhxxlist = baseDAL.Get<yhxx>().Where(c => c.hm == str).ToList();

            //ViewBag.yhxxlist = yhxxlist;

            return Json(yhxxlist);
        }


        [HttpPost]
        public ActionResult ZZQuery(SearchView zz)
        {
            List<yhxx> yhxxlist = null;
            String str = zz.Condition;
            yhxxlist = baseDAL.Get<yhxx>().Where(c => c.zz == str).ToList();

            //ViewBag.yhxxlist = yhxxlist;

            return Json(yhxxlist);
        }

        [HttpPost]
        public ActionResult TELQuery(SearchView tel)
        {
            List<yhxx> yhxxlist = null;
            String str = tel.Condition;
            yhxxlist = baseDAL.Get<yhxx>().Where(c => c.tel == str).ToList();

            //ViewBag.yhxxlist = yhxxlist;

            return Json(yhxxlist);
        }

        [HttpPost]

        public ActionResult UpdateNewUser(SearchConditionView a)
        {
           // yhxx newYhxx= null;
           // newYhxx.hm = a.Condition1;
           // newYhxx.tel=a.Condition2;
          //  newYhxx.kpbh=int.Parse(a.Condition3);
            int kpbh = int.Parse(a.Condition3);
            int ywbhNum;
            int total = baseDAL.Get<bhIndex>().ToList().Count;
            if (total == 0)
            {
                ywbhNum = 1;
            }
            else
            {

                ywbhNum = total + 1;
            }
            var bhIndex = new bhIndex
            {
                ywbh = ywbhNum,
            };
            baseDAL.AddItem<bhIndex>(bhIndex);
            if (a.Condition1 != null)
            {
                var newUser = baseDAL.Get<yhxx>().FirstOrDefault(c => c.kpbh == kpbh);
              //  newUser.hm = newYhxx.hm;
              //  newUser.tel = newYhxx.tel;
             //  newUser.ywbh = ywbhNum;
                newUser.hm = a.Condition1;
                newUser.tel = a.Condition2;
                 newUser.ywbh = ywbhNum;                 
                newUser.gqcs = 1;
                Session["ywbh"] = newUser.ywbh;
                Session["hm"] = newUser.hm;
                Session["zz"] = newUser.zz;
                Session["yhlx"] = newUser.yhlx;
                Session["tel"] = newUser.tel;
                Session["gqcs"] = newUser.gqcs;
                baseDAL.SaveAllChanges();

            }
            else
            {
                Session["ywbh"] = ywbhNum;
                Session["hm"] = null;
                Session["zz"] = null;
                Session["yhlx"] = 0;
                Session["tel"] = null;
                Session["gqcs"] = 0;

            }

            return Json(ywbhNum);

        }



        [HttpPost]
        public void deleteEdit(SearchConditionView s) {
          int   kpbh = int.Parse(s.Condition1);
          var newUser = baseDAL.Get<yhxx>().FirstOrDefault(c => c.kpbh == kpbh);
          baseDAL.DeleteItem(newUser);
          baseDAL.SaveAllChanges();
        
        }
        //
        // GET: /Statistics/Details/5

        public ActionResult OperatingData()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Statistics/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Statistics/Create

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
        // GET: /Statistics/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Statistics/Edit/5

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
        // GET: /Statistics/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Statistics/Delete/5

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
