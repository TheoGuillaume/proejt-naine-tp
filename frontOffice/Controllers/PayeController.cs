using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontOffice.Models;

namespace frontOffice.Controllers;

public class PayeController : Controller
{
      public IActionResult Index()
      {
            return View("~/Views/Paye/Index.cshtml");
      }
}
