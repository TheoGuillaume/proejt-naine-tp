using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontOffice.Models;

namespace frontOffice.Controllers;

public class FicheController : Controller
{
      public IActionResult Index()
      {
            return View("~/Views/Fiche/Index.cshtml");
      }
}
