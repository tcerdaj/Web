using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JehovaJireh.Web.UI.Controllers
{
    public class BibleController : Controller
    {
        // GET: Bible
        public ActionResult Index(string version = null, string book = null, string chapterid = null)
        {
            return View();
        }

        public ActionResult Seach(string query, string dam_id)
        {
            return View();
        }
    }
}