using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace frontOffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly string connectionString = "Server=localhost;Database=RazorPageCrud;User Id=sa;Password=#sqlServer2023;Encrypt=True;TrustServerCertificate=True";

        [HttpGet("search")]
        public async Task<IActionResult> Search(string annee, string mois)
        {
            var employeeId = Request.Cookies["EmployeeId"];
            if (string.IsNullOrEmpty(employeeId))
            {
                return Unauthorized("EmployeeId cookie is missing");
            }

            var results = new List<dynamic>();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT * FROM view_1 
                WHERE EmployeeId = @EmployeeId 
                AND annee = @Annee 
                AND nameMois = @Mois";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@Annee", annee);
                    command.Parameters.AddWithValue("@Mois", mois);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            results.Add(new
                            {
                                Employee = reader["Nom"],
                                Mois = reader["nameMois"],
                                Semaine = reader["nbSemaine"],
                                HT = reader["HT"],
                                HN = reader["HN"],
                                HS = reader["HS"],
                                HS130 = reader["HS130"],
                                HS150 = reader["HS150"],
                                HN30 = reader["HN30"],
                                Hdim40 = reader["Hdim40"]
                            });
                        }
                    }
                }
            }

            return Ok(results);
        }

        [HttpGet("getMois")]
        public IActionResult GetMois()
        {
            var moisList = new List<dynamic>();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Mois";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    moisList.Add(new
                    {
                        id = reader.GetInt32(reader.GetOrdinal("Id")),
                        mois = reader.GetString(reader.GetOrdinal("mois"))
                    });
                }
            }

            return Ok(moisList);
        }

        [HttpGet("recherche")]
        public async Task<IActionResult> recherche(string annee, string mois)
        {
            var employeeId = Request.Cookies["EmployeeId"];
            if (string.IsNullOrEmpty(employeeId))
            {
                return Unauthorized("EmployeeId cookie is missing");
            }

            var results = new List<dynamic>();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT *
            FROM view_3
            WHERE EmployeeId = @EmployeeId
              AND nameMois = @Mois
              AND annee = @Annee";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@Annee", annee);
                    command.Parameters.AddWithValue("@Mois", mois);

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            results.Add(new
                            {
                                Employee = reader["Nom"],
                                Mois = reader["nameMois"],
                                Annee = reader["annee"],
                                Hs = reader["totalHs"],
                                HN30 = reader["totalHN30"],
                                Hdim40 = reader["totalHdim40"],
                                HSNI130 = reader["HSNI_130"],
                                HSNI150 = reader["HSNI_150"],
                                HSI130 = reader["HSI_130"],
                                HSI150 = reader["HSI_150"]
                            });
                        }
                    }
                }
            }

            return Ok(results);
        }

        [HttpGet("fiche")]
        public async Task<IActionResult> fiche(string annee, string mois)
        {
            var employeeId = Request.Cookies["EmployeeId"];
            if (string.IsNullOrEmpty(employeeId))
            {
                return Unauthorized("Le cookie EmployeeId est manquant.");
            }

            var results = new List<dynamic>();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            view_3.EmployeeId,
            Nom,
            view_3.nameMois,
            view_3.annee,
            e.Salary,
            view_3.HSNI_130,
            view_3.HSNI_150,
            view_3.HSI_130,
            view_3.HSI_150,
            view_3.totalHN30 AS HN_30,
            view_3.totalHdim40 AS Hdim_40,
            (e.Salary / 173.33) AS TauxHoraire,
            (e.Salary / 173.33) * view_3.HSNI_130 * 1.3 AS Montant_HSNI_130,
            (e.Salary / 173.33) * view_3.HSNI_150 * 1.5 AS Montant_HSNI_150,
            (e.Salary / 173.33) * view_3.HSI_130 * 1.3 AS Montant_HSI_130,
            (e.Salary / 173.33) * view_3.HSI_150 * 1.5 AS Montant_HSI_150,
            (e.Salary / 173.33) * view_3.totalHN30 * 0.3 AS Montant_HN_30,
            (e.Salary / 173.33) * view_3.totalHdim40 * 0.4 AS Montant_HDim_40,
            (e.Salary)
            + (e.Salary / 173.33) * view_3.HSNI_130 * 1.3 
            + (e.Salary / 173.33) * view_3.HSNI_150 * 1.5 
            + (e.Salary / 173.33) * view_3.HSI_130 * 1.3 
            + (e.Salary / 173.33) * view_3.HSI_150 * 1.5 
            + (e.Salary / 173.33) * view_3.totalHN30 * 0.3
            + (e.Salary / 173.33) * view_3.totalHdim40 * 0.4 AS SalaireBrut
        FROM 
            view_3
        LEFT JOIN 
            Employees e ON view_3.EmployeeId = e.Id
        WHERE 
            view_3.EmployeeId = @EmployeeId
            AND view_3.nameMois = @Mois
            AND view_3.annee = @Annee";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@Annee", annee);
                    command.Parameters.AddWithValue("@Mois", mois);

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            results.Add(new
                            {
                                Employee = reader["Nom"],
                                Mois = reader["nameMois"],
                                Annee = reader["annee"],
                                Salary = reader["Salary"],
                                HSNI130 = reader["HSNI_130"],
                                HSNI150 = reader["HSNI_150"],
                                HSI130 = reader["HSI_130"],
                                HSI150 = reader["HSI_150"],
                                HN30 = reader["HN_30"],
                                Hdim40 = reader["Hdim_40"],
                                TauxHoraire = reader["TauxHoraire"],
                                Montant_HSNI_130 = reader["Montant_HSNI_130"],
                                Montant_HSNI_150 = reader["Montant_HSNI_150"],
                                Montant_HSI_130 = reader["Montant_HSI_130"],
                                Montant_HSI_150 = reader["Montant_HSI_150"],
                                Montant_HN_30 = reader["Montant_HN_30"],
                                Montant_Hdim_40 =  reader["Montant_HDim_40"],
                                SalaireBrut = reader["SalaireBrut"]
                            });
                        }
                    }
                }
            }

            return Ok(results);
        }


    }
}
