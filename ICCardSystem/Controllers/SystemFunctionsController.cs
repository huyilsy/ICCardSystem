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
    public class SystemFunctionsController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /SystemFunctions/

        public ActionResult Password()
        {
            return View();
        }

        public ActionResult BinManagement()
        {
            return View();
        }

    }
}
