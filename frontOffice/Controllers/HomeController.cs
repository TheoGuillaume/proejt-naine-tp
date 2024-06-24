using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using frontOffice.Models;
using Microsoft.Extensions.Logging;
using FrontOffice;

namespace frontOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyEmployeeCode(string codeEmployee)
        {
            using (var connection = new Connection().GetSqlconnect())
            {
                string query = "SELECT * from Employees e WHERE e.codeEmployee = @codeEmployee";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@codeEmployee", codeEmployee);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    // Store employee information in cookies
                    var options = new CookieOptions
                    {
                        HttpOnly = true,
                        IsEssential = true, // Ensure cookie is essential
                        Secure = true // Ensure cookie is sent only over HTTPS
                    };

                    Response.Cookies.Append("EmployeeId", reader.GetGuid(reader.GetOrdinal("Id")).ToString(), options);
                    Response.Cookies.Append("EmployeeEmail", reader.GetString(reader.GetOrdinal("Email")), options);

                    return RedirectToAction("Index", "Paye");
                }
                else
                {
                    ViewBag.ErrorMessage = "Code employé n'existe pas.";
                    return View("Index");
                }
            }
        }

        public IActionResult PayPage()
        {
            // Logique de la page PayPage ici
            return View();
        }

        public IActionResult Logout()
        {
            // Supprimer les cookies
            Response.Cookies.Delete("EmployeeId");
            Response.Cookies.Delete("EmployeeEmail");

            // Rediriger vers une page de déconnexion ou une autre page appropriée
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
