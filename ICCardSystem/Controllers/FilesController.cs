using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICCardSystem.Controllers
{
    public class FilesController : Controller
    {
        //
        // GET: /Files/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFiles()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(FormCollection collection)
        {
            foreach (string upload in Request.Files.AllKeys)
            {
                if (Request.Files[upload] == null)
                {
                    continue;
                }
                else
                {
                    //Save file
                }
            }
            return View();
        }

    }
}
