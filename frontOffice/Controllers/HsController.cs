using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontOffice.Models;

namespace frontOffice.Controllers;

public class HsController : Controller
{
      public IActionResult Index()
      {
            return View("~/Views/Hs/Index.cshtml");
      }
}
