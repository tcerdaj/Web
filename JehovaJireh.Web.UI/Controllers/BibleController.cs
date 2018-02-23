using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JehovaJireh.Web.UI.Controllers
{
    public class BibleController : Controller
    {
        // GET: Bible
        public ActionResult Index(string version = null, string book = null, string chapter = null)
        {
            ViewBag.Version = version;
            ViewBag.Book = book;
            ViewBag.Chapter = chapter;
            return View();
        }

        public ActionResult Seach(string query, string dam_id)
        {
            return View();
        }

        [HttpGet]
        [ActionName("Version")]
        public async Task<ActionResult> Version(string language)
        {
            string html = string.Empty;
            var response = new HttpResponseMessage();
            var endPoint = string.Format("{0}bible/Versionshtml?language={1}", GetBaseUrl(), language);

            using (var client = new System.Net.Http.HttpClient())
            {
                response = await client.GetAsync(endPoint);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                html = await response.Content.ReadAsStringAsync();
                return Content(html);
                
            }
        }
        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;
            var authority = "jehovajireh.web.service";

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            if (request.Url.Authority.Contains("localhost"))
                authority = "localhost:58095";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, authority, appUrl);

            return baseUrl;
        }
    }
}