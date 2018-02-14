using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JehovaJireh.Web.Services.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BibleController : ApiController
    {
        private static string apiKey = "";
        private static string endPoint = "";
        public BibleController()
        {
            apiKey = ConfigurationManager.AppSettings["bibleapikey"];
            endPoint = ConfigurationManager.AppSettings["bibleendpoint"];
        }

        /// <summary>
        /// User needs to select a language. Retrieve a list of language families for which audio volumes are available.
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<HttpResponseMessage> Languages()
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "library";
                var action = "volumelanguagefamily";
                var parameters = string.Format("&media=audio");
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Versions()
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "library";
                var action = "version";
                var parameters = string.Format("&media=audio");
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// User needs to select a Bible. Retrieve a list of volumes available in that language. Group these volumes into Bibles by the first 6 characters of the DAM ID.
        /// </summary>
        /// <param name="language_family_code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Volumes(string language_family_code)
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "library";
                var action = "volume";
                var parameters = string.Format("&media=audio&language_code={0}", language_family_code);
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }


      
        /// <summary>
        /// User needs to select a book of the Bible and a chapter. Retrieve a list of books for this Bible.
        /// </summary>
        /// <param name="dam_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Books(string dam_id)
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "library";
                var action = "book";
                var parameters = string.Format("&dam_id={0}", dam_id);
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Retrieve the text for that book/chapter.
        /// </summary>
        /// <param name="dam_id"></param>
        /// <param name="book_id"></param>
        /// <param name="chapter_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Chapter(string dam_id, string book_id, string chapter_id)
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "text";
                var action = "verse";
                var parameters = string.Format("&dam_id={0}&book_id={1}&chapter_id={2}", dam_id, book_id, chapter_id);
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Retrieve the Content Delivery Network base location.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Servers()
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "audio";
                var action = "location";
                var parameters = "";
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Retrieve the path to the audio file for this book and chapter.
        /// </summary>
        /// <param name="dam_id"></param>
        /// <param name="book_id"></param>
        /// <param name="chapter_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Path(string dam_id, string book_id, string chapter_id)
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "audio";
                var action = "path";
                var parameters = string.Format("&dam_id={0}&book_id={1}&chapter_id={2}", dam_id, book_id, chapter_id);
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        /// <summary>
        /// Retrieve and display copyright information for the volume.
        /// </summary>
        /// <param name="dam_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Copyright(string dam_id)
        {
            HttpResponseMessage response = null;
            try
            {
                var controller = "library";
                var action = "metadata";
                var parameters = string.Format("&dam_id={0}", dam_id);
                response = await GetAsync(controller, action, parameters);
            }
            catch (System.Exception)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        private async Task<HttpResponseMessage> GetAsync(string controller, string action, string parameters)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                HttpResponseMessage responseMessage = null;
                client.DefaultRequestHeaders.Add("Authorization", "Token " + apiKey);
                responseMessage = await client.GetAsync(string.Format("{0}{1}/{2}?key={3}{4}&v=2", endPoint, controller, action, apiKey, parameters));
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return responseMessage;
            }
        }
    }
}
