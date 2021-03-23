using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdwardGarcia.Controllers
{
    using EdwardGarciaLibrary;
    using HtmlAgilityPack;
    public class DataGetController : Controller
    {
        IDataProvider cmd;
        public DataGetController()
        {
            cmd = new DataProvider();
        }
        public JsonResult GetMailRecords(int CurrentStatusID)
        {
            //= cmd.GetUserType();
            var data = cmd.GetMailList(CurrentStatusID);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        [HttpGet]
        public ActionResult ScrapData(string paramCity)
        {

            List<string> mySearchCategory = new List<string>();

            mySearchCategory.Add("Culture");
            mySearchCategory.Add("Travel");
            mySearchCategory.Add("Tourist Destinations");

            Random x = new Random();

            
            string link = $"https://www.bing.com/images/search?q={paramCity} {mySearchCategory[x.Next(0, 2)]} Philippines";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(link);
            List<string> listResults = new List<string>();
            int i = 0;
            List<string> myContentImages = new List<string>();
            foreach (HtmlNode node in doc.DocumentNode.Descendants("div").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("img_cont hoff")))
            {
                if (i == 24)
                {
                    break;
                }
                else
                {
                    if (node.InnerHtml.Contains("data:"))
                    {
                        continue;
                    }
                    else
                    {
                        var parseSrc = node.InnerHtml.Replace("data-src", "src").Replace(" vimgld", "");
                        parseSrc = ($" <div class='grid-item'> {parseSrc} </div>");
                        myContentImages.Add(parseSrc);
                    }
                }

                i++;
            }

            string passData = string.Empty;

            foreach (var m in myContentImages)
            {
                passData += m;
            }

            passData = "<div class='grid' id='content'>" + passData + "</div>";
           
            JsonResult json = Json(passData.ToString(), JsonRequestBehavior.AllowGet) ;


            return json;
        }

        public ActionResult ScrapDataFinal(string paramCity)
        {
            string passData = string.Empty;
            string link = $"https://www.bing.com/images/search?q={paramCity} tourism -facebook -youtube";

            if (paramCity == "" || paramCity == null || paramCity == string.Empty || paramCity==" ")
            {
                passData = $"<div class='grid' id='content'><p>Please provide a valid data.</p></div>";
            }
            else
            {
                HtmlWeb web = new HtmlWeb();
                List<string> mySearchCategory = new List<string>();
                List<string> myContentImages = new List<string>();
                mySearchCategory.Add("Culture");
                mySearchCategory.Add("Travel");
                mySearchCategory.Add("Tourist Destinations");

                Random random = new Random();
            //    string link = $"https://www.bing.com/images/search?q={paramCity} Philippines Tourist -traveloka -facebook -explore.traveloka -detourista -youtube -slideshare";

                HtmlDocument doc = web.Load(link);

                try
                {
                    var mainNode = doc.DocumentNode.SelectNodes("//div[@class='imgpt']").ToList();
                    int i = 1;
                    foreach (var item in mainNode)
                    {
                        //    string x = item.SelectSingleNode("/a").Attributes["href"].Value;
                        var linker = item.Descendants("a").First(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "iusc");
                        string hrefValue = linker.Attributes["m"].Value;

                        int start = hrefValue.IndexOf("murl");
                        int end = hrefValue.IndexOf("turl");
                        string stringBetweenTwoStrings = hrefValue.Substring(start, end - start + 7).Replace("murl", "").Replace("turl", "").Replace("quot", "").Replace("&;:&;", "").Replace("&;,&;&qu", "");


                        int purlStart = hrefValue.IndexOf("purl&quot;:&quot;");
                        int purlEnd = hrefValue.IndexOf("&quot;,&quot;murl&qu");
                        string stringBetweenTwoStringsOfPurl = hrefValue.Substring(purlStart, purlEnd - purlStart + 7).Replace("&quot;,", "").Replace("purl&quot;:&quot;", "");


                        stringBetweenTwoStrings = ($" <div class='grid-item'><img src='{stringBetweenTwoStrings}' class='mimg' id='imageID' alt='Sorry! >.<' longdesc='{stringBetweenTwoStringsOfPurl}' onerror='img404(this);'></div>");
                        myContentImages.Add(stringBetweenTwoStrings);

                        i++;
                    }


                    foreach (var m in myContentImages)
                    {
                        passData += m;
                    }
                }
                catch (Exception ex)
                {
                    passData = $"<div class='grid' id='content'><p>Oops... No data found in {paramCity}. You can also check other areas!</p></div>";
                }
            }

            passData = $"<div class='grid' id='content'>" + passData + "</div>";
            JsonResult json = Json(passData.ToString(), JsonRequestBehavior.AllowGet);

            return json;

        }
    }
}