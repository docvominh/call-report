using ReportCall.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace ReportCall.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Report()
        {
            ViewBag.Message = "Call Report Server";

            ReportViewModel model = InitReportData();

            return View(model);
        }

        [HttpPost]
        public ActionResult TestReport(string sometext)
        {
            return Json(new { message = sometext }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownLoadReport(string sometext)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:8080/birt/frameset?__report=report\test.rptdesign&__format=pdf&user=" + sometext);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentLength = 0;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms, "application/pdf", sometext + ".pdf");
                }
            }
        }

        [HttpPost]
        public JsonResult GetReport(ReportViewModel model)
        {
            System.Diagnostics.Debug.WriteLine(RequestGET("http://dantri.com.vn:80"));
            return Json(new { message = "fucking success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private ReportViewModel InitReportData()
        {
            ReportViewModel model = new ReportViewModel();

            ICollection<SelectListItem> listAgentId = new List<SelectListItem>();
            ICollection<SelectListItem> listSomeId = new List<SelectListItem>();
            ICollection<SelectListItem> listCurrencyId = new List<SelectListItem>();
            ICollection<SelectListItem> listPropertyId = new List<SelectListItem>();
            ICollection<SelectListItem> listAvailabilityId = new List<SelectListItem>();
            ICollection<SelectListItem> listLocale = new List<SelectListItem>();

            listAgentId.Add(new SelectListItem { Text = "Agent47", Value = "e2f1805f-dfd2-e511-80f2-3863bb3cd5a0", Selected = true });
            listSomeId.Add(new SelectListItem { Text = "Som", Value = "acd1daf5-9249-e011-8563-00155d0b5502", Selected = true });
            listCurrencyId.Add(new SelectListItem { Text = "X", Value = "a1933d3a-5ccb-de11-88b8-0050569266c3", Selected = true });

            // Property Id
            listPropertyId.Add(new SelectListItem { Text = "1 Changi", Value = "81a346a9-95d6-e511-80f5-3863bb2e4458", Selected = true });
            //listPropertyId.Add(new SelectListItem { Text = "158 Cecil Street", Value = "054d9c9e-fed0-e511-80f2-3863bb3cd5a0" });

            // Property Id
            listAvailabilityId.Add(new SelectListItem { Text = "1", Value = "f3e6a031-5464-e611-80eb-c4346bdcd151", Selected = true });
            listAvailabilityId.Add(new SelectListItem { Text = "2", Value = "c9aac80b-dcd8-e511-80f4-3863bb3cd5a0" });
            listAvailabilityId.Add(new SelectListItem { Text = "3", Value = "c155ea1a-5b29-e611-80e0-5065f38a5a41" });
            listAvailabilityId.Add(new SelectListItem { Text = "4", Value = "beaac80b-dcd8-e511-80f4-3863bb3cd5a0" });

            listLocale.Add(new SelectListItem { Text = "US", Value = "en-us", Selected = true });
            listLocale.Add(new SelectListItem { Text = "JP", Value = "ja-jp", Selected = true });

            model.AgentId = listAgentId;
            model.SomeId = listSomeId;
            model.CurrencyId = listCurrencyId;
            model.PropertyId = listPropertyId;
            model.AvailabilityId = listAvailabilityId;
            model.Locale = listLocale;

            return model;
        }

        private string RequestGET(string url)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentLength = 0;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        private string RequestGetStream(string url)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentLength = 0;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        private Stream RequestPOST(string url, string reportParameter)
        {
            string result = string.Empty;

            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create("http://dantri.com.vn ");

            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = reportParameter;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();

            // Display the status.
            System.Diagnostics.Debug.WriteLine("Display the status." + ((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Display the content.
            System.Diagnostics.Debug.WriteLine(responseFromServer);

            // Clean up the streams.
            reader.Close();
            //dataStream.Close();
            response.Close();

            return dataStream;
        }
    }
}