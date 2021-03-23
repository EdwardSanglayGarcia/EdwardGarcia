using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdwardGarcia.Controllers
{
    using EdwardGarciaLibrary;
    public class TouristaController : Controller
    {

        IDataProvider cmd;

        public TouristaController()
        {
            cmd = new DataProvider();
        }

        // GET: Tourista
        public ActionResult Application()
        {
            return View();
        }



    }
}