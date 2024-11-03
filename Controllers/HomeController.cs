using HR.Language;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStringLocalizer<SharedResource> _localizer;

        public HomeController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewBag.Msg = _localizer["DASHBOARD"];
            return View();
        }

    }
}
