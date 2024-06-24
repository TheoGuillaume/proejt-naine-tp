CREATE VIEW [dbo].[view_1]
AS
    SELECT
        p.Id,
        e.Id AS EmployeeId,
        e.Name AS Nom,
        p.heureTotal AS HT,
        e.heureDeTravail AS HN,
        m.mois AS nameMois,
        CASE 
        WHEN (p.heureTotal - e.heureDeTravail) < 0 THEN 0 
        ELSE (p.heureTotal - e.heureDeTravail) 
    END AS HS,
        CASE
        WHEN (p.heureTotal - e.heureDeTravail) < 8 THEN 
            CASE 
                WHEN (p.heureTotal - e.heureDeTravail) < 0 THEN 0 
                ELSE (p.heureTotal - e.heureDeTravail) 
            END
        ELSE 8
    END AS HS130,
        CASE
        WHEN (p.heureTotal - e.heureDeTravail) <= 8 THEN 0
        ELSE (p.heureTotal - e.heureDeTravail) - 8
    END AS HS150,
        p.nbSemaine,
        p.mois,
        p.HeureNuit AS HN30,
        p.heureDimanche AS Hdim40,
        p.annee
    FROM
        Paye p
        LEFT JOIN
        Employees e ON p.EmployeeId = e.Id
        LEFT JOIN
        Mois m ON p.nbMois = m.id;

CREATE VIEW [dbo].[view_3]
AS
SELECT 
    EmployeeId, 
    Nom, 
    nameMois, 
    annee, 
    totalHs,
    totalHN30,
    totalHdim40,
    totalHS130,
    totalHS150,
    CASE 
        WHEN totalHs <= 20 THEN totalHs
        ELSE 20
    END AS totalHeuresSuppNonImposables,
    CASE 
        WHEN totalHs > 20 THEN totalHs - 20
        ELSE 0
    END AS totalHeuresSuppImposables,
    CASE 
        WHEN totalHs <= 18 THEN totalHs
        ELSE 18
    END AS HSNI_130,
    CASE 
        WHEN totalHs > 18 AND totalHs <= 20 THEN totalHs - 18
        WHEN totalHs > 20 THEN 2
        ELSE 0
    END AS HSNI_150,
    CASE 
        WHEN totalHs > 20 AND totalHs - 20 <= 11 THEN totalHs - 20
        WHEN totalHs > 20 THEN 11
        ELSE 0
    END AS HSI_130,
    CASE 
        WHEN totalHs > 20 AND totalHs - 20 > 11 THEN totalHs - 20 - 11
        ELSE 0
    END AS HSI_150
FROM view_2;
