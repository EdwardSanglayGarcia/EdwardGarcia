using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdwardGarcia.Controllers
{
    using EdwardGarciaLibrary;
    public class HomeController : Controller
    {
        IDataProvider cmd;
        public HomeController()
        {
            cmd = new DataProvider();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "assets/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Garcia Resume.pdf");
            string fileName = "Garcia Resume.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public ActionResult SendMail(MailList mailList)
        {
            TempData["messageSent"] = "<script>Swal.fire( 'Message Sent!', 'I will provide a feedback ASAP! :)', 'success' )</script>";
            cmd.InsertMailList(mailList);
            return View("Index");
        }

        public ActionResult EdwardMailList()
        {
            cmd.GetMailList(1);
            return View();
        }

        public ActionResult LoadMailList(int CurrentStatusID)
        {
            cmd.GetMailList(CurrentStatusID);
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(MasterAdministrator masterAdministrator)
        {

            var userDetail = cmd.GetAccount().Where(x => x.Username == masterAdministrator.Username && x.Password == masterAdministrator.Password).FirstOrDefault();

            if (userDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["Username"] = masterAdministrator.Username;
                Session["Password"] = masterAdministrator.Password;
                return RedirectToAction("EdwardMailList", "Home");

            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}