using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(HangFire.Startup))]
namespace Hangfire.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}