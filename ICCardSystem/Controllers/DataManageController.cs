using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICCardSystem.Models;
using ICCardSystem.Models.VModels;
using ICCardSystem.Utility;
using System.Data.SqlClient;


namespace ICCardSystem.Controllers
{
    public class DataManageController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());

        //新建ADO.NET数据实体模型的实体化对象 DALBase,实现数据的查询、添加、修改等数据操作，using ICCardSystem.Utility
        // GET: /DataManage/


        public ActionResult NewAccount()
        {
            var djglList = baseDAL.Get<djgl>().ToList();
            var jdlxList = baseDAL.Get<jdlx>().ToList();
            var zxglList = baseDAL.Get<zxgl>().ToList();
            var yhxxList = baseDAL.Get<yhxx>().ToList();
            ViewBag.djglList = djglList;
            ViewBag.jdlxList = jdlxList;
            ViewBag.zxglList = zxglList;
            ViewBag.yhxxList = yhxxList;
            return View();
        }

        [HttpPost]
        public ActionResult NewAccount(yhxx yhxx)
        {
            baseDAL.AddItem<yhxx>(yhxx);
            baseDAL.SaveAllChanges();
            var yhxxList = baseDAL.Get<yhxx>().ToList();
            return Json(yhxxList);
        }

        public ActionResult PresaleCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YHBHQuery(SearchConditionView searchCondition)
        {
            
            int number = int.Parse(searchCondition.Condition1); 
            List<yhxx> yhxxlist = null;
            yhxxlist = baseDAL.Get<yhxx>().Where(c => c.jdlx == number).ToList();

            return Json(yhxxlist);
        }

        [HttpPost]
        public ActionResult KHQuery(SearchConditionView searchCondition)
        {
            int number = int.Parse(searchCondition.Condition1);
            List<yhxx> yhxxlist = null;
            yhxxlist = baseDAL.Get<yhxx>().Where(c => c.kpbh == number).ToList();

            return Json(yhxxlist);
        }

        public ActionResult Modify()
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
        public ActionResult UpdateNewUser()
        {

            return View();

        }

        public ActionResult Lock()
        {
            return View();
        }

        public ActionResult Check()
        {
            return View();
        }

        public ActionResult CancelAccount()
        {
            return View();
        }

        public ActionResult Transfer()
        {
            return View();
        }                    
    }
}
