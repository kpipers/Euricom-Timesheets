using System.Web.Mvc;
using Euricom.Timesheets.Models;

namespace Euricom.Timesheets.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeModel());
        }        
    }
}
