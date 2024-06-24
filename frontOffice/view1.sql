CREATE VIEW [dbo].[view_2]
AS
    SELECT EmployeeId, Nom, nameMois, annee, sum(HT) as totalHt,
        sum(HN) as totalHn, sum(HS) as totalHs,
        sum(HS130) as totalHS130, sum(HS150) as totalHS150,
        sum(HN30) as totalHN30, sum(Hdim40) as totalHdim40
    FROM view_1
    group by EmployeeId,Nom,nameMois,annee;
